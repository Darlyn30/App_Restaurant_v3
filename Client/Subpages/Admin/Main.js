const URLS = {
    //Restaurants URLS
    GET_RESTAURANTS: "https://localhost:7075/api/Restaurant",
    DELETE_RESTAURANT: id => `https://localhost:7075/api/Restaurant/${id}`,
    //CATEGORIES URLS
    GET_CATEGORIES: "https://localhost:7075/api/Category",
    DELETE_CATEGORY: id => `http://localhost:7075/api/Category/${id}`
};

document.addEventListener("DOMContentLoaded", () => {
    const navLinks = document.querySelectorAll(".nav-link");

    navLinks.forEach(link => {
        link.addEventListener("click", e => {
            e.preventDefault();

            const target = link.getAttribute("data-section");

            document.querySelectorAll(".section").forEach(sec => sec.classList.add("hidden"));
            document.getElementById(target).classList.remove("hidden");

            navLinks.forEach(nav => nav.classList.remove("active"));
            link.classList.add("active");

            if(target === "restaurants"){
                GetRestaurants();
            } else if(target === "categories"){
                GetCategories();
            } else {
                //#region pending
                //TODO: // crear la funcion para el dashboard, haremos todo de forma dinamica
            }
        })
    })

    document.querySelector(".logout-btn").addEventListener("click", () => {
        swal({
            title: "Esta seguro?",
            text: "Quieres cerrar sesion?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                swal(`Adios, vuelve cuando gustes`, {
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
    })

    function GetRestaurants(){
        fetch(URLS.GET_RESTAURANTS)
        .then(res => res.json())
        .then(data => {
            const container = document.getElementById("restaurant-list");
            container.innerHTML = "";

            if(data.length === 0){
                container.innerHTML = "<p>No hay restaurantes disponibles</p>"
                return;
            }

            data.forEach(r => {
                const card = document.createElement("div");
                card.className = "restaurant-card";
                card.innerHTML = `
                    <img src="${r.imgUrl || 'https://via.placeholder.com/300x150'}" alt="${r.name}" />
                    <h4>${r.name}</h4>
                    <p>${r.description}</p>
                    <p class="${r.active ? "open" : "closed"}">${r.active ? "Abierto" : "Cerrado"}</p>
                    <p>${r.openingTime} - ${r.closingTime}</p>
                    <div class="card-actions">
                        <button class="add-btn" data-id="${r.id}">Agregar Comida</button>
                        <button class="edit-btn" data-id="${r.id}">Editar</button>
                        <button class="delete-btn" data-id="${r.id}">Eliminar</button>
                    </div>
                `;

                container.appendChild(card);
            });

            document.querySelectorAll(".edit-btn").forEach(btn => {
                btn.addEventListener("click", () => {
                    const id = btn.dataset.id;
                    alert(`Editar restaurante ID: ${id} (funcionalidad pendiente...)`);
                })
            })

            document.querySelectorAll("delete-btn").forEach(btn => {
                btn.addEventListener("click", () => {
                    const id = btn.dataset.id;
                    
                    if(confirm("Estas seguro de que quieres eliminar este restaurante?")){
                        
                        fetch(URLS.DELETE_RESTAURANT(id), {method: "DELETE"} )
                        .then(res => {
                            if(res.ok){
                                swal({
                                    title: "Good job!",
                                    text: "Restaurante eliminado",
                                    icon: "success",
                                    button: "Volver",
                                })
                                .then(res => {
                                    GetRestaurants();
                                })
                            } else {
                                swal({
                                    title: "Error!",
                                    text: "error al eliminar restaurante",
                                    icon: "error",
                                    button: "Volver",
                                })
                                .then(res => {
                                    GetRestaurants();
                                })
                            }
                        })
                        .catch(err => console.error("Error cargando restaurantes ", err))
                    }
                })
            })
        })
    }




    function addResturant(){
        const body = {
            id: 0,
            name: document.getElementById('name').value,
            description: document.getElementById('description').value,
            active: document.getElementById('active').value === "true",
            openingTime: document.getElementById('openingTime').value,
            closingTime: document.getElementById('closingTime').value,
            deliveryAvailable: document.getElementById('deliveryAvailable').value === "true",
            imgUrl: document.getElementById('imgUrl').value,
            categoryId: parseInt(document.getElementById('categoryId').value)
        };
        //post para agregar restaurantes
        fetch(URLS.GET_RESTAURANTS, {
            method: "POST",
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(body)//TODO: conseguir el body que se va a mandar
        })
        .then(res => {
            if(res.ok) return res.json();
            else throw new Error("Error al guardar");
        })
        .then(result => {
            swal({
                title: "Good job!",
                text: "Restaurante agregado exitosamente!",
                icon: "success",
                button: "Volver"
            })
            .then(res => {
                closeModal();
            });
            
        })
        .catch(err => console.error("Error al guardar restaurante: ", err));
    }

    function GetCategories(){
        fetch(URLS.GET_CATEGORIES)
        .then(res => res.json())
        .then(data => {
            const container = document.getElementById("category-list");
            container.innerHTML = "";

            if(data.length === 0){
                container.innerHTML = "<p>No hay categorias disponibles</p>"
                return;
            }

            data.forEach(c => {
                const card = document.createElement("div");
                card.className = "restaurant-card";
                card.innerHTML = `
                    <img src="${c.imgUrl || 'https://via.placeholder.com/300x150'}" alt="${c.name}" />
                    <h4>${c.name}</h4>
                    <div class="card-actions">
                        <button class="edit-btn" data-id="${c.id}">Editar</button>
                        <button class="delete-btn" data-id="${c.id}">Eliminar</button>
                    </div>
                `;

                container.appendChild(card);
            });

            document.querySelectorAll(".edit-btn").forEach(btn => {
                btn.addEventListener("click", () => {
                    const id = btn.dataset.id;
                    alert(`Editar categoria ID: ${id} (funcionalidad pendiente...)`);
                })
            })

            document.querySelectorAll("delete-btn").forEach(btn => {
                btn.addEventListener("click", () => {
                    const id = btn.dataset.id;
                    
                    if(confirm("Estas seguro de que quieres eliminar esta categoria?")){
                        
                        fetch(URLS.DELETE_CATEGORY(id), {method: "DELETE"} )
                        .then(res => {
                            if(res.ok){
                                swal({
                                    title: "Good job!",
                                    text: "Categoria eliminada",
                                    icon: "success",
                                    button: "Volver",
                                })
                                .then(res => {
                                    GetCategories();
                                })
                            } else {
                                swal({
                                    title: "Error!",
                                    text: "error al eliminar categoria",
                                    icon: "error",
                                    button: "Volver",
                                })
                                .then(res => {
                                    GetCategories();
                                })
                            }
                        })
                        .catch(err => console.error("Error cargando restaurantes ", err))
                    }
                })
            })
        })
    }

    function GetDashboard(){}

    function openModal(){
        document.querySelectorAll(".modal-overlay").forEach(modal => {
            modal.style.display = "flex";
        })
    }

    function closeModal(){
        document.querySelectorAll(".modal-overlay").forEach(modal => {
            modal.style.display = "none";
        })
    }

    document.addEventListener("keydown", (e) => {
        if(e.key === "Escape") closeModal();
    })

    document.getElementById("addForm").addEventListener("submit", (e) => {
        e.preventDefault();
        addResturant();
    })

    document.querySelector(".btn-add-restaurant").addEventListener("click", () => {
        openModal();
    })

    document.querySelector(".btn-add-category").addEventListener("click", () => {
        openModal();
    })

    document.querySelectorAll(".modal-close").forEach(btn => {
        btn.addEventListener("click", () => {
            closeModal();
        })
    })
})