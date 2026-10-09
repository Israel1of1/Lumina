document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("registerForm");
  const linkCodeInput = document.getElementById("linkCode");
  const codeMsg = document.getElementById("codeValidationMsg");
  const btnSubmit = document.getElementById("btnSubmit");
  let codigoValido = false;
  // Validación dinámica del código de vinculación al perder foco o tras escribir
  let debounceTimer;
  linkCodeInput.addEventListener("input", () => {
    clearTimeout(debounceTimer);
    const code = linkCodeInput.value.trim();
    if (!code) {
      codeMsg.textContent = "Introduce el código proporcionado por la institución educativa.";
      codeMsg.style.color = "#6b7280";
      codigoValido = false;
      return;
    }
    debounceTimer = setTimeout(async () => {
      try {
        codeMsg.textContent = "Validando código...";
        codeMsg.style.color = "#3b82f6";
        const res = await AuthService.validateLinkCode(code);
        if (res && (res.role || res.isValid !== false)) {
          const rol = res.role || res.targetRole || "usuario";
          codeMsg.textContent = `✓ Código válido para perfil ${rol}.`;
          codeMsg.style.color = "#10b981";
          codigoValido = true;
        } else {
          codeMsg.textContent = "El código de vinculación no es válido o ha expirado.";
          codeMsg.style.color = "#ef4444";
          codigoValido = false;
        }
      } catch (err) {
        // Si el endpoint responde 404 o error
        codeMsg.textContent = err.message || "Código no válido o no encontrado.";
        codeMsg.style.color = "#ef4444";
        codigoValido = false;
      }
    }, 600);
  });
   form.addEventListener("submit", async (e) => {
    e.preventDefault();
    const code = linkCodeInput.value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirmPassword").value;
    if (password !== confirmPassword) {
      alert("Las contraseñas no coinciden. Por favor verifica.");
      return;
    }
    if (password.length < 8) {
      alert("La contraseña debe tener al menos 8 caracteres.");
      return;
    }
    try {
      btnSubmit.disabled = true;
      btnSubmit.textContent = "Creando cuenta...";
      const datosRegistro = {
        code,
        email,
        password,
        confirmPassword
      };
      await AuthService.register(datosRegistro);
      alert("¡Cuenta creada exitosamente! Iniciando sesión...");
      // Autologin
      try {
        const authData = await AuthService.login(email, password);
        localStorage.setItem("lumina_token", authData.token);
        localStorage.setItem("lumina_user", JSON.stringify(authData.user));
        AppRouter.redirigirSegunRol(authData.user.roles);
      } catch {
        window.location.href = "login.html";
      }
    } catch (err) {
      console.error("Error al registrarse:", err);
      alert(`Error al registrar cuenta: ${err.message || "Verifica los datos e intenta nuevamente."}`);
      btnSubmit.disabled = false;
      btnSubmit.textContent = "Crear cuenta →";
    }
  });
});