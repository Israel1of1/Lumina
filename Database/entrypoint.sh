#!/bin/bash
# Iniciar SQL Server en segundo plano
/opt/mssql/bin/sqlservr &
pid=$!

echo "Esperando que SQL Server inicie..."
for i in {1..60}; do
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]; then
        echo "SQL Server listo. Ejecutando scripts de inicialización..."
        break
    fi
    sleep 2
done

# Ejecutar el script init.sql si no existe la base de datos o para configurarla
if [ -f /docker-entrypoint-initdb.d/init.sql ]; then
    echo "Aplicando /docker-entrypoint-initdb.d/init.sql..."
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -i /docker-entrypoint-initdb.d/init.sql
    echo "Inicialización completada."
fi

# Mantener vivo el proceso principal
wait $pid
