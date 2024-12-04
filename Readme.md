## ServicioCorreo

**ServicioCorreo** es una solución robusta para el envío de correos electrónicos, diseñada con MediatR y capacidades avanzadas de personalización mediante plantillas dinámicas. Este servicio soporta parámetros personalizados, listas dinámicas y plantillas HTML almacenadas en base de datos.

### Descripción

ServicioCorreo facilita la integración de funcionalidades de envío de correos electrónicos en proyectos .NET, utilizando una arquitectura basada en comandos y plantillas HTML dinámicas.

### Beneficios

- **Soporte para Plantillas HTML:** Almacena y reutiliza plantillas HTML directamente desde la base de datos.
- **Parámetros Dinámicos:** Sustitución automática de valores en plantillas.
- **Compatibilidad con Listas:** Generación de tablas dinámicas o listas a partir de datos proporcionados.
- **Fácil Integración:** Arquitectura basada en MediatR para manejar solicitudes y comandos.

## Características

- **Envío de correos con plantillas:** Usa plantillas definidas o genera contenido HTML personalizado al vuelo.
- **Sustitución dinámica:** Integra parámetros y listas directamente en el cuerpo del correo.
- **Manejo robusto de errores:** Validación de datos de entrada y captura de errores de envío.

## Instalación

1. Clona este repositorio:

```
    git clone https://github.com/tu-repositorio/ServicioCorreo.git
```

2.  Navega al directorio del proyecto:

```
    cd ServicioCorreo
```

3. Restaura las dependencias del proyecto:

```
    dotnet restore
```

4. Configura las cadenas de conexión y la configuración del servidor de correos en el archivo **appsettings.json**.

## Configuración

### Configuración de Correo

Define los parámetros de tu servidor SMTP en **appsettings.json**:

```json
{
  "ConfiguracionCorreo": {
    "ServidorSmtp": "smtp.gmail.com",
    "Puerto": 587,
    "CorreoRemitente": "tucorreo@gmail.com",
    "NombreRemitente": "Tu Remitente",
    "Contraseña": "Tu Clave",
    "UsarSsl": true
  }
}
```

## Configuración de la Base de Datos

Define la conexión a tu base de datos para almacenar plantillas y otros datos:

```json
{
  "ConnectionStrings": {
    "ContextCorreo": "Server=mi-servidor;Database=mi-bd;User Id=mi-usuario;Password=mi-contrasena;"
  }
}
```

## Migración y Plantillas Prediseñadas

Como parte del proceso de migración, se crean automáticamente las tablas necesarias para el funcionamiento del sistema, junto con una serie de plantillas prediseñadas que facilitan la configuración inicial.

### Tablas Generadas

1. **Tabla de Logs**: Esta tabla registra todas las actividades importantes del sistema, permitiendo un seguimiento detallado de las acciones y eventos ocurridos.
2. **Tabla de Plantillas**: Se crea una tabla que contiene las plantillas prediseñadas, las cuales se utilizan para la configuración inicial de la aplicación.

### Plantillas Prediseñadas

