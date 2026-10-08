document.addEventListener("DOMContentLoaded", async () => {

  // ============================================================
  // 1. VALIDAR SESIÓN
  // ============================================================

  const usuario = AppRouter.getUsuario();
  const tieneSesionValida = AppRouter.estaAutenticado();
  const esGuardian = usuario?.roles?.includes("GUARDIAN");

  if (!tieneSesionValida || !esGuardian) {
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    return;
  }


  // ============================================================
  // 2. ELEMENTOS DEL HTML
  // ============================================================

  const toast = document.getElementById("guardian-toast");
  const sidebar = document.querySelector(".guardian-sidebar");
  const formulario = document.getElementById("form-perfil-nino");

  const camposPerfil = formulario.querySelectorAll(
    "input, select, textarea"
  );

  const btnEditar = document.getElementById(
    "btn-editar-perfil-nino"
  );

  const btnGuardar = document.getElementById(
    "btn-guardar-perfil-nino"
  );

  const btnCancelar = document.getElementById(
    "btn-cancelar-perfil-nino"
  );


  // ============================================================
  // 3. VARIABLES
  // ============================================================

  let temporizadorToast;

  let valoresOriginales = {};

  // Aquí guardaremos el ID del niño
  let estudianteId = null;


  // ============================================================
  // 4. MOSTRAR MENSAJES
  // ============================================================

  function mostrarMensaje(mensaje) {

    toast.textContent = mensaje;

    toast.classList.add(
      "guardian-toast--visible"
    );

    clearTimeout(temporizadorToast);

    temporizadorToast = setTimeout(() => {

      toast.classList.remove(
        "guardian-toast--visible"
      );

    }, 3000);
  }


  // ============================================================
  // 5. GUARDAR VALORES ORIGINALES
  // ============================================================

  function guardarValoresOriginales() {

    valoresOriginales = {};

    camposPerfil.forEach((campo) => {

      valoresOriginales[campo.id] =
        campo.value;

    });
  }


  // ============================================================
  // 6. ACTIVAR EDICIÓN
  // ============================================================

  function activarEdicion() {

    guardarValoresOriginales();

    camposPerfil.forEach((campo) => {

      campo.disabled = false;

    });

    btnEditar.hidden = true;

    btnGuardar.hidden = false;

    btnCancelar.hidden = false;

    mostrarMensaje(
      "Ahora puedes editar la información del niño."
    );
  }


  // ============================================================
  // 7. CANCELAR EDICIÓN
  // ============================================================

  function cancelarEdicion() {

    camposPerfil.forEach((campo) => {

      campo.value =
        valoresOriginales[campo.id];

      campo.disabled = true;

    });

    btnEditar.hidden = false;

    btnGuardar.hidden = true;

    btnCancelar.hidden = true;

    mostrarMensaje(
      "Los cambios fueron cancelados."
    );
  }


  // ============================================================
  // 8. FINALIZAR EDICIÓN
  // ============================================================

  function finalizarEdicion() {

    camposPerfil.forEach((campo) => {

      campo.disabled = true;

    });

    btnEditar.hidden = false;

    btnGuardar.hidden = true;

    btnCancelar.hidden = true;
  }


  // ============================================================
  // 9. CARGAR PERFIL DEL NIÑO
  // ============================================================

  async function cargarPerfilNino() {

    try {

      mostrarMensaje(
        "Cargando información del niño..."
      );


      // --------------------------------------------------------
      // Pedimos al backend los niños relacionados
      // --------------------------------------------------------

      const respuesta =
        await GuardianService.getMyWards();


      console.log(
        "Respuesta de my-wards:",
        respuesta
      );


      // --------------------------------------------------------
      // Verificamos que exista información
      // --------------------------------------------------------

      if (
        !respuesta ||
        !respuesta.data ||
        respuesta.data.length === 0
      ) {

        mostrarMensaje(
          "No se encontró ningún niño relacionado con este Guardian."
        );

        return;
      }


      // --------------------------------------------------------
      // Obtenemos el primer niño relacionado
      // --------------------------------------------------------

      const nino =
        respuesta.data[0];


      console.log(
        "Niño seleccionado:",
        nino
      );


      // --------------------------------------------------------
      // Guardamos el ID del estudiante
      // --------------------------------------------------------

      estudianteId =
        nino.id;


      console.log(
        "StudentId:",
        estudianteId
      );


      // ========================================================
      // DATOS DEL FORMULARIO
      // ========================================================

      const campoNombre =
        document.getElementById(
          "nino-nombre"
        );

      const campoApellido =
        document.getElementById(
          "nino-apellido"
        );

      const campoFechaNacimiento =
        document.getElementById(
          "nino-fecha-nacimiento"
        );

      const campoGrupo =
        document.getElementById(
          "nino-grupo"
        );

      const campoNivelComunicacion =
        document.getElementById(
          "nino-nivel-comunicacion"
        );


      // --------------------------------------------------------
      // Nombre
      // --------------------------------------------------------

      if (campoNombre) {

        campoNombre.value =
          nino.firstName ?? "";
      }


      // --------------------------------------------------------
      // Apellido
      // --------------------------------------------------------

      if (campoApellido) {

        campoApellido.value =
          nino.lastName ?? "";
      }


      // --------------------------------------------------------
      // Fecha de nacimiento
      // --------------------------------------------------------

      if (campoFechaNacimiento) {

        campoFechaNacimiento.value =
          nino.birthDate
            ? nino.birthDate.substring(0, 10)
            : "";
      }


      // --------------------------------------------------------
      // Grupo
      // --------------------------------------------------------

      if (campoGrupo) {

        campoGrupo.value =
          nino.groupId
            ? `Grupo ${nino.groupId}`
            : "";
      }


      // --------------------------------------------------------
      // Nivel de comunicación
      // --------------------------------------------------------

      if (campoNivelComunicacion) {

        const nivel =
          (nino.languageLevel ?? "")
            .toLowerCase();


        if (nivel === "basico") {

          campoNivelComunicacion.value =
            "No verbal";

        } else if (nivel === "intermedio") {

          campoNivelComunicacion.value =
            "Verbal limitado";

        } else if (nivel === "avanzado") {

          campoNivelComunicacion.value =
            "Verbal funcional";
        }
      }


      // ========================================================
      // NOMBRE COMPLETO
      // ========================================================

      const nombreCompleto =
        `${nino.firstName ?? ""} ${nino.lastName ?? ""}`
          .trim();


      // ========================================================
      // TOPBAR
      // ========================================================

      const descripcionTopbar =
        document.querySelector(
          ".guardian-topbar p"
        );

      if (descripcionTopbar) {

        descripcionTopbar.textContent =
          `Información y preferencias de ${nombreCompleto}`;
      }


      // ========================================================
      // TÍTULO DEL PERFIL
      // ========================================================

      const tituloPerfil =
        document.querySelector(
          ".child-profile-heading h2"
        );

      if (tituloPerfil) {

        tituloPerfil.textContent =
          `Perfil de ${nombreCompleto}`;
      }


      // ========================================================
      // TÍTULO DE LOS DATOS
      // ========================================================

      const tituloDatos =
        document.querySelector(
          ".child-profile-form-card h2"
        );

      if (tituloDatos) {

        tituloDatos.textContent =
          `Datos de ${nino.firstName ?? "niño"}`;
      }


      // ========================================================
      // TARJETA RESUMEN
      // ========================================================

      const nombreTarjeta =
        document.querySelector(
          ".child-profile-summary h2"
        );

      if (nombreTarjeta) {

        nombreTarjeta.textContent =
          nombreCompleto;
      }


      // ========================================================
      // EDAD Y FECHA DE NACIMIENTO
      // ========================================================

      if (nino.birthDate) {

        const fechaNacimiento =
          new Date(nino.birthDate);

        const hoy =
          new Date();


        let edad =
          hoy.getFullYear() -
          fechaNacimiento.getFullYear();


        const diferenciaMes =
          hoy.getMonth() -
          fechaNacimiento.getMonth();


        if (
          diferenciaMes < 0 ||
          (
            diferenciaMes === 0 &&
            hoy.getDate() <
              fechaNacimiento.getDate()
          )
        ) {

          edad--;
        }


        const detalles =
          document.querySelector(
            ".child-profile-summary__details"
          );


        if (detalles) {

          const parrafos =
            detalles.querySelectorAll("p");


          // ----------------------------------------------------
          // Edad
          // ----------------------------------------------------

          if (parrafos[0]) {

            parrafos[0].innerHTML =
              `<span>♙</span> ${edad} años`;
          }


          // ----------------------------------------------------
          // Fecha
          // ----------------------------------------------------

          if (parrafos[1]) {

            parrafos[1].innerHTML =
              `<span>◷</span> Nació el ${
                fechaNacimiento.toLocaleDateString(
                  "es-NI"
                )
              }`;
          }


          // ----------------------------------------------------
          // Género
          // ----------------------------------------------------

          if (parrafos[2]) {

            parrafos[2].innerHTML =
              `<span>◌</span> ${
                nino.gender ?? ""
              }`;
          }
        }
      }


      // ========================================================
      // PERFIL CARGADO
      // ========================================================

      mostrarMensaje(
        "Información del niño cargada correctamente."
      );


    } catch (error) {

      console.error(
        "Error al cargar el perfil del niño:",
        error
      );

      mostrarMensaje(
        "No se pudo cargar la información del niño."
      );
    }
  }


  // ============================================================
  // 10. CERRAR SESIÓN
  // ============================================================

  const btnCerrarSesion =
    document.getElementById(
      "btn-cerrar-sesion"
    );


  if (btnCerrarSesion) {

    btnCerrarSesion.addEventListener(
      "click",
      () => {

        AppRouter.cerrarSesion();

      }
    );
  }


  // ============================================================
  // 11. COLAPSAR MENÚ
  // ============================================================

  const btnColapsarMenu =
    document.getElementById(
      "btn-colapsar-menu"
    );


  if (btnColapsarMenu) {

    btnColapsarMenu.addEventListener(
      "click",
      () => {

        sidebar.classList.toggle(
          "guardian-sidebar--collapsed"
        );

      }
    );
  }


  // ============================================================
  // 12. BOTÓN EDITAR
  // ============================================================

  btnEditar.addEventListener(
    "click",
    activarEdicion
  );


  // ============================================================
  // 13. BOTÓN CANCELAR
  // ============================================================

  btnCancelar.addEventListener(
    "click",
    cancelarEdicion
  );


  // ============================================================
  // 14. GUARDAR CAMBIOS
  // ============================================================

  formulario.addEventListener(
    "submit",
    async (evento) => {

      evento.preventDefault();


      /*
       * POR AHORA NO ENVIAMOS EL PUT.
       *
       * Primero comprobamos que el perfil
       * se cargue correctamente desde el backend.
       *
       * Cuando confirmemos que funciona,
       * conectaremos aquí el endpoint
       * para actualizar al estudiante.
       */

      finalizarEdicion();


      mostrarMensaje(
        "Los datos del niño fueron cargados correctamente."
      );
    }
  );


  // ============================================================
  // 15. EDITAR CONTACTO
  // ============================================================

  const btnEditarContacto =
    document.getElementById(
      "btn-editar-contacto"
    );


  if (btnEditarContacto) {

    btnEditarContacto.addEventListener(
      "click",
      () => {

        mostrarMensaje(
          "Próximamente podrás editar el contacto de emergencia."
        );

      }
    );
  }


  // ============================================================
  // 16. RECURSOS
  // ============================================================

  const navRecursos =
    document.getElementById(
      "nav-recursos"
    );


  if (navRecursos) {

    navRecursos.addEventListener(
      "click",
      (evento) => {

        evento.preventDefault();

        mostrarMensaje(
          "Abriendo los recursos disponibles."
        );

      }
    );
  }


  // ============================================================
  // 17. ACTIVIDADES
  // ============================================================

  const navActividades =
    document.getElementById(
      "nav-actividades"
    );


  if (navActividades) {

    navActividades.addEventListener(
      "click",
      (evento) => {

        evento.preventDefault();

        mostrarMensaje(
          "Abriendo las actividades adaptadas."
        );

      }
    );
  }


  // ============================================================
  // 18. CARGAR INFORMACIÓN AL ENTRAR
  // ============================================================

  await cargarPerfilNino();

});