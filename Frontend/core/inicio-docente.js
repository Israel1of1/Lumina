/**
 * LÚMINA - Portal Docente (Lógica de Inicio)
 */
const DocenteData = {
  usuario: {
    nombre: "María González",
    rol: "Docente de Integración",
    avatar: "MG"
  },
  estudiantes: [
    { id: 1, nombre: "Mateo Ramírez", grupo: "Grupo A - Mañana" },
    { id: 2, nombre: "Sofía Morales", grupo: "Grupo A - Mañana" },
    { id: 3, nombre: "Lucas Gutiérrez", grupo: "Grupo B - Mañana" },
    { id: 4, nombre: "Valentina Torres", grupo: "Grupo B - Mañana" },
    { id: 5, nombre: "Thiago Herrera", grupo: "Grupo A - Mañana" },
    { id: 6, nombre: "Camila Benítez", grupo: "Grupo E - Tarde" },
    { id: 7, nombre: "Joaquín Navarro", grupo: "Grupo A - Mañana" },
    { id: 8, nombre: "Daniela Salazar", grupo: "Grupo B - Mañana" },
    { id: 9, nombre: "Emiliano Castro", grupo: "Grupo E - Tarde" }
  ],
  planes: [
    { id: 101, titulo: "Rutina PECS de entrada y anticipación visual", materia: "Comunicación", estado: "PUBLISHED", fecha: "Hoy, 08:30 AM" },
    { id: 102, titulo: "Conteo manipulativo con regletas sensoriales", materia: "Matemáticas", estado: "PUBLISHED", fecha: "Ayer" },
    { id: 103, titulo: "Taller de autorregulación y rincón de la calma", materia: "Socioemocional", estado: "DRAFT", fecha: "02 Sep 2026" },
    { id: 104, titulo: "Discriminación de pictogramas de necesidades básicas", materia: "Autonomía", estado: "PUBLISHED", fecha: "29 Ago 2026" }
  ],
  grupos: [
    { nombre: "Grupo A - Mañana", matriculados: 8, cupo: 10 },
    { nombre: "Grupo B - Mañana", matriculados: 7, cupo: 10 },
    { nombre: "Grupo C - Mañana", matriculados: 9, cupo: 10 },
    { nombre: "Grupo D - Tarde", matriculados: 5, cupo: 10 },
    { nombre: "Grupo E - Tarde", matriculados: 4, cupo: 10 }
  ],
  recursos: [
    { id: 1, titulo: "Set de Pictogramas Rutina" },
    { id: 2, titulo: "Agenda Visual Diaria" },
    { id: 3, titulo: "Tablero de Elección" },
    { id: 4, titulo: "Guía de Adaptación TEA" }
  ]
};

document.addEventListener("DOMContentLoaded", () => {
  iniciarPagina();
  configurarSidebar();
});

function iniciarPagina() {
  const usuario = DocenteData.usuario;
  const primerNombre = usuario.nombre.split(" ")[0];

  // 1. Saludo y perfil
  const saludoElem = document.getElementById("saludo");
  if (saludoElem) saludoElem.textContent = `¡Hola, ${primerNombre}!`;

  const avatarElem = document.getElementById("topbar-avatar");
  if (avatarElem) avatarElem.textContent = usuario.avatar;

  const nombreDocenteElem = document.getElementById("topbar-docente-nombre");
  if (nombreDocenteElem) nombreDocenteElem.textContent = usuario.nombre;

  // 2. Tarjetas de Estadísticas
  const statEstudiantes = document.getElementById("stat-estudiantes");
  if (statEstudiantes) statEstudiantes.textContent = DocenteData.estudiantes.length;

  const statPlanesPub = document.getElementById("stat-planes-publicados");
  if (statPlanesPub) {
    statPlanesPub.textContent = DocenteData.planes.filter(p => p.estado === "PUBLISHED").length;
  }

  const statPlanesBorr = document.getElementById("stat-planes-borrador");
  if (statPlanesBorr) {
    statPlanesBorr.textContent = DocenteData.planes.filter(p => p.estado === "DRAFT").length;
  }

  const statRecursos = document.getElementById("stat-recursos");
  if (statRecursos) statRecursos.textContent = DocenteData.recursos.length;

  // 3. Renderizar Ocupación de Grupos (barras con estilo idéntico al mockup)
  renderizarOcupacionGrupos();

  // 4. Renderizar Planes Recientes
  renderizarPlanesRecientes();
}

function renderizarOcupacionGrupos() {
  const contenedor = document.getElementById("lista-ocupacion-grupos");
  if (!contenedor) return;

  contenedor.innerHTML = DocenteData.grupos.map(g => {
    const porcentaje = Math.round((g.matriculados / g.cupo) * 100);
    return `
      <div class="ocupacion-item">
        <div class="ocupacion-item__cabecera">
          <span>${g.nombre}</span>
          <span class="ocupacion-item__conteo">${g.matriculados} / ${g.cupo}</span>
        </div>
        <div class="barra-progreso">
          <div class="barra-progreso__relleno" style="width: ${porcentaje}%;"></div>
        </div>
      </div>
    `;
  }).join("");
}

function renderizarPlanesRecientes() {
  const contenedor = document.getElementById("lista-planes-recientes");
  if (!contenedor) return;

  if (DocenteData.planes.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">Aún no tienes planes registrados.</p>';
    return;
  }

  contenedor.innerHTML = DocenteData.planes.map(p => `
    <div class="plan-reciente-fila">
      <div class="plan-reciente-fila__info">
        <span class="plan-reciente-fila__titulo">${p.titulo}</span>
        <span class="plan-reciente-fila__meta">${p.materia} • ${p.fecha}</span>
      </div>
      <span class="badge ${p.estado === 'PUBLISHED' ? 'badge-publicado' : 'badge-borrador'}">
        ${p.estado === 'PUBLISHED' ? 'Publicado' : 'Borrador'}
      </span>
    </div>
  `).join("");
}

function configurarSidebar() {
  const toggleBtn = document.getElementById("sidebar-toggle");
  const sidebar = document.getElementById("sidebar");

  if (toggleBtn && sidebar) {
    toggleBtn.addEventListener("click", () => {
      sidebar.classList.toggle("colapsado");
      toggleBtn.textContent = sidebar.classList.contains("colapsado") ? "›" : "‹";
    });
  }
}