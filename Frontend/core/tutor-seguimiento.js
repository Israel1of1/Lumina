document.addEventListener('DOMContentLoaded', () => {
  const usuario = AppRouter.getUsuario();
  const tieneSesionValida = AppRouter.estaAutenticado();
  const esTutor = usuario?.roles?.includes('GUARDIAN');

  if (!tieneSesionValida || !esTutor) {
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    return;
  }

  const toast = document.getElementById('guardian-toast');
  let temporizadorToast;

  function mostrarMensaje(mensaje) {
    toast.textContent = mensaje;
    toast.classList.add('guardian-toast--visible');

    clearTimeout(temporizadorToast);

    temporizadorToast = setTimeout(() => {
      toast.classList.remove('guardian-toast--visible');
    }, 3000);
  }

  document.getElementById('btn-cerrar-sesion').addEventListener('click', () => {
    AppRouter.cerrarSesion();
  });

  document.getElementById('btn-ver-calendario').addEventListener('click', () => {
    mostrarMensaje('Abriendo el calendario de Thiago.');
  });

  document.getElementById('btn-ver-asistencia').addEventListener('click', () => {
    mostrarMensaje('Abriendo el historial de asistencia.');
  });

  document.getElementById('btn-ver-observaciones').addEventListener('click', () => {
    mostrarMensaje('Mostrando todas las observaciones de Thiago.');
  });

  document.getElementById('btn-ver-actividad-casa').addEventListener('click', () => {
    mostrarMensaje('Abriendo la actividad para casa.');
  });

  const opcionesMenu = {
    'nav-recursos': 'Abriendo los recursos disponibles.',
    'nav-actividades': 'Abriendo las actividades adaptadas.',
    'nav-pecs': 'Abriendo Comunicación PECS.',
    'nav-perfil-tutor': 'Abriendo el perfil del tutor.',
    'nav-perfil-nino': 'Abriendo el perfil del niño.'
  };

  Object.entries(opcionesMenu).forEach(([id, mensaje]) => {
    document.getElementById(id).addEventListener('click', (evento) => {
      evento.preventDefault();
      mostrarMensaje(mensaje);
    });
  });
});