# 📦 MiniERP

**MiniERP** es una aplicación web de gestión empresarial ligera desarrollada con **ASP.NET Core 9 MVC** y **Entity Framework Core**.

Incluye un sistema completo de autenticación y autorización con roles, gestión de usuarios y CRUD de las entidades básicas de un ERP. El proyecto está diseñado para ser **didáctico y práctico**, con un enfoque en buenas prácticas, escalabilidad y despliegue mediante Docker.

---

## 🚀 Características principales

- **Autenticación y autorización** con ASP.NET Core Identity y roles:
  - 👑 **Admin** → control total (usuarios, roles, etc.)
  - 🧑‍💼 **RRHH** → gestión de empleados y departamentos
  - 🏭 **Operario** → gestión de productos y categorías
- **CRUD completo** para: Empleados, Departamentos, Categorías y Productos
- **Gestión de usuarios**: crear, editar, eliminar, asignar roles y resetear contraseña
- **Exportaciones client-side** integradas en todas las tablas vía DataTables: Excel (.xlsx), CSV, PDF e impresión directa
- **Base de datos**: SQLite (por defecto, lista para usar sin configuración adicional)
- **Interfaz moderna**: Bootstrap 5 + Bootstrap Icons, tablas responsivas con DataTables, filtros y búsqueda en tiempo real
- **Dockerfile multietapa** para despliegue ligero en cualquier entorno
- **Tests** unitarios e integración incluidos

---

## 🛠️ Tecnologías utilizadas

| Categoría | Tecnología | Versión |
|-----------|-----------|---------|
| Framework | ASP.NET Core MVC | 9.0 |
| ORM | Entity Framework Core | 9.0.9 |
| Base de datos | SQLite | 9.0.9 |
| Autenticación | ASP.NET Core Identity | 9.0.9 |
| Frontend | Bootstrap 5 + Bootstrap Icons | — |
| Tablas / Exportación | DataTables (JS) | — |
| Contenedores | Docker | — |
| Tests | xUnit (unitarios e integración) | — |

---

## 📸 Capturas de pantalla

<p align="center">
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/2aa6a55f-770e-4d6d-91c6-50cdd294a864" width="700"/>
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/91309a47-b480-4f75-a36d-515c52fe1743" width="700"/>
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/68a2cf55-1cad-4ac6-9f3d-521f83183f04" width="700"/>
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/73e7175f-6c29-4720-b670-dffd8c32aca8" width="700"/>
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/48900b08-668e-4fe9-8d86-eb055e4a923a" width="700"/>
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/a62ddd95-4df2-4098-bf0b-5a876d5d8ca1" width="700"/>
  <img src="https://github.com/SergioGO83/MiniERP/assets/120395542/1e5fff7f-24dc-4f3a-9c00-2dc4ceef65e0" width="700"/>
</p>

---

## ⚙️ Instalación y configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/SergioGO83/MiniERP.git
cd MiniERP
```

### 2. Base de datos

El proyecto usa **SQLite** por defecto — no requiere instalar ningún servidor de base de datos. La cadena de conexión en `appsettings.json` es:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=MiniERP.db"
}
```

### 3. Aplicar migraciones y datos iniciales

```bash
dotnet ef database update
```

El seeding inicial crea los siguientes usuarios:

| Rol | Email | Contraseña |
|-----|-------|-----------|
| Admin | admin@erp.com | Admin123! |
| RRHH | rrhh@erp.com | Rrhh123! |
| Operario | operario@erp.com | Operario123! |

### 4. Ejecutar la aplicación

```bash
dotnet run
```

La app estará disponible en:
- https://localhost:7255
- http://localhost:5256

---

## 🐳 Ejecución con Docker

### 1. Construir la imagen

```bash
docker build -t minierp .
```

### 2. Ejecutar el contenedor

```bash
docker run -d -p 8080:80 --name minierp_app minierp
```

Acceder en: http://localhost:8080

---

## 📁 Estructura del proyecto

```
MiniERP/
├── Controllers/             → Lógica de controladores MVC
├── Data/                    → DbContext y Seed inicial
├── Models/                  → Entidades del dominio
├── ViewModels/              → Modelos para vistas específicas
├── Views/                   → Vistas Razor (MVC)
├── wwwroot/                 → Archivos estáticos (css, js, imágenes)
├── Dockerfile               → Imagen de despliegue multietapa
└── appsettings.json         → Configuración (cadena de conexión, etc.)

MiniERP.Tests.Unit/          → Tests unitarios
MiniERP.Tests.Integration/   → Tests de integración
```

---

## 🗺️ Roadmap (mejoras futuras)

- [x] Tests unitarios e integración
- [x] Dockerfile para despliegue con Docker
- [ ] Soporte para SQL Server y PostgreSQL
- [ ] CI/CD con GitHub Actions
- [ ] Dashboard con gráficos de métricas
- [ ] Multi-idioma (i18n)
- [ ] API REST pública para integraciones externas

---

## 👨‍💻 Autor

**Sergio Gay Ortiz**
- 💼 [LinkedIn](https://www.linkedin.com/in/sergio-gay-ortiz/)
- 🐙 [GitHub](https://github.com/SergioGO83)

---

## 📜 Licencia

MIT License © 2025
