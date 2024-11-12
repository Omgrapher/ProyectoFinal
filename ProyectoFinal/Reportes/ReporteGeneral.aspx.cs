using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.XSSF.UserModel;  // Para archivos .xlsx
using NPOI.SS.UserModel;
using ProyectoFinal.BaseDatos;    // Para interfaces comunes
using System.IO;  // Para MemoryStream
using System.Configuration;
using Parcial2.Util;


namespace ProyectoFinal.Reportes
{
    public partial class ReporteGeneral : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);

        protected void limpiar()
        {
            txtNombreCliente.Text = txtNitCliente.Text = txtApellidoCliente.Text = "";
            txtFechaInicio.Text = txtFechaFin.Text = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        //REPORTE VENTAS
        public class VentaReporte
        {
            public DateTime FechaVenta { get; set; }
            public string Cliente { get; set; }
            public string Producto { get; set; }
            public string TipoProducto { get; set; }
            public string FormaPago { get; set; }
            public string Empleado { get; set; }
            public decimal TotalVenta { get; set; }
            public decimal PrecioVenta { get; set; }
            public string Descripcion { get; set; }
            public decimal Subtotal { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioCosto { get; set; }
        }
        protected void btnReporteVentas_Click(object sender, EventArgs e)
        {
            // Registrar el script para mostrar el modal
            string script = "var myModal = new bootstrap.Modal(document.getElementById('" +
                           reporteVentasModal.ClientID + "')); myModal.show();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", script, true);
        }
        public class ReporteVentaGenerator
        {
            private readonly milinqDataContext db;

            public ReporteVentaGenerator(milinqDataContext context)
            {
                db = context;
            }
            public byte[] GenerarReporteVentas(DateTime? fechaInicio = null, DateTime? fechaFin = null)
            {
                // Si no se proporcionan fechas, se omiten los filtros
                var query = from enc in db.Enca_Ventas
                            join cli in db.Clientes on enc.id_cliente equals cli.id_cliente
                            join det in db.Detalle_Ventas on enc.id_venta equals det.id_venta
                            join prod in db.Productos on det.id_producto equals prod.id_producto
                            join tipoProd in db.Tipo_Productos on prod.id_tipo_producto equals tipoProd.id_tipo_producto
                            join formaPago in db.Forma_Pagos on det.id_forma_pago equals formaPago.id_forma_pago
                            join emp in db.Empleados on det.id_empleado equals emp.id_empleado
                            select new VentaReporte
                            {
                                FechaVenta = enc.fecha_venta,
                                Cliente = cli.nombre1_cliente + " " + cli.apellido1_cliente,
                                Producto = tipoProd.nombre,
                                TipoProducto = tipoProd.nombre,
                                FormaPago = formaPago.nombre,
                                Empleado = emp.nombre1 + " " + emp.apellido1,
                                TotalVenta = enc.total_venta,
                                PrecioVenta = det.precio_venta,
                                Descripcion = det.descripcion_venta,
                                Subtotal = det.subtotal,
                                Cantidad = Convert.ToInt32(det.cantidad),
                                PrecioCosto = det.precio_costo
                            };

                if (fechaInicio.HasValue && fechaFin.HasValue)
                {
                    query = query.Where(enc => Convert.ToDateTime(enc.FechaVenta)>= fechaInicio.Value &&  Convert.ToDateTime(enc.FechaVenta) <= fechaFin.Value);
                }

                // Obtener todos los resultados de la consulta
                var resultados = query.ToList();

                foreach (var venta in resultados)
                {
                    venta.FechaVenta = DateTime.Parse(venta.FechaVenta.ToString("yyyy-MM-dd")); // Convertir a string después de la consulta
                }
                // Generar el archivo Excel con los resultados obtenidos
                return GenerarExcel(resultados);
            }

            // Método para generar el archivo Excel
            private byte[] GenerarExcel(List<VentaReporte> ventas)
            {
                using (var workbook = new XSSFWorkbook())
                {
                    var sheet = workbook.CreateSheet("Reporte de Ventas");

                    // Crear estilo para el encabezado
                    var headerStyle = workbook.CreateCellStyle();
                    var font = workbook.CreateFont();
                    font.IsBold = true;
                    headerStyle.SetFont(font);
                    headerStyle.FillForegroundColor = IndexedColors.LightBlue.Index;
                    headerStyle.FillPattern = FillPattern.SolidForeground;

                    font.Color = IndexedColors.White.Index;

                    // Crear encabezados
                    var headerRow = sheet.CreateRow(0);
                    var headers = new[] { "Fecha Venta", "Cliente", "Producto", "Tipo Producto",
                          "Forma de Pago", "Empleado", "Total Venta",
                          "Precio Venta", "Descripción", "Subtotal", "Cantidad", "Precio Costo" };

                    // Agregar los encabezados en el archivo Excel
                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = headerRow.CreateCell(i);
                        cell.SetCellValue(headers[i]);
                    }

                    // Agregar los datos de cada venta
                    int rowNum = 1;
                    foreach (var venta in ventas)
                    {
                        var row = sheet.CreateRow(rowNum++);
                        row.CreateCell(0).SetCellValue(venta.FechaVenta.ToString("yyyy-MM-dd"));

                        row.CreateCell(1).SetCellValue(venta.Cliente ?? string.Empty);
                        row.CreateCell(2).SetCellValue(venta.Producto ?? string.Empty);
                        row.CreateCell(3).SetCellValue(venta.TipoProducto ?? string.Empty);
                        row.CreateCell(4).SetCellValue(venta.FormaPago ?? string.Empty);
                        row.CreateCell(5).SetCellValue(venta.Empleado ?? string.Empty);
                        row.CreateCell(6).SetCellValue(venta.TotalVenta.ToString());
                        row.CreateCell(7).SetCellValue(venta.PrecioVenta.ToString());
                        row.CreateCell(8).SetCellValue(venta.Descripcion ?? string.Empty);
                        row.CreateCell(9).SetCellValue(venta.Subtotal.ToString());
                        row.CreateCell(10).SetCellValue(venta.Cantidad);
                        row.CreateCell(11).SetCellValue(venta.PrecioCosto.ToString());
                    }

                    // Ajustar el tamaño de las columnas
                    for (int i = 0; i < headers.Length; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    // Convertir a bytes para la descarga
                    using (var ms = new MemoryStream())
                    {
                        workbook.Write(ms);
                        return ms.ToArray();
                    }
                }
            }
        }
        protected void btnGenerarReporteVentas_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener las fechas de inicio y fin del reporte
                DateTime? fechaInicio = null;
                DateTime? fechaFin = null;

                // Verificar si se ingresaron fechas, en caso contrario usar null
                if (!string.IsNullOrEmpty(txtFechaInicio.Text))
                {
                    fechaInicio = DateTime.Parse(txtFechaInicio.Text);
                }

                if (!string.IsNullOrEmpty(txtFechaFin.Text))
                {
                    fechaFin = DateTime.Parse(txtFechaFin.Text);
                }

                // Generar el reporte de ventas
                using (var context = new milinqDataContext(conec))
                {
                    var generator = new ReporteVentaGenerator(context);
                    byte[] excelBytes = generator.GenerarReporteVentas(fechaInicio, fechaFin);

                    // Enviar el archivo al cliente
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteVentas.xlsx");
                    Response.BinaryWrite(excelBytes);
                    Response.End();
                    //limpiar();
                }
            }
            catch (Exception ex)
            {
                limpiar();
                Swal.Fire("Error al generar el reporte: " + ex.Message, "Error", SwalIcon.Error);
            }
        }


        //REPORTE PROVEEDORES
        public class ProveedorReporte
        {
            public string NombreProveedor { get; set; }
            public string Direccion { get; set; }
            public string TelefonoProveedor { get; set; }
            public string TelefonoAlternoProveedor { get; set; }
            public string Descripcion { get; set; }
            public string Municipio { get; set; }
            public string EstadoCredito { get; set; }
            public string Estado { get; set; }
        }
        public class ReporteProveedorGenerator
        {
            private readonly milinqDataContext db;

            public ReporteProveedorGenerator(milinqDataContext context)
            {
                db = context;
            }

            // Método para generar el reporte de proveedores
            public byte[] GenerarReporteProveedores()
            {
                // Consulta LINQ para obtener la información de los proveedores con las relaciones necesarias
                var query = from prov in db.Proveedors
                            join muni in db.Municipios on prov.id_muni equals muni.id_muni
                            join ec in db.Estado_Creditos on prov.id_credito equals ec.id_credito
                            select new ProveedorReporte
                            {
                                NombreProveedor = prov.nombre_proveedores,
                                Direccion = prov.direccion,
                                TelefonoProveedor = prov.telefono_proveedor,
                                TelefonoAlternoProveedor = prov.telefono_alterno_proveedor,
                                Descripcion = prov.descripcion,
                                Municipio = muni.nombre_muni,
                                EstadoCredito = ec.nombre,
                                Estado = prov.estado ? "Activo" : "Inactivo"
                            };

                // Obtener todos los resultados de la consulta
                var resultados = query.ToList();

                // Generar el archivo Excel con los resultados obtenidos
                return GenerarExcel(resultados);
            }

            // Método para generar el archivo Excel
            private byte[] GenerarExcel(List<ProveedorReporte> proveedores)
            {
                using (var workbook = new XSSFWorkbook())
                {
                    var sheet = workbook.CreateSheet("Reporte de Proveedores");

                    // Crear estilo para el encabezado
                    var headerStyle = workbook.CreateCellStyle();
                    var font = workbook.CreateFont();
                    font.IsBold = true;
                    headerStyle.SetFont(font);
                    headerStyle.FillForegroundColor = IndexedColors.LightBlue.Index;
                    headerStyle.FillPattern = FillPattern.SolidForeground;

                    font.Color = IndexedColors.White.Index;

                    // Centrar el contenido horizontal y verticalmente
                    headerStyle.Alignment = HorizontalAlignment.Center;  // Centrar horizontalmente
                    headerStyle.VerticalAlignment = VerticalAlignment.Center;  // Centrar verticalmente

                    // Establecer márgenes de celda (si es necesario)
                    headerStyle.LeftBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde izquierdo
                    headerStyle.RightBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde derecho
                    headerStyle.TopBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde superior
                    headerStyle.BottomBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde inferior
                    headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                    // Crear encabezados
                    var headerRow = sheet.CreateRow(0);
                    var headers = new[] { "Nombre Proveedor", "Dirección", "Teléfono Proveedor",
                                          "Teléfono Alterno Proveedor", "Descripción",
                                          "Municipio", "Estado Crédito", "Estado" };

                    // Agregar los encabezados en el archivo Excel
                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = headerRow.CreateCell(i);
                        cell.SetCellValue(headers[i]);
                    }

                    // Agregar los datos de cada proveedor
                    int rowNum = 1;
                    foreach (var proveedor in proveedores)
                    {
                        var row = sheet.CreateRow(rowNum++);
                        row.CreateCell(0).SetCellValue(proveedor.NombreProveedor ?? string.Empty);
                        row.CreateCell(1).SetCellValue(proveedor.Direccion ?? string.Empty);
                        row.CreateCell(2).SetCellValue(proveedor.TelefonoProveedor ?? string.Empty);
                        row.CreateCell(3).SetCellValue(proveedor.TelefonoAlternoProveedor ?? string.Empty);
                        row.CreateCell(4).SetCellValue(proveedor.Descripcion ?? string.Empty);
                        row.CreateCell(5).SetCellValue(proveedor.Municipio ?? string.Empty);
                        row.CreateCell(6).SetCellValue(proveedor.EstadoCredito ?? string.Empty);
                        row.CreateCell(7).SetCellValue(proveedor.Estado ?? string.Empty);
                    }

                    // Ajustar el tamaño de las columnas
                    for (int i = 0; i < headers.Length; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    // Convertir a bytes para la descarga
                    using (var ms = new MemoryStream())
                    {
                        workbook.Write(ms);
                        return ms.ToArray();
                    }
                }
            }
        }
        protected void btnReporteProveedores_Click(object sender, EventArgs e)
        {
            try
            {
                // Generar el reporte de proveedores
                using (var context = new milinqDataContext(conec))
                {
                    var generator = new ReporteProveedorGenerator(context);
                    byte[] excelBytes = generator.GenerarReporteProveedores();

                    // Enviar el archivo al cliente
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteProveedores.xlsx");
                    Response.BinaryWrite(excelBytes);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error con SweetAlert (si está configurado)
                Swal.Fire("Error al generar el reporte: " + ex.Message, "Error", SwalIcon.Error);
            }
        }
        


        //REPORTE CLIENTES
        public class ClienteReporte
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
            public string NIT { get; set; }
            public string Municipio { get; set; }
            public string EstadoCredito { get; set; }
            public string Estado { get; set; }
            public int id_muni { get; internal set; }
            public int id_credito { get; internal set; }
        }

        public class ReporteClienteGenerator
        {
            private readonly milinqDataContext db; 

            public ReporteClienteGenerator(milinqDataContext context)
            {
                db = context;
            }

            public byte[] GenerarReporteClientes(string nombreCliente, string apellidoCliente,
                string nitCliente, int? municipioId, int? creditoId)
            {
                // Consulta LINQ
                var query = from c in db.Clientes
                            join m in db.Municipios on c.id_muni equals m.id_muni
                            join ec in db.Estado_Creditos on c.id_credito equals ec.id_credito
                            select new ClienteReporte
                            {
                                Nombres = (c.nombre1_cliente + " " + (c.nombre2_cliente ?? "")).Trim(),
                                Apellidos = (c.apellido1_cliente + " " + (c.apellido2_cliente ?? "")).Trim(),
                                Direccion = c.direccion,
                                Telefono = c.telefono,
                                Email = c.email,
                                NIT = c.nit_cliente,
                                Municipio = m.nombre_muni,
                                EstadoCredito = ec.nombre,
                                Estado = c.estado ? "Activo" : "Inactivo"
                            };
                // Obtener todos los resultados de la consulta
                var resultados = query.ToList();

                // Generar el archivo Excel con todos los clientes
                return GenerarExcel(resultados);
            }

            private byte[] GenerarExcel(List<ClienteReporte> clientes)
            {
                using (var workbook = new XSSFWorkbook())
                {
                    var sheet = workbook.CreateSheet("Reporte de Clientes");

                    // Crear estilo para el encabezado
                    var headerStyle = workbook.CreateCellStyle();
                    var font = workbook.CreateFont();
                    font.IsBold = true;
                    headerStyle.SetFont(font);
                    headerStyle.FillForegroundColor = IndexedColors.LightBlue.Index; 
                    headerStyle.FillPattern = FillPattern.SolidForeground;  

                    font.Color = IndexedColors.White.Index;

                    // Centrar el contenido horizontal y verticalmente
                    headerStyle.Alignment = HorizontalAlignment.Center;  // Centrar horizontalmente
                    headerStyle.VerticalAlignment = VerticalAlignment.Center;  // Centrar verticalmente

                    // Establecer márgenes de celda (si es necesario)
                    headerStyle.LeftBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde izquierdo
                    headerStyle.RightBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde derecho
                    headerStyle.TopBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde superior
                    headerStyle.BottomBorderColor = IndexedColors.Black.Index;  // Establecer color para el borde inferior
                    headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                    // Crear encabezados
                    var headerRow = sheet.CreateRow(0);
                    var headers = new[] { "Nombres", "Apellidos", "Dirección", "Teléfono",
                                "Email", "NIT", "Municipio", "Estado Crédito", "Estado" };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = headerRow.CreateCell(i);
                        cell.SetCellValue(headers[i]);
                        cell.CellStyle = headerStyle;
                    }

                    // Agregar datos
                    int rowNum = 1;
                    foreach (var cliente in clientes)
                    {
                        var row = sheet.CreateRow(rowNum++);
                        row.CreateCell(0).SetCellValue(cliente.Nombres ?? string.Empty);
                        row.CreateCell(1).SetCellValue(cliente.Apellidos ?? string.Empty);
                        row.CreateCell(2).SetCellValue(cliente.Direccion ?? string.Empty);
                        row.CreateCell(3).SetCellValue(cliente.Telefono ?? string.Empty);
                        row.CreateCell(4).SetCellValue(cliente.Email ?? string.Empty);
                        row.CreateCell(5).SetCellValue(cliente.NIT ?? string.Empty);
                        row.CreateCell(6).SetCellValue(cliente.Municipio ?? string.Empty);
                        row.CreateCell(7).SetCellValue(cliente.EstadoCredito ?? string.Empty);
                        row.CreateCell(8).SetCellValue(cliente.Estado ?? string.Empty);
                    }

                    // Autoajustar columnas
                    for (int i = 0; i < headers.Length; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    // Convertir a bytes
                    using (var ms = new MemoryStream())
                    {
                        workbook.Write(ms);
                        return ms.ToArray();
                    }
                }
            }
        }

        protected void btnReporteClientes_Click(object sender, EventArgs e)
        {
            // Limpiar los campos del formulario si es necesario
            txtNombreCliente.Text = string.Empty;
            txtApellidoCliente.Text = string.Empty;
            txtNitCliente.Text = string.Empty;
            ddlMunicipio.SelectedIndex = 0;
            ddlCredito.SelectedIndex = 0;
            try
            {
                // Obtener los valores de los filtros
                string nombreCliente = txtNombreCliente.Text.Trim();
                string apellidoCliente = txtApellidoCliente.Text.Trim();
                string nitCliente = txtNitCliente.Text.Trim();

                int? municipioId = !string.IsNullOrEmpty(ddlMunicipio.SelectedValue)
                           ? (int?)Convert.ToInt32(ddlMunicipio.SelectedValue)
                           : null;

                int? creditoId = !string.IsNullOrEmpty(ddlCredito.SelectedValue)
                                 ? (int?)Convert.ToInt32(ddlCredito.SelectedValue)
                                 : null;

                // Generar el reporte
                using (var context = new milinqDataContext(conec)) // Tu contexto de Entity Framework
                {
                    var generator = new ReporteClienteGenerator(context);
                    byte[] excelBytes = generator.GenerarReporteClientes(
                        nombreCliente, apellidoCliente, nitCliente, municipioId, creditoId);

                    // Enviar el archivo al cliente
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteClientes.xlsx");
                    Response.BinaryWrite(excelBytes);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Swal.Fire("Error para generar el reporte" + ex.Message, "Error", SwalIcon.Error);

            }
            
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // Obtener los valores de los filtros
            //    string nombreCliente = txtNombreCliente.Text.Trim();
            //    string apellidoCliente = txtApellidoCliente.Text.Trim();
            //    string nitCliente = txtNitCliente.Text.Trim();

            //    int? municipioId = !string.IsNullOrEmpty(ddlMunicipio.SelectedValue)
            //               ? (int?)Convert.ToInt32(ddlMunicipio.SelectedValue)
            //               : null;

            //    int? creditoId = !string.IsNullOrEmpty(ddlCredito.SelectedValue)
            //                     ? (int?)Convert.ToInt32(ddlCredito.SelectedValue)
            //                     : null;

            //    // Generar el reporte
            //    using (var context = new milinqDataContext(conec)) // Tu contexto de Entity Framework
            //    {
            //        var generator = new ReporteClienteGenerator(context);
            //        byte[] excelBytes = generator.GenerarReporteClientes(
            //            nombreCliente, apellidoCliente, nitCliente, municipioId, creditoId);

            //        // Enviar el archivo al cliente
            //        Response.Clear();
            //        Response.Buffer = true;
            //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        Response.AddHeader("content-disposition", "attachment;filename=ReporteClientes.xlsx");
            //        Response.BinaryWrite(excelBytes);
            //        Response.End();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Swal.Fire("Error para generar el reporte"+ex.Message, "Error", SwalIcon.Error);

            //}
        }

        
    }
}