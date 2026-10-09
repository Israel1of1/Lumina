FROM nginx:alpine

# Copiar configuración personalizada
COPY Frontend/nginx.conf /etc/nginx/conf.d/default.conf

# Copiar archivos estáticos del frontend
COPY Frontend/ /usr/share/nginx/html/

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
