using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Parcial2.Util;

namespace ProyectoFinal
{
    public partial class _default : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        public string ModalClass { get; set; } = string.Empty;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);

        protected void Page_Load(object sender, EventArgs e) { }

        protected void limpiar()
        {
            UserTextBox.Text = PasswordTextBox.Text = "";
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string nombreCompleto;
            int empleadoId; // Para almacenar el id_empleado

            bool isAuthenticated = AuthenticateUser(UserTextBox.Text, PasswordTextBox.Text, out nombreCompleto, out empleadoId);

            if (isAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(UserTextBox.Text, false); // 'false' para no recordar sesión.
                string redirectUrl = "inicioF.aspx";
                limpiar();

                // Mostrar saludo y redirigir con SweetAlert
                Swal.Fire("Has iniciado sesión correctamente", $"¡Hola, {nombreCompleto}!", SwalIcon.Success, redirectUrl);

                // Guardar nombre del usuario y el id_empleado en sesión
                Session["UserName"] = nombreCompleto;
                Session["EmpleadoId"] = empleadoId; // Guardar id_empleado en la sesión
            }
            else
            {
                limpiar();
                Swal.Fire("Usuario o contraseña incorrectos", "¡Error!", SwalIcon.Error);
            }
        }

        private bool AuthenticateUser(string user, string password, out string NombreCompleto, out int EmpleadoId)
        {
            NombreCompleto = null;
            EmpleadoId = 0;

            var userPass = (from u in mibd.Empleados
                            where u.user == user && u.password == password
                            select new
                            {
                                User = u.user,
                                Pass = u.password,
                                nombreEmpleado = u.nombre1 + " " + u.apellido1,
                                EmpleadoId = u.id_empleado // Obtener id_empleado
                            }).FirstOrDefault();

            if (userPass != null)
            {
                NombreCompleto = userPass.nombreEmpleado;
                EmpleadoId = userPass.EmpleadoId; // Asignar id_empleado
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
