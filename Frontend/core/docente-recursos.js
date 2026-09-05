const recursos = [
 
  {
    id: 1,
    categoria: "videos",
    titulo: "La canción de las vocales",
    descripcion: "Aprende A, E, I, O, U de forma divertida.",
    icono: "🔤",
    youtube: "auVio9x6zbs"
  },
  {
    id: 2,
    categoria: "videos",
    titulo: "Vocales AEIOU",
    descripcion: "Práctica visual y auditiva de las vocales.",
    icono: "🅰️",
    youtube: "49Ui0oT2OEA"
  },
  {
    id: 3,
    categoria: "videos",
    titulo: "Cabeza, hombros, rodillas y pies",
    descripcion: "Canción con movimientos corporales.",
    icono: "🧍",
    youtube: "h4eueDYPTIg"
  },
  {
    id: 4,
    categoria: "videos",
    titulo: "Canción de los colores",
    descripcion: "Video infantil para identificar colores.",
    icono: "🎨",
    youtube: "qhOTU8_1Af4"
  },
  {
    id: 5,
    categoria: "videos",
    titulo: "Los números para niños",
    descripcion: "Aprende a contar de manera visual.",
    icono: "🔢",
    youtube: "pzmB0GoEKkA"
  },
  {
    id: 6,
    categoria: "videos",
    titulo: "Respiración para calmarse",
    descripcion: "Guía sencilla para regular las emociones.",
    icono: "🌈",
    youtube: "ZToicYcHIOU"
  },

  /* MÚSICA: recursos distintos */
  {
    id: 7,
    categoria: "musica",
    titulo: "Respiración tranquila",
    descripcion: "Música suave para realizar respiraciones lentas.",
    icono: "🌿",
    audio: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3"
  },
  {
    id: 8,
    categoria: "musica",
    titulo: "Momento de calma",
    descripcion: "Melodía para bajar el nivel de estímulo.",
    icono: "☁️",
    audio: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-2.mp3"
  },
  {
    id: 9,
    categoria: "musica",
    titulo: "Piano suave",
    descripcion: "Acompaña actividades tranquilas y de enfoque.",
    icono: "🎹",
    audio: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-3.mp3"
  },
  {
    id: 10,
    categoria: "musica",
    titulo: "Tiempo de descanso",
    descripcion: "Sonido ambiental para pausas sensoriales.",
    icono: "🌙",
    audio: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-4.mp3"
  },
  {
    id: 11,
    categoria: "musica",
    titulo: "Sonidos de la naturaleza",
    descripcion: "Música tranquila para relajarse en el aula.",
    icono: "🍃",
    audio: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-5.mp3"
  },
  {
    id: 12,
    categoria: "musica",
    titulo: "Concentración suave",
    descripcion: "Melodía para pintar, leer o descansar.",
    icono: "✨",
    audio: "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-6.mp3"
  },

  /* PECS: seis tableros */
  {
    id: 13,
    categoria: "pecs",
    titulo: "Necesidades básicas",
    descripcion: "Apoya la comunicación de necesidades cotidianas.",
    icono: "💧",
    tarjetas: [
      ["🙋", "Yo quiero", "Yo quiero"],
      ["💧", "Agua", "Quiero agua"],
      ["🍎", "Comer", "Quiero comer"],
      ["🧸", "Jugar", "Quiero jugar"],
      ["😴", "Descansar", "Quiero descansar"],
      ["❓", "Ayuda", "Necesito ayuda"]
    ]
  },
  {
    id: 14,
    categoria: "pecs",
    titulo: "Emociones",
    descripcion: "Ayuda a reconocer y comunicar emociones.",
    icono: "😊",
    tarjetas: [
      ["😊", "Feliz", "Me siento feliz"],
      ["😢", "Triste", "Me siento triste"],
      ["😡", "Enojado", "Me siento enojado"],
      ["😨", "Asustado", "Tengo miedo"],
      ["😌", "Tranquilo", "Me siento tranquilo"],
      ["🤗", "Abrazo", "Quiero un abrazo"]
    ]
  },
  {
    id: 15,
    categoria: "pecs",
    titulo: "Lavarse las manos",
    descripcion: "Rutina visual paso a paso de higiene.",
    icono: "🧼",
    tarjetas: [
      ["💧", "Mojar manos", "Abre la llave y moja tus manos."],
      ["🧴", "Aplicar jabón", "Coloca jabón en tus manos."],
      ["🫧", "Frotar", "Frota las palmas y los dedos."],
      ["🚿", "Enjuagar", "Enjuaga tus manos con agua."],
      ["🧻", "Secar", "Seca tus manos con una toalla."],
      ["✨", "Listo", "Tus manos están limpias. ¡Muy bien!"]
    ]
  },
  {
    id: 16,
    categoria: "pecs",
    titulo: "Rutina de clase",
    descripcion: "Secuencia de acciones dentro del aula.",
    icono: "🏫",
    tarjetas: [
      ["🎒", "Llegar", "Llegué a la clase."],
      ["🪑", "Sentarse", "Me siento en mi silla."],
      ["👂", "Escuchar", "Escucho a mi docente."],
      ["✏️", "Trabajar", "Realizo mi actividad."],
      ["🙋", "Participar", "Levanto mi mano."],
      ["🧹", "Ordenar", "Guardo mis materiales."]
    ]
  },
  {
    id: 17,
    categoria: "pecs",
    titulo: "Rutina de comida",
    descripcion: "Pasos para la hora de la merienda.",
    icono: "🍽️",
    tarjetas: [
      ["🧼", "Lavar manos", "Primero lavo mis manos."],
      ["🪑", "Sentarse", "Me siento en mi lugar."],
      ["🍎", "Comer", "Como mi merienda."],
      ["🥤", "Beber", "Bebo agua."],
      ["🧻", "Limpiar", "Limpio mi espacio."],
      ["🙌", "Terminé", "Terminé de comer."]
    ]
  },
  {
    id: 18,
    categoria: "pecs",
    titulo: "Pedir ayuda",
    descripcion: "Tarjetas para expresar necesidades en clase.",
    icono: "🆘",
    tarjetas: [
      ["🙋", "Yo necesito", "Yo necesito"],
      ["❓", "Ayuda", "Necesito ayuda"],
      ["🔁", "Otra vez", "Quiero intentarlo otra vez"],
      ["⏸️", "Pausa", "Necesito una pausa"],
      ["🔇", "Silencio", "Necesito silencio"],
      ["🤗", "Acompañamiento", "Quiero que me acompañes"]
    ]
  }
];

