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
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        public string ModalClass { get; set; } = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);
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
                // Mostrar SweetAlert de bienvenida con redirección
                string alertScript = $"setTimeout(function() {{ " +
                    $"{SweetAlertUtils.ShowSuccess($"¡Hola, {nombreCompleto}!", "Has iniciado sesión correctamente!")} " +
                    $"setTimeout(function() {{ window.location.href = 'inicioF.aspx'; }}, 1000); " +
                    $"}} , 10);";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowWelcomeAlert", alertScript, true);
                Session["UserName"] = nombreCompleto;
            }
            else
            {
                limpiar();
                // Mostrar SweetAlert de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorAlert",
                    SweetAlertUtils.ShowError("Error!", "Usuario o contraseña incorrectos."), true);
            }
        }

        private bool AuthenticateUser(string user, string password, out string NombreCompleto)
        {
            NombreCompleto = null;

            var UserPass = from u in mibd.Empleados
                           where user == u.user && password == u.password
                           select new { 
                               User = u.user,
                               Pass = u.password,
                               nombreEmpleado = u.nombre1+" "+u.apellido1
                           };


            if (UserPass.FirstOrDefault() != null)
            {
                NombreCompleto = UserPass.ToList()[0].nombreEmpleado.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}