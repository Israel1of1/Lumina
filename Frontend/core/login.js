document.addEventListener('DOMContentLoaded', () => {
  const form = document.getElementById('form-login');
  const mensajeError = document.getElementById('mensaje-error');
  const mensajeInfo = document.getElementById('mensaje-info');

  form.addEventListener('submit', (e) => {
    e.preventDefault(); // Evita que el form recargue la página

    // ============================================
    // VALIDACIÓN REAL (comentada por ahora)
    // Descomentar esto cuando conectes con el backend
    // ============================================
    /*
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;

    mensajeError.classList.add('oculto');
    mensajeInfo.classList.add('oculto');

    if (!email || !password) {
      mensajeError.textContent = 'Por favor completa todos los campos.';
      mensajeError.classList.remove('oculto');
      return;
    }

    // Ejemplo de llamada al servicio de autenticación
    ApiServices.login(email, password)
      .then((respuesta) => {
        if (respuesta.ok) {
          window.location.href = 'dashboard.html';
        } else {
          mensajeError.textContent = 'Correo o contraseña incorrectos.';
          mensajeError.classList.remove('oculto');
        }
      })
      .catch(() => {
        mensajeError.textContent = 'Ocurrió un error. Intenta de nuevo.';
        mensajeError.classList.remove('oculto');
      });
    */
document.getElementById('btn-ver-password').addEventListener('click', () => {
  const input = document.getElementById('password');
  input.type = input.type === 'password' ? 'text' : 'password';
});

document.getElementById('enlace-olvido').addEventListener('click', (evento) => {
  evento.preventDefault();
  mostrarInfo('La recuperación de contraseña aún no está disponible en esta versión.');
});

    // ============================================
    // MODO TEMPORAL: entra directo sin validar
    // Borrar/comentar esta línea cuando actives la validación de arriba
    // ============================================
    window.location.href = 'dashboard.html';
  });
});