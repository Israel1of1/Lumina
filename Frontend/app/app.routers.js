
// Router / navegación — LUMINA Panel de Administración
// Maneja: sesión, protección de rutas, y el sidebar dinámico.


// Definición del menú lateral 
const MENU_ITEMS = [
  { id: 'dashboard',     label: 'Dashboard',     href: APP_CONFIG.ROUTES.DASHBOARD,      seccion: 'MENU',    icono: 'grid' },
  { id: 'docentes',      label: 'Docentes',      href: APP_CONFIG.ROUTES.TEACHERS,       seccion: 'MENU',    icono: 'user' },
  { id: 'estudiantes',   label: 'Estudiantes',   href: APP_CONFIG.ROUTES.STUDENTS,       seccion: 'MENU',    icono: 'users' },
  { id: 'grupos',        label: 'Grupos',        href: APP_CONFIG.ROUTES.GROUPS,         seccion: 'MENU',    icono: 'layers' },
  { id: 'materias',      label: 'Materias',      href: APP_CONFIG.ROUTES.SUBJECTS,       seccion: 'MENU',    icono: 'book' },
  { id: 'asignaciones',  label: 'Asignaciones',  href: APP_CONFIG.ROUTES.GROUP_SUBJECTS, seccion: 'MENU',    icono: 'link' },
  { id: 'codigos',       label: 'Códigos',       href: APP_CONFIG.ROUTES.LINK_CODES,     seccion: 'GENERAL', icono: 'key' }
];

//  Set de íconos SVG simples (esquinas redondeadas, sin librerías) 
const ICON_SVGS = {
  grid:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="7" height="7" rx="2"/><rect x="14" y="3" width="7" height="7" rx="2"/><rect x="3" y="14" width="7" height="7" rx="2"/><rect x="14" y="14" width="7" height="7" rx="2"/></svg>',
  user:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="4"/><path d="M4 21c0-4 4-6 8-6s8 2 8 6"/></svg>',
  users:  '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="9" cy="8" r="3.5"/><circle cx="17" cy="9" r="3"/><path d="M2 21c0-3.5 3-5.5 7-5.5s7 2 7 5.5"/><path d="M15 15.2c3 .3 5 2.1 5 5.8"/></svg>',
  layers: '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 3 3 8l9 5 9-5-9-5Z"/><path d="m3 13 9 5 9-5"/></svg>',
  book:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M4 4.5A2.5 2.5 0 0 1 6.5 2H20v17H6.5A2.5 2.5 0 0 0 4 21.5Z"/><path d="M4 4.5v15A2.5 2.5 0 0 0 6.5 22H20"/></svg>',
  link:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 17H7a5 5 0 0 1 0-10h2"/><path d="M15 7h2a5 5 0 0 1 0 10h-2"/><line x1="8" y1="12" x2="16" y2="12"/></svg>',
  key:    '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="8" cy="15" r="4"/><path d="m10.5 12.5 8-8"/><path d="M16 7h3v3"/></svg>'
};


// AppRouter — sesión + protección de rutas + render del sidebar

