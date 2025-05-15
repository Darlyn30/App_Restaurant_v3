const URLS = {
  URL_RESTAURANT: "https://localhost:7075/api/Restaurant",
  URL_CATEGORY: "https://localhost:7075/api/Category",
  URL_FOOD_BY_RESTAURANT: "https://localhost:7075/api/Food/restaurant/",
  URL_GET_CART : "https://localhost:7075/api/Cart/me"
};

const img404 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkjuaCHtdwkToFkspREKsD_FTNvuxsg7CFbg&s"; // cuando algo no tenga imagen o no carga

const TOKEN = JSON.parse(localStorage.getItem("token")) || null;
const USER = JSON.parse(localStorage.getItem("account")) || null;
const btn_logout = document.querySelector(".logout-btn");
const cat_container = document.getElementById("category-list");
const res_container = document.getElementById("restaurant-list");
const cart_nav = document.getElementById("cart-icon"); // es un div que se esta usando como boton, el que muestra el carrito
console.log(TOKEN);

let allRestaurants = [];
let selectedRestaurantId = null;
let selectedRestaurantName = "";

//   localStorage.removeItem("token");
//   localStorage.removeItem("account");

document.addEventListener("DOMContentLoaded", () => {
    showCart(URLS.URL_GET_CART); // el length del cart
    // renderCategories(URLS.URL_CATEGORY, cat_container);
    reenderRestaurants(URLS.URL_RESTAURANT, res_container);

    //moverse hacia el carrito
    cart_nav.addEventListener("click", () => {
        window.location.href = "../carrito/Carrito.html";
    })

    //cerrar sesion
    btn_logout.addEventListener("click", () => {
        cerrarSesion();
    })

    //#region nav sections
    document.querySelectorAll(".nav-link").forEach(link => {
        link.addEventListener("click", (e) => {
            e.preventDefault();
            const target = link.getAttribute("data-section");

            //ocultar todas las secciones

            document.querySelectorAll(".section").forEach(sec => {
                sec.classList.add("hidden");
            })

            //mostrar seccion seleccionada
            document.getElementById(target).classList.remove("hidden");

            //resaltar la seccion activa
            document.querySelectorAll(".nav-link").forEach(l => l.classList.remove("active"));
            link.classList.add("active");

            //cargar los datos de la cuenta
            if(target === "account-section"){
                infoCuenta();
            }
        })
    })
})


