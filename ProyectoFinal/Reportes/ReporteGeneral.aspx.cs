using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal.Reportes
{
    public partial class ReporteGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReporteClientes_Click(object sender, EventArgs e)
        {
            // Limpiar los campos del formulario si es necesario
            txtNombreCliente.Text = string.Empty;
            txtApellidoCliente.Text = string.Empty;
            txtNitCliente.Text = string.Empty;
            ddlMunicipio.SelectedIndex = 0;
            ddlCredito.SelectedIndex = 0;

            // Registrar el script para mostrar el modal
            string script = "var myModal = new bootstrap.Modal(document.getElementById('" +
                           reporteClienteModal.ClientID + "')); myModal.show();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", script, true);
        }
    }
}