let categoriaActual = "todos";
let pecsActual = null;
let pasoActual = 0;

const $ = selector => document.querySelector(selector);

function nombreCategoria(categoria) {
  return {
    videos: "VIDEO",
    musica: "MÚSICA",
    pecs: "PECS"
  }[categoria];
}

function renderizarRecursos() {
  const texto = $("#buscar-recurso").value.toLowerCase();

  const categoria = $("#filtro-categoria").value !== "todos"
    ? $("#filtro-categoria").value
    : categoriaActual;

  const lista = recursos.filter(recurso => {
    const categoriaCorrecta =
      categoria === "todos" || recurso.categoria === categoria;

    const textoCorrecto =
      recurso.titulo.toLowerCase().includes(texto) ||
      recurso.descripcion.toLowerCase().includes(texto);

    return categoriaCorrecta && textoCorrecto;
  });

  $("#contador-recursos").textContent =
    `${lista.length} recursos disponibles`;

  $("#contenedor-recursos").innerHTML = lista.map(recurso => `
    <article class="tarjeta-recurso">
      <div class="recurso-ilustracion ${recurso.categoria}">
        ${recurso.icono}

        ${recurso.categoria === "videos"
          ? '<span class="play-ilustracion">▶</span>'
          : ''}
      </div>

      <div class="tarjeta-recurso__contenido">
        <span class="etiqueta-recurso ${recurso.categoria}">
          ${nombreCategoria(recurso.categoria)}
        </span>

        <h3>${recurso.titulo}</h3>
        <p>${recurso.descripcion}</p>

        <button class="btn-abrir-recurso" onclick="abrirRecurso(${recurso.id})">
          ${recurso.categoria === "videos" ? "Reproducir video" : ""}
          ${recurso.categoria === "musica" ? "Escuchar música" : ""}
          ${recurso.categoria === "pecs" ? "Abrir paso a paso" : ""}
        </button>
      </div>
    </article>
  `).join("");
}