const AppRouter = {
  // Sesión
  guardarSesion(token, usuario) {
    localStorage.setItem(APP_CONFIG.STORAGE_KEYS.TOKEN, token);
    localStorage.setItem(APP_CONFIG.STORAGE_KEYS.USER, JSON.stringify(usuario));
  },

  getToken() {
    return localStorage.getItem(APP_CONFIG.STORAGE_KEYS.TOKEN);
  },

  getUsuario() {
    const raw = localStorage.getItem(APP_CONFIG.STORAGE_KEYS.USER);
    return raw ? JSON.parse(raw) : null;
  },

  decodificarToken(token) {
    try {
      const payload = token.split('.')[1];
      const decoded = atob(payload.replace(/-/g, '+').replace(/_/g, '/'));
      return JSON.parse(decoded);
    } catch {
      return null;
    }
  },

  estaAutenticado() {
    const token = this.getToken();
    if (!token) return false;

    const payload = this.decodificarToken(token);
    if (!payload || !payload.exp) return false;

    const ahoraEnSegundos = Math.floor(Date.now() / 1000);
    return payload.exp > ahoraEnSegundos;
  },


obtenerRutaPorRol(roles) {
    if (!Array.isArray(roles)) roles = roles ? [roles] : [];
    const rolesNormalizados = roles.map(r => String(r).trim().toUpperCase());
    if (rolesNormalizados.includes('INSTITUTION')) return APP_CONFIG.ROUTES.DASHBOARD;
    if (rolesNormalizados.includes('TEACHER')) return APP_CONFIG.ROUTES.TEACHER_HOME || 'inicio-docente.html';
    if (rolesNormalizados.includes('GUARDIAN') || rolesNormalizados.includes('TUTOR')) return APP_CONFIG.ROUTES.TUTOR_DASHBOARD;
    return APP_CONFIG.ROUTES.LOGIN;
  },

  obtenerRolesPermitidosPorPagina() {
    const path = window.location.pathname.toLowerCase();
    const nombreArchivo = path.substring(path.lastIndexOf('/') + 1) || '';

    // Páginas de Docente (excepto docentes.html que es del panel admin)
    if (
      (nombreArchivo.startsWith('docente-') ||
       nombreArchivo.startsWith('docentes-') ||
       nombreArchivo === 'inicio-docente.html') &&
      nombreArchivo !== 'docentes.html'
    ) {
      return ['TEACHER'];
    }

    // Páginas de Tutor / Guardian
    if (nombreArchivo.startsWith('tutor-')) {
      return ['GUARDIAN', 'TUTOR'];
    }

    // Páginas de Institución / Admin
    if (
      nombreArchivo === 'dashboard.html' ||
      nombreArchivo === 'docentes.html' ||
      nombreArchivo === 'estudiantes.html' ||
      nombreArchivo === 'grupos.html' ||
      nombreArchivo === 'materias.html' ||
      nombreArchivo === 'asignaciones.html' ||
      nombreArchivo === 'codigos.html'
    ) {
      return ['INSTITUTION'];
    }

    return null;
  },

  /**
   * Valida sesión y roles permitidos para la pantalla actual.
   * Redirige al login si no hay sesión válida o a la vista correspondiente si el rol no coincide.
  */
  protegerPagina(rolesPermitidos = null) {
    if (!this.estaAutenticado()) {
      window.location.href = APP_CONFIG.ROUTES.LOGIN;
      return false;
    }   const usuario = this.getUsuario();
    const rolesUsuario = (usuario?.roles || []).map(r => String(r).trim().toUpperCase());

    const permitidos = rolesPermitidos || this.obtenerRolesPermitidosPorPagina();

    if (permitidos && permitidos.length > 0) {
      const tieneRolPermitido = permitidos.some(rol =>
        rolesUsuario.includes(rol.toUpperCase())
      );
      if (!tieneRolPermitido) {
        const vistaCorrecta = this.obtenerRutaPorRol(rolesUsuario);
        window.location.href = vistaCorrecta;
        return false;
      }
    }
    return true;
  },

  cerrarSesion() {
    localStorage.removeItem(APP_CONFIG.STORAGE_KEYS.TOKEN);
    localStorage.removeItem(APP_CONFIG.STORAGE_KEYS.USER);
    sessionStorage.clear();
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
  },

  //  Sidebar dinámico 
   renderSidebar(idActivo) {
    const contenedor = document.getElementById('sidebar-contenedor');
    if (!contenedor) return;

    const grupoMenu = MENU_ITEMS.filter(item => item.seccion === 'MENU');
    const grupoGeneral = MENU_ITEMS.filter(item => item.seccion === 'GENERAL');

    const renderLinks = (items) => items.map(item => `
      <a class="sidebar__link ${item.id === idActivo ? 'activo' : ''}" href="${item.href}">
        ${ICON_SVGS[item.icono] || ''}
        <span>${item.label}</span>
      </a>
    `).join('');

    const colapsado = localStorage.getItem('lumina_sidebar_colapsado') === 'true';

    contenedor.innerHTML = `
      <aside class="sidebar ${colapsado ? 'colapsado' : ''}" id="sidebar-elemento">
        <button class="sidebar__toggle" id="btn-toggle-sidebar" aria-label="Colapsar u expandir menú">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <path d="m15 18-6-6 6-6"/>
          </svg>
        </button>

        <div class="sidebar__marca">
          <img src="../img/logo.png" alt="LUMINA" onerror="this.style.display='none'" style="height:32px" />
          <span>LUMINA</span>
        </div>

        <div class="sidebar__seccion-titulo">Menú</div>
        ${renderLinks(grupoMenu)}

        <div class="sidebar__seccion-titulo">General</div>
        ${renderLinks(grupoGeneral)}
      </aside>
    `;

    document.getElementById('btn-toggle-sidebar').addEventListener('click', () => this.alternarSidebar());
  },

  //  Alternar colapsado/expandido
  alternarSidebar() {
    const sidebar = document.getElementById('sidebar-elemento');
    if (!sidebar) return;

    const colapsado = sidebar.classList.toggle('colapsado');
    localStorage.setItem('lumina_sidebar_colapsado', colapsado);
  },

  //  Barra superior: usuario + logout
  renderTopbarUsuario() {
    const contenedor = document.getElementById('topbar-usuario-contenedor');
    if (!contenedor) return;

    const usuario = this.getUsuario();
    const correo = usuario?.email || 'Institución';
    const inicial = correo.charAt(0).toUpperCase();

    contenedor.innerHTML = `
      <div class="topbar__usuario">
        <div class="topbar__avatar">${inicial}</div>
        <span>${correo}</span>
      </div>
      <button class="btn-logout" onclick="AppRouter.cerrarSesion()">Cerrar sesión</button>
    `;
  }
};