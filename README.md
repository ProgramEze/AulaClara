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

Estado: completada.

Se implementaron las primeras funcionalidades de autenticación:

- Registro de usuarios/profes.
- Hash seguro de contraseña.
- Validación de email duplicado.
- Login básico con email y contraseña.

Esta etapa dejó preparada la base para incorporar autenticación con JWT.

Endpoint disponible:

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

### Etapa 5 - Autenticación con JWT

Estado: completada.

Se extendió el login para generar un token JWT después de validar email y contraseña.

Se agregó:

- Generación de token JWT.
- Configuración de emisor, audiencia y expiración.
- Clave secreta local mediante user-secrets.
- Endpoint protegido con `[Authorize]`.
- Lectura de claims del usuario autenticado.

Endpoints disponibles:

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
	"email": "ezequiel@test.com",
	"token": "jwt-generado",
	"expiraEnUtc": "fecha-de-expiracion-utc"
}
```

---

```http
GET /api/autenticacion/perfil
```

Este endpoint requiere enviar el token JWT en el header `Authorization`:

```http
Authorization: Bearer jwt-generado
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

### Etapa 6 - Gestión protegida de alumnos

Estado: completada.

Se implementó el primer módulo real protegido por autenticación JWT.

Funcionalidades agregadas:

- Crear alumnos asociados al usuario autenticado.
- Listar solo los alumnos del usuario autenticado.
- Consultar un alumno por ID validando que pertenezca al usuario autenticado.
- Proteger los endpoints con `[Authorize]`.
- Obtener el `UsuarioId` desde los claims del token JWT.
- Evitar que el cliente envíe `UsuarioId` desde el body.

Endpoints disponibles:

```http
POST /api/alumnos
```

Ejemplo de request:

```json
{
	"nombre": "Zoe",
	"edad": 12,
	"observaciones": "Necesita consignas simples y ejemplos concretos.",
	"responsableNombre": "Madre de Zoe",
	"responsableContacto": "contacto de prueba"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-del-alumno",
	"nombre": "Zoe",
	"edad": 12,
	"observaciones": "Necesita consignas simples y ejemplos concretos.",
	"responsableNombre": "Madre de Zoe",
	"responsableContacto": "contacto de prueba",
	"activo": true
}
```

---

```http
GET /api/alumnos
```

Devuelve solamente los alumnos del usuario autenticado.

---

```http
GET /api/alumnos/{id}
```

Devuelve el alumno solicitado solo si pertenece al usuario autenticado.

---

### Próxima etapa - Edición y baja lógica de alumnos

Estado: pendiente.

El siguiente paso será completar el CRUD de alumnos agregando:

- Actualizar datos de un alumno.
- Dar de baja lógica a un alumno.
- Validar pertenencia del alumno al usuario autenticado.
- Evitar borrado físico de información.
