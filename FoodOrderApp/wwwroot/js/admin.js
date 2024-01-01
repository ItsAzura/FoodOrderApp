document.addEventListener("DOMContentLoaded", function () {
    const sidebars = document.querySelectorAll(".sidebar-list-item.tab-content");
    const sections = document.querySelectorAll(".section");

    for (let i = 0; i < sidebars.length; i++) {
        sidebars[i].onclick = function () {
            // Update the active tab on the server
            let action = sidebars[i].querySelector('.sidebar-link').getAttribute('data-action');

            // Perform an AJAX request to the server to update the active tab
            // Example using fetch API:
            // fetch(`/Admin/SetActiveTab?tab=${action}`, { method: 'POST' });
        };
    }

    // Handle open add product modal
    let btnAddProduct = document.getElementById("btn-add-product");
    btnAddProduct.addEventListener("click", () => {
        document.querySelectorAll(".add-product-e").forEach(item => {
            item.style.display = "block";
        })
        document.querySelectorAll(".edit-product-e").forEach(item => {
            item.style.display = "none";
        })
        document.querySelector(".add-product").classList.add("open");
    });
});


for (let i = 0; i < sidebars.length; i++) {
    sidebars[i].onclick = function () {
        document.querySelector(".sidebar-list-item.active").classList.remove("active");
        sidebars[i].classList.add("active");
    };
}

document.addEventListener('DOMContentLoaded', function () {
    const openDetailBtns = document.querySelectorAll('.btn-detail');
    const detailModals = document.querySelectorAll('.modal.detail-order');
    Array.from(openDetailBtns).forEach((btn, i) => {
        btn.addEventListener('click', () => {
            const openModal = document.querySelector(".modal.detail-order.open");
            if (openModal) {
                openModal.classList.remove('open')
            }
            detailModals[i].classList.add("open");
        })
    })
})