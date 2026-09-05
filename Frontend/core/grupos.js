// ============================================================
// Lógica del módulo Grupos — LUMINA
// ============================================================

let gruposCache = [];
let estudiantesCache = [];

function iniciarPagina() {
  document.getElementById('btn-nuevo-grupo').addEventListener('click', abrirNuevoGrupo);
  document.getElementById('form-grupo').addEventListener('submit', guardarGrupo);
  document.getElementById('buscador-grupos').addEventListener('input', filtrarGrupos);

  cargarDatos();
}

async function cargarDatos() {
  const contenedor = document.getElementById('grupos-grid');
  contenedor.innerHTML = '<div class="cargando">Cargando...</div>';

  try {
    const [grupos, estudiantes] = await Promise.all([
      ClassGroupService.getAll(),
      StudentService.getAll()
    ]);

    gruposCache = grupos || [];
    estudiantesCache = estudiantes || [];

    renderGrupos(gruposCache);
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar grupos: ${error.message}</p>`;
  }
}

function contarEstudiantesActivos(groupId) {
  return estudiantesCache.filter(e => e.groupId === groupId && e.isActive).length;
}

function renderGrupos(lista) {
  const contenedor = document.getElementById('grupos-grid');

  if (lista.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">No hay grupos registrados.</p>';
    return;
  }

  contenedor.innerHTML = lista.map(g => {
    const cantidad = contarEstudiantesActivos(g.id);
    const porcentaje = Math.min((cantidad / APP_CONFIG.MAX_STUDENTS_PER_GROUP) * 100, 100);
    const lleno = cantidad >= APP_CONFIG.MAX_STUDENTS_PER_GROUP;

    return `
      <div class="tarjeta-grupo">
        <div class="tarjeta-grupo__header">
          <div>
            <div class="tarjeta-grupo__nombre">${g.name}</div>
            <div class="tarjeta-grupo__nivel">${g.gradeLevel || 'Sin nivel especificado'}</div>
          </div>
          <span class="badge ${g.isActive ? 'badge-activo' : 'badge-inactivo'}">${g.isActive ? 'Activo' : 'Inactivo'}</span>
        </div>

        <p class="tarjeta-grupo__descripcion">${g.description || 'Sin descripción.'}</p>

        <div>
          <div class="tarjeta-grupo__cupo">
            <span>Estudiantes</span>
            <span>${cantidad} / ${APP_CONFIG.MAX_STUDENTS_PER_GROUP}</span>
          </div>
          <div class="barra-ocupacion">
            <div class="barra-ocupacion__relleno ${lleno ? 'lleno' : ''}" style="width: ${porcentaje}%"></div>
          </div>
        </div>

        <div class="tarjeta-grupo__acciones">
          <button class="btn-icono" onclick="verEstudiantes(${g.id})">Ver estudiantes</button>
          <button class="btn-icono" onclick="abrirEditarGrupo(${g.id})">Editar</button>
          <button class="btn-icono" onclick="alternarEstado(${g.id}, ${g.isActive ? 'false' : 'true'})">
            ${g.isActive ? 'Desactivar' : 'Activar'}
          </button>
        </div>
      </div>
    `;
  }).join('');
}

function filtrarGrupos() {
  const texto = document.getElementById('buscador-grupos').value.trim().toLowerCase();

  if (!texto) {
    renderGrupos(gruposCache);
    return;
  }

  const filtrados = gruposCache.filter(g => g.name.toLowerCase().includes(texto));
  renderGrupos(filtrados);
}

// ===== Crear =====
function abrirNuevoGrupo() {
  document.getElementById('modal-grupo-titulo').textContent = 'Nuevo grupo';
  document.getElementById('form-grupo').reset();
  document.getElementById('grupo-id').value = '';
  ocultarError('grupo-error');
  abrirModal('modal-grupo');
}

// ===== Editar =====
function abrirEditarGrupo(id) {
  const grupo = gruposCache.find(g => g.id === id);
  if (!grupo) return;

  document.getElementById('modal-grupo-titulo').textContent = 'Editar grupo';
  document.getElementById('grupo-id').value = grupo.id;
  document.getElementById('grupo-nombre').value = grupo.name;
  document.getElementById('grupo-nivel').value = grupo.gradeLevel || '';
  document.getElementById('grupo-descripcion').value = grupo.description || '';
  ocultarError('grupo-error');
  abrirModal('modal-grupo');
}

async function guardarGrupo(evento) {
  evento.preventDefault();
  ocultarError('grupo-error');

  const id = document.getElementById('grupo-id').value;

  const datos = {
    name: document.getElementById('grupo-nombre').value.trim(),
    gradeLevel: document.getElementById('grupo-nivel').value.trim() || null,
    description: document.getElementById('grupo-descripcion').value.trim() || null
  };

  try {
    if (id) {
      await ClassGroupService.update(id, datos);
    } else {
      await ClassGroupService.create(datos);
    }

    cerrarModal('modal-grupo');
    await cargarDatos();
  } catch (error) {
    mostrarError('grupo-error', error.message);
  }
}

// ===== Activar / Desactivar =====
async function alternarEstado(id, activar) {
  try {
    await ClassGroupService.setActive(id, activar);
    await cargarDatos();
  } catch (error) {
    alert(`No se pudo cambiar el estado: ${error.message}`);
  }
}

// ===== Ver estudiantes del grupo =====
function verEstudiantes(id) {
  const grupo = gruposCache.find(g => g.id === id);
  if (!grupo) return;

  const estudiantesDelGrupo = estudiantesCache.filter(e => e.groupId === id);

  document.getElementById('titulo-estudiantes-grupo').textContent = `Estudiantes de ${grupo.name}`;

  const contenedor = document.getElementById('lista-estudiantes-grupo');

  if (estudiantesDelGrupo.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">Este grupo aún no tiene estudiantes.</p>';
  } else {
    contenedor.innerHTML = estudiantesDelGrupo.map(e => `
      <div class="lista-estudiantes-grupo__item">
        <span>${e.firstName} ${e.lastName || ''}</span>
        <span class="badge ${e.isActive ? 'badge-activo' : 'badge-inactivo'}">${e.isActive ? 'Activo' : 'Inactivo'}</span>
      </div>
    `).join('');
  }

  abrirModal('modal-estudiantes-grupo');
}

// ===== Utilidades =====
function abrirModal(id) { document.getElementById(id).classList.remove('oculto'); }
function cerrarModal(id) { document.getElementById(id).classList.add('oculto'); }
function mostrarError(id, texto) {
  const el = document.getElementById(id);
  el.textContent = texto;
  el.classList.remove('oculto');
}
function ocultarError(id) { document.getElementById(id).classList.add('oculto'); }