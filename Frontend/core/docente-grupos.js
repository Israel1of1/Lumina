let grupos = [
  {
    id: 1,
    nombre: "Grupo A",
    grado: "3.er grado",
    turno: "Mañana",
    descripcion: "Grupo alegre, participativo y creativo."
  },
  {
    id: 2,
    nombre: "Grupo B",
    grado: "3.er grado",
    turno: "Tarde",
    descripcion: "Estudiantes con gran interés por las actividades artísticas."
  },
  {
    id: 3,
    nombre: "Grupo C",
    grado: "4.to grado",
    turno: "Mañana",
    descripcion: "Grupo colaborativo y muy comprometido."
  }
];

let estudiantes = [
  {
    id: 1,
    nombre: "Mateo García",
    edad: 8,
    grupo: 1,
    grado: "3.er grado",
    tea: "Nivel 1 · Requiere apoyo",
    juguete: "Bloques de construcción",
    color: "Azul",
    responsable: "Laura García",
    observaciones: "Le gustan las actividades con bloques y rompecabezas.",
    foto: ""
  },
  {
    id: 2,
    nombre: "Sofía López",
    edad: 8,
    grupo: 1,
    grado: "3.er grado",
    tea: "No aplica",
    juguete: "Muñecas",
    color: "Morado",
    responsable: "Elena López",
    observaciones: "Le gusta dibujar y escuchar cuentos.",
    foto: ""
  },
  {
    id: 3,
    nombre: "Daniel Martínez",
    edad: 9,
    grupo: 2,
    grado: "3.er grado",
    tea: "Nivel 2 · Apoyo moderado",
    juguete: "Carritos",
    color: "Rojo",
    responsable: "Carlos Martínez",
    observaciones: "Responde mejor a instrucciones cortas y visuales.",
    foto: ""
  }
];

let grupoActual = null;
let grupoEditando = null;
let estudianteEditando = null;

const $ = selector => document.querySelector(selector);

function obtenerFoto(estudiante) {
  if (estudiante.foto) return estudiante.foto;

  return `https://ui-avatars.com/api/?name=${encodeURIComponent(
    estudiante.nombre
  )}&background=dbeaff&color=075fc9&bold=true`;
}

function mostrarVista(id) {
  document.querySelectorAll(".vista").forEach(vista => {
    vista.classList.remove("activa");
  });

  $("#" + id).classList.add("activa");
}

function cantidadEstudiantes(idGrupo) {
  return estudiantes.filter(estudiante => estudiante.grupo === idGrupo).length;
}

function renderizarGrupos() {
  const texto = $("#buscar-grupo").value.toLowerCase();

  const lista = grupos.filter(grupo =>
    grupo.nombre.toLowerCase().includes(texto) ||
    grupo.grado.toLowerCase().includes(texto)
  );

  $("#contenedor-grupos").innerHTML = lista.map(grupo => `
    <article class="tarjeta-grupo">
      <div class="icono-grupo">${grupo.nombre.replace("Grupo ", "")}</div>

      <h3>${grupo.nombre}</h3>

      <p class="meta">
        ${grupo.grado} · ${grupo.turno}<br>
        ${cantidadEstudiantes(grupo.id)} estudiantes
      </p>

      <p class="descripcion">${grupo.descripcion}</p>

      <div class="acciones-tarjeta">
        <button
          class="boton-icono"
          title="Ver estudiantes"
          onclick="abrirGrupo(${grupo.id})"
        >
          ◉
        </button>

        <button
          class="boton-icono editar"
          title="Editar grupo"
          onclick="editarGrupo(${grupo.id})"
        >
          ✎
        </button>

        <button
          class="boton-icono eliminar"
          title="Eliminar grupo"
          onclick="eliminarGrupo(${grupo.id})"
        >
          ⌫
        </button>
      </div>
    </article>
  `).join("");
}

function abrirGrupo(id) {
  grupoActual = id;

  const grupo = grupos.find(item => item.id === id);

  $("#titulo-estudiantes").textContent = grupo.nombre;

  $("#subtitulo-estudiantes").textContent =
    `${grupo.grado} · ${grupo.turno} · ${cantidadEstudiantes(id)} estudiantes`;

  mostrarVista("vista-estudiantes");
  renderizarEstudiantes();
}

