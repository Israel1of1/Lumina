document.getElementById('btn-iniciar-sesion').addEventListener('click', () => {
  window.location.href = 'login.html';
});

document.getElementById('btn-registrarse').addEventListener('click', () => {
  // El registro de Docente/Tutor requiere un código de vinculación emitido
  // por la institución (ver Lumina.md) — todavía no está construido.
  alert('El registro con código de vinculación aún no está disponible en esta versión.');
});