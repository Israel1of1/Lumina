// ============================================================
// Lógica del módulo Materias — LUMINA
// Nota: Subject no tiene isActive en el modelo (según el SQL),
// así que este módulo solo permite crear y editar, sin dar de baja.
// ============================================================

let materiasCache = [];

function iniciarPagina() {
  document.getElementById('btn-nueva-materia').addEventListener('click', abrirNuevaMateria);
  document.getElementById('form-materia').addEventListener('submit', guardarMateria);
  document.getElementById('buscador-materias').addEventListener('input', filtrarMaterias);
  document.getElementById('materia-color').addEventListener('input', actualizarVistaColor);

  cargarMaterias();
}

async function cargarMaterias() {
  const contenedor = document.getElementById('materias-grid');
  contenedor.innerHTML = '<div class="cargando">Cargando...</div>';

  try {
    materiasCache = await SubjectService.getAll() || [];
    renderMaterias(materiasCache);
  } catch (error) {
    contenedor.innerHTML = `<p class="texto-error">Error al cargar materias: ${error.message}</p>`;
  }
}

function renderMaterias(lista) {
  const contenedor = document.getElementById('materias-grid');

  if (lista.length === 0) {
    contenedor.innerHTML = '<p class="texto-suave">No hay materias registradas.</p>';
    return;
  }

  contenedor.innerHTML = lista.map(m => `
    <div class="tarjeta-materia" style="border-top-color: ${m.color || 'var(--primary)'}">
      <div class="tarjeta-materia__header">
        <div class="tarjeta-materia__icono">${m.icon || '📘'}</div>
        <div class="tarjeta-materia__nombre">${m.name}</div>
      </div>
      <p class="tarjeta-materia__descripcion">${m.description || 'Sin descripción.'}</p>
      <div class="tarjeta-materia__acciones">
        <button class="btn-icono" onclick="abrirEditarMateria(${m.id})">Editar</button>
      </div>
    </div>
  `).join('');
}

function filtrarMaterias() {
  const texto = document.getElementById('buscador-materias').value.trim().toLowerCase();

  if (!texto) {
    renderMaterias(materiasCache);
    return;
  }

  const filtradas = materiasCache.filter(m => m.name.toLowerCase().includes(texto));
  renderMaterias(filtradas);
}

function actualizarVistaColor() {
  document.getElementById('vista-color-hex').textContent = document.getElementById('materia-color').value;
}

// ===== Crear =====
function abrirNuevaMateria() {
  document.getElementById('modal-materia-titulo').textContent = 'Nueva materia';
  document.getElementById('form-materia').reset();
  document.getElementById('materia-id').value = '';
  document.getElementById('materia-color').value = '#00347a';
  actualizarVistaColor();
  ocultarError('materia-error');
  abrirModal('modal-materia');
}

// ===== Editar =====
function abrirEditarMateria(id) {
  const materia = materiasCache.find(m => m.id === id);
  if (!materia) return;

  document.getElementById('modal-materia-titulo').textContent = 'Editar materia';
  document.getElementById('materia-id').value = materia.id;
  document.getElementById('materia-nombre').value = materia.name;
  document.getElementById('materia-descripcion').value = materia.description || '';
  document.getElementById('materia-color').value = materia.color || '#00347a';
  document.getElementById('materia-icono').value = materia.icon || '';
  actualizarVistaColor();
  ocultarError('materia-error');
  abrirModal('modal-materia');
}

async function guardarMateria(evento) {
  evento.preventDefault();
  ocultarError('materia-error');

  const id = document.getElementById('materia-id').value;

  const datos = {
    name: document.getElementById('materia-nombre').value.trim(),
    description: document.getElementById('materia-descripcion').value.trim() || null,
    color: document.getElementById('materia-color').value,
    icon: document.getElementById('materia-icono').value.trim() || null
  };

  try {
    if (id) {
      await SubjectService.update(id, datos);
    } else {
      await SubjectService.create(datos);
    }

    cerrarModal('modal-materia');
    await cargarMaterias();
  } catch (error) {
    mostrarError('materia-error', error.message);
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