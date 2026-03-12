Raiz del Proyecto
<img width="682" height="596" alt="image" src="https://github.com/user-attachments/assets/49a67934-be1b-45d7-b31e-33af263d3d19" />
<img width="703" height="604" alt="image" src="https://github.com/user-attachments/assets/b589d183-4f6a-4b17-b5d4-a3912558f01e" />
<img width="737" height="466" alt="image" src="https://github.com/user-attachments/assets/bf41c5a3-bf5a-4458-9805-f9a756996f78" />
<img width="723" height="419" alt="image" src="https://github.com/user-attachments/assets/be78f711-3090-4e80-a81d-c649f2e16db6" />
🍽️ Restaurante Peru Sabor
Sistema de gestión para restaurante desarrollado en C# WPF con arquitectura en capas, base de datos SQL Server mediante Entity Framework Core 8 y generación de comprobantes en PDF.

📋 Descripción
Aplicación de escritorio para la gestión integral de un restaurante: control de mesas, registro de pedidos, cobro con descuentos y generación automática de boletas y facturas electrónicas en PDF con logo personalizado.

✨ Funcionalidades

🔐 Autenticación con roles (Administrador / Cajero) desde base de datos
🪑 Recepción — registro de clientes y asignación de mesas
🍲 Pedidos — menú interactivo con platos y bebidas, carrito en tiempo real
💰 Cobro — aplicación de descuentos, selección de comprobante
🧾 PDF automático — boleta o factura con logo, detalle de items e IGV
📊 Reportes — ventas del día con montos y comprobantes
🗄️ Persistencia — toda la información guardada en SQL Server


🛠️ Tecnologías
TecnologíaUsoC# · .NET 8.0Lenguaje y plataformaWPF (XAML)Interfaz gráfica de escritorioEntity Framework Core 8ORM para acceso a datosSQL Server ExpressBase de datos relacionalQuestPDF 2024Generación de PDFPatrón RepositorySeparación de lógica de datos
🗄️ Base de datos
5 tablas en SQL Server:
Usuarios → Clientes → Pedidos → DetallePedidos
                              → Ventas
🚀 Instalación y uso
Requisitos previos

Visual Studio 2022+
.NET 8.0 SDK
SQL Server Express
SQL Server Management Studio (SSMS)
Pasos
1. Clonar el repositorio
git clone https://github.com/ElTioCode/RestaurantePeruSabor.git
2. Crear la base de datos
Abrir SSMS, conectarse a localhost\SQLEXPRESS y ejecutar el script:
Database/crear_bd_restaurante.sql
3. Verificar la cadena de conexión
En Data/RestauranteContext.cs ajustar el nombre del servidor si es necesario:
csharp@"Server=TU_SERVIDOR\SQLEXPRESS;Database=RestaurantePeruSabor;
  Trusted_Connection=True;TrustServerCertificate=True;"
4. Instalar paquetes NuGet
bashdotnet restore
5. Ejecutar
Presionar F5 en Visual Studio.
Credenciales de prueba
Usuario  Clave      Rol           
admin    1234   Administrador
cajero   0000     Cajero
📄 Comprobantes PDF
Los PDFs se generan automáticamente en:
Documentos/RestaurantePeruSabor/Comprobantes/
Incluyen: logo del restaurante, RUC, datos del cliente, detalle de items, IGV (18%) y total.

👨‍💻 Autor
Alexander · Universidad Tecnológica del Perú (UTP)
GitHub: ElTioCode
