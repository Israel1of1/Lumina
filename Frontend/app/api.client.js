async function apiClient(endpoint, { method = 'GET', body = null } = {}) {
    const headers = { 'Content-Type': 'application/json' };

    const token = AppRouter.getToken();
    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    let response;
    try {
        response = await fetch(`${APP_CONFIG.API_BASE_URL}${endpoint}`, {
            method,
            headers,
            body: body ? JSON.stringify(body) : undefined
        });
    } catch (error) {
        throw new Error(`No se pudo conectar al servidor. Revisa tu conexión.`);
    }
    let data = null;
    try{
        data = await response.json();

    } catch {

    }

    if (!response.ok) {
        throw new Error(data?.message || `Ocurrió un error (${response.status}).`);
    }

    return data;
}