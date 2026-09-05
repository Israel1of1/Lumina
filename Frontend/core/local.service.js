const DB_KEY = 'lumina_local_db';

// ===== Utilidades internas =====
function _leerDB() {
  const raw = localStorage.getItem(DB_KEY);
  return raw ? JSON.parse(raw) : null;
}

function _guardarDB(db) {
  localStorage.setItem(DB_KEY, JSON.stringify(db));
}

function _nuevoId(coleccion) {
  return coleccion.length > 0 ? Math.max(...coleccion.map(x => x.id)) + 1 : 1;
}

function _hoy(offsetDias = 0) {
  const fecha = new Date();
  fecha.setDate(fecha.getDate() + offsetDias);
  return fecha.toISOString();
}

function _delay(valor, ms = 150) {
  return new Promise(resolve => setTimeout(() => resolve(valor), ms));
}

// ===== Semilla de datos (se genera solo la primera vez) =====
function _generarSemilla() {
  const roles = [
    { id: 1, name: 'INSTITUTION', description: 'Institución', isActive: true, createdAt: _hoy() },
    { id: 2, name: 'TEACHER', description: 'Docente', isActive: true, createdAt: _hoy() },
    { id: 3, name: 'GUARDIAN', description: 'Tutor', isActive: true, createdAt: _hoy() }
  ];

  const users = [
    { id: 1, email: 'institucion@lumina.com', passwordHash: 'Admin123!', nationalId: null, isActive: true, lastLoginAt: null, createdAt: _hoy() }
  ];

  const userRoles = [{ id: 1, userId: 1, roleId: 1, assignedAt: _hoy() }];

  // ===== Materias (10) =====
  const nombresMaterias = [
    'Comunicación funcional', 'Habilidades sociales', 'Matemática básica',
    'Lecto-escritura', 'Motricidad fina', 'Motricidad gruesa',
    'Autorregulación emocional', 'Vida práctica', 'Arte y expresión', 'Música y ritmo'
  ];
  const coloresMaterias = ['#00347a', '#725b21', '#40361d', '#2e7d32', '#8e24aa', '#d84315', '#0277bd', '#6d4c41', '#c2185b', '#00838f'];
  const iconosMaterias = ['🗣️', '🤝', '🔢', '📖', '✋', '🏃', '🧘', '🧺', '🎨', '🎵'];

  const subjects = nombresMaterias.map((nombre, i) => ({
    id: i + 1,
    name: nombre,
    description: `Materia enfocada en el desarrollo de ${nombre.toLowerCase()}.`,
    color: coloresMaterias[i],
    icon: iconosMaterias[i],
    createdAt: _hoy(-30 + i)
  }));

  // ===== Grupos (10) =====
  const nombresGrupos = [
    'Grupo A - Mañana', 'Grupo B - Mañana', 'Grupo C - Mañana',
    'Grupo D - Tarde', 'Grupo E - Tarde', 'Grupo F - Tarde',
    'Grupo G - Preescolar', 'Grupo H - Preescolar', 'Grupo I - Inicial', 'Grupo J - Inicial'
  ];
  const niveles = ['Preescolar', 'Preescolar', '1er grado', '1er grado', '2do grado', '2do grado', 'Preescolar', 'Preescolar', 'Inicial', 'Inicial'];

  const classGroups = nombresGrupos.map((nombre, i) => ({
    id: i + 1,
    name: nombre,
    gradeLevel: niveles[i],
    description: `Grupo de apoyo con enfoque individualizado — ${nombre}.`,
    isActive: true,
    createdAt: _hoy(-60 + i)
  }));

  // ===== Docentes (10) =====
  const nombresDocentes = [
    ['Ana', 'Martínez'], ['Carlos', 'López'], ['María', 'Gómez'], ['José', 'Hernández'],
    ['Laura', 'Pérez'], ['Miguel', 'Sánchez'], ['Sofía', 'Ramírez'], ['Diego', 'Torres'],
    ['Valentina', 'Flores'], ['Andrés', 'Castro']
  ];
  const especialidades = [
    'Educación especial', 'Terapia del lenguaje', 'Psicopedagogía', 'Educación inicial',
    'Terapia ocupacional', 'Educación especial', 'Psicología educativa', 'Educación inicial',
    'Terapia del lenguaje', 'Psicopedagogía'
  ];

  const teachers = nombresDocentes.map(([nombre, apellido], i) => ({
    id: i + 1,
    userId: null,
    firstName: nombre,
    lastName: apellido,
    nationalId: `001-${(100000 + i).toString()}-000${i}A`,
    personalEmail: `${nombre.toLowerCase()}.${apellido.toLowerCase()}@correo.com`,
    phone: `8888-${1000 + i}`,
    address: `Colonia Central, casa #${i + 1}`,
    city: 'Managua',
    photo: null,
    specialty: especialidades[i],
    degree: 'Licenciatura',
    entityStatus: i === 9 ? 'ON_LEAVE' : 'ACTIVE',
    dismissalDate: null,
    dismissalReason: null,
    createdAt: _hoy(-90 + i)
  }));

  // ===== Estudiantes (10) — repartidos entre los primeros 5 grupos =====
  const nombresEstudiantes = [
    ['Mateo', 'Rivas'], ['Emma', 'Ortega'], ['Liam', 'Morales'], ['Isabella', 'Cruz'],
    ['Noah', 'Reyes'], ['Sofía', 'Vega'], ['Lucas', 'Aguilar'], ['Mía', 'Navarro'],
    ['Daniel', 'Ríos'], ['Camila', 'Guzmán']
  ];
  const nivelesLenguaje = ['No verbal', 'Verbal limitado', 'Verbal funcional', 'No verbal', 'Verbal limitado'];

  const students = nombresEstudiantes.map(([nombre, apellido], i) => ({
    id: i + 1,
    groupId: (i % 5) + 1,
    userId: null,
    firstName: nombre,
    lastName: apellido,
    uniqueNumber: `LUM-2026-${(i + 1).toString().padStart(3, '0')}`,
    birthDate: `20${18 + (i % 4)}-0${(i % 9) + 1}-15`,
    gender: i % 2 === 0 ? 'M' : 'F',
    languageLevel: nivelesLenguaje[i % nivelesLenguaje.length],
    clinicalInfo: 'Diagnóstico dentro del espectro autista, seguimiento regular.',
    observations: 'Responde bien a refuerzos visuales.',
    isActive: true,
    createdAt: _hoy(-45 + i)
  }));

  // ===== Asignaciones (10) =====
  const groupSubjects = Array.from({ length: 10 }, (_, i) => ({
    id: i + 1,
    groupId: (i % 5) + 1,
    subjectId: i + 1,
    teacherId: (i % 9) + 1,
    isActive: true,
    assignmentDate: _hoy(-30 + i).substring(0, 10),
    endDate: null,
    createdAt: _hoy(-30 + i)
  }));

  // ===== Códigos de vinculación (10) — mezcla de propósitos y estados =====
  const propositos = ['ENROLLMENT', 'TEACHER_CONTRACT'];
  const estados = ['PENDING', 'PENDING', 'PENDING', 'USED', 'USED', 'EXPIRED', 'REVOKED', 'PENDING', 'USED', 'PENDING'];

  const linkCodes = Array.from({ length: 10 }, (_, i) => {
    const proposito = propositos[i % 2];
    const estado = estados[i];
    const prefijo = proposito === 'ENROLLMENT' ? 'ENR' : 'DOC';

    return {
      id: i + 1,
      code: `LUM-${prefijo}-2026-${(i + 1).toString().padStart(4, '0')}`,
      purpose: proposito,
      status: estado,
      issuedById: 1,
      expiresAt: estado === 'EXPIRED' ? _hoy(-5) : _hoy(30),
      usedById: estado === 'USED' ? 1 : null,
      usedAt: estado === 'USED' ? _hoy(-2) : null,
      createdAt: _hoy(-20 + i),
      updatedAt: null
    };
  });

  return { roles, users, userRoles, subjects, classGroups, teachers, students, groupSubjects, linkCodes };
}

