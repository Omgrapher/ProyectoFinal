using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal.Proveedores
{
    public partial class EliminarProveedor : System.Web.UI.Page
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
            var buscar = from b in mibd.Proveedores
                         where (b.nombre_proveedores.Contains(TextBoxBuscar.Text))
                               && b.estado == true
                         select new
                         {
                             ID = b.id_proveedores,
                             Nombre = b.nombre_proveedores,
                             Telefono = b.telefono_proveedor
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
                string script = SweetAlertUtils.ShowAlert("Alerta", errorMessage);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", script, true);
                limpiar();
            }
        }
        public static string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridViewResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = GridViewResultado.SelectedRow.Cells[0].Text;
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

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                var proveedor = (from p in mibd.Proveedores
                               where p.id_proveedores == Convert.ToInt32(id) && p.estado == true
                               select p).FirstOrDefault();

                if (proveedor != null)
                {
                    proveedor.estado = false;

                    mibd.SubmitChanges();

                    string script = SweetAlertUtils.ShowSuccess("Proveedor Eliminado", "El proveedor ha sido Eliminado exitosamente.");
                    ClientScript.RegisterStartupScript(this.GetType(), "proveedorEliminado", script, true);
                    limpiar();
                }
                else
                {
                    string script = SweetAlertUtils.ShowAlert("proveedor ya eliminado", "El proveedor ya no se encuentra vigente");
                    ClientScript.RegisterStartupScript(this.GetType(), "proveedorEliminado", script, true);
                }
            }
            catch (Exception ex)
            {
                limpiar();
                string errorMessage = "Ha ocurrido un error al intentar eliminar el proveedor: " + ex.Message;
                string script = SweetAlertUtils.ShowError("Error", errorMessage);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {

        }
    }
}