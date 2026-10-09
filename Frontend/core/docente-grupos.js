console.log("Se esta ejecutando correctamente")
if (!AppRouter.protegerPagina(['TEACHER', 'DOCENTE'])) {
  throw new Error('Acceso no autorizado');
}

let grupoActual = null;
let grupos = [];
let estudiantes = [];

async function cargarDatosReales() {
  const perfil = await TeacherService.getMyProfile();
  const asignaciones = await GroupSubjectService.getByTeacher(perfil.id);

  const idsGruposUnicos = [...new Set(asignaciones.map(a => a.groupId))];

  const gruposCompletos = await Promise.all(
    idsGruposUnicos.map(id => ClassGroupService.getById(id))
  );

  grupos = gruposCompletos.map(g => ({
    id: g.id,
    nombre: g.name,
    grado: g.gradeLevel || "",
    descripcion: g.description || ""
  }));

  const estudiantesPorGrupo = await Promise.all(
    idsGruposUnicos.map(id => StudentService.getByGroup(id))
  );

  const todosRaw = estudiantesPorGrupo.flat();

  estudiantes = await Promise.all(todosRaw.map(async e => {
    let gustos = "";
    let responsable = "";

    try {
      const [intereses, relaciones] = await Promise.all([
        StudentInterestService.getByStudent(e.id).catch(() => []),
        StudentRelationService.getByStudent(e.id).catch(() => [])
      ]);

      if (Array.isArray(intereses) && intereses.length > 0) {
        gustos = intereses.map(i => i.name || i.description).filter(Boolean).join(", ");
      }

      if (Array.isArray(relaciones) && relaciones.length > 0) {
        const principal = relaciones.find(r => r.isActive) || relaciones[0];
        const nom = [principal.entityFirstName, principal.entityLastName].filter(Boolean).join(" ");
        responsable = nom ? `${nom} (${principal.relationType || "Tutor"})` : (principal.relationType || "");
      }
    } catch (err) {
      console.warn("Error cargando detalles del estudiante:", err);
    }

    return {
      id: e.id,
      nombre: `${e.firstName} ${e.lastName || ""}`.trim(),
      edad: calcularEdad(e.birthDate),
      grupo: e.groupId,
      grado: grupos.find(g => g.id === e.groupId)?.grado || "",
      tea: e.clinicalInfo || "No registrado",
      juguete: gustos || "",
      color: "",
      responsable: responsable || "",
      observaciones: e.observations || "",
      foto: ""
    };
  }));
}

function calcularEdad(fechaNacimiento) {
  if (!fechaNacimiento) return "—";
  const nacimiento = new Date(fechaNacimiento);
  const hoy = new Date();
  let edad = hoy.getFullYear() - nacimiento.getFullYear();
  const mesActual = hoy.getMonth() - nacimiento.getMonth();
  if (mesActual < 0 || (mesActual === 0 && hoy.getDate() < nacimiento.getDate())) edad--;
  return edad;
}

const $ = selector => document.querySelector(selector);

function obtenerFoto(estudiante) {
  if (estudiante.foto) return estudiante.foto;

  return `https://ui-avatars.com/api/?name=${encodeURIComponent(
    estudiante.nombre
  )}&background=dbeaff&color=075fc9&bold=true`;
}

function mostrarVista(id) {
  document.querySelectorAll(".vista").forEach(vista => {
    vista.classList.remove("activa");
  });

  $("#" + id).classList.add("activa");
}

function cantidadEstudiantes(idGrupo) {
  return estudiantes.filter(estudiante => estudiante.grupo === idGrupo).length;
}

function renderizarGrupos() {
  const texto = $("#buscar-grupo").value.toLowerCase();

  const lista = grupos.filter(grupo =>
    grupo.nombre.toLowerCase().includes(texto) ||
    grupo.grado.toLowerCase().includes(texto)
  );

  $("#contenedor-grupos").innerHTML = lista.map(grupo => `
    <article class="tarjeta-grupo">
      <div class="icono-grupo">${grupo.nombre.replace("Grupo ", "")}</div>

      <h3>${grupo.nombre}</h3>

      <p class="meta">
        ${grupo.grado}<br>
        ${cantidadEstudiantes(grupo.id)} estudiantes
      </p>

      <p class="descripcion">${grupo.descripcion}</p>

      <div class="acciones-tarjeta">
        <button
          class="boton-icono"
          title="Ver estudiantes"
          onclick="abrirGrupo(${grupo.id})"
        >
          ◉ ver estudiantes
        </button>


      </div>
    </article>
  `).join("");
}

function abrirGrupo(id) {
  grupoActual = id;

  const grupo = grupos.find(item => item.id === id);

  $("#titulo-estudiantes").textContent = grupo.nombre;

  $("#subtitulo-estudiantes").textContent =
  `${grupo.grado} · ${cantidadEstudiantes(id)} estudiantes`;
  mostrarVista("vista-estudiantes");
  renderizarEstudiantes();
}

