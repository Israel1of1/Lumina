(function () {
  "use strict";

  var CLAVE = "LUMINA_PLANES_V2";
  var memoria = null;
  var localDisponible = true;

  try {
    window.localStorage.setItem("__lumina_check__", "1");
    window.localStorage.removeItem("__lumina_check__");
  } catch (e) {
    localDisponible = false;
  }

  function generarPortada(texto) {
    var paleta = [
      ["#00347a", "#2F5A9E"],
      ["#0F5132", "#1E7A4C"],
      ["#7A4B12", "#C98A2C"],
      ["#5B1B6B", "#8C3AA0"],
      ["#8A1F2B", "#C6414F"]
    ];
    var suma = 0;
    for (var i = 0; i < texto.length; i++) { suma += texto.charCodeAt(i); }
    var par = paleta[suma % paleta.length];
    var svg = '<svg xmlns="http://www.w3.org/2000/svg" width="400" height="200" viewBox="0 0 400 200">' +
      '<defs><linearGradient id="g" x1="0" y1="0" x2="1" y2="1">' +
      '<stop offset="0" stop-color="' + par[0] + '"/><stop offset="1" stop-color="' + par[1] + '"/>' +
      '</linearGradient></defs>' +
      '<rect width="400" height="200" fill="url(#g)"/>' +
      '<g opacity="0.16" fill="#ffffff"><rect x="140" y="55" width="120" height="95" rx="10"/></g>' +
      '<g fill="none" stroke="#ffffff" stroke-width="4" stroke-linecap="round" stroke-linejoin="round" opacity="0.9">' +
      '<path d="M160 88h80M160 108h80M160 128h50"/></g></svg>';
    return "data:image/svg+xml;utf8," + encodeURIComponent(svg);
  }

  var SEMILLA = [
    { id: "p1", nombre: "Manejo de Turnos", nivel: "Nivel 3", aula: "Aula 1", duracion: "30", estado: "PUBLICADO", fecha: "2025-05-20", objetivo: "Promover la espera y el respeto del turno en actividades grupales.", notas: "", etapas: [], portada: generarPortada("Manejo de Turnos") },
    { id: "p2", nombre: "Coordinación Motora", nivel: "Nivel 1", aula: "Aula 1", duracion: "30", estado: "PUBLICADO", fecha: "2025-05-15", objetivo: "Desarrollar habilidades motoras finas a través de actividades manipulativas.", notas: "", etapas: [], portada: generarPortada("Coordinación Motora") },
    { id: "p3", nombre: "Vocabulario de Frutas", nivel: "Nivel 2", aula: "Aula 1", duracion: "20", estado: "BORRADOR", fecha: "2025-05-08", objetivo: "Ampliar el vocabulario a través del reconocimiento de frutas.", notas: "", etapas: [], portada: generarPortada("Vocabulario de Frutas") },
    { id: "p4", nombre: "Rutinas y Transiciones", nivel: "Nivel 2", aula: "Aula 1", duracion: "25", estado: "BORRADOR", fecha: "2025-05-02", objetivo: "Fomentar la adaptación a cambios de actividad con apoyo visual.", notas: "", etapas: [], portada: generarPortada("Rutinas y Transiciones") }
  ];

  function leer() {
    if (localDisponible) {
      try {
        var crudo = window.localStorage.getItem(CLAVE);
        if (crudo) return JSON.parse(crudo);
      } catch (e) {}
    }
    if (memoria) return memoria;
    memoria = JSON.parse(JSON.stringify(SEMILLA));
    guardar(memoria);
    return memoria;
  }

  function guardar(lista) {
    memoria = lista;
    if (localDisponible) {
      try { window.localStorage.setItem(CLAVE, JSON.stringify(lista)); } catch (e) {}
    }
  }

  function eliminarPlan(id) {
    var lista = leer().filter(function (p) { return p.id !== id; });
    guardar(lista);
  }

  function actualizarEstado(id, estado) {
    var lista = leer();
    var idx = lista.findIndex(function (p) { return p.id === id; });
    if (idx === -1) return;
    lista[idx].estado = estado;
    guardar(lista);
  }

  var estadoUI = { filtro: "TODOS", busqueda: "", idPorBorrar: null };

  var gridPlanes = document.getElementById("grid-planes");
  var buscadorPlanes = document.getElementById("buscador-planes");
  var tabsEstado = document.getElementById("tabs-estado");
  var btnNuevoPlan = document.getElementById("btn-nuevo-plan");
  var modalBorrar = document.getElementById("modal-borrar");
  var btnCancelarBorrado = document.getElementById("btn-cancelar-borrado");
  var btnConfirmarBorrado = document.getElementById("btn-confirmar-borrado");

  function formatearFecha(iso) {
    var partes = iso.split("-");
    var meses = ["ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"];
    return partes[2] + " " + meses[parseInt(partes[1], 10) - 1] + ", " + partes[0];
  }

  function escaparHtml(texto) {
    var div = document.createElement("div");
    div.textContent = texto || "";
    return div.innerHTML;
  }

  var ICONO_EDITAR = '<svg class="pc-icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 20h9"/><path d="M16.5 3.5a2.1 2.1 0 0 1 3 3L7 19l-4 1 1-4Z"/></svg>';
  var ICONO_PUBLICAR = '<svg class="pc-icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 12a9 9 0 1 1-3-6.7"/><path d="M21 3v6h-6"/></svg>';
  var ICONO_ELIMINAR = '<svg class="pc-icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M3 6h18"/><path d="M8 6V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/><path d="M19 6l-1 14a2 2 0 0 1-2 2H8a2 2 0 0 1-2-2L5 6"/></svg>';
  var ICONO_PUNTOS = '<svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" width="18" height="18"><circle cx="12" cy="5" r="1.5"/><circle cx="12" cy="12" r="1.5"/><circle cx="12" cy="19" r="1.5"/></svg>';
  var ICONO_LIBRO = '<svg class="pc-icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M4 19.5A2.5 2.5 0 0 1 6.5 17H20"/><path d="M6.5 2H20v20H6.5A2.5 2.5 0 0 1 4 19.5v-15A2.5 2.5 0 0 1 6.5 2Z"/></svg>';
  var ICONO_CALENDARIO = '<svg class="pc-icono" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="4" width="18" height="18" rx="2"/><path d="M16 2v4M8 2v4M3 10h18"/></svg>';

  function renderizar() {
    var todos = leer();

    var filtrados = todos.filter(function (plan) {
      var pasaEstado = estadoUI.filtro === "TODOS" || plan.estado === estadoUI.filtro;
      var texto = (plan.nombre + " " + plan.objetivo).toLowerCase();
      var pasaBusqueda = texto.indexOf(estadoUI.busqueda.toLowerCase()) !== -1;
      return pasaEstado && pasaBusqueda;
    });

    if (filtrados.length === 0) {
      gridPlanes.innerHTML = '<div class="pc-estado"><h3>No se encontraron planes</h3><p>Intenta con otra búsqueda o crea un nuevo plan de clase.</p></div>';
      return;
    }

    gridPlanes.innerHTML = filtrados.map(tarjetaHTML).join("");

    gridPlanes.querySelectorAll("[data-abrir]").forEach(function (tarjeta) {
      tarjeta.addEventListener("click", function () {
        window.location.href = "plan-editor.html?id=" + encodeURIComponent(tarjeta.dataset.abrir);
      });
    });

    gridPlanes.querySelectorAll("[data-menu-boton]").forEach(function (btn) {
      btn.addEventListener("click", function (e) {
        e.stopPropagation();
        var lista = btn.parentElement.querySelector(".pc-menu__lista");
        cerrarMenus();
        lista.classList.toggle("abierto");
      });
    });

    gridPlanes.querySelectorAll("[data-editar]").forEach(function (btn) {
      btn.addEventListener("click", function (e) {
        e.stopPropagation();
        window.location.href = "plan-editor.html?id=" + encodeURIComponent(btn.dataset.editar);
      });
    });

    gridPlanes.querySelectorAll("[data-publicar]").forEach(function (btn) {
      btn.addEventListener("click", function (e) {
        e.stopPropagation();
        var lista = leer();
        var plan = lista.find(function (p) { return p.id === btn.dataset.publicar; });
        if (!plan) return;
        actualizarEstado(plan.id, plan.estado === "PUBLICADO" ? "BORRADOR" : "PUBLICADO");
        renderizar();
      });
    });

    gridPlanes.querySelectorAll("[data-eliminar]").forEach(function (btn) {
      btn.addEventListener("click", function (e) {
        e.stopPropagation();
        estadoUI.idPorBorrar = btn.dataset.eliminar;
        modalBorrar.classList.remove("oculto");
      });
    });
  }

  function tarjetaHTML(plan) {
    var esPublicado = plan.estado === "PUBLICADO";
    var badgeClase = esPublicado ? "pc-badge--publicado" : "pc-badge--borrador";
    var badgeTexto = esPublicado ? "Publicado" : "Borrador";
    var accionPublicar = esPublicado ? "Pasar a borrador" : "Publicar plan";
    var imagen = plan.portada || generarPortada(plan.nombre);

    return (
      '<article class="pc-tarjeta" data-abrir="' + plan.id + '">' +
        '<div class="pc-tarjeta__media" style="background-image:url(\'' + imagen + '\')">' +
          '<span class="pc-badge ' + badgeClase + '">' + badgeTexto + '</span>' +
        '</div>' +
        '<div class="pc-tarjeta__cuerpo">' +
          '<div class="pc-tarjeta__cabecera">' +
            '<h3>' + escaparHtml(plan.nombre) + '</h3>' +
            '<div class="pc-menu">' +
              '<button class="pc-menu__boton" data-menu-boton type="button">' + ICONO_PUNTOS + '</button>' +
              '<div class="pc-menu__lista">' +
                '<button data-editar="' + plan.id + '">' + ICONO_EDITAR + ' Editar plan</button>' +
                '<button data-publicar="' + plan.id + '">' + ICONO_PUBLICAR + ' ' + accionPublicar + '</button>' +
                '<button class="pc-menu__eliminar" data-eliminar="' + plan.id + '">' + ICONO_ELIMINAR + ' Eliminar</button>' +
              '</div>' +
            '</div>' +
          '</div>' +
          '<div class="pc-tarjeta__meta">' + ICONO_LIBRO + ' ' + escaparHtml(plan.nivel) + ' · ' + escaparHtml(plan.aula) + '</div>' +
          '<div class="pc-tarjeta__meta">' + ICONO_CALENDARIO + ' ' + formatearFecha(plan.fecha) + '</div>' +
          '<p class="pc-tarjeta__descripcion">' + escaparHtml(plan.objetivo) + '</p>' +
        '</div>' +
      '</article>'
    );
  }

  function cerrarMenus() {
    document.querySelectorAll(".pc-menu__lista.abierto").forEach(function (m) { m.classList.remove("abierto"); });
  }

  btnNuevoPlan.addEventListener("click", function () {
    window.location.href = "plan-editor.html";
  });

  btnCancelarBorrado.addEventListener("click", function () {
    modalBorrar.classList.add("oculto");
    estadoUI.idPorBorrar = null;
  });

  btnConfirmarBorrado.addEventListener("click", function () {
    if (estadoUI.idPorBorrar) { eliminarPlan(estadoUI.idPorBorrar); }
    modalBorrar.classList.add("oculto");
    estadoUI.idPorBorrar = null;
    renderizar();
  });

  modalBorrar.addEventListener("click", function (e) {
    if (e.target === modalBorrar) {
      modalBorrar.classList.add("oculto");
      estadoUI.idPorBorrar = null;
    }
  });

  tabsEstado.addEventListener("click", function (e) {
    var btn = e.target.closest(".pc-tab");
    if (!btn) return;
    tabsEstado.querySelectorAll(".pc-tab").forEach(function (t) { t.classList.remove("activa"); });
    btn.classList.add("activa");
    estadoUI.filtro = btn.dataset.filtro;
    renderizar();
  });

  buscadorPlanes.addEventListener("input", function (e) {
    estadoUI.busqueda = e.target.value;
    renderizar();
  });

  document.addEventListener("click", cerrarMenus);
  document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") {
      cerrarMenus();
      modalBorrar.classList.add("oculto");
    }
  });

  renderizar();
})();