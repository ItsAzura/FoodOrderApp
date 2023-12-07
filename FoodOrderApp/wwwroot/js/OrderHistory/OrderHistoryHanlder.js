function closeModal() {
    var modalCart = document.querySelector('.modal');
    modalCart.classList.remove('open');
}

function formatDateForDatabase(date) {
    if (typeof date === 'string') {
        // If it's already a string, return it as is
        return date;
    } else if (date instanceof Date) {
        // If it's a Date object, convert to ISO string
        return date.toISOString();
    } else {
        // Handle other cases or throw an error if needed
        throw new Error('Invalid date format');
    }
}

function detailOrder(orderJson) {
    const order = { orderJson: orderJson };
    
    document.querySelector(".modal.detail-order").classList.add("open");
    let detailOrderHtml = `<ul class="detail-order-group">
        <li class="detail-order-item">
            <span class="detail-order-item-left"><i class="fa-light fa-calendar-days"></i> Ngày đặt hàng</span>
            <span class="detail-order-item-right">${formatDateForDatabase(orderJson.orderDate)}</span>
        </li>
        <li class="detail-order-item">
            <span class="detail-order-item-left"><i class="fa-light fa-truck"></i> Hình thức giao</span>
            <span class="detail-order-item-right">${orderJson.orderStatus}</span>
        </li>
        <li class="detail-order-item">
            <span class="detail-order-item-left"><i class="fa-light fa-clock"></i> Ngày nhận hàng</span>
            <span class="detail-order-item-right">${(formatDateForDatabase(orderJson.receiveDate) == "" ? "" : (formatDateForDatabase(orderJson.orderDate) + " - ")) + formatDateForDatabase(orderJson.receiveDate) }</span>
        </li>
        <li class="detail-order-item">
            <span class="detail-order-item-left"><i class="fa-light fa-location-dot"></i> Địa điểm nhận</span>
            <span class="detail-order-item-right">${orderJson.location}</span>
        </li>
        <li class="detail-order-item">
            <span class="detail-order-item-left"><i class="fa-thin fa-person"></i> Người nhận</span>
            <span class="detail-order-item-right">${orderJson.appUser.name}</span>
        </li>
        <li class="detail-order-item">
            <span class="detail-order-item-left"><i class="fa-light fa-phone"></i> Số điện thoại nhận</span>
            <span class="detail-order-item-right">${orderJson.appUser.phoneNumber}</span>
        </li>
    </ul>`
    document.querySelector(".detail-order-content").innerHTML = detailOrderHtml;
}