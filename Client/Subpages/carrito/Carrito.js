const URLs = {
    URL_GET_CART : "https://localhost:7075/api/Cart/me",
    URL_DELETE_CART : `https://localhost:7075/api/Cart/remove-item/`
}

const TOKEN = JSON.parse(localStorage.getItem("token")) || null;

const img404 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkjuaCHtdwkToFkspREKsD_FTNvuxsg7CFbg&s"; // cuando algo no tenga imagen o no carga

const checkoutModal = document.getElementById('checkout-modal');

window.onload = function () {
    const cartContainer = document.getElementById('cart-item');
    const emptyCart = document.getElementById('empty-cart');


    fetch(URLs.URL_GET_CART, {
        method: "GET", 
        headers: {
            "Authorization": `Bearer ${TOKEN}`, // debo pasarle el id del token o si no la API devolvera un 401
            "Content-Type" : "application/json"
        }
    })
    .then(res => res.json())
    .then(data => {
        console.log(data);
        if(data.length === 0){
            emptyCart.style.display = 'block';
        } else {
            
            data.cartItems.forEach(item => {
                console.log(item);
                const div = document.createElement('div');
                div.className = 'cart-item';
                div.innerHTML = `
                    <img src="${item.imgUrl || img404}" alt="${item.name}" class="cart-item-img">
                    <div class="cart-item-details">
                        <h4>${item.foodName}</h4>
                        <p>Topping Pizza Grande: Queso</p>
                        <span class="price">$${item.price}</span>
                    </div>
                    <div class="cart-item-actions">
                        <button class="btn-delete">🗑️</button>
                    </div>
                `;
                cartContainer.appendChild(div);

                console.log(item.id);

                document.querySelector(".btn-delete").addEventListener("click", () => {
                    const fullUrl = `${URLs.URL_DELETE_CART}${item.id}`;
                    deleteCartItem(fullUrl);
                })
            });
        }
    })
};

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
    window.location.href = '../home/Home.html'; 
});

async function deleteCartItem(url){
    try {
        fetch(url, {
            method : "DELETE",
            headers: {
                "Authorization" : `Bearer ${TOKEN}`,
                "Content-Type" : "application/json"
            }
        })
    } catch(err) {
        throw Error(err);
    }
}
