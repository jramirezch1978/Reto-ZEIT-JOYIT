# Checklist de Desarrollo

## Historia de Usuario (Scrum)

- [X] Historia de usuario documentada en README

### Criterios de aceptación (Gestión de Proveedores)

- [X] Registrar un nuevo proveedor con todos los datos requeridos
- [X] Actualizar la información de un proveedor existente
- [X] Desactivar un proveedor para que no pueda ser seleccionado en operaciones futuras, sin eliminar su historial
- [X] Eliminar un proveedor si ya no es relevante para la empresa
- [X] Todas las acciones protegidas y requieren autenticación de usuario con permisos de administrador

---

## Requisitos del Reto Técnico

- [X] Separación de capas (Clean Architecture)
- [X] Uso de base de datos relacional (PostgreSQL)
- [X] No usar Entity Framework
- [X] CRUD completo vía API para proveedores
- [X] CRUD completo vía API para usuarios
- [X] Endpoints para creación de usuarios
- [ ] Endpoints para login/autenticación y endpoints protegidos
- [X] Capa de acceso a datos separada
- [ ] Capa de lógica de negocio separada (servicios)
- [X] Carga inicial de usuario admin solo si no existe
- [X] Campo updated_at es null en la creación inicial
- [X] Credenciales separadas de la cadena de conexión
- [X] .gitignore configurado para evitar subir archivos sensibles
- [X] Código limpio y mantenible
- [ ] Pruebas unitarias para acceso a datos, lógica de negocio y endpoints
- [ ] Dockerfile para ejecución local (opcional, recomendado)
- [ ] Dockerfile (opcional, pero suma puntos)
