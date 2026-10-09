# 🚀 Guía de Despliegue y Actualización Continua — LUMINA

Esta guía describe cómo está estructurado y desplegado el proyecto **LUMINA** en la máquina virtual utilizando **Docker Compose**, así como el procedimiento para aplicar actualizaciones continuas tras cada `git push`.

---

## 🏗️ Arquitectura de Contenedores

El proyecto está orquestado con Docker Compose dividiéndose en 3 servicios independientes conectados mediante una red interna tipo bridge (`lumina-net`):

```
                       [ Internet / Usuario ]
                                 |
         +-----------------------+-----------------------+
         | Puerto 8080                                   | Puerto 5005
         v                                               v
+-----------------+                             +-----------------+
| lumina-frontend |                             | lumina-backend  |
| (Nginx Alpine)  |                             | (.NET 8 WebAPI) |
+-----------------+                             +-----------------+
         |                                               |
         | (Proxy /api/ opcional)                        | Puerto 1433 (interno)
         +-----------------------------------------------+
                                 |
                                 v
                        +-----------------+
                        |    lumina-db    |
                        | (SQL Server)    |
                        | Vol: db_data    |
                        +-----------------+
```

### 1. Base de Datos (`lumina-db`)
- **Imagen Base:** `mcr.microsoft.com/mssql/server:2022-latest`
- **Puerto:** `1433` (mapeado para inspección externa)
- **Persistencia:** Volumen Docker `lumina_db_data` montado en `/var/opt/mssql`.
- **Inicialización:** Script `Database/entrypoint.sh` que aguarda el encendido del motor e importa automáticamente `Database/init.sql` (esquema, tablas y datos iniciales en codificación UTF-8).
- **Healthcheck:** Consulta cada 10s `SELECT 1` para confirmar estado listo antes de arrancar la API.

### 2. Backend (`lumina-backend`)
- **Tecnología:** .NET 8 (C# Web API).
- **Puerto:** `5005`.
- **Dockerfile:** Multi-etapa (etapa de compilación con `dotnet/sdk:8.0` y runtime ligero con `dotnet/aspnet:8.0`).
- **Swagger UI:** Accesible en `/swagger/index.html`.
- **CORS:** Habilitado para permitir solicitudes desde el frontend y diferentes orígenes.
- **Conexión a BD:** Inyectada mediante variable de entorno `ConnectionStrings__DefaultConnection` apuntando a `database,1433`.

### 3. Frontend (`lumina-frontend`)
- **Tecnología:** HTML / Vanilla JavaScript / CSS.
- **Servidor Web:** Nginx (Alpine Linux).
- **Puerto:** `8080`.
- **Ruta de inicio:** Redirige por defecto a `src/login.html`.
- **Configuración Nginx:** `Frontend/nginx.conf` con soporte para enrutamiento SPA y proxy pass hacia la API.

---

## 🛡️ Configuración de Red y Firewall

### Reglas de Seguridad en la Nube (Security Groups / Firewall de la VM)
Para permitir el acceso desde el navegador a la aplicación, se deben configurar las siguientes reglas de entrada (**Inbound Rules**):

| Protocolo | Puerto | Origen (Source) | Finalidad |
|---|---|---|---|
| **TCP** | `8080` | `0.0.0.0/0` | Acceso a la interfaz web (Frontend) |
| **TCP** | `5005` | `0.0.0.0/0` | Acceso a la API y Swagger UI |
| **TCP** | `22` | Tu IP (Recomendado) | Acceso SSH a la máquina virtual |
| **TCP** | `1433` | Tu IP *(Opcional)* | Conexión directa a SQL Server Management Studio |

### Firewall Local del Servidor (`ufw`)
En la máquina virtual se habilitaron los puertos:
```bash
sudo ufw allow 8080/tcp
sudo ufw allow 5005/tcp
sudo ufw status
```

---

## 🔄 Flujo de Despliegue y Actualizaciones

Para que cualquier cambio subido al repositorio GitHub (`git push`) se refleje en producción de forma limpia y rápida, se diseñó el script automatizado `update-and-run.sh`.

### ¿Qué hace el script `update-and-run.sh`?
1. Ejecuta `git pull origin main` para descargar los últimos cambios de código.
2. Ejecuta `docker compose up -d --build` para recompilar las imágenes que tuvieron cambios (si se tocó Frontend o Backend se reconstruyen; si no, reutiliza la caché).
3. Mantiene intactos los datos de la base de datos gracias al volumen Docker persistente.
4. Muestra el estado final con `docker compose ps`.

### Comando para actualizar en el servidor:
Cada vez que subas cambios a GitHub, solo conéctate al servidor por SSH y corre:
```bash
cd /root/Lumina
./update-and-run.sh
```

---

## 📋 Comandos Útiles de Operación

### Ver el estado de los contenedores
```bash
docker compose ps
```

### Ver logs en tiempo real
- **Ver todo:** `docker compose logs -f`
- **Solo Backend:** `docker compose logs -f backend`
- **Solo Base de Datos:** `docker compose logs -f database`
- **Solo Frontend:** `docker compose logs -f frontend`

### Reiniciar un servicio puntual
```bash
docker compose restart backend
```

### Detener los contenedores
```bash
docker compose down
```

### Detener y reiniciar limpiando datos de BD *(Cuidado: borra volumen)*
```bash
docker compose down -v
docker compose up -d
```

---

## 🌐 URLs de Acceso Rápido

- **Frontend (Web):** `http://<IP_PUBLICA_SERVIDOR>:8080/`
- **Swagger UI (Documentación API):** `http://<IP_PUBLICA_SERVIDOR>:5005/swagger/`
- **Endpoint API base:** `http://<IP_PUBLICA_SERVIDOR>:5005/api/`
