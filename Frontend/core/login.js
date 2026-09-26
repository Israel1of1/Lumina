document.addEventListener('DOMContentLoaded', () => {

  const form = document.getElementById('form-login');
  const mensajeError = document.getElementById('mensaje-error');
  const mensajeInfo = document.getElementById('mensaje-info');

  const inputEmail = document.getElementById('email');
  const inputPassword = document.getElementById('password');

  const btnVerPassword = document.getElementById('btn-ver-password');
  const enlaceOlvido = document.getElementById('enlace-olvido');

  const VISTAS_POR_ROL = {
    INSTITUTION: 'dashboard.html',
    TEACHER: 'inicio-docente.html',
    TUTOR: 'tutor-dashboard.html',
    GUARDIAN: 'tutor-dashboard.html'
  };

  function mostrarError(mensaje) {
    mensajeError.textContent = mensaje;
    mensajeError.classList.remove('oculto');
    mensajeInfo.classList.add('oculto');
  }

  function mostrarInfo(mensaje) {
    mensajeInfo.textContent = mensaje;
    mensajeInfo.classList.remove('oculto');
    mensajeError.classList.add('oculto');
  }

  function limpiarMensajes() {
    mensajeError.classList.add('oculto');
    mensajeInfo.classList.add('oculto');
  }

  if (btnVerPassword) {
    btnVerPassword.addEventListener('click', () => {
      inputPassword.type =
        inputPassword.type === 'password' ? 'text' : 'password';
    });
  }

  if (enlaceOlvido) {
    enlaceOlvido.addEventListener('click', (evento) => {
      evento.preventDefault();
      mostrarInfo(
        'La recuperación de contraseña aún no está disponible en esta versión.'
      );
    });
  }

  form.addEventListener('submit', async (e) => {
    e.preventDefault();

    limpiarMensajes();

    const email = inputEmail.value.trim();
    const password = inputPassword.value;

    if (!email || !password) {
      mostrarError('Por favor completa todos los campos.');
      return;
    }

    try {
      const respuesta = await AuthService.login(email, password);

      console.log('Respuesta completa:', respuesta);

      if (!respuesta || !respuesta.token || !respuesta.user) {
        mostrarError('No se pudo iniciar sesión.');
        return;
      }

      sessionStorage.setItem('lumina_token', respuesta.token);

      sessionStorage.setItem(
        'lumina_user',
        JSON.stringify(respuesta.user)
      );

      const roles = Array.isArray(respuesta.user.roles)
        ? respuesta.user.roles
        : [];

      console.log('Usuario:', respuesta.user);
      console.log('Roles:', roles);

      if (roles.length === 0) {
        mostrarError('Tu usuario no tiene un rol asignado.');
        return;
      }

      let vista = null;

      for (const rol of roles) {
        const rolNormalizado = String(rol)
          .trim()
          .toUpperCase();

        if (VISTAS_POR_ROL[rolNormalizado]) {
          vista = VISTAS_POR_ROL[rolNormalizado];
          break;
        }
      }

      if (!vista) {
        console.error('Roles recibidos:', roles);
        mostrarError('Tu rol no tiene una vista asignada.');
        return;
      }

      window.location.href = vista;

    } catch (error) {
      console.error('Error al iniciar sesión:', error);

      mostrarError(
        error.message || 'Correo o contraseña incorrectos.'
      );
    }
  });

});