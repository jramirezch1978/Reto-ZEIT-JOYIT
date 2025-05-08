# Reto-ZEIT-JOYIT

## Historia de Usuario (Scrum)

Como **administrador del sistema**, quiero **gestionar el ciclo de vida de los proveedores** (añadir, actualizar, desactivar y eliminar proveedores), para asegurar que la información de los socios comerciales esté siempre actualizada, sea confiable y permita un control efectivo sobre las relaciones comerciales de la empresa.

**Criterios de aceptación:**

- Puedo registrar un nuevo proveedor con todos los datos requeridos.
- Puedo actualizar la información de un proveedor existente.
- Puedo desactivar un proveedor para que no pueda ser seleccionado en operaciones futuras, sin eliminar su historial.
- Puedo eliminar un proveedor si ya no es relevante para la empresa.
- Todas las acciones deben estar protegidas y requerir autenticación de usuario con permisos de administrador.

## Estructura de la Solución

La solución sigue los principios de **Clean Architecture** y está compuesta por los siguientes proyectos:

- **CleanArchitecture.API**  
  Proyecto principal de la API REST. Expone los endpoints, configura la autenticación JWT y la inyección de dependencias.

- **CleanArchitecture.Application**  
  Contiene la lógica de negocio (servicios), validaciones y utilidades.

- **CleanArchitecture.Domain**  
  Define las entidades, modelos y contratos (interfaces) del dominio.

- **CleanArchitecture.Infrastructure**  
  Implementa el acceso a datos usando Dapper y la conexión a PostgreSQL.

- **CleanArchitecture.Tests**  
  Proyecto de pruebas unitarias y de integración para servicios y endpoints.

---

## Componentes principales

- **Controladores:**  
  - `UserController`: Gestión de usuarios.
  - `PartnerController`: Gestión de proveedores.
  - `AuthController`: Autenticación y generación de tokens JWT.
  - `HomeController`: Página de inicio informativa.

- **Servicios:**  
  - `UserService`: Lógica de negocio para usuarios.
  - `PartnerService`: Lógica de negocio para proveedores.

- **Repositorios:**  
  - `UserRepository`: Acceso a datos de usuarios.
  - `PartnerRepository`: Acceso a datos de proveedores.

---

## Endpoints Disponibles

### Autenticación

- **POST** `/api/auth/login`  
  Autentica un usuario y devuelve un JWT.  
  **Body:**  
  ```json
  {
    "usernameOrEmail": "usuario",
    "password": "contraseña"
  }
  ```
  **Respuesta:**  
  ```json
  {
    "token": "jwt_token",
    "user": { "id": 1, "username": "usuario", "email": "mail", "role": "ADMIN" }
  }
  ```

### Usuarios (`/api/user`)

- **GET** `/api/user`  
  Lista todos los usuarios (requiere JWT).

- **GET** `/api/user/{id}`  
  Obtiene un usuario por ID.

- **POST** `/api/user`  
  Crea un usuario.  
  **Body:**  
  ```json
  {
    "username": "nuevo",
    "email": "nuevo@mail.com",
    "password": "1234",
    "firstName": "Nombre",
    "lastName": "Apellido",
    "role": "USER",
    "isActive": true
  }
  ```

- **PUT** `/api/user/{id}`  
  Actualiza un usuario.

- **DELETE** `/api/user/{id}`  
  Elimina un usuario.

### Proveedores (`/api/partner`)

- **GET** `/api/partner`  
  Lista todos los proveedores (requiere JWT).

- **GET** `/api/partner/{id}`  
  Obtiene un proveedor por ID.

- **POST** `/api/partner`  
  Crea un proveedor.  
  **Body:**  
  ```json
  {
    "razonSocial": "Empresa SAC",
    "taxId": "12345678901",
    "type": "Proveedor",
    "contactName": "Juan Perez",
    "contactEmail": "contacto@empresa.com",
    "contactPhone": "999888777",
    "address": "Calle 123",
    "city": "Lima",
    "state": "Lima",
    "country": "Perú",
    "postalCode": "15000",
    "isActive": true
  }
  ```

- **PUT** `/api/partner/{id}`  
  Actualiza un proveedor.

- **DELETE** `/api/partner/{id}`  
  Elimina un proveedor.

---

## Uso del Token JWT

1. Autentícate en `/api/auth/login` para obtener el token.
2. En cada request a endpoints protegidos, agrega el header:
   ```
   Authorization: Bearer <token>
   ```

---

## Estructura de los tests

- **Servicios:**  
  Pruebas unitarias usando Moq para simular los repositorios.
- **Endpoints:**  
  Pruebas de integración usando `WebApplicationFactory` para simular requests HTTP reales.

Ejemplo de ejecución de tests:
```bash
dotnet test CleanArchitecture.Tests
```

---

## Instalación y ejecución

### 1. Instalar .NET SDK (usando Chocolatey en Windows)
```bash
choco install dotnet-sdk
```

### 2. Instalar dependencias del proyecto
```bash
dotnet restore
```

### 3. Compilar la solución
```bash
dotnet build
```

### 4. Ejecutar la API localmente
```bash
dotnet run --project CleanArchitecture.API
```

### 5. Ejecutar los tests
```bash
dotnet test CleanArchitecture.Tests
```

---

## Docker

### Explicación del Dockerfile

- **Multi-stage build:**  
  Compila y publica la app en una imagen intermedia, luego copia solo los archivos necesarios a una imagen final ligera.
- **Expone el puerto 7000** para HTTP.
- **Variables de entorno** para el entorno y la URL de escucha.

### Comandos para compilar y ejecutar en Docker remoto

#### 1. Compilar la imagen (desde tu máquina, usando Docker remoto)
```bash
# Configura la IP de tu servidor Docker remoto
export DOCKER_HOST=tcp://192.168.0.254:2375

# Compila la imagen
docker build -t clean-architecture-api .
```

#### 2. Ejecutar el contenedor
```bash
docker run -d --name clean-architecture-api -p 7000:7000 clean-architecture-api
```

#### 3. Probar la API
Accede a:  
```
http://192.168.0.254:7000
```
o a los endpoints documentados arriba.

---

## Configuración del servidor Docker remoto

- Asegúrate de que Docker esté instalado y corriendo en el servidor remoto.
- El puerto 2375 debe estar abierto y configurado para aceptar conexiones TCP (solo en redes seguras).
- Puedes usar Portainer para administrar visualmente los contenedores.

---

## Otros comandos útiles

- **Ver logs del contenedor:**
  ```bash
  docker logs clean-architecture-api
  ```
- **Detener y eliminar el contenedor:**
  ```bash
  docker stop clean-architecture-api
  docker rm clean-architecture-api
  ```
- **Eliminar la imagen:**
  ```bash
  docker rmi clean-architecture-api
  ```

---

## Notas finales

- El archivo `appsettings.json` contiene la configuración de la base de datos y la clave JWT.
- Los endpoints están protegidos por JWT y filtro de usuario activo.
- El proyecto sigue buenas prácticas de seguridad y arquitectura limpia.

---
