# txt
La aplicación permite crear notas al estilo de *Notion*, utilizando el lenguaje de marcado *Markdown*, y organizarlas en carpetas

### Caracteristicas y funcionalidades
- **Registro y login**: el usuario deberá llenar un formulario de registro si es la primera vez que ingresa, o de login si ya fue registrado
- **Procesamiento linea por linea**: cada linea se procesa de manera individual, de forma que el usuario escribe el *Markdown*, y al deseleccionar la linea se convierte
- **Arbol de directorios**: el usuario creará una carpeta raíz con el nombre que desee, en la que podrá ir creando notas o más subcarpetas, formando un arbol
- **Modo oscuro**: un switch permite cambiar la paleta de colores a una más ocscura para cuidar la vista


### Tecnologías usadas
- **Blazor**: la base del proyecto, una característica de *ASP.NET* que permitió el desarrollo de esta aplicación sin utilizar *JavaScript*, facilitando la creación de las distintas vistas o páginas utilizando componentes *Razor*
- **SQLServer**: sistema de gestion de base de datos relacional
- **MudBlazor**: libreria para la creación de componentes, sencilla de usar y que agilizó el diseño e implementación de componentes
- **Bootstrap**: framework de *CSS* usado para el estilado de la aplicación 
- **AutoMapper**: libreria usada para el mapeo y transferencia de datos entre objetos de clases diferentes
- **EntityFramework**: permite crear la capa de acceso a los datos y traspasar las entidades definidas en el dominio de la aplicación a una base de datos en SqlServer
- **IdentityFramework**: brinda herramientas para implementar el registro, autenticacion y autorizacion de usuarios

### Arquitectura

La aplicación está divida en tres capas:
 - **Front-end**: El frontend está estructurado de manera sencilla, dividido en páginas y compartiendo ciertos componentes y *layouts* (en el lenguaje de documentación de Blazor refiriéndose a componentes que envuelven la página).
La autorización se hace a partir de una solución provisto por defecto mediante el componente CascadingAuthenticationState, que mediante un middleware pide a la API un conjunto de claims que permiten identificar al usuario. Este solo es identificado cuando se tiene un token de sesión almacenado en Local Storage del navegador, que se guarda al iniciar sesión mediante la pantalla de logueo.
 - **Back-end**: El backend se trata de una API REST. Está basado en arquitectura limpia, por lo que se divide en
   - Api: Consume, mediante un mediador, indirectamente, los comandos y las consultas de la capa de aplicación. Define los endpoints consumibles por el exterior. Implementa la lógica de autorización y autenticación. Implementa la inyección de dependencias para todos los proyectos.
   - Aplicación: Implementa la lógica de negocio utilizando la información provista por el dominio mediante el patrón CQRS, dividiendo las acciones que modifican el estado (comandos) y las que extraen información (consultas) haciendo uso de los repositorios definidos en el dominio e implementados en infraestructura. 
   - Dominio: Define las interfaces de los repositorios. Define las entidades. Define los enumerados usados en el sistema. El dominio define cuatro entidades que son User, Folder, Note y NoteLine.
   - Infraestructura: Implementa los repositorios de las interfacese definidas en el dominio, por lo que también implementa la lógica de dominio. Consulta directamente a la base de datos. Define la conexión de la base de datos.
 - **Base de datos**: La base de datos es una base relacional gestionada por SQL Server 2022.


### Mejoras a futuro
- **Exportación como PDF**: nos gustaría que el usuario sea capaz de exportar sus notas como PDF
- **Problemas de estabilidad**: aun hay ciertos problemas de estabilidad en la conexión y autenticación que deben ser revisados
- **Pulido del estilo visual**: hay ciertos componentes cuyos estilos pueden mejorarse