function _inicializarDB() {
  let db = _leerDB();
  if (!db) {
    db = _generarSemilla();
    _guardarDB(db);
  }
  return db;
}

_inicializarDB();

// ============================================================
// AuthService
// ============================================================
const AuthService = {
  async login(email, password) {
    const db = _leerDB();
    const usuario = db.users.find(u => u.email === email);

    if (!usuario || usuario.passwordHash !== password) {
      throw new Error('Correo o contraseña incorrectos');
    }

    const rolesDelUsuario = db.userRoles
      .filter(ur => ur.userId === usuario.id)
      .map(ur => db.roles.find(r => r.id === ur.roleId)?.name)
      .filter(Boolean);

    usuario.lastLoginAt = _hoy();
    _guardarDB(db);

    const tokenFalso = `${btoa(JSON.stringify({ alg: 'none' }))}.${btoa(JSON.stringify({ sub: usuario.id, exp: Math.floor(Date.now() / 1000) + 3600 * 8 }))}.firma`;

    return _delay({
      token: tokenFalso,
      user: { id: usuario.id, email: usuario.email, roles: rolesDelUsuario }
    });
  },

  logout() {
    sessionStorage.clear();
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
  }
};

// ============================================================
// TeacherService
// ============================================================
const TeacherService = {
  getAll: () => _delay([..._leerDB().teachers]),
  getById: (id) => _delay(_leerDB().teachers.find(t => t.id === parseInt(id))),
  create(datos) {
    const db = _leerDB();
    const nuevo = { id: _nuevoId(db.teachers), userId: null, entityStatus: 'ACTIVE', dismissalDate: null, dismissalReason: null, createdAt: _hoy(), updatedAt: null, ...datos };
    db.teachers.push(nuevo);
    _guardarDB(db);
    return _delay(nuevo);
  },
  update(id, datos) {
    const db = _leerDB();
    const idx = db.teachers.findIndex(t => t.id === parseInt(id));
    if (idx === -1) throw new Error('Docente no encontrado');
    db.teachers[idx] = { ...db.teachers[idx], ...datos, updatedAt: _hoy() };
    _guardarDB(db);
    return _delay(db.teachers[idx]);
  },
  setStatus(id, entityStatus, dismissalReason = null) {
    const db = _leerDB();
    const idx = db.teachers.findIndex(t => t.id === parseInt(id));
    if (idx === -1) throw new Error('Docente no encontrado');
    db.teachers[idx].entityStatus = entityStatus;
    db.teachers[idx].dismissalReason = dismissalReason;
    db.teachers[idx].dismissalDate = entityStatus === 'INACTIVE' ? _hoy().substring(0, 10) : null;
    db.teachers[idx].updatedAt = _hoy();
    _guardarDB(db);
    return _delay(db.teachers[idx]);
  }
};