function renderizarEstudiantes() {
  const texto = $("#buscar-estudiante").value.toLowerCase();

  const lista = estudiantes.filter(estudiante => {
    const pertenece = !grupoActual || estudiante.grupo === grupoActual;
    const coincide = estudiante.nombre.toLowerCase().includes(texto);

    return pertenece && coincide;
  });

  $("#contenedor-estudiantes").innerHTML = lista.map(estudiante => `
    <article class="tarjeta-estudiante">
      <img
        class="foto-estudiante"
        src="${obtenerFoto(estudiante)}"
        alt="Foto de ${estudiante.nombre}"
      >

      <div class="info-estudiante">
        <h3>${estudiante.nombre}</h3>
        <p>${estudiante.edad} años · ${estudiante.grado}</p>

        <div class="acciones-estudiante">
          <button
            class="boton-estudiante"
            onclick="verPerfil(${estudiante.id})"
          >
            ◉ Ver perfil

        </div>
      </div>
    </article>
  `).join("");
}

function verPerfil(id) {
  const estudiante = estudiantes.find(item => item.id === id);
  const grupo = grupos.find(item => item.id === estudiante.grupo);

  $("#vista-perfil").innerHTML = `
    <div class="encabezado-docente">
      <div>
        <button
          class="boton-volver"
          type="button"
          onclick="mostrarVista('vista-estudiantes')"
        >
          ←
        </button>

        <h1>Perfil del estudiante</h1>
        <p>Información importante para acompañar su aprendizaje.</p>
      </div>
    </div>

    <article class="perfil-estudiante">
      <div class="perfil-superior">
        <div class="perfil-identidad">
          <img
            class="foto-perfil"
            src="${obtenerFoto(estudiante)}"
            alt="Foto de ${estudiante.nombre}"
          >

          <div>
            <h2>${estudiante.nombre}</h2>
            <p>${estudiante.edad} años · ${estudiante.grado} · ${grupo.nombre}</p>
          </div>
        </div>
      </div>

      <div class="datos-perfil">
        <div class="dato-perfil">
          <small>GRUPO</small>
          <strong>${grupo? grupo.nombre : "Sin grupo"}</strong>
        </div>

        <div class="dato-perfil">
          <small>GRADO</small>
            <strong>${estudiante.grado || "No registrado"}</strong>
        </div>

        <div class="dato-perfil">
          <small>NIVEL DE TEA</small>
          <strong>${estudiante.tea || "No registrado"}</strong>
        </div>

        <div class="dato-perfil">
          <small>GUSTOS / INTERESES</small>
          <strong>${estudiante.gustos || "No registrado"}</strong>
        </div>

        <div class="dato-perfil">
          <small>RESPONSABLE</small>
          <strong>${estudiante.responsable || "No registrado"}</strong>
        </div>

        <div class="dato-perfil" style="grid-column: 1 / -1">
          <small>OBSERVACIONES</small>
          <strong>${estudiante.observaciones || "Sin observaciones."}</strong>
        </div>
      </div>
    </article>
  `;

  mostrarVista("vista-perfil");
}


$("#btn-menu")?.addEventListener("click", () => {
  $("#menu-docente").classList.toggle("colapsado");

  $("#btn-menu").textContent = $("#menu-docente").classList.contains("colapsado")
    ? "›"
    : "‹";
});

$("#volver-grupos")?.addEventListener("click", () => {
  grupoActual = null;
  mostrarVista("vista-grupos");
});

$("#buscar-grupo")?.addEventListener("input", renderizarGrupos);
$("#buscar-estudiante")?.addEventListener("input", renderizarEstudiantes);

$("#btn-cerrar-sesion")?.addEventListener("click", () => {
  if (confirm("¿Deseas cerrar sesión?")) {
    authService.logout();
  }
});

const btnAgregarGrupo = $("#btn-agregar-grupo");
if (btnAgregarGrupo) btnAgregarGrupo.style.display = "none";
const btnAgregarEstudiante = $("#btn-agregar-estudiante");
if (btnAgregarEstudiante) btnAgregarEstudiante.style.display = "none";

// Actualizar usuario en topbar
const usuarioActual = AuthService.getUser();
if (usuarioActual) {
  const nombreEl = document.querySelector(".topbar-docente__datos strong");
  if (nombreEl) nombreEl.textContent = usuarioActual.fullName || usuarioActual.username || "Docente";
  const avatarEl = document.querySelector(".topbar-docente__avatar");
  if (avatarEl) {
    const iniciales = (usuarioActual.fullName || usuarioActual.username || "D").split(" ").map(w => w[0]).join("").slice(0, 2).toUpperCase();
    avatarEl.textContent = iniciales;
  }
}

async function iniciarVistaGrupos() {
  console.log("1. Iniciando vista de grupos");

  try {
    console.log("2. Buscando perfil del maestro...");
    const perfil = await TeacherService.getMyProfile();
    console.log("Perfil obtenido:", perfil);

    console.log("3. Buscando asignaciones...");
    const asignaciones = await GroupSubjectService.getByTeacher(perfil.id);
    console.log("Asignaciones obtenidas:", asignaciones);

    await cargarDatosReales();

    console.log("4. Grupos:", grupos);
    console.log("5. Estudiantes:", estudiantes);

    renderizarGrupos();
    renderizarEstudiantes();

    console.log("6. Vista cargada correctamente");
  } catch (error) {
    console.error("ERROR AL CARGAR LA VISTA:", error);
  }
}

iniciarVistaGrupos();