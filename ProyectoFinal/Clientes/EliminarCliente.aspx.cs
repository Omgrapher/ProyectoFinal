using Parcial2.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal
{
    public partial class EliminarCliente : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);
        private void limpiar()
        {
            TextBoxBuscar.Text = "";
            GridViewResultado.Visible = false;

        }
        private void CargarDatos(int pageIndex)
        {
            var buscar = from b in mibd.Clientes
                         where (b.nombre1_cliente.Contains(TextBoxBuscar.Text) ||
                                b.nit_cliente.Contains(TextBoxBuscar.Text) ||
                                b.nombre2_cliente.Contains(TextBoxBuscar.Text))
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
        public static string nit = "";
        protected void GridViewResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            nit = GridViewResultado.SelectedRow.Cells[0].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShoweliminarModal", "var myModal = new bootstrap.Modal(document.getElementById('eliminarModal')); myModal.show();", true);
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos(0);
        }
        protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarDatos(e.NewPageIndex);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = (from c in mibd.Clientes
                               where c.nit_cliente == nit && c.estado == true
                               select c).FirstOrDefault();

                if (cliente != null)
                {
                    cliente.estado = false;

                    mibd.SubmitChanges();

                    Swal.Fire("El cliente ha sido Eliminado exitosamente.", "Cliente Eliminado",SwalIcon.Success);
                    limpiar();
                }
                else 
                {
                    Swal.Fire("El cliente ya no se encuentra vigente", "Cliente ya eliminado",SwalIcon.Info);
                }
            }
            catch (Exception ex)
            {
                limpiar();
                string errorMessage = "Ha ocurrido un error al intentar eliminar el cliente: " + ex.Message;
                Swal.Fire(errorMessage, "Error", SwalIcon.Error);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}