const AuthService = {
    async login(email, password){
        const respuesta = await apiClient('/auth/login', {
            method: 'POST',
            body: { email, password }
        });

        const { userId, email: correoUsuario, roles, token } = respuesta.data;

        return {
            token,
            user: {id: userId, email: correoUsuario, roles}
        };
    },
        
        logout() {
            localStorage.removeItem(APP_CONFIG.STORAGE_KEYS.TOKEN);
            localStorage.removeItem(APP_CONFIG.STORAGE_KEYS.USER);
            window.location.href = APP_CONFIG.ROUTES.LOGIN;
        }
};





// Reordena cualquier lista para que lo más reciente quede primero.
// La usan todos los *Service.getAll() de este archivo (Subject,
// y después Teacher, Student, etc.).

function ordenarPorFechaDesc(lista) {
  return [...lista].sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));
}


// SubjectService — conecta la pantalla de Materias con el Backend.
// materias.js llama a estas funciones sin saber que antes
// usaban localStorage.

const SubjectService = {

  async getAll() {
    const respuesta = await apiClient('/subjects?pageNumber=1&pageSize=1000');
    return ordenarPorFechaDesc(respuesta.data);
  },

  async getById(id) {
    return await apiClient(`/subjects/${id}`);
  },

  async create(datos) {
    return await apiClient('/subjects', {
      method: 'POST',
      body: datos
    });
  },

  async update(id, datos) {
    return await apiClient(`/subjects/${id}`, {
      method: 'PUT',
      body: datos
    });
  }

};