// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Event listener for "Add to Cart" buttons
//document.querySelectorAll('.add-to-cart').forEach(button => {
//    button.addEventListener('click', function () {
//        const productCard = this.closest('.product-card');
//        if (productCard) {
//            const productNameElement = productCard.querySelector('.product-info h3');
//            const productPriceElement = productCard.querySelector('.product-info h4');

//            // Ensure both elements exist before accessing their properties
//            if (productNameElement && productPriceElement) {
//                const productName = productNameElement.textContent;
//                const productPrice = parseFloat(productPriceElement.textContent.replace('R ', ''));
//                addItemToCart(productName, productPrice);
//            } else {
//                console.error('Product name or price element not found');
//            }
//        } else {
//            console.error('Product card not found');
//        }
//    });
//});


function addItemToCart(productId, quantity) {
    $.ajax({
        url: '/Cart/AddToCart',
        type: 'POST',
        data: { id: productId, quantity: quantity },
        success: function (result) {
            // Inject the updated cart HTML into the DOM
            $('#cartItems').html(result.cartItemsHtml);
            $('#cartTotal').text(result.cartTotal);

            // Optional: Show a confirmation message
            $('#cartConfirmation').text('Item added to cart!').fadeIn().delay(2000).fadeOut();
        },
        error: function () {
            console.error('Failed to add item to cart');
        }
    });
}


// Update cart total
function updateCartTotal() {
    let total = 0;
    const rows = document.querySelectorAll('#cartItems tr');
    rows.forEach(row => {
        const priceCell = row.querySelector('td:nth-child(3)');
        total += parseFloat(priceCell.textContent.replace('R ', ''));
    });
    const cartTotalElement = document.getElementById('cartTotal');
    if (cartTotalElement) {
        cartTotalElement.textContent = 'R ' + total.toFixed(2);
    } else {
        console.error('cartTotal element not found');
    }
}
