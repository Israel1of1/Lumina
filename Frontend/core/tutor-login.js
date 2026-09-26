document.addEventListener('DOMContentLoaded', () => {
  const form = document.getElementById('form-login');
  const mensajeError = document.getElementById('mensaje-error');
  const mensajeInfo = document.getElementById('mensaje-info');

  document.getElementById('btn-ver-password').addEventListener('click', () => {
    const input = document.getElementById('password');
    input.type = input.type === 'password' ? 'text' : 'password';
  });

  document.getElementById('enlace-olvido').addEventListener('click', (evento) => {
    evento.preventDefault();

    mensajeInfo.textContent =
      'La recuperación de contraseña aún no está disponible en esta versión.';

    mensajeInfo.classList.remove('oculto');
  });

  form.addEventListener('submit', async (evento) => {
    evento.preventDefault();
    evento.stopImmediatePropagation();

    const email = document.getElementById('email').value.trim().toLowerCase();
    const password = document.getElementById('password').value;

    mensajeError.classList.add('oculto');
    mensajeInfo.classList.add('oculto');

    try {
      const respuesta = await AuthService.login(email, password);

      sessionStorage.setItem(APP_CONFIG.STORAGE_KEYS.TOKEN, respuesta.token);

      sessionStorage.setItem(
        APP_CONFIG.STORAGE_KEYS.USER,
        JSON.stringify(respuesta.user)
      );

      if (respuesta.user.roles.includes('GUARDIAN')) {
        window.location.href = APP_CONFIG.ROUTES.TUTOR_DASHBOARD;
        return;
      }

      if (respuesta.user.roles.includes('INSTITUTION')) {
        window.location.href = APP_CONFIG.ROUTES.DASHBOARD;
        return;
      }

      throw new Error('Tu usuario no tiene una vista asignada.');
    } catch (error) {
      mensajeError.textContent =
        error.message || 'Correo o contraseña incorrectos.';

      mensajeError.classList.remove('oculto');
    }
  }, true);
});