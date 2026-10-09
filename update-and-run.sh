#!/bin/bash
set -e

echo "=== Actualizando repositorio Lumina ==="
git pull origin main || git pull

echo "=== Reconstruyendo y levantando contenedores Docker ==="
docker compose up -d --build

echo "=== Estado de los servicios ==="
docker compose ps
