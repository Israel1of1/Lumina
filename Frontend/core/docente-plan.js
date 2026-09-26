const datosIniciales = [
  {
    id: 1,
    titulo: "Manejo de Turnos",
    estado: "publicado",
    nivel: "Nivel 3",
    aula: "Aula 1",
    icono: "🧱",
    descripcion: "Promueve el respeto y la espera del turno en actividades grupales.",
    duracion: "45 minutos",
    materia: "Convivencia",
    objetivo: "Respetar el turno propio y el de los compañeros.",
    actividades: "1. Conversación sobre esperar el turno.\n2. Juego de construcción.\n3. Cierre con reflexión grupal.",
    materiales: "Bloques, tarjetas de turno y reloj visual.",
    evaluacion: "Observación directa del respeto por los turnos.",
    portada: "",
    recursos: []
  }
];

const estructuraPlan = {
  titulo: "",
  estado: "borrador",
  nivel: "",
  aula: "",
  icono: "📚",
  descripcion: "",
  duracion: "",
  materia: "",
  objetivo: "",
  actividades: "",
  materiales: "",
  evaluacion: "",
  portada: "",
  recursos: []
};

const guardados = JSON.parse(localStorage.getItem("lumina-planes") || "null");

let planes = (guardados || datosIniciales).map((plan) => ({
  ...estructuraPlan,
  ...plan,
  recursos: Array.isArray(plan.recursos) ? plan.recursos : []
}));

let filtroActual = "todos";
let portadaNueva = "";
let recursosNuevos = [];

const lista = document.querySelector("#lista-planes");
const busqueda = document.querySelector("#buscar-planes");
const sinResultados = document.querySelector("#sin-resultados");
const modalPlan = document.querySelector("#modal-plan");
const modalFormulario = document.querySelector("#modal-formulario");
const inputPortada = document.querySelector("#input-portada");
const inputRecursos = document.querySelector("#input-recursos");

function guardar() {
  localStorage.setItem("lumina-planes", JSON.stringify(planes));
}

function texto(valor, respaldo = "Sin información registrada.") {
  return valor && String(valor).trim() ? valor : respaldo;
}

function estado(valor) {
  return valor === "publicado" ? "Publicado" : "Borrador";
}

function iconoRecurso(tipo = "") {
  if (tipo.startsWith("image/")) return "🖼️";
  if (tipo.startsWith("video/")) return "🎬";
  if (tipo.includes("pdf")) return "📕";
  if (tipo.includes("word")) return "📘";
  if (tipo.includes("presentation")) return "📙";
  return "📄";
}

function mostrarPlanes() {
  const consulta = busqueda.value.trim().toLowerCase();

  const visibles = planes.filter((plan) => {
    const coincideFiltro =
      filtroActual === "todos" || plan.estado === filtroActual;

    const coincideBusqueda =
      `${plan.titulo} ${plan.nivel} ${plan.aula}`
        .toLowerCase()
        .includes(consulta);

    return coincideFiltro && coincideBusqueda;
  });

  lista.innerHTML = visibles.map((plan) => `
    <article class="plan">
      <div class="plan__imagen">
        ${
          plan.portada
            ? `<img src="${plan.portada}" alt="Portada de ${plan.titulo}">`
            : plan.icono
        }
      </div>

      <div class="plan__contenido">
        <h2 class="plan__titulo">${texto(plan.titulo, "Plan sin título")}</h2>

        <span class="estado ${plan.estado}">
          ${estado(plan.estado)}
        </span>

        <div class="plan__meta">
          <span>▣ ${texto(plan.nivel, "Sin nivel")}</span>
          <span>·</span>
          <span>${texto(plan.aula, "Sin aula")}</span>
        </div>

        <p class="plan__descripcion">${texto(plan.descripcion)}</p>

        <div class="plan__pie">
          <button class="plan__abrir" data-open="${plan.id}" type="button">
            Ver plan →
          </button>

          <div class="menu-plan-contenedor">
            <button
              class="menu-plan"
              type="button"
              data-menu="${plan.id}"
              aria-label="Opciones del plan"
            >
              ⋮
            </button>

            <div class="opciones-plan" id="opciones-${plan.id}" hidden>
              <button type="button" data-open="${plan.id}">
                Ver plan
              </button>

              <button
                type="button"
                class="opcion-eliminar"
                data-delete="${plan.id}"
              >
                Eliminar plan
              </button>
            </div>
          </div>
        </div>
      </div>
    </article>
  `).join("");

  sinResultados.hidden = visibles.length > 0;
}

