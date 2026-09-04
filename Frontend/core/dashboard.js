// ============================================================
// Lógica del Dashboard — LUMINA
// Se ejecuta desde app.js vía window.iniciarPagina()
// ============================================================

function iniciarPagina() {
  cargarEstadisticas();
  cargarOcupacionGrupos();
  cargarUltimosCodigos();
  cargarDocentesRecientes();
}

async function cargarEstadisticas() {
  try {
    const [docentes, estudiantes, grupos, materias, codigos] = await Promise.all([
      TeacherService.getAll(),
      StudentService.getAll(),
      ClassGroupService.getAll(),
      SubjectService.getAll(),
      LinkCodeService.getAll()
    ]);

    const docentesActivos = (docentes || []).filter(d => d.entityStatus === 'ACTIVE');
    const gruposActivos = (grupos || []).filter(g => g.isActive);
    const codigosPendientes = (codigos || []).filter(c => c.status === 'PENDING');

    establecerStat('stat-docentes', docentesActivos.length);
    establecerStat('stat-estudiantes', (estudiantes || []).length);
    establecerStat('stat-grupos', gruposActivos.length);
    establecerStat('stat-materias', (materias || []).length);
    establecerStat('stat-codigos-pendientes', codigosPendientes.length);
  } catch (error) {
    console.error('Error al cargar estadísticas:', error);
  }
}

async function cargarOcupacionGrupos() {
  const contenedor = document.getElementById('lista-ocupacion-grupos');

  try {
    const [grupos, estudiantes] = await Promise.all([
      ClassGroupService.getAll(),
      StudentService.getAll()
    ]);

    const gruposActivos = (grupos || []).filter(g => g.isActive);

    if (gruposActivos.length === 0) {
      contenedor.innerHTML = '<p class="texto-suave">No hay grupos registrados.</p>';
      return;
    }

    contenedor.innerHTML = gruposActivos.map(grupo => {
      const cantidad = (estudiantes || []).filter(e => e.groupId === grupo.id && e.isActive).length;
      const porcentaje = Math.min((cantidad / APP_CONFIG.MAX_STUDENTS_PER_GROUP) * 100, 100);
      const lleno = cantidad >= APP_CONFIG.MAX_STUDENTS_PER_GROUP;

      return `
        <div class="grupo-ocupacion-item">
          <div class="grupo-ocupacion-item__header">
            <span class="grupo-ocupacion-item__nombre">${grupo.name}</span>
            <span>${cantidad} / ${APP_CONFIG.MAX_STUDENTS_PER_GROUP}</span>
          </div>
          <div class="barra-ocupacion">
            <div class="barra-ocupacion__relleno ${lleno ? 'lleno' : ''}" style="width: ${porcentaje}%"></div>
          </div>
        </div>
      `;
    }).join('');
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar ocupación: ${error.message}</p>`;
  }
}

async function cargarUltimosCodigos() {
  const contenedor = document.getElementById('lista-ultimos-codigos');

  try {
    const codigos = await LinkCodeService.getAll();
    const ultimos = (codigos || [])
      .sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt))
      .slice(0, 6);

    if (ultimos.length === 0) {
      contenedor.innerHTML = '<p class="texto-suave">No hay códigos generados.</p>';
      return;
    }

    contenedor.innerHTML = ultimos.map(c => `
      <div class="codigo-item">
        <div>
          <div class="codigo-item__code">${c.code}</div>
          <div class="texto-suave">${etiquetaProposito(c.purpose)}</div>
        </div>
        <span class="badge ${badgeClaseEstadoCodigo(c.status)}">${etiquetaEstadoCodigo(c.status)}</span>
      </div>
    `).join('');
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar códigos: ${error.message}</p>`;
  }
}

async function cargarDocentesRecientes() {
  const contenedor = document.getElementById('tabla-docentes-recientes');

  try {
    const docentes = await TeacherService.getAll();
    const recientes = (docentes || [])
      .sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt))
      .slice(0, 5);

    if (recientes.length === 0) {
      contenedor.innerHTML = '<p class="texto-suave">No hay docentes registrados.</p>';
      return;
    }

    contenedor.innerHTML = `
      <table>
        <thead>
          <tr><th>Nombre</th><th>Especialidad</th><th>Estado</th></tr>
        </thead>
        <tbody>
          ${recientes.map(d => `
            <tr>
              <td>${d.firstName} ${d.lastName}</td>
              <td>${d.specialty || '—'}</td>
              <td><span class="badge ${d.entityStatus === 'ACTIVE' ? 'badge-activo' : 'badge-inactivo'}">${d.entityStatus}</span></td>
            </tr>
          `).join('')}
        </tbody>
      </table>
    `;
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar docentes: ${error.message}</p>`;
  }
}

// ===== Helpers compartidos (también los usarán docentes.js, codigos.js, etc.) =====
function etiquetaProposito(purpose) {
  return purpose === 'ENROLLMENT' ? 'Matrícula' : 'Contratación docente';
}

function etiquetaEstadoCodigo(status) {
  const mapa = { PENDING: 'Pendiente', USED: 'Usado', EXPIRED: 'Expirado', REVOKED: 'Revocado' };
  return mapa[status] || status;
}

function badgeClaseEstadoCodigo(status) {
  if (status === 'PENDING') return 'badge-activo';
  if (status === 'USED') return 'badge-inactivo';
  return 'badge-alerta';
}

function establecerStat(id, valor) {
  const el = document.getElementById(id);
  if (el) el.textContent = valor;
}