// ============================================================
// StudentService
// ============================================================
const StudentService = {
  getAll: () => _delay([..._leerDB().students]),
  getById: (id) => _delay(_leerDB().students.find(s => s.id === parseInt(id))),
  getByGroup: (groupId) => _delay(_leerDB().students.filter(s => s.groupId === parseInt(groupId))),
  create(datos) {
    const db = _leerDB();
    const nuevo = { id: _nuevoId(db.students), userId: null, isActive: true, createdAt: _hoy(), updatedAt: null, ...datos };
    db.students.push(nuevo);
    _guardarDB(db);
    return _delay(nuevo);
  },
  update(id, datos) {
    const db = _leerDB();
    const idx = db.students.findIndex(s => s.id === parseInt(id));
    if (idx === -1) throw new Error('Estudiante no encontrado');
    db.students[idx] = { ...db.students[idx], ...datos, updatedAt: _hoy() };
    _guardarDB(db);
    return _delay(db.students[idx]);
  },
  setActive(id, isActive) {
    const db = _leerDB();
    const idx = db.students.findIndex(s => s.id === parseInt(id));
    if (idx === -1) throw new Error('Estudiante no encontrado');
    db.students[idx].isActive = isActive;
    db.students[idx].updatedAt = _hoy();
    _guardarDB(db);
    return _delay(db.students[idx]);
  }
};

