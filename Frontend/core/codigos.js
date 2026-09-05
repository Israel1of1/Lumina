// ============================================================
// Lógica del módulo Códigos de vinculación (LinkCode) — LUMINA
// ============================================================

let codigosCache = [];

function iniciarPagina() {
  document.getElementById('btn-generar-codigo').addEventListener('click', abrirGenerarCodigo);
  document.getElementById('form-generar').addEventListener('submit', generarCodigo);
  document.getElementById('btn-copiar-codigo').addEventListener('click', copiarCodigo);
  document.getElementById('filtro-proposito').addEventListener('change', aplicarFiltros);
  document.getElementById('filtro-estado').addEventListener('change', aplicarFiltros);

  cargarCodigos();
}

async function cargarCodigos() {
  const contenedor = document.getElementById('tabla-codigos');
  contenedor.innerHTML = '<p class="cargando">Cargando...</p>';

  try {
    codigosCache = await LinkCodeService.getAll() || [];
    renderTabla(codigosCache);
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar códigos: ${error.message}</p>`;
  }
}

function renderTabla(lista) {
  const contenedor = document.getElementById('tabla-codigos');

  if (lista.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">No hay códigos registrados.</p>';
    return;
  }

  const ordenada = [...lista].sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));

  contenedor.innerHTML = `
    <table>
      <thead>
        <tr>
          <th>Código</th>
          <th>Propósito</th>
          <th>Estado</th>
          <th>Generado</th>
          <th>Expira</th>
          <th>Usado</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        ${ordenada.map(c => `
          <tr>
            <td class="codigo-texto">${c.code}</td>
            <td>${etiquetaProposito(c.purpose)}</td>
            <td><span class="badge ${claseBadgeEstado(c.status)}">${etiquetaEstado(c.status)}</span></td>
            <td>${c.createdAt ? c.createdAt.substring(0,10) : '—'}</td>
            <td>${c.expiresAt ? c.expiresAt.substring(0,10) : 'Sin vencimiento'}</td>
            <td>${c.usedAt ? c.usedAt.substring(0,10) : '—'}</td>
            <td class="acciones-fila">
              ${c.status === 'PENDING' ? `<button class="btn-icono" onclick="revocarCodigo(${c.id})">Revocar</button>` : ''}
            </td>
          </tr>
        `).join('')}
      </tbody>
    </table>
  `;
}

function aplicarFiltros() {
  const proposito = document.getElementById('filtro-proposito').value;
  const estado = document.getElementById('filtro-estado').value;

  let filtrados = codigosCache;

  if (proposito) filtrados = filtrados.filter(c => c.purpose === proposito);
  if (estado) filtrados = filtrados.filter(c => c.status === estado);

  renderTabla(filtrados);
}

// ===== Generar =====
function abrirGenerarCodigo() {
  document.getElementById('form-generar').reset();
  ocultarError('generar-error');
  abrirModal('modal-generar');
}

async function generarCodigo(evento) {
  evento.preventDefault();
  ocultarError('generar-error');

  const purpose = document.getElementById('gen-proposito').value;
  const expiresAt = document.getElementById('gen-expiracion').value || null;

  try {
    const nuevoCodigo = await LinkCodeService.generate(purpose, expiresAt);

    cerrarModal('modal-generar');
    mostrarCodigoGenerado(nuevoCodigo.code);
    await cargarCodigos();
  } catch (error) {
    mostrarError('generar-error', error.message);
  }
}

function mostrarCodigoGenerado(codigo) {
  document.getElementById('codigo-generado-texto').textContent = codigo;
  document.getElementById('copiado-aviso').classList.add('oculto');
  abrirModal('modal-resultado');
}

async function copiarCodigo() {
  const texto = document.getElementById('codigo-generado-texto').textContent;

  try {
    await navigator.clipboard.writeText(texto);
    const aviso = document.getElementById('copiado-aviso');
    aviso.classList.remove('oculto');
  } catch {
    alert('No se pudo copiar automáticamente. Selecciona el código manualmente.');
  }
}

// ===== Revocar =====
async function revocarCodigo(id) {
  if (!confirm('¿Seguro que quieres revocar este código? Ya no podrá usarse.')) return;

  try {
    await LinkCodeService.revoke(id);
    await cargarCodigos();
  } catch (error) {
    alert(`No se pudo revocar el código: ${error.message}`);
  }
}

// ===== Helpers =====
function etiquetaProposito(purpose) {
  return purpose === 'ENROLLMENT' ? 'Matrícula' : 'Contratación docente';
}

function etiquetaEstado(status) {
  const mapa = { PENDING: 'Pendiente', USED: 'Usado', EXPIRED: 'Expirado', REVOKED: 'Revocado' };
  return mapa[status] || status;
}

function claseBadgeEstado(status) {
  if (status === 'PENDING') return 'badge-activo';
  if (status === 'USED') return 'badge-inactivo';
  return 'badge-alerta';
}

function abrirModal(id) { document.getElementById(id).classList.remove('oculto'); }
function cerrarModal(id) { document.getElementById(id).classList.add('oculto'); }
function mostrarError(id, texto) {
  const el = document.getElementById(id);
  el.textContent = texto;
  el.classList.remove('oculto');
}
function ocultarError(id) { document.getElementById(id).classList.add('oculto'); }