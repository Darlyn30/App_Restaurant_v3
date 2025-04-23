const isDark = () => document.body.classList.contains('dark');
const themeBtn = document.getElementById('themeBtn');
const nameGroup = document.getElementById('nameGroup');
const confirmGroup = document.getElementById('confirmGroup');
const formTitle = document.getElementById('formTitle');
const toggleText = document.getElementById('toggleText');
const toggleForm = document.getElementById('toggleForm');
const submitBtn = document.getElementById('submitBtn');
let isLogin = true;

let id = 0;


let token = localStorage.getItem("token");
console.log(token);
if(token){
  window.location = "../subpages/home/Home.html";
}

const URLS = {
  URL_LOGIN : "https://localhost:7075/api/Account/login", //POST
  URL_SIGN_UP : "https://localhost:7075/api/User", //POST
  URL_GET_ID : `https://localhost:7075/api/User/${id}` //GET
}


// Tema claro/oscuro
themeBtn.addEventListener('click', () => {
    document.body.classList.toggle('dark');
    document.body.classList.toggle('light');
    themeBtn.innerHTML = isDark() ? '<i class="bi bi-sun-fill"></i>' : '<i class="bi bi-moon-fill"></i>';
});

// Toggle entre login y registro
toggleForm.addEventListener('click', (e) => {
    e.preventDefault();
    isLogin = !isLogin;
  
    nameGroup.classList.toggle('d-none', isLogin);
    confirmGroup.classList.toggle('d-none', isLogin);
    formTitle.innerText = isLogin ? 'Iniciar Sesión' : 'Crear Cuenta';
    submitBtn.innerText = isLogin ? 'Ingresar' : 'Registrarme';
    toggleText.innerHTML = isLogin
      ? '¿No tienes cuenta? <a href="#" id="toggleForm">Crea una aquí</a>'
      : '¿Ya tienes cuenta? <a href="#" id="toggleForm">Inicia sesión</a>';
  
    // Reasignar listener porque innerHTML borra eventos
    document.getElementById('toggleForm').addEventListener('click', (e) => {
      e.preventDefault();
      toggleForm.click();
    });
});

// Mostrar/ocultar contraseña
document.querySelectorAll('.toggle-password').forEach(icon => {
    icon.addEventListener('click', () => {
      const target = document.getElementById(icon.getAttribute('data-target'));
      const type = target.getAttribute('type') === 'password' ? 'text' : 'password';
      target.setAttribute('type', type);
      icon.classList.toggle('bi-eye-slash-fill');
    });
});

//se asegura que el correo sea de google
document.getElementById('authForm').addEventListener('submit', function(event) {

  const email = document.getElementById('email').value;
  const warningMessage = document.getElementById('warningMessage');
  
  // Verifica si el correo termina en '@gmail.com'
  if (!email.endsWith('@gmail.com')) {
    event.preventDefault(); // Evita el envío del formulario
    warningMessage.classList.remove('d-none'); // Muestra la advertencia
  } else {
    warningMessage.classList.add('d-none'); // Oculta la advertencia si el correo es válido
  }
});

//logica de la web app

// Nuevo eventListener para manejar el inicio de sesión y el registro
document.getElementById('authForm').addEventListener('submit', function(event) {
  // Solo proceder si el correo es válido (ya lo verificaste en el listener anterior)
  const email = document.getElementById('email').value;
  if (!email.endsWith('@gmail.com')) {
      return; // Si el correo no es válido, el listener anterior ya manejó el error
  }

  event.preventDefault(); // Evita el envío del formulario por defecto

  // Obtener los valores del formulario
  const emailValue = document.getElementById('email').value;
  const passwordValue = document.getElementById('password').value;

  if (isLogin) {
      // Modo: Iniciar Sesión
      console.log('Datos de inicio de sesión:', emailValue, passwordValue);

      const dataLogin = {
        Email: emailValue,
        Password: passwordValue
      }

      fetch(URLS.URL_LOGIN, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(dataLogin)
      })
      .then(async response => {
        const data = await response.json();
        if (!response.ok) {
          console.error("Error:", data);
          return;
        }
        console.log("Token:", data.token);
        console.log("perfil: ", data.userVm);
        localStorage.setItem("token", data.token);
        localStorage.setItem("account", data.userVm);

        swal({
          title: "Inicio de sesion exitoso!",
          text: "Bienvenido " +data.userVm.name + "!",
          icon: "success",
        })
        .then(res => {

          
          if(!data.userVm.isActive == true){
            window.location = "../subpages/Verification.html";
          }
          window.location = "../subpages/home/Home.html";
        });
      })
      .catch(error => console.error("Catch error:", error));

  } else {
      // Modo: Registro
      const nameValue = document.getElementById('name').value;
      const confirmPasswordValue = document.getElementById('confirmPassword').value;

      const registerData = {
          name: nameValue,
          email: emailValue,
          password: passwordValue, 
      };
      console.log('Datos de registro:', registerData);

      fetch(URLS.URL_SIGN_UP, {
        method: 'POST',
        headers : {
          'Content-Type' : 'application/json'
        },
        body: JSON.stringify(registerData)
      })
      .then(res => {
        if(registerData.name == "" || registerData.email == "" || registerData.password == "" || confirmPasswordValue == ""){
          swal({
            title: "ERROR!",
            text: "no puede haber campos vacíos \n O no podemos encontrar su cuenta \n O crea una",
            icon: "warning",
          });
        } else if(registerData.password != confirmPasswordValue){
          swal({
            title: "ERROR!",
            text: "Las contraseñas no coinciden",
            icon: "warning",
          });
        } else {
          console.log("success", res);
        
          swal({
              title: "Cuenta creada con exito!",
              text: "Su cuenta ha sido creada exitosamente!",
              icon: "success",
          })
          .then(res => {
              window.location = "../subpages/verification/Verification.html";
          });
        }
      })
  }
});
