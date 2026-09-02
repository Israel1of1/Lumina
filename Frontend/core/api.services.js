// ============================================================
// Servicios de conexión con la API — LUMINA
// Wrapper genérico + un servicio por entidad. Listo para apuntar
// al backend real en cuanto exista (solo ajusta APP_CONFIG.API_BASE_URL).
// ============================================================

/**
 * Wrapper central para todas las llamadas a la API.
 * Agrega el token automáticamente y normaliza el manejo de errores.
 */
async function apiRequest(endpoint, options = {}) {
  const token = sessionStorage.getItem(APP_CONFIG.STORAGE_KEYS.TOKEN);

  const headers = {
    'Content-Type': 'application/json',
    ...(options.headers || {})
  };

  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  let response;
  try {
    response = await fetch(`${APP_CONFIG.API_BASE_URL}${endpoint}`, {
      ...options,
      headers
    });
  } catch (error) {
    throw new Error('No se pudo conectar con el servidor. Verifica que la API esté corriendo.');
  }

  if (response.status === 401) {
    sessionStorage.clear();
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
    throw new Error('Sesión expirada');
  }

  if (response.status === 204) {
    return null; // Sin contenido (ej. DELETE exitoso)
  }

  let body = null;
  try {
    body = await response.json();
  } catch {
    // Respuesta sin cuerpo JSON
  }

  if (!response.ok) {
    const mensaje = body?.message || body?.title || `Error ${response.status}`;
    const error = new Error(mensaje);
    error.status = response.status;
    error.body = body;
    throw error;
  }

  return body;
}

const httpClient = {
  get: (endpoint) => apiRequest(endpoint, { method: 'GET' }),
  post: (endpoint, data) => apiRequest(endpoint, { method: 'POST', body: JSON.stringify(data) }),
  put: (endpoint, data) => apiRequest(endpoint, { method: 'PUT', body: JSON.stringify(data) }),
  patch: (endpoint, data) => apiRequest(endpoint, { method: 'PATCH', body: data ? JSON.stringify(data) : undefined }),
  delete: (endpoint) => apiRequest(endpoint, { method: 'DELETE' })
};

// ============================================================
// AuthService — inicio de sesión de la Institución
// ============================================================
const AuthService = {
  /**
   * POST /api/Auth/login
   * Body esperado: { email, password }
   * Respuesta esperada: { token, user: { id, email, roles: [...] } }
   */
  login: (email, password) => httpClient.post('/Auth/login', { email, password }),

  logout: () => {
    sessionStorage.clear();
    window.location.href = APP_CONFIG.ROUTES.LOGIN;
  }
};

// ============================================================
// TeacherService — gestión de docentes
// ============================================================
const TeacherService = {
  getAll: () => httpClient.get('/Teacher'),
  getById: (id) => httpClient.get(`/Teacher/${id}`),
  create: (teacher) => httpClient.post('/Teacher', teacher),
  update: (id, teacher) => httpClient.put(`/Teacher/${id}`, teacher),
  // entityStatus: 'ACTIVE' | 'ON_LEAVE' | 'INACTIVE'
  setStatus: (id, entityStatus, dismissalReason = null) =>
    httpClient.patch(`/Teacher/${id}/status`, { entityStatus, dismissalReason })
};

// ============================================================
// StudentService — gestión de estudiantes (registro inicial;
// el perfil completo lo llena el tutor después de vincularse)
// ============================================================
const StudentService = {
  getAll: () => httpClient.get('/Student'),
  getById: (id) => httpClient.get(`/Student/${id}`),
  getByGroup: (groupId) => httpClient.get(`/Student?groupId=${groupId}`),
  create: (student) => httpClient.post('/Student', student),
  update: (id, student) => httpClient.put(`/Student/${id}`, student),
  setActive: (id, isActive) => httpClient.patch(`/Student/${id}/status`, { isActive })
};

// ============================================================
// ClassGroupService — grupos (cuota máxima de 10 estudiantes, RN-01)
// ============================================================
const ClassGroupService = {
  getAll: () => httpClient.get('/ClassGroup'),
  getById: (id) => httpClient.get(`/ClassGroup/${id}`),
  create: (group) => httpClient.post('/ClassGroup', group),
  update: (id, group) => httpClient.put(`/ClassGroup/${id}`, group),
  setActive: (id, isActive) => httpClient.patch(`/ClassGroup/${id}/status`, { isActive })
};

// ============================================================
// SubjectService — catálogo de materias (sin isActive en el modelo)
// ============================================================
const SubjectService = {
  getAll: () => httpClient.get('/Subject'),
  getById: (id) => httpClient.get(`/Subject/${id}`),
  create: (subject) => httpClient.post('/Subject', subject),
  update: (id, subject) => httpClient.put(`/Subject/${id}`, subject)
};

// ============================================================
// GroupSubjectService — asignación de materia + docente a un grupo
// ============================================================
const GroupSubjectService = {
  getAll: () => httpClient.get('/GroupSubject'),
  getByGroup: (groupId) => httpClient.get(`/GroupSubject?groupId=${groupId}`),
  create: (groupSubject) => httpClient.post('/GroupSubject', groupSubject),
  // Finaliza una asignación (equivalente a "baja" del vínculo, con fecha de fin)
  end: (id, endDate) => httpClient.patch(`/GroupSubject/${id}/end`, { endDate })
};

// ============================================================
// LinkCodeService — códigos de vinculación (matrícula / contratación)
// ============================================================
const LinkCodeService = {
  getAll: () => httpClient.get('/LinkCode'),
  getByPurpose: (purpose) => httpClient.get(`/LinkCode?purpose=${purpose}`),
  // El backend genera el código (formato tipo LUM-ENR-2026-0001) y lo devuelve
  generate: (purpose, expiresAt = null) => httpClient.post('/LinkCode', { purpose, expiresAt }),
  revoke: (id) => httpClient.patch(`/LinkCode/${id}/revoke`)
};