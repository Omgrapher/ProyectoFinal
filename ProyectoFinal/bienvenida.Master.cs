using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    public partial class bienvenida : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                string userName = Session["UserName"].ToString();
                lblUserName.Text = "Hola, " + userName;
            }
        }

        protected void OnClickButtonCerrarSesion(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect(ResolveUrl("~/default.aspx"));
        }
    }
}