function renderizarEstudiantes() {
  const texto = $("#buscar-estudiante").value.toLowerCase();

  const lista = estudiantes.filter(estudiante => {
    const pertenece = !grupoActual || estudiante.grupo === grupoActual;
    const coincide = estudiante.nombre.toLowerCase().includes(texto);

    return pertenece && coincide;
  });

  $("#contenedor-estudiantes").innerHTML = lista.map(estudiante => `
    <article class="tarjeta-estudiante">
      <img
        class="foto-estudiante"
        src="${obtenerFoto(estudiante)}"
        alt="Foto de ${estudiante.nombre}"
      >

      <div class="info-estudiante">
        <h3>${estudiante.nombre}</h3>
        <p>${estudiante.edad} años · ${estudiante.grado}</p>

        <div class="acciones-estudiante">
          <button
            class="boton-estudiante"
            onclick="verPerfil(${estudiante.id})"
          >
            ◉ Ver
          </button>

          <button
            class="boton-estudiante editar"
            onclick="editarEstudiante(${estudiante.id})"
          >
            ✎ Editar
          </button>

          <button
            class="boton-estudiante eliminar"
            title="Eliminar estudiante"
            onclick="eliminarEstudiante(${estudiante.id})"
          >
            ⌫
          </button>
        </div>
      </div>
    </article>
  `).join("");
}

function verPerfil(id) {
  const estudiante = estudiantes.find(item => item.id === id);
  const grupo = grupos.find(item => item.id === estudiante.grupo);

  $("#vista-perfil").innerHTML = `
    <div class="encabezado-docente">
      <div>
        <button
          class="boton-volver"
          type="button"
          onclick="mostrarVista('vista-estudiantes')"
        >
          ←
        </button>

        <h1>Perfil del estudiante</h1>
        <p>Información importante para acompañar su aprendizaje.</p>
      </div>
    </div>

    <article class="perfil-estudiante">
      <div class="perfil-superior">
        <div class="perfil-identidad">
          <img
            class="foto-perfil"
            src="${obtenerFoto(estudiante)}"
            alt="Foto de ${estudiante.nombre}"
          >

          <div>
            <h2>${estudiante.nombre}</h2>
            <p>${estudiante.edad} años · ${estudiante.grado} · ${grupo.nombre}</p>
          </div>
        </div>

        <div class="acciones-tarjeta">
          <button
            class="boton-icono editar"
            title="Editar estudiante"
            onclick="editarEstudiante(${estudiante.id})"
          >
            ✎
          </button>

          <button
            class="boton-icono eliminar"
            title="Eliminar estudiante"
            onclick="eliminarEstudiante(${estudiante.id})"
          >
            ⌫
          </button>
        </div>
      </div>

      <div class="datos-perfil">
        <div class="dato-perfil">
          <small>GRUPO</small>
          <strong>${grupo.nombre}</strong>
        </div>

        <div class="dato-perfil">
          <small>GRADO</small>
          <strong>${estudiante.grado}</strong>
        </div>

        <div class="dato-perfil">
          <small>NIVEL DE TEA</small>
          <strong>${estudiante.tea}</strong>
        </div>

        <div class="dato-perfil">
          <small>JUGUETE FAVORITO</small>
          <strong>${estudiante.juguete || "No registrado"}</strong>
        </div>

        <div class="dato-perfil">
          <small>COLOR FAVORITO</small>
          <strong>${estudiante.color || "No registrado"}</strong>
        </div>

        <div class="dato-perfil">
          <small>RESPONSABLE</small>
          <strong>${estudiante.responsable || "No registrado"}</strong>
        </div>

        <div class="dato-perfil" style="grid-column: 1 / -1">
          <small>OBSERVACIONES</small>
          <strong>${estudiante.observaciones || "Sin observaciones."}</strong>
        </div>
      </div>
    </article>
  `;

  mostrarVista("vista-perfil");
}

function llenarGrupos() {
  $("#select-grupo").innerHTML = grupos.map(grupo => `
    <option value="${grupo.id}">
      ${grupo.nombre} · ${grupo.grado}
    </option>
  `).join("");
}

function abrirModalGrupo(grupo = null) {
  grupoEditando = grupo;

  const form = $("#form-grupo");

  form.reset();

  $("#titulo-modal-grupo").textContent = grupo
    ? "Editar grupo"
    : "Agregar grupo";

  if (grupo) {
    form.nombre.value = grupo.nombre;
    form.grado.value = grupo.grado;
    form.turno.value = grupo.turno;
    form.descripcion.value = grupo.descripcion;
  }

  $("#modal-grupo").showModal();
}

function editarGrupo(id) {
  const grupo = grupos.find(item => item.id === id);
  abrirModalGrupo(grupo);
}

function eliminarGrupo(id) {
  const grupo = grupos.find(item => item.id === id);

  if (cantidadEstudiantes(id) > 0) {
    alert(`No puedes eliminar ${grupo.nombre} porque tiene estudiantes.`);
    return;
  }

  if (!confirm(`¿Deseas eliminar ${grupo.nombre}?`)) return;

  grupos = grupos.filter(item => item.id !== id);

  renderizarGrupos();
}

