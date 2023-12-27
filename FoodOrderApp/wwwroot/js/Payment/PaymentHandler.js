const PHIVANCHUYEN = 30000;

const FormDeliveryCategoryEnum = {
    GiaoTanNoi: 'GiaoTanNoi',
    TuDenLay: 'TuDenLay'
};

const OrderStatusCategoryEnum = {
    DangXuLy: 'DangXuLy',
    DaXuLy: 'DaXuLy'
}

var giaotannoi = document.querySelector('#giaotannoi');
var tudenlay = document.querySelector('#tudenlay');
var tudenlayGroup = document.querySelector('#tudenlay-group');
var chkShip = document.querySelectorAll(".chk-ship");

var deliveryCost = document.querySelector('.priceFlx.chk-ship');

window.addEventListener('load', function () {
    displayFinalPrice();
    GenerateDeliveryDay();
});

tudenlay.addEventListener('click', () => {
    giaotannoi.classList.remove("active");
    tudenlay.classList.add("active");
    chkShip.forEach(item => {
        item.style.display = "none";
    });
    tudenlayGroup.style.display = "block";
    deliveryCost.getAttribute('isDeliveryCost');
    deliveryCost.setAttribute('isDeliveryCost', 'False');
    displayFinalPrice();
})

giaotannoi.addEventListener('click', () => {
    tudenlay.classList.remove("active");
    giaotannoi.classList.add("active");
    tudenlayGroup.style.display = "none";
    chkShip.forEach(item => {
        item.style.display = "flex";
    });
    deliveryCost.getAttribute('isDeliveryCost');
    deliveryCost.setAttribute('isDeliveryCost', 'True');
    displayFinalPrice();
})

var foodTotalElements = document.querySelectorAll('#list-order-checkout .food-total');
var checkoutCartPriceFinalElement = document.getElementById('checkout-cart-price-final');

function getTotalFoodPrice(foodTotalElement) {
    var infoFoodElement = foodTotalElement.querySelector('.info-food');
    var foodPrice = parseFloat(infoFoodElement.querySelector('span').getAttribute('food-price'));
    return foodPrice || 0;
}

function calculateTotalPrice(foodTotalElements, isDeliveryCost) {
    var totalPrice = 0;

    foodTotalElements.forEach(foodTotalElement => {
        totalPrice += getTotalFoodPrice(foodTotalElement);
    });

    if (isDeliveryCost) {
        totalPrice += PHIVANCHUYEN;
    }

    return totalPrice;
}

function displayFinalPrice() {
    if (foodTotalElements.length <= 0) {
        checkoutCartPriceFinalElement.innerHTML = "";
    } else {
        var isDeliveryCost = deliveryCost.getAttribute('isDeliveryCost') === "True";
        var totalPrice = calculateTotalPrice(foodTotalElements, isDeliveryCost);

        checkoutCartPriceFinalElement.innerHTML = totalPrice + "Đ";
        checkoutCartPriceFinalElement.setAttribute('final-price', totalPrice);
    }
}


function formatDateForDatabase(date) {
    return date.toISOString();
}

function GenerateDeliveryDay() {
    let today = new Date();
    let ngaymai = new Date();
    let ngaykia = new Date();
    ngaymai.setDate(today.getDate() + 1);
    ngaykia.setDate(today.getDate() + 2);

    let dateorderhtml = `<a href="javascript:;" class="pick-date active" data-date="${formatDateForDatabase(today)}">
        <span class="text">Hôm nay</span>
        <span class="date">${today.getDate()}/${today.getMonth() + 1}</span>
        </a>
        <a href="javascript:;" class="pick-date" data-date="${formatDateForDatabase(ngaymai)}">
            <span class="text">Ngày mai</span>
            <span class="date">${ngaymai.getDate()}/${ngaymai.getMonth() + 1}</span>
        </a>
        <a href="javascript:;" class="pick-date" data-date="${formatDateForDatabase(ngaykia)}">
            <span class="text">Ngày kia</span>
            <span class="date">${ngaykia.getDate()}/${ngaykia.getMonth() + 1}</span>
    </a>`;

    document.querySelector('.date-order').innerHTML = dateorderhtml;
    let pickdate = document.getElementsByClassName('pick-date');
    for (let i = 0; i < pickdate.length; i++) {
        pickdate[i].onclick = function () {
            document.querySelector(".pick-date.active").classList.remove("active");
            this.classList.add('active');
        };
    }
}

var paymentViewModel;

function ReceiveViewModel(paymentViewModelJson) {
    var paymentViewModel = JSON.parse(paymentViewModelJson);

    // Sử dụng paymentViewModel như một đối tượng JavaScript bình thường
    console.log(paymentViewModel.CartUser);
    console.log(paymentViewModel.CartUserDetails);
    console.log(paymentViewModel.OrderUser);
}

var selectedLocation;

function handleRadioChanged(selectedRadio) {
    var selectedValue = selectedRadio.value;
    selectedLocation = selectedRadio.nextElementSibling.textContent; // Database Location based on Tu Den Lay
}

function PlaceOrder(AppUserIdJSON) {
    var placeOrderButton = document.querySelector('.type-order-btn.active');
    var formDeliveryType = placeOrderButton.getAttribute('data-form-delivery');
    const currentDate = new Date();
    const formattedCurrentDateTime = formatDateForDatabase(currentDate);
    const pickedDated = document.querySelector('.pick-date.active').getAttribute('data-date');
    var orderStatus = OrderStatusCategoryEnum.DangXuLy;
    var receiverInput = document.getElementById('tennguoinhan');
    var receiverValue = receiverInput.value;
    var phoneInput = document.getElementById('sdtnhan');
    var phoneValue = phoneInput.value;
    var notedOrder = document.querySelector('.note-order');
    var notedValue = notedOrder.value;
    var finalPrice = checkoutCartPriceFinalElement.getAttribute('final-price');

    // Check if required fields are filled
    if (receiverValue == "" || phoneValue == "" || selectedLocation == "") {
        toast({ title: 'Chú ý', message: 'Vui lòng nhập đầy đủ thông tin !', type: 'warning', duration: 4000 });
        console.log("Called");
        return;
    }

    switch (formDeliveryType) {
        case FormDeliveryCategoryEnum.GiaoTanNoi:
            var locationInput = document.getElementById('diachinhan');
            selectedLocation = locationInput.value;
            break;

        case FormDeliveryCategoryEnum.TuDenLay:
            
            break;
    }

    // Common fetch configuration
    var fetchConfig = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            OrderDate: formattedCurrentDateTime,
            ReceiveDate: pickedDated,
            OrderStatus: orderStatus,
            FormDelivery: formDeliveryType,
            Receiver: receiverValue,
            Location: selectedLocation,
            PhoneNumber: phoneValue,
            Note: notedValue,
            AppUserId: AppUserIdJSON
        })
    };

    // Fetch request
    fetch('/Payment/AddOrder', fetchConfig)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            // Handle success
            console.log(data);

            // Assuming data contains information about the success of the operation
            if (data.success) {
                // Redirect to the OrderHistory/Index page
                window.location.href = '/OrderHistory/Index';
            } else {
                // Handle any other logic for failure
            }
        })
        .catch(handleError);
}

function handleError(error) {
    console.error('Error:', error.message);

    if (error.response) {
        error.response.text().then(errorMessage => {
            console.error('Server Error:', errorMessage);
        });
    }
}
