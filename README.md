##TaskManager - Sistema Completo de Gestión de Tareas
# Una aplicación full-stack moderna para la gestión de tareas, construida con Angular 17+ en el frontend y .NET 8 en el backend.

##🚀 Características Principales

#✅ Gestión de Tareas

- CRUD Completo: Crear, leer, editar y eliminar tareas

- Marcar como completadas: Cambiar estado de tareas

- Categorías y prioridades: Organizar tareas eficientemente

- Fechas y recordatorios: Gestión de tiempos y vencimientos

#🔐 Autenticación y Seguridad

- Registro e inicio de sesión: Autenticación segura de usuarios

- JWT Tokens: Autenticación basada en tokens

- Protección de rutas: Guards en frontend y autorización en backend

- Passwords encriptadas: Seguridad de credenciales

#📊 Dashboard y Analytics

- Estadísticas visuales: Tareas completadas vs pendientes

- Métricas de productividad: Gráficos y resúmenes

- Filtros y búsqueda: Encontrar tareas rápidamente


##🛠️ Tecnologías Utilizadas

#Frontend (Client)

- Angular 17+ - Framework principal

- TypeScript - Lenguaje de programación

- Tailwind CSS - Framework de estilos

- RxJS - Manejo de estado reactivo

- Lucide Icons - Librería de iconos


#Backend (Server)

- .NET 8 - Framework backend

- Entity Framework Core - ORM para base de datos

- SQL Server - Base de datos relacional

- JWT Bearer - Autenticación por tokens

- Swagger/OpenAPI - Documentación de API


##📋 Prerrequisitos

#Software Requerido

- Node.js 18+

- .NET 8 SDK

- SQL Server 2019+

- Git

##🚀 Instalación y Configuración

#Paso 1: Clonar el Repositorio
git clone https://github.com/jvivas-rock/PruebaTecnica20251031.git
cd PruebaTecnica20251031

#Paso 2: Configuración de la Base de Datos (Server)
1. Ejecutar SQL Server Management Studio
2. Crear la base de datos:
CREATE DATABASE TaskManagerDB;

3. Ejecutar el script de inicialización:
Ejecutar el script SQLQuery.sql

#Paso 3: Configuración del Backend (.NET 8)
1. Navegar al directorio del servidor:
cd PruebaTecnica.BE/

2. Configurar connection string:
Editar TaskAppAPI/appsettings.json:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagerDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
Reemplazar localhost con el servidor local de tu ordenador.

3. Restaurar dependencias:
dotnet restore

4. Ejecutar el servidor (Preferiblemente con IIS):
dotnet run

El API estará disponible en: https://localhost:44354

#Paso 4: Configuración del Frontend (Angular)
1. Navegar al directorio del cliente:
cd PruebaTecnica.FE/

2. Instalar dependencias:
npm install

3. Configurar environment:

Editar src/environments/environment.ts:

export const environment = {
  production: false,
  apiUrl: 'https://localhost:44354/api'
};

4.Ejecutar la aplicación:
ng serve

La aplicación estará disponible en: http://localhost:4200

##📡 Endpoints del API
#Autenticación
POST /api/Auth/register - Registrar nuevo usuario

POST /api/Auth/login - Iniciar sesión

#Tareas
GET /api/Tasks - Obtener todas las tareas del usuario

POST /api/Tasks - Crear nueva tarea

PUT /api/Tasks/{id} - Actualizar tarea

DELETE /api/Tasks/{id} - Eliminar tarea


#Dashboard
GET /api/Dashboard/statistics - Obtener estadísticas

GET /api/Dashboard/recent-tasks - Tareas recientes

##👤 Usuarios de Prueba
Después de ejecutar el script SQL, puedes usar:

Username: admin
Password: admin123

O

Username: juan.perez
Password: admin123

O registra un nuevo usuario desde la aplicación

##🏃‍♂️ Comandos Rápidos
#Backend (.NET)
cd PruebaTecnica.BE/
dotnet restore          # Restaurar paquetes
dotnet build           # Compilar proyecto
dotnet run            # Ejecutar servidor
dotnet test           # Ejecutar pruebas

#Frontend (Angular)
cd PruebaTecnica.FE/
npm install           # Instalar dependencias
ng serve             # Servidor desarrollo
ng build             # Build producción
ng test              # Ejecutar pruebas
ng lint              # Análisis de código

##🐛 Solución de Problemas Comunes
#Error de Conexión a BD
Verificar que SQL Server está ejecutándose
sudo systemctl status mssql-server

Verificar connection string
"Server=localhost;Database=TaskManagerDB;Trusted_Connection=true;TrustServerCertificate=true;"
Error de CORS
Verificar que el backend tiene configurado CORS para http://localhost:4200