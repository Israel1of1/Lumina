FROM mcr.microsoft.com/mssql/server:2022-latest

USER root
RUN mkdir -p /docker-entrypoint-initdb.d

COPY Database/init.sql /docker-entrypoint-initdb.d/init.sql
COPY Database/entrypoint.sh /usr/local/bin/entrypoint.sh
RUN chmod +x /usr/local/bin/entrypoint.sh

USER mssql
ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]
