using Parcial2.Util;
using ProyectoFinal.BaseDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
        private void CargarDatos(int pageIndex)
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
            CargarDatos(0);
        }

        protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarDatos(e.NewPageIndex);
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
            if(!IsPostBack)
                GridViewResultado.Visible = false;
        }
    }
}