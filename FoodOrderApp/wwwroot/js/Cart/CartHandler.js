function openCart() {
    var modalCart = document.querySelector('.modal-cart');
    console.log("Open")
    modalCart.classList.add('open');
}

function closeCart() {
    var modalCart = document.querySelector('.modal-cart');
    console.log("Close")
    modalCart.classList.remove('open');
    console.log(modalContainer)
}