// ============================================================
// ClassGroupService
// ============================================================
const ClassGroupService = {
  getAll: () => _delay([..._leerDB().classGroups]),
  getById: (id) => _delay(_leerDB().classGroups.find(g => g.id === parseInt(id))),
  create(datos) {
    const db = _leerDB();
    const nuevo = { id: _nuevoId(db.classGroups), isActive: true, createdAt: _hoy(), ...datos };
    db.classGroups.push(nuevo);
    _guardarDB(db);
    return _delay(nuevo);
  },
  update(id, datos) {
    const db = _leerDB();
    const idx = db.classGroups.findIndex(g => g.id === parseInt(id));
    if (idx === -1) throw new Error('Grupo no encontrado');
    db.classGroups[idx] = { ...db.classGroups[idx], ...datos };
    _guardarDB(db);
    return _delay(db.classGroups[idx]);
  },
  setActive(id, isActive) {
    const db = _leerDB();
    const idx = db.classGroups.findIndex(g => g.id === parseInt(id));
    if (idx === -1) throw new Error('Grupo no encontrado');
    db.classGroups[idx].isActive = isActive;
    _guardarDB(db);
    return _delay(db.classGroups[idx]);
  }
};

// ============================================================
// SubjectService
// ============================================================
const SubjectService = {
  getAll: () => _delay([..._leerDB().subjects]),
  getById: (id) => _delay(_leerDB().subjects.find(s => s.id === parseInt(id))),
  create(datos) {
    const db = _leerDB();
    const nuevo = { id: _nuevoId(db.subjects), createdAt: _hoy(), ...datos };
    db.subjects.push(nuevo);
    _guardarDB(db);
    return _delay(nuevo);
  },
  update(id, datos) {
    const db = _leerDB();
    const idx = db.subjects.findIndex(s => s.id === parseInt(id));
    if (idx === -1) throw new Error('Materia no encontrada');
    db.subjects[idx] = { ...db.subjects[idx], ...datos };
    _guardarDB(db);
    return _delay(db.subjects[idx]);
  }
};

// ============================================================
// GroupSubjectService
// ============================================================
const GroupSubjectService = {
  getAll: () => _delay([..._leerDB().groupSubjects]),
  getByGroup: (groupId) => _delay(_leerDB().groupSubjects.filter(gs => gs.groupId === parseInt(groupId))),
  create(datos) {
    const db = _leerDB();
    const nuevo = { id: _nuevoId(db.groupSubjects), isActive: true, endDate: null, createdAt: _hoy(), ...datos };
    db.groupSubjects.push(nuevo);
    _guardarDB(db);
    return _delay(nuevo);
  },
  end(id, endDate) {
    const db = _leerDB();
    const idx = db.groupSubjects.findIndex(gs => gs.id === parseInt(id));
    if (idx === -1) throw new Error('Asignación no encontrada');
    db.groupSubjects[idx].isActive = false;
    db.groupSubjects[idx].endDate = endDate;
    _guardarDB(db);
    return _delay(db.groupSubjects[idx]);
  }
};

// ============================================================
// LinkCodeService
// ============================================================
const LinkCodeService = {
  getAll: () => _delay([..._leerDB().linkCodes]),
  getByPurpose: (purpose) => _delay(_leerDB().linkCodes.filter(c => c.purpose === purpose)),
  generate(purpose, expiresAt = null) {
    const db = _leerDB();
    const prefijo = purpose === 'ENROLLMENT' ? 'ENR' : 'DOC';
    const consecutivo = (db.linkCodes.length + 1).toString().padStart(4, '0');

    const nuevo = {
      id: _nuevoId(db.linkCodes),
      code: `LUM-${prefijo}-${new Date().getFullYear()}-${consecutivo}`,
      purpose, status: 'PENDING', issuedById: 1, expiresAt,
      usedById: null, usedAt: null, createdAt: _hoy(), updatedAt: null
    };

    db.linkCodes.push(nuevo);
    _guardarDB(db);
    return _delay(nuevo);
  },
  revoke(id) {
    const db = _leerDB();
    const idx = db.linkCodes.findIndex(c => c.id === parseInt(id));
    if (idx === -1) throw new Error('Código no encontrado');
    db.linkCodes[idx].status = 'REVOKED';
    db.linkCodes[idx].updatedAt = _hoy();
    _guardarDB(db);
    return _delay(db.linkCodes[idx]);
  }
};

// ===== Utilidad de consola: reiniciar la base local a la semilla =====
function reiniciarBaseLocal() {
  localStorage.removeItem(DB_KEY);
  _inicializarDB();
  console.log('Base local de LUMINA reiniciada. Recarga la página.');
}