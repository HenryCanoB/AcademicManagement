# Galaxy Academic Management API

Sistema de gestión académica desarrollado con .NET 9 siguiendo los principios de Clean Architecture.

## 🏗️ Arquitectura

- **Domain Layer**: Entidades, Value Objects, Enums
- **Application Layer**: DTOs, Handlers (CQRS con MediatR), Contracts
- **Infrastructure Layer**: Repositorios, DbContext, Servicios
- **API Layer**: Controllers REST

## 🚀 Tecnologías

- .NET 9.0
- Entity Framework Core 9.0.9
- PostgreSQL
- MediatR
- ASP.NET Core Identity
- JWT Authentication
- Serilog + Elasticsearch
- Docker

## 📋 Requisitos Previos

- .NET 9 SDK
- Docker Desktop
- PostgreSQL (vía Docker)

## ⚙️ Configuración

1. **Iniciar contenedores Docker**:
```bash
docker-compose up -d
```

2. **Crear migraciones**:
```bash
# Academic Context
dotnet ef migrations add InitialCreate --context AcademicManagementDbContext 

# Identity Context
dotnet ef migrations add InitialIdentity --context IdentityDbContext 

3. **Aplicar migraciones**:
```bash
dotnet ef database update --context AcademicManagementDbContext 
dotnet ef database update --context IdentityDbContext
```

4. **Ejecutar la aplicación**:
```bash
cd Galaxy.AcademicManagement.API
dotnet run
```

## 🔐 Endpoints Principales

### Authentication
- `POST /api/auth/login` - Iniciar sesión

### Students
- `GET /api/students` - Listar estudiantes
- `POST /api/students` - Crear estudiante

### Professors
- `GET /api/professors` - Listar profesores
- `POST /api/professors` - Crear profesor

### Courses
- `GET /api/courses` - Listar cursos
- `POST /api/courses` - Crear curso

### Enrollments
- `GET /api/enrollments` - Listar todas las matrículas
- `GET /api/enrollments/student/{studentId}` - Matrículas por estudiante
- `GET /api/enrollments/course/{courseId}` - Estudiantes por curso
- `POST /api/enrollments` - Crear matrícula
- `PATCH /api/enrollments/{id}/withdraw` - Retirar matrícula de curso

## 🗄️ Puertos

- **API**: https://localhost:7162
- **PostgreSQL**: 1601
- **Elasticsearch**: 9201
- **Kibana**: 5602

## 📦 Estructura del Proyecto

```
Galaxy.AcademicManagement/
├── Galaxy.AcademicManagement.API/          # API REST
├── Galaxy.AcademicMagement.Application/    # Lógica de aplicación
├── Galaxy.AcademicMagement.Domain/         # Dominio
└── Galaxy.AcademicMagement.Infrastructure/ # Infraestructura
```

## 👤 Usuarios por Defecto

Los usuarios se crean automáticamente al iniciar la aplicación mediante el seeder.

## 📝 Notas

- Las matrículas tienen dos estados: `Enrolled` (Matriculado) y `Withdrawn` (Retirado)
- Todos los endpoints (excepto login) requieren autenticación JWT
- Los logs se envían a Elasticsearch para visualización en Kibana
