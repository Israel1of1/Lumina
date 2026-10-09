FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos csproj para optimizar restore de capas
COPY Backend/API/API.csproj Backend/API/
COPY Backend/Business/Business.csproj Backend/Business/
COPY Backend/Core/Core.csproj Backend/Core/
COPY Backend/DataAcess/DataAccess.csproj Backend/DataAcess/
COPY Backend/Backend.sln Backend/

RUN dotnet restore Backend/Backend.sln

# Copiar el resto del código y compilar
COPY Backend/ Backend/
WORKDIR /src/Backend/API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5005
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5005
ENTRYPOINT ["dotnet", "API.dll"]
