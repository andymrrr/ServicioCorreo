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

## Uso

El comando principal es **EnviarCorreosComando**, que permite enviar correos electrónicos personalizados.

| Propiedad               | Tipo                             | Requerido | Descripción                                                                |
| ----------------------- | -------------------------------- | --------- | -------------------------------------------------------------------------- |
| Destinatario            | string                           | Sí        | Dirección de correo del destinatario.                                      |
| Asunto                  | string                           | Sí        | Asunto del correo.                                                         |
| IdPlantilla             | int?                             | No        | ID de la plantilla almacenada en la base de datos.                         |
| CuerpoHtmlPersonalizado | string                           | No        | Contenido HTML personalizado en caso de no usar una plantilla.             |
| Parametros              | Dictionary<string, string>       | No        | Valores dinámicos que se sustituyen en la plantilla o el cuerpo HTML.      |
| Items                   | List<Dictionary<string, object>> | No        | Lista de elementos dinámicos (usualmente para tablas o listas detalladas). |
