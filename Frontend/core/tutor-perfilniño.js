document.addEventListener("DOMContentLoaded", () => {
  const usuario = AppRouter.getUsuario();
  const tieneSesionValida = AppRouter.estaAutenticado();
  const esTutor = usuario?.roles?.includes("GUARDIAN");

  if (!tieneSesionValida || !esTutor) {
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    return;
  }

  const toast = document.getElementById("guardian-toast");
  const sidebar = document.querySelector(".guardian-sidebar");
  const formulario = document.getElementById("form-perfil-nino");
  const camposPerfil = formulario.querySelectorAll("input, select, textarea");

  const btnEditar = document.getElementById("btn-editar-perfil-nino");
  const btnGuardar = document.getElementById("btn-guardar-perfil-nino");
  const btnCancelar = document.getElementById("btn-cancelar-perfil-nino");

  let temporizadorToast;
  let valoresOriginales = {};

  function mostrarMensaje(mensaje) {
    toast.textContent = mensaje;
    toast.classList.add("guardian-toast--visible");

    clearTimeout(temporizadorToast);

    temporizadorToast = setTimeout(() => {
      toast.classList.remove("guardian-toast--visible");
    }, 3000);
  }

  function guardarValoresOriginales() {
    valoresOriginales = {};

    camposPerfil.forEach((campo) => {
      valoresOriginales[campo.id] = campo.value;
    });
  }

  function activarEdicion() {
    guardarValoresOriginales();

    camposPerfil.forEach((campo) => {
      campo.disabled = false;
    });

    btnEditar.hidden = true;
    btnGuardar.hidden = false;
    btnCancelar.hidden = false;

    mostrarMensaje("Ahora puedes editar la información de Thiago.");
  }

  function cancelarEdicion() {
    camposPerfil.forEach((campo) => {
      campo.value = valoresOriginales[campo.id];
      campo.disabled = true;
    });

    btnEditar.hidden = false;
    btnGuardar.hidden = true;
    btnCancelar.hidden = true;

    mostrarMensaje("Los cambios fueron cancelados.");
  }

  function finalizarEdicion() {
    camposPerfil.forEach((campo) => {
      campo.disabled = true;
    });

    btnEditar.hidden = false;
    btnGuardar.hidden = true;
    btnCancelar.hidden = true;
  }

  document
    .getElementById("btn-cerrar-sesion")
    .addEventListener("click", () => {
      AppRouter.cerrarSesion();
    });

  document
    .getElementById("btn-colapsar-menu")
    .addEventListener("click", () => {
      sidebar.classList.toggle("guardian-sidebar--collapsed");
    });

  btnEditar.addEventListener("click", activarEdicion);

  btnCancelar.addEventListener("click", cancelarEdicion);

  formulario.addEventListener("submit", (evento) => {
    evento.preventDefault();

    finalizarEdicion();
    mostrarMensaje("El perfil de Thiago se actualizó correctamente.");
  });

  document
    .getElementById("btn-editar-contacto")
    .addEventListener("click", () => {
      mostrarMensaje("Próximamente podrás editar el contacto de emergencia.");
    });

  document
    .getElementById("nav-recursos")
    .addEventListener("click", (evento) => {
      evento.preventDefault();
      mostrarMensaje("Abriendo los recursos disponibles.");
    });

  document
    .getElementById("nav-actividades")
    .addEventListener("click", (evento) => {
      evento.preventDefault();
      mostrarMensaje("Abriendo las actividades adaptadas.");
    });
});