function abrirModalEstudiante(estudiante = null) {
  estudianteEditando = estudiante;

  const form = $("#form-estudiante");

  form.reset();
  llenarGrupos();

  $("#titulo-modal-estudiante").textContent = estudiante
    ? "Editar estudiante"
    : "Agregar estudiante";

  if (estudiante) {
    form.nombre.value = estudiante.nombre;
    form.edad.value = estudiante.edad;
    form.grupo.value = estudiante.grupo;
    form.grado.value = estudiante.grado;
    form.tea.value = estudiante.tea;
    form.juguete.value = estudiante.juguete;
    form.color.value = estudiante.color;
    form.responsable.value = estudiante.responsable;
    form.observaciones.value = estudiante.observaciones;
  } else if (grupoActual) {
    const grupo = grupos.find(item => item.id === grupoActual);

    form.grupo.value = grupo.id;
    form.grado.value = grupo.grado;
  }

  $("#modal-estudiante").showModal();
}

function editarEstudiante(id) {
  const estudiante = estudiantes.find(item => item.id === id);
  abrirModalEstudiante(estudiante);
}

function eliminarEstudiante(id) {
  const estudiante = estudiantes.find(item => item.id === id);

  if (!confirm(`¿Deseas eliminar a ${estudiante.nombre}?`)) return;

  estudiantes = estudiantes.filter(item => item.id !== id);

  renderizarGrupos();
  renderizarEstudiantes();
  mostrarVista("vista-estudiantes");
}

$("#btn-menu").addEventListener("click", () => {
  $("#menu-docente").classList.toggle("colapsado");

  $("#btn-menu").textContent = $("#menu-docente").classList.contains("colapsado")
    ? "›"
    : "‹";
});

$("#btn-agregar-grupo").addEventListener("click", () => {
  abrirModalGrupo();
});

$("#btn-agregar-estudiante").addEventListener("click", () => {
  abrirModalEstudiante();
});

$("#volver-grupos").addEventListener("click", () => {
  grupoActual = null;
  mostrarVista("vista-grupos");
});

$("#buscar-grupo").addEventListener("input", renderizarGrupos);
$("#buscar-estudiante").addEventListener("input", renderizarEstudiantes);

document.querySelectorAll("[data-cerrar]").forEach(boton => {
  boton.addEventListener("click", () => {
    $("#" + boton.dataset.cerrar).close();
  });
});

$("#form-grupo").addEventListener("submit", event => {
  event.preventDefault();

  const form = event.target;

  const datos = {
    id: grupoEditando ? grupoEditando.id : Date.now(),
    nombre: form.nombre.value,
    grado: form.grado.value,
    turno: form.turno.value,
    descripcion: form.descripcion.value
  };

  if (grupoEditando) {
    const indice = grupos.findIndex(item => item.id === grupoEditando.id);
    grupos[indice] = datos;
  } else {
    grupos.push(datos);
  }

  $("#modal-grupo").close();
  renderizarGrupos();
});

$("#form-estudiante").addEventListener("submit", event => {
  event.preventDefault();

  const form = event.target;
  const archivo = form.foto.files[0];

  function guardar(fotoNueva = "") {
    const datos = {
      id: estudianteEditando ? estudianteEditando.id : Date.now(),
      nombre: form.nombre.value,
      edad: form.edad.value,
      grupo: Number(form.grupo.value),
      grado: form.grado.value,
      tea: form.tea.value,
      juguete: form.juguete.value,
      color: form.color.value,
      responsable: form.responsable.value,
      observaciones: form.observaciones.value,
      foto: fotoNueva || (estudianteEditando ? estudianteEditando.foto : "")
    };

    if (estudianteEditando) {
      const indice = estudiantes.findIndex(item => item.id === estudianteEditando.id);
      estudiantes[indice] = datos;
    } else {
      estudiantes.push(datos);
    }

    $("#modal-estudiante").close();

    renderizarGrupos();
    renderizarEstudiantes();

    if (estudianteEditando) {
      verPerfil(datos.id);
    }
  }

  if (archivo) {
    const lector = new FileReader();

    lector.onload = () => guardar(lector.result);
    lector.readAsDataURL(archivo);
  } else {
    guardar();
  }
});

$("#btn-cerrar-sesion").addEventListener("click", () => {
  if (confirm("¿Deseas cerrar sesión?")) {
    window.location.href = "login.html";
  }
});

renderizarGrupos();
renderizarEstudiantes();