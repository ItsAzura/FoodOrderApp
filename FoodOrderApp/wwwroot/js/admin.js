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


