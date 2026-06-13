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
