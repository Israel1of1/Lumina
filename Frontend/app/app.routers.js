
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



const AppRouter = {
  // Sesión
  guardarSesion(token, usuario) {
     if (token) {
      localStorage.setItem(APP_CONFIG.STORAGE_KEYS.TOKEN, token);
      sessionStorage.setItem(APP_CONFIG.STORAGE_KEYS.TOKEN, token);
      localStorage.setItem('lumina_token', token);
      sessionStorage.setItem('lumina_token', token);
    }
    if (usuario) {
      const serialized = JSON.stringify(usuario);
      localStorage.setItem(APP_CONFIG.STORAGE_KEYS.USER, serialized);
      sessionStorage.setItem(APP_CONFIG.STORAGE_KEYS.USER, serialized);
      localStorage.setItem('lumina_user', serialized);
      sessionStorage.setItem('lumina_user', serialized);
    }
  },

  getToken() {
    return localStorage.getItem(APP_CONFIG.STORAGE_KEYS.TOKEN) ||
           sessionStorage.getItem(APP_CONFIG.STORAGE_KEYS.TOKEN) ||
           localStorage.getItem('lumina_token') ||
           sessionStorage.getItem('lumina_token') ||
           null;  
  },

  getUsuario() {
    const raw = localStorage.getItem(APP_CONFIG.STORAGE_KEYS.USER) ||
                sessionStorage.getItem(APP_CONFIG.STORAGE_KEYS.USER) ||
                localStorage.getItem('lumina_user') ||
                sessionStorage.getItem('lumina_user');
    if (!raw) return null;
    try {
      return JSON.parse(raw);
    } catch {
      return null;
    }
  },

  decodificarToken(token) {
    try {
     if (!token || typeof token !== 'string') return null;
      const partes = token.split('.');
      if (partes.length < 2) return null;
      let base64 = partes[1].replace(/-/g, '+').replace(/_/g, '/');
      while (base64.length % 4 !== 0) {
        base64 += '=';
      }
      const decodedStr = atob(base64);
      try {
        const jsonPayload = decodeURIComponent(
          decodedStr.split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join('')
        );
        return JSON.parse(jsonPayload);
      } catch {
        return JSON.parse(decodedStr);
      }
    } catch (e) {
      console.warn('Error al decodificar token JWT:', e);
      return null;
    }
  },

  estaAutenticado() {
    const token = this.getToken();
    if (!token) return false;

    const payload = this.decodificarToken(token);
    if (!payload || !payload.exp) {
      return token.split('.').length === 3;
    }

    const ahoraEnSegundos = Math.floor(Date.now() / 1000);
    return payload.exp > ahoraEnSegundos;
  },

  normalizarRol(rol) {
    if (!rol) return '';
    const r = String(rol).trim().toUpperCase();
    if (r === 'DOCENTE' || r === 'TEACHER') return 'TEACHER';
    if (r === 'TUTOR' || r === 'GUARDIAN') return 'GUARDIAN';
    if (r === 'INSTITUCION' || r === 'INSTITUTION') return 'INSTITUTION';
    return r;
  },




obtenerRutaPorRol(roles) {
    if (!Array.isArray(roles)) roles = roles ? [roles] : [];
    const rolesCanonicos = roles.map(r => this.normalizarRol(r));
    if (rolesCanonicos.includes('INSTITUTION')) return APP_CONFIG.ROUTES.DASHBOARD;
    if (rolesCanonicos.includes('TEACHER')) return APP_CONFIG.ROUTES.TEACHER_HOME || 'inicio-docente.html';
    if (rolesCanonicos.includes('GUARDIAN')) return APP_CONFIG.ROUTES.TUTOR_DASHBOARD;
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
      return ['TEACHER', ['Docente']];
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
      console.warn('[Guardia] Usuario no autenticado. Redirigiendo al login.');
      window.location.href = APP_CONFIG.ROUTES.LOGIN;
      return false;
    }   
    const usuario = this.getUsuario();
    const tokenPayload = this.decodificarToken(this.getToken());
    // Extraer roles de usuario o de los claims del JWT si usuario no los tiene
    let rolesRaw = usuario?.roles || [];
    if (!rolesRaw || rolesRaw.length === 0) {
      const claimRoles = tokenPayload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
                         tokenPayload?.role ||
                         tokenPayload?.roles;
      if (claimRoles) {
        rolesRaw = Array.isArray(claimRoles) ? claimRoles : [claimRoles];
      }
    }
    const rolesUsuarioCanonicos = (Array.isArray(rolesRaw) ? rolesRaw : [rolesRaw])
      .map(r => this.normalizarRol(r))
      .filter(Boolean);

    const permitidosRaw = rolesPermitidos || this.obtenerRolesPermitidosPorPagina();
    const permitidosCanonicos = (permitidosRaw || [])
      .map(r => this.normalizarRol(r))
      .filter(Boolean);

    if (permitidosCanonicos.length > 0) {
      const tieneRolPermitido = permitidosCanonicos.some(rol =>
        rolesUsuarioCanonicos.includes(rol)
      );
      if (!tieneRolPermitido) {
        console.warn('[Guardia] Acceso denegado. Roles usuario:', rolesUsuarioCanonicos, 'Permitidos:', permitidosCanonicos);
        const vistaCorrecta = this.obtenerRutaPorRol(rolesUsuarioCanonicos);
        window.location.href = vistaCorrecta;
        return false;
      }
    }

    return true;
  },

    cerrarSesion() {
    localStorage.removeItem(APP_CONFIG.STORAGE_KEYS.TOKEN);
    localStorage.removeItem(APP_CONFIG.STORAGE_KEYS.USER);
    localStorage.removeItem('lumina_token');
    localStorage.removeItem('lumina_user');
    localStorage.removeItem('lumina_local_db');
    localStorage.removeItem('lumina_tutor_active_student_id');
    localStorage.removeItem('lumina-planes-extra');
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