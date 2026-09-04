// ============================================================
// Punto de entrada de la app — LUMINA Panel de Administración
// Se incluye en TODAS las páginas protegidas (todas menos inicio.html).
// Requiere: app.config.js, api.services.js y app.routers.js cargados antes.
// ============================================================

document.addEventListener('DOMContentLoaded', () => {
//  if (!AppRouter.protegerPagina()) return;

  const paginaActual = document.body.dataset.pagina || '';
  AppRouter.renderSidebar(paginaActual);
  AppRouter.renderTopbarUsuario();

  // Punto de extensión: cada página define window.iniciarPagina()
  // en su propio archivo (ej. core/dashboard.js) para su lógica específica.
  if (typeof iniciarPagina === 'function') {
    iniciarPagina();
  }
});