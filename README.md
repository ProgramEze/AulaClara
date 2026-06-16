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

### Etapa 7 - Edición y baja lógica de alumnos

Estado: completada.

Se completó el CRUD inicial de alumnos agregando edición y baja lógica.

Funcionalidades agregadas:

- Actualizar los datos de un alumno propio.
- Dar de baja lógica a un alumno propio.
- Mantener la validación de pertenencia mediante el `UsuarioId` obtenido desde el JWT.
- Evitar el borrado físico de información.
- Excluir alumnos dados de baja de los listados y búsquedas activas.
- Probar los endpoints protegidos desde Swagger usando el botón `Authorize` con token Bearer.

Endpoints agregados:

```http
PUT /api/alumnos/{id}
```

Ejemplo de request:

```json
{
	"nombre": "Fefi",
	"edad": 32,
	"observaciones": "Necesita paciencia y un lugar tranquilo para estudiar, es aplicado pero no tiene paciencia.",
	"responsableNombre": "El mismo",
	"responsableContacto": "contacto de prueba"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-del-alumno",
	"nombre": "Fefi",
	"edad": 32,
	"observaciones": "Necesita paciencia y un lugar tranquilo para estudiar, es aplicado pero no tiene paciencia.",
	"responsableNombre": "El mismo",
	"responsableContacto": "contacto de prueba",
	"activo": true
}
```

---

```http
DELETE /api/alumnos/{id}
```

Realiza una baja lógica del alumno. No elimina físicamente el registro de la base de datos.

Respuesta esperada:

```http
204 No Content
```

---

### Mejora de pruebas - Swagger con Bearer

Estado: completada.

Se configuró Swagger para permitir autenticación desde el botón `Authorize`.

Uso esperado:

```http
Authorization: Bearer jwt-generado
```

Esto permite probar endpoints protegidos directamente desde Swagger sin cargar manualmente el header en cada request.

---

### Etapa 8 - Gestión protegida de materias

Estado: completada.

Se implementó el módulo de materias asociado al usuario autenticado.

Funcionalidades agregadas:

- Crear materias propias del usuario autenticado.
- Listar solo las materias activas del usuario autenticado.
- Consultar una materia por ID validando que pertenezca al usuario autenticado.
- Actualizar una materia propia.
- Dar de baja lógica una materia propia.
- Validar nombres duplicados por usuario.
- Evitar que un usuario acceda a materias de otro usuario.

Endpoints agregados:

```http
POST /api/materias
```

Ejemplo de request:

```json
{
	"nombre": "Ingles",
	"descripcion": "Clases de ingles para nivel inicial e intermedio"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-de-la-materia",
	"nombre": "Ingles",
	"descripcion": "Clases de ingles para nivel inicial e intermedio",
	"activa": true
}
```

---

```http
GET /api/materias
```

Devuelve solamente las materias activas del usuario autenticado.

---

```http
GET /api/materias/{id}
```

Devuelve la materia solicitada solo si pertenece al usuario autenticado.

---

```http
PUT /api/materias/{id}
```

Ejemplo de request:

```json
{
	"nombre": "Ingles",
	"descripcion": "Clases de ingles con practica oral, lectura y ejercicios"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-de-la-materia",
	"nombre": "Ingles",
	"descripcion": "Clases de ingles con practica oral, lectura y ejercicios",
	"activa": true
}
```

---

```http
DELETE /api/materias/{id}
```

Realiza una baja lógica de la materia. No elimina físicamente el registro de la base de datos.

Respuesta esperada:

```http
204 No Content
```

---

### Etapa 9 - Asociación entre alumnos y materias

Estado: completada.

Se implementó el módulo para vincular alumnos propios con materias propias.

Funcionalidades agregadas:

- Asociar un alumno activo con una materia activa del usuario autenticado.
- Listar las materias asociadas a un alumno.
- Listar los alumnos asociados a una materia.
- Evitar asociaciones duplicadas.
- Reactivar una asociación previamente dada de baja en lugar de crear una fila duplicada.
- Validar que tanto el alumno como la materia pertenezcan al usuario autenticado.
- Dar de baja lógica una asociación entre alumno y materia.

Endpoints agregados:

```http
POST /api/alumno-materias
```

Ejemplo de request:

```json
{
	"alumnoId": "guid-del-alumno",
	"materiaId": "guid-de-la-materia"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-de-la-asociacion",
	"alumnoId": "guid-del-alumno",
	"alumnoNombre": "Fefi",
	"materiaId": "guid-de-la-materia",
	"materiaNombre": "Ingles",
	"fechaAsignacionUtc": "fecha-utc",
	"activa": true
}
```

