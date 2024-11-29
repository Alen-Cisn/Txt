# txt
La aplicación permite crear notas al estilo de *Notion*, utilizando el lenguaje de marcado *Markdown*, y organizarlas en carpetas

### Caracteristicas y funcionalidades
- **Registro y login**: el usuario deberá llenar un formulario de registro si es la primera vez que ingresa, o de login si ya fue registrado
- **Procesamiento linea por linea**: cada linea se procesa de manera individual, de forma que el usuario escribe el *Markdown*, y al deseleccionar la linea se convierte
- **Arbol de directorios**: el usuario creará una carpeta raíz con el nombre que desee, en la que podrá ir creando notas o más subcarpetas, formando un arbol
- **Modo oscuro**: un switch permite cambiar la paleta de colores a una más ocscura para cuidar la vista

### Arquitectura

### Tecnologías usadas
- **Blazor**: la base del proyecto, una característica de *ASP.NET* que permitió el desarrollo de esta aplicación sin utilizar *JavaScript*, facilitando la creación de las distintas vistas o páginas utilizando componentes *Razor*
- **SQLServer**: sistema de gestion de base de datos relacional
- **MudBlazor**: libreria para la creación de componentes, sencilla de usar y que agilizó el diseño e implementación de componentes
- **Bootstrap**: framework de *CSS* usado para el estilado de la aplicación 
- **AutoMapper**: libreria usada para el mapeo y transferencia de datos entre objetos de clases diferentes
- **EntityFramework**: permite crear la capa de acceso a los datos y traspasar las entidades definidas en el dominio de la aplicación a una base de datos en SqlServer
- **IdentityFramework**: brinda herramientas para implementar el registro, autenticacion y autorizacion de usuarios

### Mejoras a futuro
- **Exportación como PDF**: nos gustaría que el usuario sea capaz de exportar sus notas como PDF
- **Problemas de estabilidad**: aun hay ciertos problemas de estabilidad en la conexión y autenticación que deben ser revisados
- **Pulido del estilo visual**: hay ciertos componentes cuyos estilos pueden mejorarse