Durante la migración, también se generan una serie de plantillas prediseñadas que se insertan automáticamente en la base de datos. Estas plantillas incluyen:
| Nombre | Descripción | Parámetros Esperados |
|-------------------------------------|-----------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------|
| Plantilla Registro Usuario | Plantilla para notificar el registro de un usuario. | ["NombreUsuario", "NombreCompleto", "CorreoElectronico", "FechaRegistro", "Estado", "CorreoSoporte"] |
| Plantilla Factura | Plantilla para la generación de facturas. | ["LogoUrl", "NombreCliente", "NombreEmpresa", "Fecha", "NumeroFactura", "Items", "Total", "CorreoEmpresa", "Estado"] |
| Plantilla Restablecimiento de Contraseña | Plantilla para notificar y gestionar el restablecimiento de contraseña. | ["NombreUsuario", "UrlRestablecimiento", "FechaExpiracion", "CorreoSoporte"] |
| Confirmación de Registro de Cuenta | Plantilla de confirmación de registro para nuevos usuarios. | ["NombreUsuario", "UrlConfirmacion", "FechaExpiracion", "CorreoSoporte"] |
| Notificación de Cambio de Contraseña | Plantilla para notificar al usuario que su contraseña ha sido cambiada. | ["NombreUsuario", "FechaCambio", "CorreoSoporte"] |
| Notificación de Pago Exitoso | Plantilla para notificar al usuario que su pago ha sido procesado exitosamente. | ["NombreUsuario", "Monto", "NumeroFactura", "FechaPago", "CorreoSoporte"] |
| Recordatorio de Renovación de Suscripción | Plantilla de recordatorio de renovación de suscripción para los usuarios. | ["NombreUsuario", "FechaVencimiento", "UrlRenovacion", "CorreoSoporte"] |
| Notificación de Actualización de Perfil | Plantilla de notificación para informar al usuario que su perfil ha sido actualizado. | ["NombreUsuario", "FechaActualizacion", "CorreoSoporte"] |
| Notificación de Suspensión de Cuenta | Plantilla para notificar al usuario que su cuenta ha sido suspendida. | ["NombreUsuario", "FechaSuspension", "MotivoSuspension", "CorreoSoporte"] |
| Notificación de Descuento en Compra | Plantilla para notificar al usuario sobre un descuento en su próxima compra. | ["NombreUsuario", "DescuentoPorcentaje", "FechaVencimiento", "UrlCompra", "CorreoSoporte"] |
| Recordatorio de Factura Pendiente | Plantilla para notificar al usuario sobre una factura pendiente de pago. | ["NombreUsuario", "MontoFactura", "FechaVencimiento", "UrlPago", "CorreoSoporte"] |

### ¿Cómo se aplican las migraciones?

Al ejecutar las migraciones, tanto las tablas como las plantillas prediseñadas se crean de manera automática, asegurando que la aplicación esté lista para su uso desde el inicio.

## Uso

### Propiedades del Comando

El comando principal es **EnviarCorreosComando**, que permite enviar correos electrónicos personalizados.

| Propiedad               | Tipo                             | Requerido | Descripción                                                                |
| ----------------------- | -------------------------------- | --------- | -------------------------------------------------------------------------- |
| Destinatario            | string                           | Sí        | Dirección de correo del destinatario.                                      |
| Asunto                  | string                           | Sí        | Asunto del correo.                                                         |
| IdPlantilla             | int?                             | No        | ID de la plantilla almacenada en la base de datos.                         |
| CuerpoHtmlPersonalizado | string                           | No        | Contenido HTML personalizado en caso de no usar una plantilla.             |
| Parametros              | Dictionary<string, string>       | No        | Valores dinámicos que se sustituyen en la plantilla o el cuerpo HTML.      |
| Items                   | List<Dictionary<string, object>> | No        | Lista de elementos dinámicos (usualmente para tablas o listas detalladas). |

## Endpoints de la API

La API proporciona dos endpoints principales para la gestión de plantillas y el envío de correos. A continuación, se detalla su funcionalidad y uso.

---

### 1. Listar Plantillas con Paginación

**Endpoint**: `/api/Plantilla/paginacion`  
**Método**: `GET`

Este endpoint lista las plantillas disponibles en el sistema. Permite realizar búsquedas de manera flexible a través de los siguientes parámetros:

- **Búsqueda por Nombre**:
  - Ejemplo: `/api/Plantilla/paginacion?busqueda=usuario`
- **Búsqueda por ID**:
  - Ejemplo: `/api/Plantilla/paginacion?PlantillaId=1`

#### Respuesta

El endpoint devuelve un objeto con la siguiente estructura:

```json
{
  "totalRegistros": 1,
  "paginaActual": 1,
  "cantidadRegistroPorPagina": 10,
  "datos": [
    {
      "destinatario": null,
      "asunto": "Correo basado en Plantilla Registro Usuario",
      "idPlantilla": 1,
      "cuerpoHtmlPersonalizado": null,
      "parametros": {
        "NombreUsuario": "",
        "NombreCompleto": "",
        "CorreoElectronico": "",
        "FechaRegistro": "",
        "Estado": "",
        "CorreoSoporte": ""
      },
      "items": []
    }
  ],
  "totalPaginas": 1
}
```

Este resultado no solo proporciona los datos de las plantillas disponibles, sino que también muestra cómo luce el request para enviar un correo usando una plantilla específica.
