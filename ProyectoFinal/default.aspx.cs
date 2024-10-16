using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Parcial2.Util;
using System.Web.Security;


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
                FormsAuthentication.SetAuthCookie(UserTextBox.Text, false); // 'false' para no recordar sesión.
                string redirectUrl = "inicioF.aspx";
                limpiar();

                Swal.Fire("Has iniciado sesión correctamente", $"¡Hola, {nombreCompleto}!", SwalIcon.Success,redirectUrl);

                Session["UserName"] = nombreCompleto;
            }
            else
            {
                limpiar();

                Swal.Fire("Usuario o contraseña incorrectos","¡Error!",SwalIcon.Error);
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