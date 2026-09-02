// ============================================================
// Router / navegación — LUMINA Panel de Administración
// Maneja: sesión, protección de rutas, y el sidebar dinámico.
// ============================================================

// ===== Definición del menú lateral =====
const MENU_ITEMS = [
  { id: 'dashboard',     label: 'Dashboard',     href: APP_CONFIG.ROUTES.DASHBOARD,      seccion: 'MENU',    icono: 'grid' },
  { id: 'docentes',      label: 'Docentes',      href: APP_CONFIG.ROUTES.TEACHERS,       seccion: 'MENU',    icono: 'user' },
  { id: 'estudiantes',   label: 'Estudiantes',   href: APP_CONFIG.ROUTES.STUDENTS,       seccion: 'MENU',    icono: 'users' },
  { id: 'grupos',        label: 'Grupos',        href: APP_CONFIG.ROUTES.GROUPS,         seccion: 'MENU',    icono: 'layers' },
  { id: 'materias',      label: 'Materias',      href: APP_CONFIG.ROUTES.SUBJECTS,       seccion: 'MENU',    icono: 'book' },
  { id: 'asignaciones',  label: 'Asignaciones',  href: APP_CONFIG.ROUTES.GROUP_SUBJECTS, seccion: 'MENU',    icono: 'link' },
  { id: 'codigos',       label: 'Códigos',       href: APP_CONFIG.ROUTES.LINK_CODES,     seccion: 'GENERAL', icono: 'key' }
];

// ===== Set de íconos SVG simples (esquinas redondeadas, sin librerías) =====
const ICON_SVGS = {
  grid:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="7" height="7" rx="2"/><rect x="14" y="3" width="7" height="7" rx="2"/><rect x="3" y="14" width="7" height="7" rx="2"/><rect x="14" y="14" width="7" height="7" rx="2"/></svg>',
  user:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="8" r="4"/><path d="M4 21c0-4 4-6 8-6s8 2 8 6"/></svg>',
  users:  '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="9" cy="8" r="3.5"/><circle cx="17" cy="9" r="3"/><path d="M2 21c0-3.5 3-5.5 7-5.5s7 2 7 5.5"/><path d="M15 15.2c3 .3 5 2.1 5 5.8"/></svg>',
  layers: '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 3 3 8l9 5 9-5-9-5Z"/><path d="m3 13 9 5 9-5"/></svg>',
  book:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M4 4.5A2.5 2.5 0 0 1 6.5 2H20v17H6.5A2.5 2.5 0 0 0 4 21.5Z"/><path d="M4 4.5v15A2.5 2.5 0 0 0 6.5 22H20"/></svg>',
  link:   '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 17H7a5 5 0 0 1 0-10h2"/><path d="M15 7h2a5 5 0 0 1 0 10h-2"/><line x1="8" y1="12" x2="16" y2="12"/></svg>',
  key:    '<svg class="sidebar__icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="8" cy="15" r="4"/><path d="m10.5 12.5 8-8"/><path d="M16 7h3v3"/></svg>'
};

// ============================================================
// AppRouter — sesión + protección de rutas + render del sidebar
// ============================================================
const AppRouter = {
  // ===== Sesión =====
  guardarSesion(token, usuario) {
    sessionStorage.setItem(APP_CONFIG.STORAGE_KEYS.TOKEN, token);
    sessionStorage.setItem(APP_CONFIG.STORAGE_KEYS.USER, JSON.stringify(usuario));
  },

  getToken() {
    return sessionStorage.getItem(APP_CONFIG.STORAGE_KEYS.TOKEN);
  },

  getUsuario() {
    const raw = sessionStorage.getItem(APP_CONFIG.STORAGE_KEYS.USER);
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

  /**
   * Debe llamarse al cargar cualquier página protegida (todas menos inicio.html).
   * Redirige al login si no hay sesión válida. Devuelve true/false.
   */
  protegerPagina() {
    if (!this.estaAutenticado()) {
      window.location.href = APP_CONFIG.ROUTES.LOGIN;
      return false;
    }
    return true;
  },

  cerrarSesion() {
    AuthService.logout();
  },

  // ===== Sidebar dinámico =====
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

    contenedor.innerHTML = `
      <aside class="sidebar">
        <div class="smarca-logo">
          <img src="../img/logo.png" alt="LUMINA" onerror="this.style.display='none'" />
          LUMINA
        </div>

        <div class="sidebar__seccion-titulo">Menú</div>
        ${renderLinks(grupoMenu)}

        <div class="sidebar__seccion-titulo">General</div>
        ${renderLinks(grupoGeneral)}
      </aside>
    `;
  },

  // ===== Barra superior: usuario + logout =====
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