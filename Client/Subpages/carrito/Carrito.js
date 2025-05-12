const cart = [];

window.onload = function () {
    const cartContainer = document.getElementById('cart-items');
    const emptyCart = document.getElementById('empty-cart');
    const checkoutModal = document.getElementById('checkout-modal');

    if (cart.length === 0) {
        emptyCart.style.display = 'block';
    } else {
        cart.forEach(item => {
            const div = document.createElement('div');
            div.className = 'cart-item';
            div.innerHTML = `
                <div class="cart-item-info">
                    <img src="${item.image}" alt="${item.name}">
                    <div>
                        <strong>${item.name}</strong>
                        <span>${item.description}</span>
                        <span>$${item.price.toFixed(2)}</span>
                        <span>Cantidad: ${item.quantity}</span>
                    </div>
                </div>
            `;
            cartContainer.appendChild(div);
        });
    }

    // Botón para abrir modal de pago
    document.getElementById('checkout-btn').addEventListener('click', () => {
        checkoutModal.style.display = 'flex';
    });

    // Cerrar modal
    document.getElementById('close-modal').addEventListener('click', () => {
        checkoutModal.style.display = 'none';
        window.location.href = "./Carrito.html";
    });

    // Cerrar modal al hacer clic fuera del contenido
    window.onclick = function(event) {
        if (event.target === checkoutModal) {
            checkoutModal.style.display = "none";
        }
    };

    // Continuar comprando
    document.getElementById('continue-btn').addEventListener('click', () => {
        window.location.href = '../home/Home.html'; // Cambia esta ruta al home real
    });
};
