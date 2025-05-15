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
document.getElementById('authForm').addEventListener('submit', async function(event) {
  // Solo proceder si el correo es válido (ya lo verificaste en el listener anterior)
  const email = document.getElementById('email').value;
  if (!email.endsWith('@gmail.com')) {
      return; // Si el correo no es válido, el listener anterior ya manejó el error
  }
  //#region gmailService

  event.preventDefault(); // Evita el envío del formulario por defecto

  // Obtener los valores del formulario
  const emailValue = document.getElementById('email').value;
  const passwordValue = document.getElementById('password').value;
  //#region login
  if (isLogin) {
      // Modo: Iniciar Sesión
      console.log('Datos de inicio de sesión:', emailValue, passwordValue);

      const dataLogin = {
        Email: emailValue,
        Password: passwordValue
      }
      try {
          const data = await apiCall(URLS.URL_LOGIN, "POST", dataLogin); // auth = false

          console.log("Token:", data.token);
          console.log("perfil: ", data.userVm);
          localStorage.setItem("token", JSON.stringify(data.token));
          localStorage.setItem("account", JSON.stringify(data.userVm));
          const user = data.userVm;

          swal({
              title: "Inicio de sesión exitoso!",
              text: `Bienvenido ${user.name}!`,
              icon: "success"
          }).then(() => {
              if (!user.isActive) {
                  window.location = "../subpages/Verification.html";
              } else if (user.role === "Client") {
                  window.location = "../subpages/home/Home.html";
              } else {
                  window.location = "../subpages/Admin/Index.html";
              }
          });
      } catch (error) {
          swal("Ha ocurrido un error", error.message, "warning");
      }
      //#region signup
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

    if (!nameValue || !emailValue || !passwordValue || !confirmPasswordValue) {
        return swal("ERROR!", "No puede haber campos vacíos", "warning");
    }

    if (passwordValue !== confirmPasswordValue) {
        return swal("ERROR!", "Las contraseñas no coinciden", "warning");
    }

    try {
        const data = await apiCall(URLS.URL_SIGN_UP, "POST", registerData, false); // auth = false

        swal("Cuenta creada con éxito", "Redirigiendo a verificación...", "success")
            .then(() => {
                window.location = "../subpages/verification/Verification.html";
            });
    } catch (error) {
        swal("Error al registrar", error.message, "error");
    }
      }
    });

async function apiCall(url, method, body = null) {
    const options = {
        method,
        headers : {
            "Content-Type" : "application/json"
        }
    }

    if(body) options.body = JSON.stringify(body);

    try {
        const res = await fetch(url, options);

        const data = res.json();
        if(!res.ok) throw new Error(data.message || "");

        return data;
    } catch(err){
        console.error("Error en la peticion: ", err.message);
        throw err;
    }
}
