document.addEventListener('DOMContentLoaded', () => {
  const usuario = AppRouter.getUsuario();
  const tieneSesionValida = AppRouter.estaAutenticado();
  const esTutor = usuario?.roles?.includes('GUARDIAN');

  if (!tieneSesionValida || !esTutor) {
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    return;
  }

  const CLAVE_PERFIL = 'lumina_perfil_tutor';

  const toast = document.getElementById('guardian-toast');
  const sidebar = document.querySelector('.guardian-sidebar');
  const formulario = document.getElementById('form-perfil-tutor');

  const botonEditar = document.getElementById('btn-editar-perfil');
  const botonCancelar = document.getElementById('btn-cancelar-edicion');
  const botonGuardar = document.getElementById('btn-guardar-perfil');

  const campos = formulario.querySelectorAll('input, select');

  let temporizadorToast;
  let datosIniciales = {};

  function mostrarMensaje(mensaje) {
    toast.textContent = mensaje;
    toast.classList.add('guardian-toast--visible');

    clearTimeout(temporizadorToast);

    temporizadorToast = setTimeout(() => {
      toast.classList.remove('guardian-toast--visible');
    }, 3000);
  }

  function obtenerDatosFormulario() {
    return {
      nombre: document.getElementById('perfil-nombre').value.trim(),
      apellido: document.getElementById('perfil-apellido').value.trim(),
      correo: document.getElementById('perfil-correo').value.trim(),
      telefono: document.getElementById('perfil-telefono').value.trim(),
      ciudad: document.getElementById('perfil-ciudad').value.trim(),
      relacion: document.getElementById('perfil-relacion').value
    };
  }

  function cargarDatosGuardados() {
    const datosGuardados = localStorage.getItem(CLAVE_PERFIL);

    if (!datosGuardados) return;

    const perfil = JSON.parse(datosGuardados);

    document.getElementById('perfil-nombre').value = perfil.nombre;
    document.getElementById('perfil-apellido').value = perfil.apellido;
    document.getElementById('perfil-correo').value = perfil.correo;
    document.getElementById('perfil-telefono').value = perfil.telefono;
    document.getElementById('perfil-ciudad').value = perfil.ciudad;
    document.getElementById('perfil-relacion').value = perfil.relacion;
  }

  function activarEdicion() {
    datosIniciales = obtenerDatosFormulario();

    campos.forEach((campo) => {
      campo.disabled = false;
    });

    botonEditar.hidden = true;
    botonCancelar.hidden = false;
    botonGuardar.hidden = false;

    mostrarMensaje('Ahora puedes editar tu información.');
  }

  function cancelarEdicion() {
    document.getElementById('perfil-nombre').value = datosIniciales.nombre;
    document.getElementById('perfil-apellido').value = datosIniciales.apellido;
    document.getElementById('perfil-correo').value = datosIniciales.correo;
    document.getElementById('perfil-telefono').value = datosIniciales.telefono;
    document.getElementById('perfil-ciudad').value = datosIniciales.ciudad;
    document.getElementById('perfil-relacion').value = datosIniciales.relacion;

    campos.forEach((campo) => {
      campo.disabled = true;
    });

    botonEditar.hidden = false;
    botonCancelar.hidden = true;
    botonGuardar.hidden = true;

    mostrarMensaje('Los cambios fueron descartados.');
  }

  cargarDatosGuardados();

  document.getElementById('btn-cerrar-sesion').addEventListener('click', () => {
    AppRouter.cerrarSesion();
  });

  document.getElementById('btn-colapsar-menu').addEventListener('click', () => {
    sidebar.classList.toggle('guardian-sidebar--collapsed');
  });

  botonEditar.addEventListener('click', activarEdicion);

  botonCancelar.addEventListener('click', cancelarEdicion);

  formulario.addEventListener('submit', (evento) => {
    evento.preventDefault();

    const perfil = obtenerDatosFormulario();

    if (
      !perfil.nombre ||
      !perfil.apellido ||
      !perfil.correo ||
      !perfil.telefono ||
      !perfil.ciudad
    ) {
      mostrarMensaje('Completa todos los campos antes de guardar.');
      return;
    }

    localStorage.setItem(CLAVE_PERFIL, JSON.stringify(perfil));

    campos.forEach((campo) => {
      campo.disabled = true;
    });

    botonEditar.hidden = false;
    botonCancelar.hidden = true;
    botonGuardar.hidden = true;

    mostrarMensaje('Tu información se guardó correctamente.');
  });

  document.getElementById('btn-cambiar-password').addEventListener('click', () => {
    mostrarMensaje('La opción para cambiar contraseña estará disponible próximamente.');
  });

  const opcionesMenu = {
    'nav-recursos': 'Abriendo los recursos disponibles.',
    'nav-actividades': 'Abriendo las actividades adaptadas.',
    'nav-perfil-nino': 'Abriendo el perfil del niño.'
  };

  Object.entries(opcionesMenu).forEach(([id, mensaje]) => {
    document.getElementById(id).addEventListener('click', (evento) => {
      evento.preventDefault();
      mostrarMensaje(mensaje);
    });
  });
});