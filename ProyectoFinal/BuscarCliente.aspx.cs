using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal
{
    public partial class BuscarCliente : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var buscar = from b in mibd.Clientes
                         where b.nombre1_cliente.Contains(TextBoxBuscar.Text) || b.nit_cliente.Contains(TextBoxBuscar.Text) ||
                         b.nombre2_cliente.Contains(TextBoxBuscar.Text)
                         select new
                         {
                             Nit = b.nit_cliente,
                             Nombre = b.nombre1_cliente + " " + b.nombre2_cliente,
                             Apellido = b.apellido1_cliente + " " + b.apellido2_cliente
                         };
            if (buscar.FirstOrDefault() != null)
            {
                GridViewResultado.DataSource = buscar;
                GridViewResultado.DataBind();
                GridViewResultado.Visible = true;
            }
        }
    }
}