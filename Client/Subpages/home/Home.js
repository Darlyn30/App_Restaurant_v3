const URLS = {
  URL_RESTAURANT: "https://localhost:7075/api/Restaurant",
  URL_CATEGORY: "https://localhost:7075/api/Category",
  URL_FOOD_BY_RESTAURANT: "https://localhost:7075/api/Food/restaurant/"
};

let allRestaurants = [];
let selectedRestaurantId = null;
let selectedRestaurantName = "";

                //   localStorage.removeItem("token");
                //   localStorage.removeItem("account");


document.addEventListener("DOMContentLoaded", function () {
  const searchBtn = document.getElementById("search-btn");
  const searchInput = document.getElementById("search-input");

    document.getElementById('cart-icon').addEventListener('click', () => {
        window.location.href = "../carrito/Carrito.html";
    });
  
  //cuando se le haga click en cerrar sesion
  document.querySelector('.logout-btn').addEventListener("click", () => {

      swal({
          title: "Esta seguro?",
          text: "Quieres cerrar sesion?",
          icon: "warning",
          buttons: true,
          dangerMode: true,
      })
      .then((willDelete) => {
          if (willDelete) {
              swal(`Adios ${user.name}, vuelve cuando gustes`, {
                  icon: "success",
              })
              .then(res => {
                  localStorage.removeItem("token");
                  localStorage.removeItem("account");

                  window.location.href = "../../Index.html";
              })
          } else {
              swal("puedes cerrar sesion cuando gustes");
          }
      });

  });


  searchBtn.addEventListener("click", function () {
      const query = searchInput.value.toLowerCase();
      const filtered = allRestaurants.filter(r =>
          r.name.toLowerCase().includes(query)
      );
      renderRestaurants(filtered);
  });
  // Cargar categorías
  fetch(URLS.URL_CATEGORY)
      .then(res => res.json())
      .then(data => {
          const list = document.getElementById("category-list");

          data.forEach(category => {
              const card = document.createElement("div");
              card.className = "card-category";
              card.innerHTML = `
                  <img src="${category.imgUrl}" alt="${category.name}">
                  <p>${category.name}</p>
              `;
              list.appendChild(card);
          });
      });

  // Cargar restaurantes
  fetch(URLS.URL_RESTAURANT)
      .then(res => res.json())
      .then(data => {
          allRestaurants = data;
          renderRestaurants(data);
      })
      .catch(err => {
          console.error("Error cargando restaurantes", err);
      });

  function renderRestaurants(restaurants) {
      const list = document.getElementById("restaurant-list");
      list.innerHTML = "";

      if (restaurants.length === 0) {
          list.innerHTML = "<p>No se encontraron restaurantes.</p>";
          return;
      }

      restaurants.forEach(restaurant => {
          const card = document.createElement("div");
          card.className = "restaurant-card";
          card.innerHTML = `
              <img src="${restaurant.imgUrl}" alt="${restaurant.name}">
              <div class="info">
                  <h4>${restaurant.name}</h4>
                  <p>${restaurant.description || "Sin descripción disponible"}</p>
                  <p class="${restaurant.active ? "open" : "closed"}">
                      ${restaurant.active ? "Abierto" : "Cerrado"}
                  </p>
                  <p>${restaurant.openingTime} - ${restaurant.closingTime}</p>
              </div>
          `;

          card.addEventListener("click", () => {
              selectedRestaurantId = restaurant.id;
              selectedRestaurantName = restaurant.name;
              cargarComidasPorRestaurante(selectedRestaurantId, selectedRestaurantName);
          });

          list.appendChild(card);
    });
  }

  function cargarComidasPorRestaurante(id, name) {
      fetch(`${URLS.URL_FOOD_BY_RESTAURANT}${id}`)
          .then(async res => {
            const data = await res.json();
            if(res.status === 404){
                const message = data.message || "Not Found";
                swal({
                    title: "Ha ocurrido un error",
                    text: message,
                    icon: "warning",
                })
                .then(res => {
                    window.location.href = "";
                })
            }

            renderFoods(data, name);
        })
  }

  function renderFoods(foods, restaurantName) {
      const list = document.getElementById("restaurant-list");
      list.innerHTML = "";

      // Ahora el h3 y el botón están en el mismo div (alineados)
      const header = document.createElement("div");
      header.className = "food-header";
      header.innerHTML = `
          <h3>Comidas de ${restaurantName}</h3>
          <button id="close-menu-btn" class="btn-close">Volver a restaurantes</button>
      `;
      list.appendChild(header);

      if (foods.length === 0) {
          list.innerHTML += "<p>No hay comidas disponibles.</p>";
          return;
      }

      const grid = document.createElement("div");
      grid.className = "grid";

      foods.forEach(food => {
          const card = document.createElement("div");
          card.className = "food-card";
          card.innerHTML = `
              <img src="${food.imgUrl || 'https://via.placeholder.com/150'}" alt="${food.name}">
            <div class="info">
                <h4>${food.name}</h4>
                <p>${food.description}</p>
                <p class="price" style="color: green;">$${food.price.toFixed(2)}</p>
                <button class="add-to-cart-btn" data-id="${food.id}">Agregar al carrito</button>
            </div>
          `;
          grid.appendChild(card);
      });

      document.addEventListener("click", (e) => {
        if(e.target && e.target.classList.contains("add-to-cart-btn")){
            const foodId = e.target.getAttribute("data-id");
            const selectedFood = foods.find(f => f.id == foodId);
            addToCart(selectedFood);
        }
      })

      function addToCart(food){
        let carrito = JSON.parse(localStorage.getItem("carrito")) || [];
        carrito.push(food);
        localStorage.setItem("carrito", JSON.stringify(carrito));
        swal({
            title: "Producto agregado",
            text: `${food.name} ha sido agregado al carrito.`,
            icon: "success",
            timer: 1500,
            buttons: false
        });
      }

      list.appendChild(grid);

      document.getElementById("close-menu-btn").addEventListener("click", () => {
          renderRestaurants(allRestaurants);
      });
  }

  const user = JSON.parse(localStorage.getItem("account"));

  // Navegación entre secciones simuladas
  document.querySelectorAll(".nav-link").forEach(link => {
      link.addEventListener("click", e => {
          e.preventDefault();
          const target = link.getAttribute("data-section");

          // Ocultar todas las secciones
          document.querySelectorAll(".section").forEach(sec => {
              sec.classList.add("hidden");
          });

          // Mostrar la sección seleccionada
          document.getElementById(target).classList.remove("hidden");

          // Resaltar la pestaña activa
          document.querySelectorAll(".nav-link").forEach(l => l.classList.remove("active"));
          link.classList.add("active");

          // Solo cargar datos si es la sección de cuenta
          if (target === "account-section") {
              cargarDatosCuenta(user);
          }
      });
  });

  function cargarDatosCuenta(user) {
      document.getElementById("user-name").textContent = user.name;
      document.getElementById("user-email").textContent = user.email;
  }
});
