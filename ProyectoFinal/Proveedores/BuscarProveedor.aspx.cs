using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal.Proveedores
{
    public partial class BuscarProveedor : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);
        protected void limpiar()
        {
            txtPrimerNombre.Text = txtTelefonoAlt.Text =
                txtDireccion.Text = txtTelefono.Text = "";
            ddlDepartamento.SelectedIndex = 0;
            ddlMunicipio.SelectedIndex = 0;
            chkCredito.Checked = false;
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

        private void CargarDatosProveedor()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                var cargar = from c in mibd.Proveedores
                             where c.id_proveedores == Convert.ToInt32(id)
                             select new
                             {
                                 nombre1 = c.nombre_proveedores,
                                 direc = c.direccion,
                                 tel = c.telefono_proveedor,
                                 telAlt = c.telefono_alterno_proveedor,
                                 desc = c.descripcion,
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
                    txtDireccion.Text = cargar.ToList()[0].direc;
                    txtTelefono.Text = cargar.ToList()[0].tel;
                    txtTelefonoAlt.Text = cargar.ToList()[0].telAlt;
                    txtDesc.Text = cargar.ToList()[0].desc;

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
                CargarDatosProveedor();
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
            string id = GridViewResultado.SelectedRow.Cells[0].Text;
            if (!string.IsNullOrEmpty(id))
            {
                var cargar = from c in mibd.Proveedores
                             where c.id_proveedores == Convert.ToInt32(id)
                             select new
                             {
                                 nombre1 = c.nombre_proveedores,
                                 direc = c.direccion,
                                 tel = c.telefono_proveedor,
                                 telAlt = c.telefono_alterno_proveedor,
                                 desc = c.descripcion,
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
                    txtDireccion.Text = cargar.ToList()[0].direc;
                    txtTelefono.Text = cargar.ToList()[0].tel;
                    txtTelefonoAlt.Text = cargar.ToList()[0].telAlt;
                    txtDesc.Text = cargar.ToList()[0].desc;

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
                string id = GridViewResultado.SelectedRow.Cells[0].Text;

                var proveedor = (from p in mibd.Proveedores
                               where p.id_proveedores == Convert.ToInt32(id)
                               select p).FirstOrDefault();

                if (proveedor != null)
                {
                    proveedor.nombre_proveedores = txtPrimerNombre.Text.Trim();
                    proveedor.direccion = txtDireccion.Text.Trim();
                    proveedor.telefono_proveedor = txtTelefono.Text.Trim();
                    proveedor.telefono_alterno_proveedor = txtTelefonoAlt.Text.Trim();
                    proveedor.descripcion = txtDesc.Text.Trim();
                    proveedor.id_muni = Convert.ToInt32(ddlMunicipio.SelectedValue);

                    proveedor.id_credito = chkCredito.Checked ? 1 : 0;

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