function abrirRecurso(id) {
  const recurso = recursos.find(item => item.id === id);

  if (recurso.categoria === "pecs") {
    abrirPecs(recurso);
  } else {
    abrirReproductor(recurso);
  }
}

function abrirReproductor(recurso) {
  $("#titulo-reproductor").textContent = recurso.titulo;
  $("#tipo-reproductor").textContent = nombreCategoria(recurso.categoria);
  $("#tipo-reproductor").className = `etiqueta-recurso ${recurso.categoria}`;

  if (recurso.categoria === "videos") {
    $("#contenido-reproductor").innerHTML = `
      <div class="reproductor-video">
        <iframe
          src="https://www.youtube-nocookie.com/embed/${recurso.youtube}?autoplay=1&rel=0"
          title="${recurso.titulo}"
          allow="autoplay; encrypted-media; picture-in-picture"
          allowfullscreen
        ></iframe>
      </div>
    `;
  }

  if (recurso.categoria === "musica") {
    $("#contenido-reproductor").innerHTML = `
      <div class="reproductor-audio">
        <div class="nota">♫</div>
        <h3>${recurso.titulo}</h3>

        <audio controls autoplay>
          <source src="${recurso.audio}" type="audio/mpeg">
          Tu navegador no admite audio.
        </audio>
      </div>
    `;
  }

  $("#modal-reproductor").showModal();
}

function abrirPecs(recurso) {
  pecsActual = recurso;
  pasoActual = 0;

  $("#titulo-pecs").textContent = recurso.titulo;
  actualizarPasoPecs();

  $("#modal-pecs").showModal();
}

function actualizarPasoPecs() {
  const tarjetas = pecsActual.tarjetas;
  const paso = tarjetas[pasoActual];

  $("#paso-actual").textContent =
    `Paso ${pasoActual + 1} de ${tarjetas.length}`;

  $("#progreso-pecs").style.width =
    `${((pasoActual + 1) / tarjetas.length) * 100}%`;

  $("#imagen-paso-pecs").textContent = paso[0];
  $("#titulo-paso-pecs").textContent = paso[1];
  $("#texto-paso-pecs").textContent = paso[2];

  $("#pecs-anterior").disabled = pasoActual === 0;

  $("#pecs-siguiente").textContent =
    pasoActual === tarjetas.length - 1 ? "Finalizar ✓" : "Siguiente →";

  $("#contenido-pecs").innerHTML = tarjetas.map((tarjeta, indice) => `
    <button
      class="tarjeta-mini-pecs ${indice === pasoActual ? "activa" : ""}"
      type="button"
      onclick="irPasoPecs(${indice})"
    >
      <span>${tarjeta[0]}</span>
      ${tarjeta[1]}
    </button>
  `).join("");
}

function irPasoPecs(indice) {
  pasoActual = indice;
  actualizarPasoPecs();
}

function hablarPaso() {
  if (!pecsActual) return;

  window.speechSynthesis.cancel();

  const texto = pecsActual.tarjetas[pasoActual][2];
  const mensaje = new SpeechSynthesisUtterance(texto);

  mensaje.lang = "es-ES";
  mensaje.rate = 0.82;
  mensaje.pitch = 1;

  window.speechSynthesis.speak(mensaje);
}

