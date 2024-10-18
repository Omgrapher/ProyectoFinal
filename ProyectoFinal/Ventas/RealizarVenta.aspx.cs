using Parcial2.Util;
using ProyectoFinal.BaseDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal.Ventas
{
    public partial class RealizarVenta : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);

        private void limpiar()
        {
            txtBuscarCliente.Text = "";
        }
        private void CargarDatosCliente(int pageIndex)
        {
            var buscar = from b in mibd.Clientes
                         where (b.nombre1_cliente.Contains(txtBuscarCliente.Text) ||
                                b.nit_cliente.Contains(txtBuscarCliente.Text) ||
                                b.nombre2_cliente.Contains(txtBuscarCliente.Text))
                               && b.estado == true
                         select new
                         {
                             Nit = b.nit_cliente,
                             Nombre = b.nombre1_cliente + " " + b.nombre2_cliente,
                             Apellido = b.apellido1_cliente + " " + b.apellido2_cliente
                         };

            var resultados = buscar.ToList();

            if (resultados.Count > 0)
            {
                GridViewResultado.PageIndex = pageIndex; // Establecer el índice de la página actual
                GridViewResultado.DataSource = resultados;
                GridViewResultado.DataBind();
                GridViewResultado.Visible = true;
            }
            else
            {
                GridViewResultado.Visible = false; // No mostrar la tabla si no hay resultados
                string errorMessage = "No se encontraron similares...";
                Swal.Fire(errorMessage, "Alerta", SwalIcon.Warning);
                limpiar();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosCliente(0);
        }

        protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarDatosCliente(e.NewPageIndex);
        }
        protected void GridViewResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarCliente")
            {
                // Obtener el NIT del cliente seleccionado
                string nitSeleccionado = e.CommandArgument.ToString();

                // Aquí puedes buscar el cliente en la base de datos para obtener su nombre y apellido
                var clienteSeleccionado = mibd.Clientes.FirstOrDefault(c => c.nit_cliente == nitSeleccionado);
                if (clienteSeleccionado != null)
                {
                    // Mostrar el cliente seleccionado en el Label
                    lblClienteSeleccionado.Text = $"{clienteSeleccionado.nombre1_cliente} {clienteSeleccionado.nombre2_cliente} {clienteSeleccionado.apellido1_cliente} {clienteSeleccionado.apellido2_cliente}";
                    lblClienteSeleccionado.CssClass = ""; // Remover la clase d-none para mostrar el label
                    string script = @"
                        var myToast = new bootstrap.Toast(document.getElementById('toastClienteSeleccionado'));
                         myToast.show();
                         setTimeout(function() {
                            myToast.hide();
                            }, 2000);"; // 3000 milisegundos = 3 segundos

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarToast", script, true);
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewResultado.Visible = false;
                GridView1.Visible = false;
                ViewState["ProductosSeleccionados"] = new List<dynamic>(); // Inicializar el ViewState
            }
        }

        private void CargarDatosProducto(int pageIndex)
        {
            var buscar = from b in mibd.Productos
                         join i in mibd.Inventarios on b.id_producto equals i.id_producto
                         where b.Tipo_Producto.nombre.Contains(txtBuscarProducto.Text) || b.id_producto.ToString().Contains(txtBuscarProducto.Text)
                         select new
                         {
                             IdProduct = b.id_producto,
                             Producto = b.Tipo_Producto.nombre,
                             Stock = i.cant_disponible,
                             Materiales = b.Material_Producto.nombre_material,
                             Precio_Venta = i.precio_venta
                         };

            var resultados = buscar.ToList();

            var productosFormateados = resultados.Select(p => new
            {
                p.IdProduct,
                p.Producto,
                p.Stock,
                p.Materiales,
                Precio_Venta = "Q " + Math.Round(p.Precio_Venta).ToString() // Redondear y formatear con "Q"
            }).ToList();

            if (productosFormateados.Count > 0)
            {
                GridView1.PageIndex = pageIndex; // Establecer el índice de la página actual
                GridView1.DataSource = productosFormateados;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Visible = false; // No mostrar la tabla si no hay resultados
                string errorMessage = "No se encontraron similares...";
                Swal.Fire(errorMessage, "Alerta", SwalIcon.Warning);
                limpiar();
            }
        }
        protected void btnBuscarP_Click(object sender, EventArgs e)
        {
            CargarDatosProducto(0);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarDatosProducto(e.NewPageIndex);
        }
        private static string id = "";
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarProducto")
            {
                id = e.CommandArgument.ToString();

                var productoSeleccionado = mibd.Productos.FirstOrDefault(p => p.id_producto == Convert.ToInt32(id));
                if (productoSeleccionado != null)
                {
                    // Mostrar el cliente seleccionado en el Label
                    LabelProductoSeleccionado.Text = $"Producto Seleccionado: {productoSeleccionado.Tipo_Producto.nombre}, Material:  {productoSeleccionado.Material_Producto.nombre_material}";
                    LabelProductoSeleccionado.CssClass = ""; // Remover la clase d-none para mostrar el label



                    string script = @"
                        var myToast = new bootstrap.Toast(document.getElementById('toastProductoSeleccionado'));
                         myToast.show();
                         setTimeout(function() {
                            myToast.hide();
                            }, 2000);";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarToast", script, true);
                }
            }
        }
    }
}