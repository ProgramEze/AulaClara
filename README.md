## Avance del proyecto

### Etapa 0 - Estructura inicial

Estado: completada.

Se creó la estructura base del repositorio con enfoque monorepo:

```txt
AulaClara
├── aula-clara-api
└── aula-clara-web
```

También se creó la solución backend con .NET 10:

```txt
AulaClara.Api
AulaClara.Dominio
AulaClara.Aplicacion
AulaClara.Infraestructura
```

---

### Etapa 1 - Configuración base de la API

Estado: completada.

Se configuró la API inicial con:

- ASP.NET Core con .NET 10.
- Controladores.
- OpenAPI.
- Swagger UI en entorno de desarrollo.
- Primer endpoint de estado.

Endpoint disponible:

```http
GET /api/estado
```

Respuesta esperada:

```json
{
	"nombre": "Aula Clara API",
	"estado": "Funcionando",
	"version": "1.0.0",
	"entorno": "Development",
	"fechaUtc": "..."
}
```

---

### Etapa 2 - Dominio base de la versión 1.0

Estado: completada.

Se creó el modelo inicial del dominio de Aula Clara.

Entidades principales agregadas:

- Usuario
- Alumno
- Materia
- AlumnoMateria
- Clase
- MaterialClase
- EjercicioClase
- SeguimientoClase

También se agregaron enums para representar valores controlados del sistema:

- EstadoClase
- TipoMaterial
- NivelEjercicio

Esta etapa define la base del negocio de la aplicación: usuarios/profes, alumnos, materias, clases, materiales, ejercicios y seguimiento docente.

---

### Etapa 3 - Persistencia con SQLite y Entity Framework Core

Estado: completada.

Se configuró la persistencia de datos usando:

- Entity Framework Core 10.
- SQLite como base de datos local de desarrollo.
- DbContext principal del sistema.
- Configuración de entidades.
- Migración inicial de base de datos.

La base de datos local se genera como archivo SQLite y no se versiona en GitHub.

---

### Etapa 4 - Autenticación inicial

Estado: en progreso.

Se implementaron las primeras funcionalidades de autenticación:

- Registro de usuarios/profes.
- Hash seguro de contraseña.
- Validación de email duplicado.
- Login básico con email y contraseña.

> Nota: el login todavía no devuelve JWT. La generación de token será el siguiente paso de autenticación.

Endpoints disponibles:

```http
POST /api/autenticacion/registro
```

Ejemplo de request:

```json
{
	"nombre": "Ezequiel",
	"email": "ezequiel@test.com",
	"contrasenia": "123456"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-del-usuario",
	"nombre": "Ezequiel",
	"email": "ezequiel@test.com"
}
```

---

```http
POST /api/autenticacion/login
```

Ejemplo de request:

```json
{
	"email": "ezequiel@test.com",
	"contrasenia": "123456"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-del-usuario",
	"nombre": "Ezequiel",
	"email": "ezequiel@test.com"
}
```
