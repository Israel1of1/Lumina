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
  const frase = document.getElementById('pecs-frase');
  const palabras = [];
  let temporizadorToast;

  function mostrarMensaje(mensaje) {
    toast.textContent = mensaje;
    toast.classList.add('guardian-toast--visible');

    clearTimeout(temporizadorToast);

    temporizadorToast = setTimeout(() => {
      toast.classList.remove('guardian-toast--visible');
    }, 3000);
  }

  function renderizarFrase() {
    if (palabras.length === 0) {
      frase.innerHTML =
        '<span class="pecs-sentence__empty">Selecciona tarjetas para formar una frase.</span>';
      return;
    }

    frase.innerHTML = palabras
      .map((palabra) => `<span class="pecs-sentence__word">${palabra}</span>`)
      .join('');
  }

  document.getElementById('btn-cerrar-sesion').addEventListener('click', () => {
    AppRouter.cerrarSesion();
  });

  document.getElementById('btn-colapsar-menu').addEventListener('click', () => {
    sidebar.classList.toggle('guardian-sidebar--collapsed');
  });

  document.getElementById('btn-limpiar-frase').addEventListener('click', () => {
    palabras.length = 0;
    renderizarFrase();
    mostrarMensaje('La frase se limpió.');
  });

  document.getElementById('btn-reproducir').addEventListener('click', () => {
    if (palabras.length === 0) {
      mostrarMensaje('Primero selecciona una o más tarjetas.');
      return;
    }

    mostrarMensaje(`Reproduciendo: ${palabras.join(' ')}.`);
  });

  document.querySelectorAll('.pecs-card').forEach((tarjeta) => {
    tarjeta.addEventListener('click', () => {
      palabras.push(tarjeta.dataset.word);
      renderizarFrase();
    });
  });

  document.querySelectorAll('.pecs-history__item').forEach((item) => {
    item.addEventListener('click', () => {
      palabras.length = 0;
      palabras.push(...item.dataset.phrase.split(' '));
      renderizarFrase();
      mostrarMensaje('Frase frecuente cargada.');
    });
  });

  document.querySelectorAll('.pecs-category-tab').forEach((tab) => {
    tab.addEventListener('click', () => {
      document.querySelector('.pecs-category-tab--active')
        .classList.remove('pecs-category-tab--active');

      tab.classList.add('pecs-category-tab--active');
      mostrarMensaje(`Mostrando categoría: ${tab.textContent}.`);
    });
  });

  document.getElementById('btn-ver-historial').addEventListener('click', () => {
    mostrarMensaje('Abriendo el historial de comunicación.');
  });

const opcionesMenu = {
  'nav-seguimiento': 'Abriendo el seguimiento de Thiago.',
  'nav-pecs': 'Abriendo Comunicación PECS.',
  'nav-recursos': 'Abriendo los recursos disponibles.',
  'nav-actividades': 'Abriendo las actividades adaptadas.',
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