// ============================================================
// Lógica del módulo Estudiantes — LUMINA
// ============================================================

let estudiantesCache = [];
let gruposCache = [];

function iniciarPagina() {
  document.getElementById('btn-nuevo-estudiante').addEventListener('click', abrirNuevoEstudiante);
  document.getElementById('form-estudiante').addEventListener('submit', guardarEstudiante);
  document.getElementById('buscador-estudiantes').addEventListener('input', aplicarFiltros);
  document.getElementById('filtro-grupo').addEventListener('change', aplicarFiltros);
  document.getElementById('estudiante-grupo').addEventListener('change', validarCupoGrupo);

  cargarDatos();
}

async function cargarDatos() {
  const contenedor = document.getElementById('tabla-estudiantes');
  contenedor.innerHTML = '<p class="cargando">Cargando...</p>';

  try {
    const [estudiantes, grupos] = await Promise.all([
      StudentService.getAll(),
      ClassGroupService.getAll()
    ]);

    estudiantesCache = estudiantes || [];
    gruposCache = (grupos || []).filter(g => g.isActive);

    poblarSelectGrupos();
    renderTabla(estudiantesCache);
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar estudiantes: ${error.message}</p>`;
  }
}

function poblarSelectGrupos() {
  const opciones = gruposCache.map(g => `<option value="${g.id}">${g.name}</option>`).join('');

  document.getElementById('estudiante-grupo').innerHTML = opciones;
  document.getElementById('filtro-grupo').innerHTML =
    '<option value="">Todos los grupos</option>' + opciones;
}

function nombreGrupo(groupId) {
  const grupo = gruposCache.find(g => g.id === groupId);
  return grupo ? grupo.name : `Grupo #${groupId}`;
}

function contarEstudiantesActivosPorGrupo(groupId) {
  return estudiantesCache.filter(e => e.groupId === groupId && e.isActive).length;
}

function renderTabla(lista) {
  const contenedor = document.getElementById('tabla-estudiantes');

  if (lista.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">No hay estudiantes registrados.</p>';
    return;
  }

  contenedor.innerHTML = `
    <table>
      <thead>
        <tr>
          <th>Nombre</th>
          <th>N.° único</th>
          <th>Grupo</th>
          <th>Nivel de lenguaje</th>
          <th>Estado</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        ${lista.map(e => `
          <tr>
            <td>${e.firstName} ${e.lastName || ''}</td>
            <td>${e.uniqueNumber || '—'}</td>
            <td>${nombreGrupo(e.groupId)}</td>
            <td>${e.languageLevel || '—'}</td>
            <td><span class="badge ${e.isActive ? 'badge-activo' : 'badge-inactivo'}">${e.isActive ? 'Activo' : 'Inactivo'}</span></td>
            <td class="acciones-fila">
              <button class="btn-icono" onclick="abrirDetalle(${e.id})">Ver</button>
              <button class="btn-icono" onclick="abrirEditarEstudiante(${e.id})">Editar</button>
              <button class="btn-icono" onclick="alternarEstado(${e.id}, ${e.isActive ? 'false' : 'true'})">
                ${e.isActive ? 'Desactivar' : 'Activar'}
              </button>
            </td>
          </tr>
        `).join('')}
      </tbody>
    </table>
  `;
}

function aplicarFiltros() {
  const texto = document.getElementById('buscador-estudiantes').value.trim().toLowerCase();
  const grupoId = document.getElementById('filtro-grupo').value;

  let filtrados = estudiantesCache;

  if (texto) {
    filtrados = filtrados.filter(e =>
      `${e.firstName} ${e.lastName || ''}`.toLowerCase().includes(texto) ||
      (e.uniqueNumber || '').toLowerCase().includes(texto)
    );
  }

  if (grupoId) {
    filtrados = filtrados.filter(e => e.groupId === parseInt(grupoId));
  }

  renderTabla(filtrados);
}

// ===== Crear =====
function abrirNuevoEstudiante() {
  document.getElementById('modal-estudiante-titulo').textContent = 'Nuevo estudiante';
  document.getElementById('form-estudiante').reset();
  document.getElementById('estudiante-id').value = '';
  document.getElementById('aviso-cupo').classList.add('oculto');
  ocultarError('estudiante-error');
  abrirModal('modal-estudiante');
  validarCupoGrupo();
}

// ===== Editar =====
function abrirEditarEstudiante(id) {
  const estudiante = estudiantesCache.find(e => e.id === id);
  if (!estudiante) return;

  document.getElementById('modal-estudiante-titulo').textContent = 'Editar estudiante';
  document.getElementById('estudiante-id').value = estudiante.id;
  document.getElementById('estudiante-nombre').value = estudiante.firstName || '';
  document.getElementById('estudiante-apellido').value = estudiante.lastName || '';
  document.getElementById('estudiante-grupo').value = estudiante.groupId;
  document.getElementById('estudiante-numero').value = estudiante.uniqueNumber || '';
  document.getElementById('estudiante-nacimiento').value = estudiante.birthDate ? estudiante.birthDate.substring(0, 10) : '';
  document.getElementById('estudiante-genero').value = estudiante.gender || '';
  document.getElementById('estudiante-lenguaje').value = estudiante.languageLevel || '';
  document.getElementById('estudiante-clinico').value = estudiante.clinicalInfo || '';
  document.getElementById('estudiante-observaciones').value = estudiante.observations || '';
  ocultarError('estudiante-error');
  abrirModal('modal-estudiante');
  validarCupoGrupo();
}

// ===== Validación visual del cupo (RN-01: máx. 10 por grupo) =====
function validarCupoGrupo() {
  const groupId = parseInt(document.getElementById('estudiante-grupo').value);
  const idActual = document.getElementById('estudiante-id').value;
  const aviso = document.getElementById('aviso-cupo');

  if (!groupId) {
    aviso.classList.add('oculto');
    return;
  }

  // Si estamos editando, no contar al propio estudiante en su grupo actual
  const cantidad = estudiantesCache.filter(e =>
    e.groupId === groupId && e.isActive && String(e.id) !== idActual
  ).length;

  aviso.classList.remove('oculto');

  if (cantidad >= APP_CONFIG.MAX_STUDENTS_PER_GROUP) {
    aviso.textContent = `Este grupo ya tiene ${cantidad}/${APP_CONFIG.MAX_STUDENTS_PER_GROUP} estudiantes (cupo lleno).`;
    aviso.className = 'aviso-cupo aviso-lleno';
  } else {
    aviso.textContent = `Este grupo tiene ${cantidad}/${APP_CONFIG.MAX_STUDENTS_PER_GROUP} estudiantes.`;
    aviso.className = 'aviso-cupo aviso-disponible';
  }
}

async function guardarEstudiante(evento) {
  evento.preventDefault();
  ocultarError('estudiante-error');

  const id = document.getElementById('estudiante-id').value;
  const groupId = parseInt(document.getElementById('estudiante-grupo').value);

  // Validación de cupo antes de mandar al backend (el backend debe validarlo también)
  const cantidadActual = estudiantesCache.filter(e =>
    e.groupId === groupId && e.isActive && String(e.id) !== id
  ).length;

  if (cantidadActual >= APP_CONFIG.MAX_STUDENTS_PER_GROUP) {
    mostrarError('estudiante-error', `El grupo seleccionado ya alcanzó el cupo máximo de ${APP_CONFIG.MAX_STUDENTS_PER_GROUP} estudiantes.`);
    return;
  }

  const datos = {
    firstName: document.getElementById('estudiante-nombre').value.trim(),
    lastName: document.getElementById('estudiante-apellido').value.trim() || null,
    groupId: groupId,
    uniqueNumber: document.getElementById('estudiante-numero').value.trim() || null,
    birthDate: document.getElementById('estudiante-nacimiento').value || null,
    gender: document.getElementById('estudiante-genero').value || null,
    languageLevel: document.getElementById('estudiante-lenguaje').value.trim() || null,
    clinicalInfo: document.getElementById('estudiante-clinico').value.trim() || null,
    observations: document.getElementById('estudiante-observaciones').value.trim() || null
  };

  try {
    if (id) {
      await StudentService.update(id, datos);
    } else {
      await StudentService.create(datos);
    }

    cerrarModal('modal-estudiante');
    await cargarDatos();
  } catch (error) {
    mostrarError('estudiante-error', error.message);
  }
}

// ===== Activar / Desactivar =====
async function alternarEstado(id, activar) {
  try {
    await StudentService.setActive(id, activar);
    await cargarDatos();
  } catch (error) {
    alert(`No se pudo cambiar el estado: ${error.message}`);
  }
}

// ===== Ver detalle =====
function abrirDetalle(id) {
  const e = estudiantesCache.find(s => s.id === id);
  if (!e) return;

  document.getElementById('detalle-contenido').innerHTML = `
    <dl>
      <dt>Nombre completo</dt><dd>${e.firstName} ${e.lastName || ''}</dd>
      <dt>N.° único</dt><dd>${e.uniqueNumber || '—'}</dd>
      <dt>Grupo</dt><dd>${nombreGrupo(e.groupId)}</dd>
      <dt>Fecha de nacimiento</dt><dd>${e.birthDate ? e.birthDate.substring(0,10) : '—'}</dd>
      <dt>Género</dt><dd>${e.gender || '—'}</dd>
      <dt>Nivel de lenguaje</dt><dd>${e.languageLevel || '—'}</dd>
      <dt>Información clínica</dt><dd>${e.clinicalInfo || '—'}</dd>
      <dt>Observaciones</dt><dd>${e.observations || '—'}</dd>
      <dt>Cuenta de acceso</dt><dd>${e.userId ? 'Vinculada' : 'Pendiente de canjear código'}</dd>
      <dt>Estado</dt><dd><span class="badge ${e.isActive ? 'badge-activo' : 'badge-inactivo'}">${e.isActive ? 'Activo' : 'Inactivo'}</span></dd>
    </dl>
  `;
  abrirModal('modal-detalle');
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