function abrirDetalle(id) {
  const plan = planes.find((item) => item.id === Number(id));

  if (!plan) return;

  const seccion = (titulo, contenido) => `
    <section class="detalle__seccion">
      <h3>${titulo}</h3>
      <p>${texto(contenido)}</p>
    </section>
  `;

  const recursos = plan.recursos.map((recurso) => `
    <li class="recurso-item">
      <span>${iconoRecurso(recurso.tipo)} ${recurso.nombre}</span>

      <a href="${recurso.archivo}" target="_blank" download="${recurso.nombre}">
        Ver / abrir
      </a>
    </li>
  `).join("");

  document.querySelector("#contenido-detalle").innerHTML = `
    ${plan.portada ? `<img class="portada-plan" src="${plan.portada}" alt="Portada">` : ""}

    <div class="detalle__cabecera">
      <div class="detalle__imagen">${plan.icono}</div>

      <div>
        <h2>${texto(plan.titulo, "Plan sin título")}</h2>
        <span class="estado ${plan.estado}">${estado(plan.estado)}</span>
      </div>
    </div>

    <p class="detalle__texto">${texto(plan.descripcion)}</p>

    <div class="detalle__datos">
      <div>Nivel<strong>${texto(plan.nivel, "Sin nivel")}</strong></div>
      <div>Área<strong>${texto(plan.materia, "General")}</strong></div>
      <div>Duración<strong>${texto(plan.duracion, "No definida")}</strong></div>
    </div>

    ${seccion("Objetivo de aprendizaje", plan.objetivo)}
    ${seccion("Actividades", plan.actividades)}
    ${seccion("Materiales", plan.materiales)}
    ${seccion("Evaluación", plan.evaluacion)}

    <section class="detalle__seccion">
      <h3>Recursos adjuntos</h3>

      <ul class="lista-recursos">
        ${recursos || "<li>No hay recursos adjuntos.</li>"}
      </ul>
    </section>
  `;

  modalPlan.showModal();
}

function mostrarPortadaNueva() {
  const vista = document.querySelector("#vista-portada-nueva");

  vista.innerHTML = portadaNueva
    ? `<img src="${portadaNueva}" alt="Portada seleccionada">`
    : "Aún no seleccionaste una portada";
}

function mostrarRecursosNuevos() {
  const contenedor = document.querySelector("#lista-recursos-nuevos");

  contenedor.innerHTML = recursosNuevos.length
    ? recursosNuevos.map((recurso, indice) => `
      <li class="recurso-item">
        <span>${iconoRecurso(recurso.tipo)} ${recurso.nombre}</span>

        <button
          class="btn-recurso"
          type="button"
          data-quitar-nuevo="${indice}"
        >
          Quitar
        </button>
      </li>
    `).join("")
    : "<li>No hay recursos adjuntos todavía.</li>";
}

function leerArchivo(archivo) {
  return new Promise((resolve) => {
    const lector = new FileReader();

    lector.onload = () => {
      resolve({
        nombre: archivo.name,
        tipo: archivo.type,
        archivo: lector.result
      });
    };

    lector.readAsDataURL(archivo);
  });
}