---

```http
GET /api/alumno-materias/alumnos/{alumnoId}/materias
```

Devuelve las materias activas asociadas al alumno indicado, siempre que el alumno pertenezca al usuario autenticado.

---

```http
GET /api/alumno-materias/materias/{materiaId}/alumnos
```

Devuelve los alumnos activos asociados a la materia indicada, siempre que la materia pertenezca al usuario autenticado.

---

```http
DELETE /api/alumno-materias/{id}
```

Realiza una baja lógica de la asociación. No elimina físicamente el registro de la base de datos.

Respuesta esperada:

```http
204 No Content
```

---

### Etapa 10 - Gestión protegida de clases

Estado: completada.

Se implementó el módulo de clases asociado al usuario autenticado.

Decisión de diseño aplicada:

- Una clase se asocia a `AlumnoMateriaId`, no a `AlumnoId` y `MateriaId` por separado.
- La asociación `AlumnoMateria` representa la relación real entre un alumno y una materia.
- Desde la clase se recuperan los datos del alumno y de la materia a través de esa asociación.
- Se mantiene `UsuarioId` en `Clase` para validar pertenencia y simplificar consultas protegidas.

Funcionalidades agregadas:

- Crear clases usando una asociación activa entre alumno y materia.
- Listar solo las clases del usuario autenticado.
- Consultar una clase por ID validando pertenencia.
- Actualizar los datos principales de una clase propia.
- Marcar una clase planificada como realizada.
- Cancelar una clase planificada.
- Evitar que una clase cancelada pueda marcarse como realizada.
- Evitar que una clase realizada pueda cancelarse.
- Evitar que un usuario acceda a clases de otro usuario.

Reglas de estado:

```txt
Planificada -> Realizada
Planificada -> Cancelada
Realizada -> no se puede cancelar
Cancelada -> no se puede realizar
```

Endpoints agregados:

```http
POST /api/clases
```

Ejemplo de request:

```json
{
	"alumnoMateriaId": "guid-de-la-asociacion",
	"fechaClaseUtc": "2026-06-14T18:00:00Z",
	"titulo": "Clase inicial de presente simple",
	"objetivo": "Repasar estructura basica del presente simple",
	"contenidoTrabajado": "Afirmativo, negativo y preguntas simples",
	"observaciones": "La alumna respondio bien a ejemplos cortos"
}
```

Ejemplo de respuesta:

```json
{
	"id": "guid-de-la-clase",
	"alumnoMateriaId": "guid-de-la-asociacion",
	"alumnoId": "guid-del-alumno",
	"alumnoNombre": "Zoe",
	"materiaId": "guid-de-la-materia",
	"materiaNombre": "Ingles",
	"fechaClaseUtc": "2026-06-14T18:00:00Z",
	"titulo": "Clase inicial de presente simple",
	"objetivo": "Repasar estructura basica del presente simple",
	"contenidoTrabajado": "Afirmativo, negativo y preguntas simples",
	"observaciones": "La alumna respondio bien a ejemplos cortos",
	"estado": 1
}
```

---

```http
GET /api/clases
```

Devuelve solamente las clases del usuario autenticado.

---

```http
GET /api/clases/{id}
```

Devuelve la clase solicitada solo si pertenece al usuario autenticado.

---

```http
PUT /api/clases/{id}
```

Ejemplo de request:

```json
{
	"fechaClaseUtc": "2026-06-15T18:00:00Z",
	"titulo": "Clase actualizada de presente simple",
	"objetivo": "Practicar oraciones afirmativas y negativas",
	"contenidoTrabajado": "Do, does, don't y doesn't",
	"observaciones": "Se recomienda seguir con ejercicios orales"
}
```

---

```http
PATCH /api/clases/{id}/realizar
```

Marca una clase planificada como realizada.

---

```http
PATCH /api/clases/{id}/cancelar
```

Marca una clase planificada como cancelada.

---

### Próxima etapa - Materiales de clase

Estado: pendiente.

El siguiente paso será implementar el módulo de materiales asociados a una clase.

Objetivo inicial:

- Crear materiales asociados a una clase propia.
- Listar materiales de una clase validando pertenencia.
- Consultar un material por ID validando pertenencia.
- Actualizar materiales propios.
- Eliminar o dar de baja materiales según la regla de negocio que definamos.
- Evitar que un usuario acceda a materiales de clases de otro usuario.
