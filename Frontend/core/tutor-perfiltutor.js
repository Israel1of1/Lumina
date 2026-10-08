document.addEventListener('DOMContentLoaded', () => {

  // ============================================================
  // 1. VALIDAR SESIÓN
  // ============================================================

  const usuario = AppRouter.getUsuario();
  const tieneSesionValida = AppRouter.estaAutenticado();

  // El Guardian utiliza el rol GUARDIAN.
  const esGuardian = usuario?.roles?.includes('GUARDIAN');

  if (!tieneSesionValida || !esGuardian) {
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    return;
  }


  // ============================================================
  // 2. ELEMENTOS DE LA PANTALLA
  // ============================================================

  const toast = document.getElementById('guardian-toast');

  const sidebar =
    document.querySelector('.guardian-sidebar');

  const formulario =
    document.getElementById('form-perfil-tutor');

  const botonEditar =
    document.getElementById('btn-editar-perfil');

  const botonCancelar =
    document.getElementById('btn-cancelar-edicion');

  const botonGuardar =
    document.getElementById('btn-guardar-perfil');

  const campos =
    formulario.querySelectorAll('input, select');


  // ============================================================
  // 3. VARIABLES
  // ============================================================

  let temporizadorToast;

  let datosIniciales = {};

  let perfilActual = null;


  // ============================================================
  // 4. MOSTRAR MENSAJES
  // ============================================================

  function mostrarMensaje(mensaje) {

    toast.textContent = mensaje;

    toast.classList.add('guardian-toast--visible');

    clearTimeout(temporizadorToast);

    temporizadorToast = setTimeout(() => {
      toast.classList.remove('guardian-toast--visible');
    }, 3000);
  }


  // ============================================================
  // 5. OBTENER DATOS DEL FORMULARIO
  // ============================================================

  function obtenerDatosFormulario() {

    return {

      firstName:
        document.getElementById('perfil-nombre').value.trim(),

      lastName:
        document.getElementById('perfil-apellido').value.trim(),

      personalEmail:
        document.getElementById('perfil-correo').value.trim(),

      phone:
        document.getElementById('perfil-telefono').value.trim(),

      city:
        document.getElementById('perfil-ciudad').value.trim(),

      relationship:
        document.getElementById('perfil-relacion').value
    };
  }


  // ============================================================
  // 6. MOSTRAR LOS DATOS DEL BACKEND EN LA PANTALLA
  // ============================================================

  function mostrarPerfil(perfil) {

    perfilActual = perfil;


    document.getElementById('perfil-nombre').value =
      perfil.firstName || '';

    document.getElementById('perfil-apellido').value =
      perfil.lastName || '';

    document.getElementById('perfil-correo').value =
      perfil.personalEmail || '';

    document.getElementById('perfil-telefono').value =
      perfil.phone || '';

    document.getElementById('perfil-ciudad').value =
      perfil.city || '';

    document.getElementById('perfil-relacion').value =
      perfil.relationship || '';


    // ==========================================================
    // Actualizar información que aparece en la tarjeta
    // lateral del perfil.
    // ==========================================================

    const nombreCompleto =
      `${perfil.firstName || ''} ${perfil.lastName || ''}`.trim();


    const nombreTarjeta =
      document.querySelector('.profile-summary-card h2');

    if (nombreTarjeta) {
      nombreTarjeta.textContent =
        nombreCompleto || 'Guardian';
    }


    // Iniciales
    const iniciales =
      obtenerIniciales(
        perfil.firstName,
        perfil.lastName
      );


    const avatar =
      document.querySelector(
        '.profile-summary-card__avatar'
      );

    if (avatar) {
      avatar.textContent = iniciales;
    }


    // Información de la tarjeta
    const detalles =
      document.querySelectorAll(
        '.profile-summary-card__details p'
      );


    if (detalles[0]) {
      detalles[0].innerHTML =
        `<span>✉</span> ${perfil.personalEmail || '—'}`;
    }


    if (detalles[1]) {
      detalles[1].innerHTML =
        `<span>☎</span> ${perfil.phone || '—'}`;
    }


    if (detalles[2]) {
      detalles[2].innerHTML =
        `<span>⌂</span> ${perfil.city || '—'}`;
    }
  }


  // ============================================================
  // 7. OBTENER INICIALES
  // ============================================================

  function obtenerIniciales(nombre, apellido) {

    const primeraLetra =
      nombre?.trim()?.charAt(0) || '';

    const segundaLetra =
      apellido?.trim()?.charAt(0) || '';

    return `${primeraLetra}${segundaLetra}`.toUpperCase();
  }


  // ============================================================
  // 8. CARGAR PERFIL DESDE EL BACKEND
  // ============================================================

  async function cargarPerfil() {

    try {

      mostrarMensaje(
        'Cargando información del Guardian...'
      );


      const perfil =
        await GuardianService.getMyProfile();


      if (!perfil) {

        mostrarMensaje(
          'No se encontró el perfil del Guardian.'
        );

        return;
      }


      mostrarPerfil(perfil);


    } catch (error) {

      console.error(
        'Error al cargar el perfil:',
        error
      );

      mostrarMensaje(
        `No se pudo cargar el perfil: ${error.message}`
      );
    }
  }


  // ============================================================
  // 9. ACTIVAR EDICIÓN
  // ============================================================

  function activarEdicion() {

    // Guardamos los datos actuales
    // por si el usuario cancela.

    datosIniciales =
      obtenerDatosFormulario();


    campos.forEach((campo) => {
      campo.disabled = false;
    });


    botonEditar.hidden = true;

    botonCancelar.hidden = false;

    botonGuardar.hidden = false;


    mostrarMensaje(
      'Ahora puedes editar tu información.'
    );
  }


  // ============================================================
  // 10. CANCELAR EDICIÓN
  // ============================================================

  function cancelarEdicion() {

    document.getElementById('perfil-nombre').value =
      datosIniciales.firstName || '';

    document.getElementById('perfil-apellido').value =
      datosIniciales.lastName || '';

    document.getElementById('perfil-correo').value =
      datosIniciales.personalEmail || '';

    document.getElementById('perfil-telefono').value =
      datosIniciales.phone || '';

    document.getElementById('perfil-ciudad').value =
      datosIniciales.city || '';

    document.getElementById('perfil-relacion').value =
      datosIniciales.relationship || '';


    campos.forEach((campo) => {
      campo.disabled = true;
    });


    botonEditar.hidden = false;

    botonCancelar.hidden = true;

    botonGuardar.hidden = true;


    mostrarMensaje(
      'Los cambios fueron cancelados.'
    );
  }


  // ============================================================
  // 11. GUARDAR PERFIL EN EL BACKEND
  // ============================================================

  async function guardarPerfil(evento) {

    evento.preventDefault();


    const datosFormulario =
      obtenerDatosFormulario();


    // Validación básica
    if (
      !datosFormulario.firstName ||
      !datosFormulario.lastName ||
      !datosFormulario.personalEmail ||
      !datosFormulario.phone ||
      !datosFormulario.city
    ) {

      mostrarMensaje(
        'Completa todos los campos antes de guardar.'
      );

      return;
    }


    try {

      mostrarMensaje(
        'Guardando cambios...'
      );


      /*
       * El backend espera estos nombres:
       *
       * FirstName
       * LastName
       * NationalId
       * PersonalEmail
       * Phone
       * Address
       * City
       * Photo
       * Relationship
       *
       * Los nombres se mandan en camelCase desde JavaScript.
       */

      const datos = {

        firstName:
          datosFormulario.firstName,

        lastName:
          datosFormulario.lastName,

        nationalId:
          perfilActual?.nationalId || null,

        personalEmail:
          datosFormulario.personalEmail,

        phone:
          datosFormulario.phone,

        address:
          perfilActual?.address || null,

        city:
          datosFormulario.city,

        photo:
          perfilActual?.photo || null,

        relationship:
          datosFormulario.relationship || null
      };


      const perfilActualizado =
        await GuardianService.updateMyProfile(datos);


      // Guardamos el nuevo perfil en memoria
      perfilActual =
        perfilActualizado;


      // Mostramos nuevamente la información
      mostrarPerfil(
        perfilActualizado
      );


      // Bloquear campos
      campos.forEach((campo) => {
        campo.disabled = true;
      });


      botonEditar.hidden = false;

      botonCancelar.hidden = true;

      botonGuardar.hidden = true;


      mostrarMensaje(
        'Tu información se guardó correctamente.'
      );


    } catch (error) {

      console.error(
        'Error al actualizar el perfil:',
        error
      );

      mostrarMensaje(
        `No se pudieron guardar los cambios: ${error.message}`
      );
    }
  }


  // ============================================================
  // 12. CERRAR SESIÓN
  // ============================================================

  document
    .getElementById('btn-cerrar-sesion')
    .addEventListener('click', () => {

      AppRouter.cerrarSesion();

    });


  // ============================================================
  // 13. COLAPSAR MENÚ
  // ============================================================

  document
    .getElementById('btn-colapsar-menu')
    .addEventListener('click', () => {

      sidebar.classList.toggle(
        'guardian-sidebar--collapsed'
      );

    });


  // ============================================================
  // 14. EDITAR / CANCELAR / GUARDAR
  // ============================================================

  botonEditar.addEventListener(
    'click',
    activarEdicion
  );

  botonCancelar.addEventListener(
    'click',
    cancelarEdicion
  );

  formulario.addEventListener(
    'submit',
    guardarPerfil
  );


  // ============================================================
  // 15. CAMBIAR CONTRASEÑA
  // ============================================================

  document
    .getElementById('btn-cambiar-password')
    .addEventListener('click', () => {

      mostrarMensaje(
        'La opción para cambiar contraseña estará disponible próximamente.'
      );

    });


  // ============================================================
  // 16. NAVEGACIÓN DEL MENÚ
  // ============================================================

  const opcionesMenu = {

    'nav-recursos':
      'Abriendo los recursos disponibles.',

    'nav-actividades':
      'Abriendo las actividades adaptadas.',

    'nav-perfil-nino':
      'Abriendo el perfil del niño.'
  };


  Object.entries(opcionesMenu).forEach(
    ([id, mensaje]) => {

      const elemento =
        document.getElementById(id);

      if (!elemento) return;

      elemento.addEventListener(
        'click',
        (evento) => {

          evento.preventDefault();

          mostrarMensaje(mensaje);

        }
      );
    }
  );


  // ============================================================
  // 17. CARGAR EL PERFIL
  // ============================================================

  cargarPerfil();

});