lista.addEventListener("click", (event) => {
  const botonAbrir = event.target.closest("[data-open]");
  const botonMenu = event.target.closest("[data-menu]");
  const botonEliminar = event.target.closest("[data-delete]");

  if (botonAbrir) {
    abrirDetalle(botonAbrir.dataset.open);
  }

  if (botonMenu) {
    document.querySelectorAll(".opciones-plan").forEach((menu) => {
      if (menu.id !== `opciones-${botonMenu.dataset.menu}`) {
        menu.hidden = true;
      }
    });

    const menuActual = document.querySelector(
      `#opciones-${botonMenu.dataset.menu}`
    );

    menuActual.hidden = !menuActual.hidden;
  }

  if (botonEliminar) {
    const id = Number(botonEliminar.dataset.delete);

    if (confirm("¿Seguro que deseas eliminar este plan?")) {
      planes = planes.filter((plan) => plan.id !== id);

      guardar();
      mostrarPlanes();
    }
  }
});

document.querySelector("#btn-nuevo").addEventListener("click", () => {
  portadaNueva = "";
  recursosNuevos = [];

  mostrarPortadaNueva();
  mostrarRecursosNuevos();

  modalFormulario.showModal();
});

document.querySelector("#btn-subir-portada").addEventListener("click", () => {
  inputPortada.click();
});

inputPortada.addEventListener("change", (event) => {
  const archivo = event.target.files[0];

  if (!archivo) return;

  const lector = new FileReader();

  lector.onload = () => {
    portadaNueva = lector.result;
    mostrarPortadaNueva();
  };

  lector.readAsDataURL(archivo);
  inputPortada.value = "";
});

document.querySelector("#btn-agregar-recursos").addEventListener("click", () => {
  inputRecursos.click();
});

inputRecursos.addEventListener("change", async (event) => {
  const archivos = [...event.target.files];

  const leidos = await Promise.all(archivos.map(leerArchivo));

  recursosNuevos.push(...leidos);

  mostrarRecursosNuevos();

  inputRecursos.value = "";
});

document.querySelector("#lista-recursos-nuevos").addEventListener("click", (event) => {
  const boton = event.target.closest("[data-quitar-nuevo]");

  if (!boton) return;

  recursosNuevos.splice(Number(boton.dataset.quitarNuevo), 1);

  mostrarRecursosNuevos();
});

document.querySelector("#form-plan").addEventListener("submit", (event) => {
  event.preventDefault();

  const datos = new FormData(event.currentTarget);

  const nuevoPlan = {
    ...estructuraPlan,
    id: Date.now(),
    titulo: datos.get("titulo"),
    nivel: datos.get("nivel"),
    aula: datos.get("aula"),
    descripcion: datos.get("descripcion"),
    duracion: datos.get("duracion"),
    materia: datos.get("materia"),
    objetivo: datos.get("objetivo"),
    actividades: datos.get("actividades"),
    materiales: datos.get("materiales"),
    evaluacion: datos.get("evaluacion"),
    estado: event.submitter.value,
    portada: portadaNueva,
    recursos: recursosNuevos
  };

  planes.unshift(nuevoPlan);

  guardar();

  event.currentTarget.reset();
  modalFormulario.close();

  filtroActual = "todos";

  document.querySelector(".filtro.activo").classList.remove("activo");
  document.querySelector('[data-filter="todos"]').classList.add("activo");

  mostrarPlanes();
  abrirDetalle(nuevoPlan.id);
});

document.querySelectorAll(".filtro").forEach((boton) => {
  boton.addEventListener("click", () => {
    document.querySelector(".filtro.activo").classList.remove("activo");

    boton.classList.add("activo");
    filtroActual = boton.dataset.filter;

    mostrarPlanes();
  });
});

busqueda.addEventListener("input", mostrarPlanes);

document.querySelectorAll("[data-close]").forEach((boton) => {
  boton.addEventListener("click", () => {
    boton.closest("dialog").close();
  });
});

document.querySelector("#btn-menu").addEventListener("click", () => {
  const menu = document.querySelector("#menu-docente");

  menu.classList.toggle("colapsado");

  document.querySelector("#btn-menu").textContent =
    menu.classList.contains("colapsado") ? "›" : "‹";
});

document.querySelector("#btn-cerrar-sesion").addEventListener("click", () => {
  sessionStorage.clear();
  window.location.href = "login.html";
});

guardar();
mostrarPlanes();