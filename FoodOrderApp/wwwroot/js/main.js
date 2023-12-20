const body = document.querySelector("body");
let modalContainer = document.querySelectorAll('.modal');
let modalBox = document.querySelectorAll('.mdl-cnt');
let formLogSign = document.querySelector('.forms');

// Click vùng ngoài sẽ tắt Popup
modalContainer.forEach(item => {
    item.addEventListener('click', closeModal);
});

modalBox.forEach(item => {
    item.addEventListener('click', function (event) {
        event.stopPropagation();
    })
});

function closeModal() {
    modalContainer.forEach(item => {
        item.classList.remove('open');
    });
    console.log(modalContainer)
    body.style.overflow = "auto";
}

document.querySelector(".filter-btn").addEventListener("click", (e) => {
    e.preventDefault();
    document.querySelector(".advanced-search").classList.toggle("open");
    document.getElementById("home-service").scrollIntoView();
})

function closeSearchAdvanced() {
    document.querySelector(".advanced-search").classList.toggle("open");
}

//Xem chi tiet san pham
async function detailProduct(event, productId) {
    console.log('detailProduct function called with productId:', productId);
    event.preventDefault();
    try {
        const response = await fetch(`/api/FoodsApi/${productId}`);

        if (!response.ok) {
            throw new Error(`Failed to fetch product details. Status: ${response.status}`);
        }

        const infoProduct = await response.json();

        const modalHtml = `
            <div class="modal-header">
                <img class="product-image" src="${infoProduct.image}" alt="">
            </div>
            <div class="modal-body">
                <h2 class="product-title">${infoProduct.name}</h2>
                <div class="product-control">
                    <div class="priceBox">
                        <span class="current-price">${infoProduct.price} VNĐ</span>
                    </div>
                    <div class="buttons_added">
                        <input class="minus is-form" type="button" value="-" onclick="decreasingNumber(this)">
                        <input class="input-qty" max="100" min="1" name="" type="number" value="1">
                        <input class="plus is-form" type="button" value="+" onclick="increasingNumber(this)">
                    </div>
                </div>
                <p class="product-description">${infoProduct.description}</p>
            </div>
            <div class="notebox">
                <p class="notebox-title">Ghi chú</p>
                <textarea class="text-note" id="popup-detail-note" placeholder="Nhập thông tin cần lưu ý..."></textarea>
            </div>
            <div class="modal-footer">
                <div class="price-total">
                    <span class="thanhtien">Thành tiền</span>
                    <span class="price">${infoProduct.price} VNĐ</span>
                </div>
                <div class="modal-footer-control">
                    <button class="button-dathangngay" data-product="${infoProduct.id}" onclick="detailProduct(event, '${infoProduct.id}')">Đặt hàng ngay</button>
                    <button class="button-dat" id="add-cart" onclick="animationCart()"><i class="fa-light fa-basket-shopping"></i></button>
                </div>
            </div>`;

        // Hiển thị modal và cập nhật nội dung
        const modal = document.querySelector('.modal.product-detail');
        document.querySelector('#product-detail-content').innerHTML = modalHtml;
        modal.classList.add('open');
        body.style.overflow = "hidden";
    } catch (error) {
        console.error('Error fetching product details:', error);
        // Xử lý lỗi tại đây (ví dụ: thông báo người dùng về lỗi)
    }
}




let perProducts = [];

$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var selectedCategory = urlParams.get('foodCategory');

    if (selectedCategory) {
        $("#advanced-search-category-select").val(selectedCategory);
    }
});

function handleCategoryChange() {
    var selectedCategory = $("#advanced-search-category-select").val();

    if (selectedCategory === "") {
        window.location.href = "/Home/Index";
    } else {
        var form = $("#advanced-search-category-select").closest("form");
        form.attr("action", "/Foods/Category");
        form.submit();
    }
}


$(document).ready(function () {
    function submitSearch(event) {
        if (event.which === 13) {
            event.preventDefault();
            var searchTerm = $("#searchTermInput").val();
            var url = "/Home/Index?searchTerm=" + encodeURIComponent(searchTerm);
            window.location.href = url;
        }
    }
    $("#searchTermInput").on("keypress", submitSearch);
});

