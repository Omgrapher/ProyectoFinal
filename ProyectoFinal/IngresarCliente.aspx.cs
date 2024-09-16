using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal
{
    public partial class IngresarCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string script = SweetAlertUtils.ShowConfirm("Confirmar Envío", "¿Estás seguro de que deseas enviar los datos?");
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlertConfirm", script, true);
        }
    }
}