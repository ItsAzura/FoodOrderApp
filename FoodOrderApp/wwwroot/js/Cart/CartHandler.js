function openCart() {
    var modalCart = document.querySelector('.modal-cart');
    modalCart.classList.add('open');
}

function closeCart() {
    var modalCart = document.querySelector('.modal-cart');
    modalCart.classList.remove('open');

    document.querySelectorAll('.input-qty').forEach(function (input) {        
        var cartDetailId = input.getAttribute('data-cart-detail-id');
       
        var updatedQuantity = input.value;

        fetch('/Home/UpdateCartDetailQuantity', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ cartDetailId: cartDetailId, updatedQuantity: updatedQuantity })
        })
            .then(response => response.json())
            .then(data => {
                // Handle success if needed
            })
            .catch(error => {
                // Handle error if needed
            });
    })
}

function addToCart(productId) {
    var textNoted = document.querySelector('.text-note');
    var textValue = textNoted.value;

    console.log(`Product id: ${productId} and textValue: ${textValue}`);

    fetch('/Home/AddToCartDetail', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ ProductId: productId, Noted: textValue })
    })
        .then(response => {
            if (response.ok) {
                // Reload the page after a successful request
                window.location.reload();
            } else {
                throw new Error('Failed to add to cart');
            }
        })
        .catch(error => {
            // Handle error if needed
            console.error('Error:', error.message);
        });
}


function increasingNumber(e) {
    if (e && e.parentNode) {
        let qty = e.parentNode.querySelector('.input-qty');
        if (qty && parseInt(qty.value) < qty.max) {
            qty.value = parseInt(qty.value) + 1;
        } else if (qty) {
            qty.value = qty.max;
        }

        updateItemPrice(e)
        updateTotalPrice()
    }
}

function decreasingNumber(e) {
    if (e && e.parentNode) {
        let qty = e.parentNode.querySelector('.input-qty');
        if (qty && qty.value > qty.min) {
            qty.value = parseInt(qty.value) - 1;
        } else if (qty) {
            qty.value = qty.min;
        }
    }

    updateItemPrice(e)
    updateTotalPrice()
}

function updateItemPrice(inputElement) {
    // Lấy giá trị từ data-price và giá trị trong input-qty
    var dataPrice = parseFloat(inputElement.parentElement.parentElement.parentElement.querySelector('.price').getAttribute('data-price'));
    var quantity = parseFloat(inputElement.parentElement.querySelector('.input-qty').value);

    // Tính toán và cập nhật giá trị vào total-price
    var totalPriceElement = inputElement.parentElement.parentElement.parentElement.querySelector('.total-price');
    if (!isNaN(dataPrice) && !isNaN(quantity)) {
        var totalPrice = dataPrice * quantity;
        totalPriceElement.textContent = totalPrice.toFixed(2) + "Đ"; // Có thể điều chỉnh số lẻ tùy ý
        totalPriceElement.setAttribute('data-totalPrice', totalPrice);
    }
}

function updateTotalPrice() {
    var textPriceElement = document.querySelector('.modal-cart .cart-footer .cart-total-price .text-price');
    var totalPriceList = document.querySelectorAll('.cart-item .total-price');

    // Khởi tạo biến để tích lũy tổng giá trị
    var textPrice = 0;

    // Duyệt qua từng phần tử và cộng giá trị vào textPrice
    totalPriceList.forEach(function (totalPriceElement) {
        var dataTotalPrice = parseFloat(totalPriceElement.getAttribute('data-totalPrice'));
        if (!isNaN(dataTotalPrice)) {
            textPrice += dataTotalPrice;
        }
    });

    // Hiển thị giá trị tổng trong textPriceElement
    textPriceElement.textContent = textPrice.toFixed(2) + "Đ";
}

function deleteCartItem(buttonElement) {
    var cartItem = buttonElement.closest('.cart-item');
    var cartItemId = cartItem.getAttribute('data-id');

    // Remove the cart item from the UI
    cartItem.remove();

    updateTotalPrice();
    updateButtonDisableState();

    // Call the server to delete the cart detail
    fetch('/Home/DeleteCartDetail', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ cartDetailId: cartItemId }),
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                console.log('CartDetail deleted successfully.');
            } else {
                console.error('Failed to delete CartDetail.');
            }
        })
        .catch(error => {
            console.error('Error during fetch:', error);
        });
}

function updateButtonDisableState() {
    var cartItems = document.querySelectorAll('.cart-item');
    var isDisabled = cartItems.length <= 0;

    var thanhToanButton = document.querySelector('.thanh-toan');

    if (isDisabled) {
        thanhToanButton.classList.add('disabled');
    } else {
        thanhToanButton.classList.remove('disabled');
    }
}


