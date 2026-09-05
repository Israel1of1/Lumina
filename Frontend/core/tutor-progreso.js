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

  document.getElementById('select-periodo').addEventListener('change', (evento) => {
    mostrarMensaje(`Mostrando progreso: ${evento.target.value}.`);
  });

  document.getElementById('btn-ver-objetivos').addEventListener('click', () => {
    mostrarMensaje('Abriendo los objetivos de Mateo.');
  });

  document.getElementById('btn-ver-areas').addEventListener('click', () => {
    mostrarMensaje('Mostrando todas las áreas de aprendizaje.');
  });

  const opcionesMenu = {
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