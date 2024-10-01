using ProyectoFinal.BaseDatos;
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
        protected void limpiar()
        {
            txtPrimerNombre.Text = txtSegundoNombre.Text = txtPrimerApellido.Text = txtSegundoApellido.Text =
                txtDireccion.Text = txtTelefono.Text = txtNIT.Text = txtEmail.Text = "";
            ddlDepartamento.SelectedIndex = 0;
            ddlMunicipio.SelectedIndex = 0;
            chkCredito.Checked = false;
        }
        private void CargarDatos(int pageIndex)
        {
            var buscar = from b in mibd.Clientes
                         where b.nombre1_cliente.Contains(TextBoxBuscar.Text) ||
                               b.nit_cliente.Contains(TextBoxBuscar.Text) ||
                               b.nombre2_cliente.Contains(TextBoxBuscar.Text)
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
            }
        }

        private void LlenarDepartamentos()
        {
            var departamento = from d in mibd.Departamentos
                               select new
                               {
                                   id = d.id_departamento,
                                   nombre = d.nombre_departamento
                               };
            ddlDepartamento.DataSource = departamento;
            ddlDepartamento.DataTextField = "nombre";
            ddlDepartamento.DataValueField = "id";
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        private void LlenarMunicipio()
        {
            int codDepa = Convert.ToInt32(ddlDepartamento.SelectedValue);
            var muni = from m in mibd.Municipios
                       where m.id_departamento.Equals(codDepa)
                       select new
                       {
                           id = m.id_muni,
                           nombre = m.nombre_muni
                       };
            ddlMunicipio.DataSource = muni;
            ddlMunicipio.DataTextField = "nombre";
            ddlMunicipio.DataValueField = "id";
            ddlMunicipio.DataBind();
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarMunicipio();
        }
        private void CargarDatosCliente()
        {
            string nit = Request.QueryString["nit"];
            if (!string.IsNullOrEmpty(nit))
            {
                var cargar = from c in mibd.Clientes
                             where c.nit_cliente == nit
                             select new
                             {
                                 nombre1 = c.nombre1_cliente,
                                 nombre2 = c.nombre2_cliente,
                                 apellido1 = c.apellido1_cliente,
                                 apellido2 = c.apellido2_cliente,
                                 direc = c.direccion,
                                 tel = c.telefono,
                                 emai = c.email,
                                 muni = c.Municipio.id_muni,
                                 credito = c.id_credito
                             };
                LlenarDepartamentos();
                var muni = from m in mibd.Municipios
                           where m.id_muni.Equals(cargar.ToList()[0].muni)
                           select new
                           {
                               id = m.id_muni,
                               nombre = m.nombre_muni
                           };
                ddlMunicipio.DataSource = muni;
                ddlMunicipio.DataTextField = "nombre";
                ddlMunicipio.DataValueField = "id";
                ddlMunicipio.DataBind();
                var munici = from m in mibd.Municipios
                             where m.id_muni == Convert.ToInt32(cargar.ToList()[0].muni)
                             select new
                             {
                                 idDepa = m.id_departamento,
                                 nombre = m.Departamento.nombre_departamento
                             };


                string depa = munici.ToList()[0].idDepa.ToString();
                string idmuni = cargar.ToList()[0].muni.ToString();

                if (cargar.FirstOrDefault() != null)
                {
                    txtPrimerNombre.Text = cargar.ToList()[0].nombre1;
                    txtSegundoNombre.Text = cargar.ToList()[0].nombre2;
                    txtPrimerApellido.Text = cargar.ToList()[0].apellido1;
                    txtSegundoApellido.Text = cargar.ToList()[0].apellido2;
                    txtDireccion.Text = cargar.ToList()[0].direc;
                    txtTelefono.Text = cargar.ToList()[0].tel;
                    txtEmail.Text = cargar.ToList()[0].emai;
                    txtNIT.Text = nit;

                    ddlDepartamento.SelectedValue = depa;
                    ddlMunicipio.SelectedValue = idmuni;
                    if (cargar.ToList()[0].credito.ToString() == "1")
                    {
                        chkCredito.Checked = true;
                    }
                    else
                    {
                        chkCredito.Checked = false;
                    }                    // Marca o desmarca el CheckBox
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewResultado.Visible = false;
                LlenarDepartamentos();
                CargarDatosCliente();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos(0);
        }

        protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarDatos(e.NewPageIndex);
        }

        protected void GridViewResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nit = GridViewResultado.SelectedRow.Cells[0].Text;
            if (!string.IsNullOrEmpty(nit))
            {
                var cargar = from c in mibd.Clientes
                             where c.nit_cliente == nit
                             select new
                             {
                                 nombre1 = c.nombre1_cliente,
                                 nombre2 = c.nombre2_cliente,
                                 apellido1 = c.apellido1_cliente,
                                 apellido2 = c.apellido2_cliente,
                                 direc = c.direccion,
                                 tel = c.telefono,
                                 emai = c.email,
                                 muni = c.Municipio.id_muni,
                                 credito = c.id_credito
                             };
                LlenarDepartamentos();
                var muni = from m in mibd.Municipios
                           where m.id_muni.Equals(cargar.ToList()[0].muni)
                           select new
                           {
                               id = m.id_muni,
                               nombre = m.nombre_muni
                           };
                ddlMunicipio.DataSource = muni;
                ddlMunicipio.DataTextField = "nombre";
                ddlMunicipio.DataValueField = "id";
                ddlMunicipio.DataBind();
                var munici = from m in mibd.Municipios
                             where m.id_muni == Convert.ToInt32(cargar.ToList()[0].muni)
                             select new
                             {
                                 idDepa = m.id_departamento,
                                 nombre = m.Departamento.nombre_departamento
                             };


                string depa = munici.ToList()[0].idDepa.ToString();
                string idmuni = cargar.ToList()[0].muni.ToString();

                if (cargar.FirstOrDefault() != null)
                {
                    txtPrimerNombre.Text = cargar.ToList()[0].nombre1;
                    txtSegundoNombre.Text = cargar.ToList()[0].nombre2;
                    txtPrimerApellido.Text = cargar.ToList()[0].apellido1;
                    txtSegundoApellido.Text = cargar.ToList()[0].apellido2;
                    txtDireccion.Text = cargar.ToList()[0].direc;
                    txtTelefono.Text = cargar.ToList()[0].tel;
                    txtEmail.Text = cargar.ToList()[0].emai;
                    txtNIT.Text = nit;

                    ddlDepartamento.SelectedValue = depa;
                    ddlMunicipio.SelectedValue = idmuni;
                    if (cargar.ToList()[0].credito.ToString() == "1")
                    {
                        chkCredito.Checked = true;
                    }
                    else
                    {
                        chkCredito.Checked = false;
                    } 
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShoweditarModalModal", "var myModal = new bootstrap.Modal(document.getElementById('editarModal')); myModal.show();", true);
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                string nit = txtNIT.Text.Trim();

                var cliente = (from c in mibd.Clientes
                               where c.nit_cliente == nit
                               select c).FirstOrDefault();

                if (cliente != null)
                {
                    cliente.nombre1_cliente = txtPrimerNombre.Text.Trim();
                    cliente.nombre2_cliente = txtSegundoNombre.Text.Trim();
                    cliente.apellido1_cliente = txtPrimerApellido.Text.Trim();
                    cliente.apellido2_cliente = txtSegundoApellido.Text.Trim();
                    cliente.direccion = txtDireccion.Text.Trim();
                    cliente.telefono = txtTelefono.Text.Trim();
                    cliente.email = txtEmail.Text.Trim();
                    cliente.id_muni = Convert.ToInt32(ddlMunicipio.SelectedValue);

                    cliente.id_credito = chkCredito.Checked ? 1 : 0;

                    mibd.SubmitChanges();

                    string script = SweetAlertUtils.ShowSuccess("Cliente Editado", "El cliente ha sido editado exitosamente.");
                    ClientScript.RegisterStartupScript(this.GetType(), "ClienteEditado", script, true);
                    limpiar();
                }
            }
            catch (Exception ex)
            {
                limpiar();
                string errorMessage = "Ha ocurrido un error al intentar editar el cliente: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('" + errorMessage + "');", true);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}