function obtenerIdYoutube(enlace) {
  if (!enlace) return "";

  if (enlace.includes("youtu.be/")) {
    return enlace.split("youtu.be/")[1].split("?")[0];
  }

  if (enlace.includes("v=")) {
    return enlace.split("v=")[1].split("&")[0];
  }

  return enlace;
}

function agregarRecurso(event) {
  event.preventDefault();

  const form = event.target;
  const categoria = form.categoria.value;
  const enlace = form.enlace.value.trim();

  const nuevo = {
    id: Date.now(),
    categoria,
    titulo: form.titulo.value,
    descripcion: form.descripcion.value || "Recurso agregado por la docente.",
    icono: form.icono.value || "🌈"
  };

  if (categoria === "videos") {
    nuevo.youtube = obtenerIdYoutube(enlace);
  }

  if (categoria === "musica") {
    nuevo.audio = enlace;
  }

  if (categoria === "pecs") {
    nuevo.tarjetas = [
      ["1️⃣", "Primer paso", "Realiza el primer paso."],
      ["2️⃣", "Segundo paso", "Ahora realiza el segundo paso."],
      ["3️⃣", "Tercer paso", "Continúa con el tercer paso."],
      ["4️⃣", "Cuarto paso", "Realiza el cuarto paso."],
      ["5️⃣", "Quinto paso", "Completa el quinto paso."],
      ["✅", "Listo", "Terminaste la rutina. ¡Muy bien!"]
    ];
  }

  recursos.push(nuevo);

  $("#modal-agregar-recurso").close();
  form.reset();

  categoriaActual = categoria;
  $("#filtro-categoria").value = categoria;

  document.querySelectorAll(".tab").forEach(tab => {
    tab.classList.toggle("activo", tab.dataset.categoria === categoria);
  });

  renderizarRecursos();
}

$("#btn-menu").addEventListener("click", () => {
  $("#menu-docente").classList.toggle("colapsado");

  $("#btn-menu").textContent =
    $("#menu-docente").classList.contains("colapsado") ? "›" : "‹";
});

$("#buscar-recurso").addEventListener("input", renderizarRecursos);

$("#filtro-categoria").addEventListener("change", event => {
  categoriaActual = event.target.value;

  document.querySelectorAll(".tab").forEach(tab => {
    tab.classList.toggle("activo", tab.dataset.categoria === categoriaActual);
  });

  renderizarRecursos();
});

document.querySelectorAll(".tab").forEach(tab => {
  tab.addEventListener("click", () => {
    categoriaActual = tab.dataset.categoria;
    $("#filtro-categoria").value = categoriaActual;

    document.querySelectorAll(".tab").forEach(item => {
      item.classList.remove("activo");
    });

    tab.classList.add("activo");
    renderizarRecursos();
  });
});

$("#btn-agregar-recurso").addEventListener("click", () => {
  $("#modal-agregar-recurso").showModal();
});

$("#form-agregar-recurso").addEventListener("submit", agregarRecurso);

$("#pecs-anterior").addEventListener("click", () => {
  if (pasoActual > 0) {
    pasoActual--;
    actualizarPasoPecs();
  }
});

$("#pecs-siguiente").addEventListener("click", () => {
  if (!pecsActual) return;

  if (pasoActual < pecsActual.tarjetas.length - 1) {
    pasoActual++;
    actualizarPasoPecs();
  } else {
    $("#modal-pecs").close();
  }
});

$("#escuchar-paso").addEventListener("click", hablarPaso);

document.querySelectorAll("[data-cerrar]").forEach(boton => {
  boton.addEventListener("click", () => {
    const modal = document.getElementById(boton.dataset.cerrar);

    window.speechSynthesis.cancel();
    modal.close();

    if (boton.dataset.cerrar === "modal-reproductor") {
      $("#contenido-reproductor").innerHTML = "";
    }
  });
});

$("#btn-cerrar-sesion").addEventListener("click", () => {
  if (confirm("¿Deseas cerrar sesión?")) {
    window.location.href = "login.html";
  }
});

renderizarRecursos();