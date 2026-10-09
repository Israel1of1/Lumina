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


function ordenarPorIdDesc(lista) {
  return [...lista].sort((a, b) => b.id - a.id);
}

// ClassGroupService — conecta la pantalla de Grupos con el Backend.


const ClassGroupService = {
  async getAll() {
    const respuesta = await apiClient('/class-groups?pageNumber=1&pageSize=1000');
    return ordenarPorIdDesc(respuesta.data.items);
  },
  async getById(id) {
    const respuesta = await apiClient(`/class-groups/${id}`);
    return respuesta.data;
  },
  async create(datos) {
    const respuesta = await apiClient('/class-groups', { method: 'POST', body: datos });
    return respuesta.data;
  },
  async update(id, datos) {
    const respuesta = await apiClient(`/class-groups/${id}`, { method: 'PUT', body: datos });
    return respuesta.data;
  },
  async setActive(id, activar) {
    const respuesta = await apiClient(`/class-groups/${id}/active`, {
      method: 'PATCH',
      body: { isActive: activar }
    });
    return respuesta.data;
  }
};




//TEACHER SERVICE conecta la pantalla de Docentes con el Backend.
const TeacherService = {

  async create(datos) {
    const respuesta = await apiClient('/teachers', { method: 'POST', body: datos });
    return respuesta.data;
  },
  async getAll() {
    const respuesta = await apiClient('/teachers?pageNumber=1&pageSize=1000');
    return ordenarPorIdDesc(respuesta.data.items);
  },
  async deactivate(id, motivo) {
    const respuesta = await apiClient(`/teachers/${id}/deactivate`, {
      method: 'PATCH',
      body: { reason: motivo }
    });
    return respuesta.data;
  },
  async getMyProfile() {
    const respuesta = await apiClient('/teachers/me');
    return respuesta.data;
  },
  
};



// STUDENT SERVICE conecta la pantalla de Estudiantes con el Backend.

const StudentService = {

  async getAll() {
    const grupos = await ClassGroupService.getAll();
    const porGrupo = await Promise.all(
      grupos.map(g =>
        apiClient(`/students/by-group/${g.id}?pageNumber=1&pageSize=1000&onlyActive=false`)
      )
    );
    const todos = porGrupo.flatMap(respuesta => respuesta.data.items);
    return ordenarPorIdDesc(todos);
  },

  async getById(id) {
    const respuesta = await apiClient(`/students/${id}`);
    return respuesta.data;
  },

  async create(datos) {
    const respuesta = await apiClient('/students', { method: 'POST', body: datos });
    return respuesta.data;
  },

  async update(id, datos) {
    const respuesta = await apiClient(`/students/${id}`, { method: 'PUT', body: datos });
    return respuesta.data;
  },

  async setActive(id, activar) {
    const respuesta = await apiClient(`/students/${id}/active`, {
      method: 'PATCH',
      body: { isActive: activar }
    });
    return respuesta.data;
  },
  
  async getByGroup(groupId) {
    const respuesta = await apiClient(`/students/by-group/${groupId}?pageNumber=1&pageSize=1000&onlyActive=false`);
    return respuesta.data.items;
  }

};



// GROUP SUBJECT SERVICE conecta la pantalla de Grupos con el Backend.

const GroupSubjectService = {

  async getAll() {
    const grupos = await ClassGroupService.getAll();
    const porGrupo = await Promise.all(
      grupos.map(g => apiClient(`/group-subjects/by-group/${g.id}`))
    );
    return porGrupo.flatMap(respuesta => respuesta.data);
  },

  async create(datos) {
    const respuesta = await apiClient('/group-subjects', { method: 'POST', body: datos });
    return respuesta.data;
  },

  async end(id) {
    const respuesta = await apiClient(`/group-subjects/${id}/active`, {
      method: 'PATCH',
      body: { isActive: false }
    });
    return respuesta.data;
  },

  async getByTeacher(teacherId) {
    const respuesta = await apiClient(`/group-subjects/by-teacher/${teacherId}`);
    return respuesta.data;
  }
};

// LESSON SERVICE conecta la pantalla de Planes de Clase con el Backend.

const LessonService = {

  async getAll() {
    const respuesta = await apiClient('/lessons?pageNumber=1&pageSize=1000');
    return respuesta.data;
  },

  async create(datos) {
    return await apiClient('/lessons', { method: 'POST', body: datos });
  },

  async update(id, datos) {
    return await apiClient(`/lessons/${id}`, { method: 'PUT', body: datos });
  }

};



// GUARDIAN SERVICE — conecta el perfil del Guardian con el Backend.

const GuardianService = {

  async getMyProfile() {
    const respuesta = await apiClient('/guardians/me');
    return respuesta.data;
  },

  async updateMyProfile(datos) {
    const respuesta = await apiClient('/guardians/me', {
      method: 'PUT',
      body: datos
    });

    return respuesta.data;
  },

  async getMyWards() {
      const respuesta = await apiClient('/student-relations/my-wards');
      return respuesta.data;
  }

};


const PecsBoardService = {
  async getByStudent(studentId) {
    const respuesta = await apiClient(`/pecs-boards/by-student/${studentId}`);
    return respuesta.data;
  },

  async create(datos) {
    const respuesta = await apiClient('/pecs-boards', { method: 'POST', body: datos });
    return respuesta.data;
  }
};

const PecsCardService = {
  async getByBoard(boardId) {
    const respuesta = await apiClient(`/pecs-cards/by-board/${boardId}`);
    return respuesta.data;
  },
  async create(datos) {
    const respuesta = await apiClient('/pecs-cards', { method: 'POST', body: datos });
    return respuesta.data;
  }
};

