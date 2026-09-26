document.addEventListener('DOMContentLoaded', () => {
  const usuario = AppRouter.getUsuario();
  const tieneSesionValida = AppRouter.estaAutenticado();
  const esTutor = usuario?.roles?.includes('GUARDIAN');

  if (!tieneSesionValida || !esTutor) {
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    return;
  }

  const toast = document.getElementById('guardian-toast');
  const sidebar = document.querySelector('.guardian-sidebar');
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

  document.getElementById('btn-colapsar-menu').addEventListener('click', () => {
    sidebar.classList.toggle('guardian-sidebar--collapsed');
  });

  document.getElementById('btn-notificaciones').addEventListener('click', () => {
    mostrarMensaje('No tienes notificaciones nuevas.');
  });

  document.getElementById('btn-ver-progreso').addEventListener('click', () => {
  window.location.href = 'tutor-progreso.html';
  });

  document.getElementById('btn-reporte-progreso').addEventListener('click', () => {
    mostrarMensaje('Preparando el detalle de progreso de Mateo.');
  });

  document.getElementById('btn-opciones-grafico').addEventListener('click', () => {
    mostrarMensaje('Puedes consultar el reporte completo de progreso.');
  });

  document.getElementById('btn-ver-reporte').addEventListener('click', () => {
    mostrarMensaje('Preparando el reporte de progreso.');
  });

  document.getElementById('btn-calendario').addEventListener('click', () => {
    mostrarMensaje('Abriendo el calendario de actividades.');
  });

  document.getElementById('btn-ver-actividad').addEventListener('click', () => {
    mostrarMensaje('Abriendo la actividad de Matemática básica.');
  });

  const opcionesMenu = {
    'nav-progreso': 'Abriendo el progreso del niño.',
    'nav-seguimiento': 'Abriendo el seguimiento de Mateo.',
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