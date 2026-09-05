// ============================================================
// Lógica del módulo Docentes — LUMINA
// ============================================================

let docentesCache = [];

function iniciarPagina() {
  document.getElementById('btn-nuevo-docente').addEventListener('click', abrirNuevoDocente);
  document.getElementById('form-docente').addEventListener('submit', guardarDocente);
  document.getElementById('form-estado').addEventListener('submit', guardarEstado);
  document.getElementById('estado-valor').addEventListener('change', alternarCampoMotivo);
  document.getElementById('buscador-docentes').addEventListener('input', filtrarTabla);

  cargarDocentes();
}

async function cargarDocentes() {
  const contenedor = document.getElementById('tabla-docentes');
  contenedor.innerHTML = '<p class="cargando">Cargando...</p>';

  try {
    docentesCache = await TeacherService.getAll() || [];
    renderTabla(docentesCache);
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar docentes: ${error.message}</p>`;
  }
}

function renderTabla(lista) {
  const contenedor = document.getElementById('tabla-docentes');

  if (lista.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">No hay docentes registrados.</p>';
    return;
  }

  contenedor.innerHTML = `
    <table>
      <thead>
        <tr>
          <th>Nombre</th>
          <th>Cédula</th>
          <th>Especialidad</th>
          <th>Cuenta</th>
          <th>Estado</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        ${lista.map(d => `
          <tr>
            <td>${d.firstName} ${d.lastName}</td>
            <td>${d.nationalId || '—'}</td>
            <td>${d.specialty || '—'}</td>
            <td>${d.userId ? '<span class="badge badge-activo">Vinculada</span>' : '<span class="badge badge-inactivo">Pendiente</span>'}</td>
            <td><span class="badge ${claseBadgeEstado(d.entityStatus)}">${etiquetaEstado(d.entityStatus)}</span></td>
            <td class="acciones-fila">
              <button class="btn-icono" onclick="abrirDetalle(${d.id})">Ver</button>
              <button class="btn-icono" onclick="abrirEditarDocente(${d.id})">Editar</button>
              <button class="btn-icono" onclick="abrirCambiarEstado(${d.id})">Estado</button>
            </td>
          </tr>
        `).join('')}
      </tbody>
    </table>
  `;
}

function filtrarTabla() {
  const texto = document.getElementById('buscador-docentes').value.trim().toLowerCase();

  if (!texto) {
    renderTabla(docentesCache);
    return;
  }

  const filtrados = docentesCache.filter(d =>
    `${d.firstName} ${d.lastName}`.toLowerCase().includes(texto) ||
    (d.nationalId || '').toLowerCase().includes(texto)
  );

  renderTabla(filtrados);
}

// ===== Crear =====
function abrirNuevoDocente() {
  document.getElementById('modal-docente-titulo').textContent = 'Nuevo docente';
  document.getElementById('form-docente').reset();
  document.getElementById('docente-id').value = '';
  ocultarError('docente-error');
  abrirModal('modal-docente');
}

// ===== Editar =====
function abrirEditarDocente(id) {
  const docente = docentesCache.find(d => d.id === id);
  if (!docente) return;

  document.getElementById('modal-docente-titulo').textContent = 'Editar docente';
  document.getElementById('docente-id').value = docente.id;
  document.getElementById('docente-nombre').value = docente.firstName || '';
  document.getElementById('docente-apellido').value = docente.lastName || '';
  document.getElementById('docente-cedula').value = docente.nationalId || '';
  document.getElementById('docente-especialidad').value = docente.specialty || '';
  document.getElementById('docente-titulo').value = docente.degree || '';
  document.getElementById('docente-correo').value = docente.personalEmail || '';
  document.getElementById('docente-telefono').value = docente.phone || '';
  document.getElementById('docente-ciudad').value = docente.city || '';
  document.getElementById('docente-direccion').value = docente.address || '';
  ocultarError('docente-error');
  abrirModal('modal-docente');
}

async function guardarDocente(evento) {
  evento.preventDefault();
  ocultarError('docente-error');

  const id = document.getElementById('docente-id').value;

  const datos = {
    firstName: document.getElementById('docente-nombre').value.trim(),
    lastName: document.getElementById('docente-apellido').value.trim(),
    nationalId: document.getElementById('docente-cedula').value.trim() || null,
    specialty: document.getElementById('docente-especialidad').value.trim() || null,
    degree: document.getElementById('docente-titulo').value.trim() || null,
    personalEmail: document.getElementById('docente-correo').value.trim() || null,
    phone: document.getElementById('docente-telefono').value.trim() || null,
    city: document.getElementById('docente-ciudad').value.trim() || null,
    address: document.getElementById('docente-direccion').value.trim() || null
  };

  try {
    if (id) {
      await TeacherService.update(id, datos);
    } else {
      await TeacherService.create(datos);
    }

    cerrarModal('modal-docente');
    await cargarDocentes();
  } catch (error) {
    mostrarError('docente-error', error.message);
  }
}

// ===== Ver detalle =====
function abrirDetalle(id) {
  const d = docentesCache.find(t => t.id === id);
  if (!d) return;

  document.getElementById('detalle-contenido').innerHTML = `
    <dl>
      <dt>Nombre completo</dt><dd>${d.firstName} ${d.lastName}</dd>
      <dt>Cédula</dt><dd>${d.nationalId || '—'}</dd>
      <dt>Especialidad</dt><dd>${d.specialty || '—'}</dd>
      <dt>Título académico</dt><dd>${d.degree || '—'}</dd>
      <dt>Correo personal</dt><dd>${d.personalEmail || '—'}</dd>
      <dt>Teléfono</dt><dd>${d.phone || '—'}</dd>
      <dt>Ciudad</dt><dd>${d.city || '—'}</dd>
      <dt>Dirección</dt><dd>${d.address || '—'}</dd>
      <dt>Cuenta de acceso</dt><dd>${d.userId ? 'Vinculada' : 'Pendiente de canjear código'}</dd>
      <dt>Estado</dt><dd><span class="badge ${claseBadgeEstado(d.entityStatus)}">${etiquetaEstado(d.entityStatus)}</span></dd>
      ${d.entityStatus === 'INACTIVE' ? `
        <dt>Fecha de baja</dt><dd>${d.dismissalDate ? d.dismissalDate.substring(0,10) : '—'}</dd>
        <dt>Motivo de baja</dt><dd>${d.dismissalReason || '—'}</dd>
      ` : ''}
    </dl>
  `;
  abrirModal('modal-detalle');
}

// ===== Cambiar estado =====
function abrirCambiarEstado(id) {
  const docente = docentesCache.find(d => d.id === id);
  if (!docente) return;

  document.getElementById('estado-id').value = docente.id;
  document.getElementById('estado-valor').value = docente.entityStatus;
  document.getElementById('estado-motivo').value = docente.dismissalReason || '';
  alternarCampoMotivo();
  ocultarError('estado-error');
  abrirModal('modal-estado');
}

function alternarCampoMotivo() {
  const esInactivo = document.getElementById('estado-valor').value === 'INACTIVE';
  document.getElementById('campo-motivo-baja').classList.toggle('oculto', !esInactivo);
}

async function guardarEstado(evento) {
  evento.preventDefault();
  ocultarError('estado-error');

  const id = document.getElementById('estado-id').value;
  const nuevoEstado = document.getElementById('estado-valor').value;
  const motivo = document.getElementById('estado-motivo').value.trim();

  if (nuevoEstado === 'INACTIVE' && !motivo) {
    mostrarError('estado-error', 'Debes indicar el motivo de la baja');
    return;
  }

  try {
    await TeacherService.setStatus(id, nuevoEstado, nuevoEstado === 'INACTIVE' ? motivo : null);
    cerrarModal('modal-estado');
    await cargarDocentes();
  } catch (error) {
    mostrarError('estado-error', error.message);
  }
}

// ===== Helpers =====
function etiquetaEstado(estado) {
  const mapa = { ACTIVE: 'Activo', ON_LEAVE: 'De permiso', INACTIVE: 'Inactivo' };
  return mapa[estado] || estado;
}

function claseBadgeEstado(estado) {
  if (estado === 'ACTIVE') return 'badge-activo';
  if (estado === 'ON_LEAVE') return 'badge-alerta';
  return 'badge-inactivo';
}

function abrirModal(id) { document.getElementById(id).classList.remove('oculto'); }
function cerrarModal(id) { document.getElementById(id).classList.add('oculto'); }
function mostrarError(id, texto) {
  const el = document.getElementById(id);
  el.textContent = texto;
  el.classList.remove('oculto');
}
function ocultarError(id) { document.getElementById(id).classList.add('oculto'); }