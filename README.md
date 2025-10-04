# 📦 MiniERP

**MiniERP** es una aplicación de gestión empresarial ligera desarrollada con **ASP.NET Core 9 MVC** y **Entity Framework Core**.  
Incluye un sistema de autenticación y autorización con roles, gestión de usuarios y CRUD completo de entidades básicas de un ERP.

El proyecto está diseñado para ser **didáctico y práctico**, con un enfoque en buenas prácticas, escalabilidad y despliegue mediante Docker.

---

## 🚀 Características principales

- **ASP.NET Core 9 + MVC + Identity** para autenticación y autorización.
- **Roles de usuario**:
  - 👑 Admin → control total (usuarios, roles, etc.)
  - 🧑‍💼 RRHH → gestión de empleados y departamentos.
  - 🏭 Operario → gestión de productos y categorías.
- **CRUD completo** para:
  - Empleados
  - Departamentos
  - Categorías
  - Productos
- **Gestión de usuarios**:
  - Crear, editar, eliminar
  - Asignar roles
  - Resetear contraseña
- **Exportaciones** integradas en todas las tablas:
  - 📊 Excel (.xlsx)
  - 📄 CSV
  - 📕 PDF
  - 🖨️ Impresión directa
- **Base de datos flexible**:
  - Soporte por defecto para **SQLite** (ideal para pruebas rápidas).
  - Compatible con **SQL Server**.
  - Compatible con **PostgreSQL**.
- **Interfaz moderna**:
  - Bootstrap 5 + Bootstrap Icons.
  - Tablas responsivas con DataTables.
  - Filtros, ordenación y búsqueda en tiempo real.
- **Dockerfile multietapa**:
  - Imagen ligera lista para despliegue en cualquier entorno con Docker.

---

## 🛠️ Tecnologías usadas

- **Backend**: ASP.NET Core 9
- **ORM**: Entity Framework Core
- **Base de datos soportadas**:
  - SQLite (default)
  - SQL Server
  - PostgreSQL
- **Frontend**: Bootstrap 5 + DataTables
- **Autenticación**: ASP.NET Core Identity
- **Contenedores**: Docker

---

### Capturas de pantalla
<img width="2554" height="747" alt="image" src="https://github.com/user-attachments/assets/2aa6a55f-770e-4d6d-91c6-50cdd294a864" />
<img width="2557" height="413" alt="image" src="https://github.com/user-attachments/assets/91309a47-b480-4f75-a36d-515c52fe1743" />
<img width="2556" height="588" alt="image" src="https://github.com/user-attachments/assets/68a2cf55-1cad-4ac6-9f3d-521f83183f04" />
<img width="2556" height="637" alt="image" src="https://github.com/user-attachments/assets/73e7175f-6c29-4720-b670-dffd8c32aca8" />
<img width="2555" height="666" alt="image" src="https://github.com/user-attachments/assets/48900b08-668e-4fe9-8d86-eb055e4a923a" />
<img width="2554" height="624" alt="image" src="https://github.com/user-attachments/assets/a62ddd95-4df2-4098-bf0b-5a876d5d8ca1" />
<img width="2552" height="647" alt="image" src="https://github.com/user-attachments/assets/1e5fff7f-24dc-4f3a-9c00-2dc4ceef65e0" />

## ⚙️ Configuración del proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/tuusuario/minierp.git
cd minierp
### 2. Configurar la base de datos

Por defecto se usa SQLite en appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Data Source=MiniERP.db"
}


👉 Para SQL Server:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MiniERP;User Id=sa;Password=TuPasswordSegura;TrustServerCertificate=True;"
}


👉 Para PostgreSQL:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=MiniERP;Username=postgres;Password=TuPasswordSegura"
}


En Program.cs puedes alternar la base de datos comentando/descomentando la línea correspondiente:

// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// SQL Server
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// PostgreSQL
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
### 3. Aplicar migraciones y datos iniciales
dotnet ef database update


El seeding inicial crea:

Usuario Admin → admin@erp.com / Admin123!

Usuario RRHH → rrhh@erp.com / Rrhh123!

Usuario Operario → operario@erp.com / Operario123!

### 4. Ejecutar la aplicación
dotnet run


La app estará disponible en:

https://localhost:7255

http://localhost:5256

## 🐳 Ejecución con Docker
### 1. Construir la imagen
docker build -t minierp .

### 2. Ejecutar el contenedor
docker run -d -p 8080:80 --name minierp_app minierp


Acceder en: http://localhost:8080

📂 Estructura del proyecto
MiniERP/
│-- Controllers/      → Lógica de controladores MVC
│-- Data/             → DbContext y Seed inicial
│-- Models/           → Entidades del dominio
│-- ViewModels/       → Modelos para vistas específicas
│-- Views/            → Vistas Razor (MVC)
│-- wwwroot/          → Archivos estáticos (css, js, imágenes)
│-- Dockerfile        → Imagen de despliegue
│-- appsettings.json  → Configuración (cadena de conexión, etc.)

🧩 Roadmap (mejoras futuras)

 Tests unitarios e integración.

 CI/CD con GitHub Actions.

 Dashboard con gráficos de métricas.

 Multi-idioma (i18n).

 API REST pública para integraciones externas.

👨‍💻 Autor

Sergio Gay Ortiz

📜 Licencia

MIT License © 2025

