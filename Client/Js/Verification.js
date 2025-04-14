const button = document.querySelector(".verify-button");
function moveToNext(current, nextFieldID) {
    if (current.value.length === 1) {
      document.getElementById(nextFieldID).focus(); // hace que cada vez que se entra un digito del codigo, cambie el 
      //enfoque al siguiente
    }
}



function verificarCodigo() {
    const code = Array.from(document.querySelectorAll('.code-input')).map(input => input.value).join('');
    
    fetch(URL)
    .then(res => res.json())
    .then(data => {
        for(let i = 0; i < data.length; i++){
            if(code == data[i].pin){
                deleteD(data[i].email);
            } else {
                alert("Codigo de verificacion no valido");
            }
        }
    })
}

function maskEmail(email) {
    // Separar el correo en la parte del usuario y el dominio usando el "@"
    const [username, domain] = email.split('@');
    
    // Tomar solo la primera letra del nombre de usuario
    const firstLetter = username.charAt(0);
    
    // Crear una cadena de asteriscos para ocultar el resto del nombre de usuario
    const maskedUsername = firstLetter + '*'.repeat(username.length - 1);
    
    // Combinar la parte enmascarada con el dominio
    return `${maskedUsername}@${domain}`;
}

// Ejemplo de uso
const email = "juanperez@gmail.com"; //TODO cambiar esto, que es de ejemplo por un correo real
const maskedEmail = maskEmail(email); // Esto devolverá "j*******@gmail.com"

// Insertar el correo enmascarado en el HTML
document.getElementById('email-display').textContent = maskedEmail;
