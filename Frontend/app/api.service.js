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