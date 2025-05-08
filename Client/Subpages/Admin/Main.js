document.addEventListener("DOMContentLoaded", () => {

    //despues que todo el contenido cargue
    document.querySelectorAll(".nav-link").forEach(link => {
        link.addEventListener("click", e => {
          e.preventDefault();
          const targetId = link.getAttribute("data-section");
      
          // Ocultar todas las secciones
          document.querySelectorAll(".section").forEach(sec => {
            sec.classList.add("hidden");
          });
      
          // Mostrar sección activa
          const targetSection = document.getElementById(targetId);
          targetSection.classList.remove("hidden");
      
          // Cambiar clase activa
          document.querySelectorAll(".nav-link").forEach(nav => nav.classList.remove("active"));
          link.classList.add("active");
      
          // Ejecutar lógica específica
          if (targetId === "restaurants") {
            cargarRestaurantes(); // Por ejemplo, puedes llamar aquí una función para cargar los restaurantes
          } else if(targetId === "categories"){}
        });
    });
})

const token = JSON.parse(localStorage.getItem("token"));
const user = JSON.parse(localStorage.getItem("account"));
console.log(token);
console.log(user);

document.querySelector(".logout-btn").addEventListener("click", () => {
    //boton para cerrar sesion
    localStorage.removeItem("token");
    localStorage.removeItem("account");

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

})


  