function cerrarSesion(){
    swal({
        title: "¿Estás seguro?",
        text: "Tu sesión se cerrará",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then(res => {
        if (res) {
            localStorage.removeItem("token");
            localStorage.removeItem("account");
            window.location.href = "../../Index.html";
        }
    });
}

//aqui quiero hacer algo como de filtro por categoria, filtrar restaurante, o 0 categorias y ya, por ahora fuera

// async function renderCategories(url, container){

//     try {
//         const data = await apiCall(url, "GET");
//         if(!data) return;

//         container.innerHTML = "";

//         data.forEach(category => {
//             const card = document.createElement("div");
//             card.classList.add("card-category");

//             card.innerHTML = `
//                 <img src="${category.imgUrl || img404}" alt="${category.name}"/>
//                 <div class="card-title">${category.name}</div>
//             `;
//             container.appendChild(card);
//         })
//     } catch(err){
//         console.error("Error: ", err);
//     }
// }

function renderRestaurants(data, container){
    container.innerHTML = "";

    if(data.length === 0){
        container.innerHTML = "<p>No se encontraron restaurantes</p>";
        return;
    }

    data.forEach(restaurant => {
        const card = document.createElement("div");
        card.className = "restaurant-card";
        card.innerHTML = `
            <img src="${restaurant.imgUrl}" alt="${restaurant.name}"/>
            <div>
                <h4>${restaurant.name}</h4>
                <p>${restaurant.description || "Sin descripcion disponible"}</p>
                <p class="${restaurant.active ? "Open" : "Closed"}">
                ${restaurant.active ? "Abierto" : "Cerrado"}
                </p>
                <p style="color: green;">${restaurant.openingTime} - ${restaurant.closingTime}</p>
            </div>
        `;

        card.addEventListener("click", () => {
            selectedRestaurantId = restaurant.id;
            selectedRestaurantName = restaurant.name;
            renderFoodsByRestaurants(URLS.URL_FOOD_BY_RESTAURANT,selectedRestaurantId, selectedRestaurantName);
        })

        container.appendChild(card);
    })
}

function infoCuenta(){
    const name = document.getElementById("user-name");
    const email = document.getElementById("user-email");

    email.innerHTML = USER.email;
    name.innerHTML = USER.name;
}

async function renderFoodsByRestaurants(url, id, name){
    try {
        const fullUrl = `${url}${id}`;
        const data = await apiCall(fullUrl, "GET");
        if(!data) return;

        renderFoods(data, name, res_container);
        console.log(name);
        
    } catch(err) {
        swal({
            title: "Ha ocurrido un error",
            text: err.message,
            icon: "warning"
        })
        .then(() => {
            window.location.href = "./Home.html";
        })
    }
}

async function reenderRestaurants(url, container){
    try {
        const data = await apiCall(url, "GET");
        if(!data) return;

        allRestaurants = data;
        renderRestaurants(data, container);
    } catch(err){
        console.log("Error al renderizar restaurantes: ", err);
    }
}

function renderFoods(data, name, container){
    container.innerHTML = "";

    const header = document.createElement("div");
    header.className = "food-header";
    header.innerHTML = `
        <h3>comidas de: ${name}</h3>
        <button id="close-menu-btn" class="btn-close"> Volver al restaurante</button>
    `;
    container.appendChild(header);

    if(data.length === 0){
        container.innerHTML += "<p>No hay comidas disponibles</p>"
        return;
    }

    const grid = document.createElement("div");
    grid.className = "grid";

    data.forEach(food => {
        const card =  document.createElement("div");
        card.className = "food-card";
        card.innerHTML = `
            <img src="${food.imgUrl || img404}" alt="${food.name}"/>
            <div class="info">
                <h4>${food.name}</h4>
                <p>${food.description}</p>
                <p class="price" style="color: green;">$${food.price.toFixed(2)}</p>
                <button class="add-to-cart-btn" data-id="${food.id}">Agregar al carrito</button>
            </div>
        `;
        grid.appendChild(card);
    })

    container.appendChild(grid);

    document.getElementById("close-menu-btn").addEventListener("click", () => {
        reenderRestaurants(URLS.URL_RESTAURANT, res_container);
    })
}

//esto trae la cantidad de objetos del carrito para mostrarla en pantalla
async function showCart(url){
    try {
        const data = await apiCall(url, "GET");

        const span = document.getElementById("cart-count");
        span.innerHTML = `${data.cartItems.length}`;
    } catch(err){
        throw Error("Error al traer el carrito: ", err);
    }
}

async function addToCart(cartId, foodId, quantity, price, url){
    //TODO: pendiente funcion para el carrito
    const data = await apiCall(url, "POST")
}

async function apiCall(url, method, body = null) {
    const options = {
        method,
        headers : {
            "Authorization" : `Bearer ${TOKEN}`, // para los endpoints que debemos de tener authorizacion de usuario
            "Content-Type" : "application/json"
        }
    }

    if(body) options.body = JSON.stringify(body);

    //esto pasa cuando el token se vence, los endpoints daran un 401 Unauthorized y me sacara de la sesion, enviandome al login
    try {
        const res = await fetch(url, options);

        if(res.status === 401){
            // 🔒 Token expirado
            localStorage.removeItem("token");
            localStorage.removeItem("account");
            swal(
                "Sesión expirada", 
                "Por favor inicia sesión de nuevo", 
                "warning")
            .then(() => {
                window.location.href = "../../Index.html";
            });

            return;
        }

        const data = await res.json();
        if(!res.ok) throw new Error(data.message || "Unknow Error");

        return data;
    } catch(err){
        console.error("Error en la peticion: ", err.message);
        throw err;
    }
}