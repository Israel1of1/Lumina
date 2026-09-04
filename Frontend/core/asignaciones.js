// ============================================================
// Lógica del módulo Asignaciones (GroupSubject) — LUMINA
// Cada asignación es un registro histórico: al "finalizar" una,
// se marca isActive=false y se guarda endDate; se puede crear una
// nueva asignación distinta para el mismo grupo/materia después.
// ============================================================

let asignacionesCache = [];
let gruposCache = [];
let materiasCache = [];
let docentesCache = [];

function iniciarPagina() {
  document.getElementById('btn-nueva-asignacion').addEventListener('click', abrirNuevaAsignacion);
  document.getElementById('form-asignacion').addEventListener('submit', guardarAsignacion);
  document.getElementById('form-finalizar').addEventListener('submit', guardarFinalizacion);
  document.getElementById('filtro-grupo-asignacion').addEventListener('change', renderTablaFiltrada);

  cargarDatos();
}

async function cargarDatos() {
  const contenedor = document.getElementById('tabla-asignaciones');
  contenedor.innerHTML = '<p class="cargando">Cargando...</p>';

  try {
    const [asignaciones, grupos, materias, docentes] = await Promise.all([
      GroupSubjectService.getAll(),
      ClassGroupService.getAll(),
      SubjectService.getAll(),
      TeacherService.getAll()
    ]);

    asignacionesCache = asignaciones || [];
    gruposCache = (grupos || []).filter(g => g.isActive);
    materiasCache = materias || [];
    docentesCache = (docentes || []).filter(d => d.entityStatus === 'ACTIVE');

    poblarSelects();
    renderTablaFiltrada();
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar asignaciones: ${error.message}</p>`;
  }
}

function poblarSelects() {
  const opcionesGrupos = gruposCache.map(g => `<option value="${g.id}">${g.name}</option>`).join('');
  const opcionesMaterias = materiasCache.map(m => `<option value="${m.id}">${m.name}</option>`).join('');
  const opcionesDocentes = docentesCache.map(d => `<option value="${d.id}">${d.firstName} ${d.lastName}</option>`).join('');

  document.getElementById('asig-grupo').innerHTML = opcionesGrupos;
  document.getElementById('asig-materia').innerHTML = opcionesMaterias;
  document.getElementById('asig-docente').innerHTML = opcionesDocentes;
  document.getElementById('filtro-grupo-asignacion').innerHTML =
    '<option value="">Todos los grupos</option>' + opcionesGrupos;
}

function nombreGrupo(id) { return gruposCache.find(g => g.id === id)?.name || `Grupo #${id}`; }
function nombreMateria(id) { return materiasCache.find(m => m.id === id)?.name || `Materia #${id}`; }
function nombreDocente(id) { return docentesCache.find(d => d.id === id) ? `${docentesCache.find(d => d.id === id).firstName} ${docentesCache.find(d => d.id === id).lastName}` : `Docente #${id}`; }

function renderTablaFiltrada() {
  const grupoId = document.getElementById('filtro-grupo-asignacion').value;
  const lista = grupoId ? asignacionesCache.filter(a => a.groupId === parseInt(grupoId)) : asignacionesCache;
  renderTabla(lista);
}

function renderTabla(lista) {
  const contenedor = document.getElementById('tabla-asignaciones');

  if (lista.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">No hay asignaciones registradas.</p>';
    return;
  }

  const ordenada = [...lista].sort((a, b) => new Date(b.assignmentDate || b.createdAt) - new Date(a.assignmentDate || a.createdAt));

  contenedor.innerHTML = `
    <table>
      <thead>
        <tr>
          <th>Grupo</th>
          <th>Materia</th>
          <th>Docente</th>
          <th>Desde</th>
          <th>Hasta</th>
          <th>Estado</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        ${ordenada.map(a => `
          <tr>
            <td>${nombreGrupo(a.groupId)}</td>
            <td>${nombreMateria(a.subjectId)}</td>
            <td>${nombreDocente(a.teacherId)}</td>
            <td>${a.assignmentDate ? a.assignmentDate.substring(0,10) : '—'}</td>
            <td>${a.endDate ? a.endDate.substring(0,10) : '—'}</td>
            <td><span class="badge ${a.isActive ? 'badge-activo' : 'badge-inactivo'}">${a.isActive ? 'Vigente' : 'Finalizada'}</span></td>
            <td class="acciones-fila">
              ${a.isActive ? `<button class="btn-icono" onclick="abrirFinalizar(${a.id})">Finalizar</button>` : ''}
            </td>
          </tr>
        `).join('')}
      </tbody>
    </table>
  `;
}

// ===== Crear asignación =====
function abrirNuevaAsignacion() {
  document.getElementById('form-asignacion').reset();
  document.getElementById('asig-fecha').value = new Date().toISOString().split('T')[0];
  ocultarError('asignacion-error');
  abrirModal('modal-asignacion');
}

async function guardarAsignacion(evento) {
  evento.preventDefault();
  ocultarError('asignacion-error');

  const groupId = parseInt(document.getElementById('asig-grupo').value);
  const subjectId = parseInt(document.getElementById('asig-materia').value);
  const teacherId = parseInt(document.getElementById('asig-docente').value);
  const assignmentDate = document.getElementById('asig-fecha').value || null;

  // Aviso local: ya existe una asignación VIGENTE con la misma materia en ese grupo
  const yaExiste = asignacionesCache.some(a =>
    a.groupId === groupId && a.subjectId === subjectId && a.isActive
  );

  if (yaExiste) {
    mostrarError('asignacion-error', 'Este grupo ya tiene un docente activo asignado a esa materia. Finaliza la asignación actual antes de crear una nueva.');
    return;
  }

  try {
    await GroupSubjectService.create({ groupId, subjectId, teacherId, assignmentDate });
    cerrarModal('modal-asignacion');
    await cargarDatos();
  } catch (error) {
    mostrarError('asignacion-error', error.message);
  }
}

// ===== Finalizar asignación =====
function abrirFinalizar(id) {
  const asignacion = asignacionesCache.find(a => a.id === id);
  if (!asignacion) return;

  document.getElementById('finalizar-id').value = asignacion.id;
  document.getElementById('finalizar-resumen').textContent =
    `${nombreGrupo(asignacion.groupId)} — ${nombreMateria(asignacion.subjectId)} — ${nombreDocente(asignacion.teacherId)}`;
  document.getElementById('finalizar-fecha').value = new Date().toISOString().split('T')[0];
  ocultarError('finalizar-error');
  abrirModal('modal-finalizar');
}

async function guardarFinalizacion(evento) {
  evento.preventDefault();
  ocultarError('finalizar-error');

  const id = document.getElementById('finalizar-id').value;
  const endDate = document.getElementById('finalizar-fecha').value;

  try {
    await GroupSubjectService.end(id, endDate);
    cerrarModal('modal-finalizar');
    await cargarDatos();
  } catch (error) {
    mostrarError('finalizar-error', error.message);
  }
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