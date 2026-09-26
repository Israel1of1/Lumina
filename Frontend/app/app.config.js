// ============================================================
// Configuración global de la aplicación LUMINA — Panel de Administración
// ============================================================

const APP_CONFIG = {
  // Ajusta esto cuando el backend esté listo (ej. 'https://localhost:7050/api')
  API_BASE_URL: 'http://localhost:5000/api',

  // Claves de almacenamiento de sesión
  STORAGE_KEYS: {
    TOKEN: 'lumina_token',
    USER: 'lumina_user'
  },

  // Rutas de las pantallas (relativas a /src/)
  ROUTES: {
    WELCOME: 'inicio.html',
    LOGIN: 'login.html',
    DASHBOARD: 'dashboard.html',
    TUTOR_DASHBOARD: 'tutor-dashboard.html',
    TEACHERS: 'docentes.html',
    STUDENTS: 'estudiantes.html',
    GROUPS: 'grupos.html',
    SUBJECTS: 'materias.html',
    GROUP_SUBJECTS: 'asignaciones.html',
    LINK_CODES: 'codigos.html'
    
  },

  // Reglas de negocio (RN-01 del documento de contexto)
  MAX_STUDENTS_PER_GROUP: 10,

  // Dominios permitidos para los CHECK del backend (evita mandar valores inválidos)
  ENUMS: {
    TEACHER_STATUS: ['ACTIVE', 'ON_LEAVE', 'INACTIVE'],
    LINK_CODE_PURPOSE: ['ENROLLMENT', 'TEACHER_CONTRACT'],
    LINK_CODE_STATUS: ['PENDING', 'USED', 'EXPIRED', 'REVOKED']
  }
};