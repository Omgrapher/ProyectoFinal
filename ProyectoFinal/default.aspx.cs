using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace ProyectoFinal
{
    public partial class _default : System.Web.UI.Page
    {
        string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void limpiar()
        {
            UserTextBox.Text = PasswordTextBox.Text = "";
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string nombreCompleto;
            bool isAuthenticated = AuthenticateUser(UserTextBox.Text,PasswordTextBox.Text, out nombreCompleto);


            if (isAuthenticated)
            {
                limpiar();
                // Mostrar modal de bienvenida
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowWelcomeModal", "var myModal = new bootstrap.Modal(document.getElementById('welcomeModal')); myModal.show();", true);
            }
            else
            {
                limpiar();
                // Mostrar modal de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorModal", "var myModal = new bootstrap.Modal(document.getElementById('errorModal')); myModal.show();", true);
            }
        }

        private bool AuthenticateUser(string user, string password, out string NombreCompleto)
        {
            NombreCompleto = null; 

            if (user == "Marlon" && password == "1234")
            {
                NombreCompleto = "Marlon";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}