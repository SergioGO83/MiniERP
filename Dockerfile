# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solo el csproj y restaurar dependencias
COPY MiniERP/*.csproj ./MiniERP/
RUN dotnet restore MiniERP/MiniERP.csproj

# Copiar todo el código
COPY . .

# Publicar
WORKDIR /src/MiniERP
RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080

# Variable de entorno
ENV ASPNETCORE_URLS=http://+:8080

# Ejecutar
ENTRYPOINT ["dotnet", "MiniERP.dll"]