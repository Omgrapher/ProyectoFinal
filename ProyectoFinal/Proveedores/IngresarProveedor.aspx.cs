using Parcial2.Util;
using ProyectoFinal.BaseDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal.Proveedores
{
    public partial class IngresarProveedor : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);

        protected void limpiar()
        {
            txtPrimerNombre.Text = txtTelefonoAlt.Text =
                txtDireccion.Text = txtTelefono.Text = "";
            ddlDepartamento.SelectedIndex = 0;
            ddlMunicipio.SelectedIndex = 0;
            ddlTipoCuenta.SelectedIndex = 0;
            chkCredito.Checked = false;
        }
        protected void cargarDepa()
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
        protected void cargarTipoCuenta()
        {
            var tc = from c in mibd.Tipo_Cuentas
                     select new
                     {
                         id = c.id_tipo_cuenta,
                         nombre = c.nombre_tipo_cuenta

                     };
            ddlTipoCuenta.DataSource = tc;
            ddlTipoCuenta.DataTextField = "nombre";
            ddlTipoCuenta.DataValueField = "id";
            ddlTipoCuenta.DataBind();
            ddlTipoCuenta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
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
        protected int GenerarCodigoUnico()
        {
            Random random = new Random();
            int codigoUnico;
            bool codigoExiste;
            do
            {
                codigoUnico = random.Next(1000, 9999);
                codigoExiste = mibd.Clientes.Any(c => c.id_cliente == codigoUnico);
            } while (codigoExiste);

            return codigoUnico;
        }

        protected void guardarProveedor()
        {
            // Generar código único para el proveedor
            int codigoProveedor = GenerarCodigoUnico();

                Proveedore nuevoProveedor = new Proveedore()
                {
                    id_proveedores = codigoProveedor,
                    nombre_proveedores = txtPrimerNombre.Text.Trim(),
                    direccion = txtDireccion.Text.Trim(),
                    telefono_proveedor = txtTelefono.Text.Trim(),
                    telefono_alterno_proveedor = txtTelefonoAlt.Text.Trim(),
                    descripcion = txtDesc.Text.Trim(),
                    id_muni = Convert.ToInt32(ddlMunicipio.SelectedValue),
                    id_credito = chkCredito.Checked ? 1 : 0,
                    estado = true
                };

                // Insertar el proveedor en la base de datos
                mibd.Proveedores.InsertOnSubmit(nuevoProveedor);
                mibd.SubmitChanges();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDepa();
                cargarTipoCuenta();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombreProv = txtPrimerNombre.Text.Trim();

            var ProveedorExistente = from p in mibd.Proveedores
                                     where p.nombre_proveedores.Contains(nombreProv) && p.estado == true
                                     select new
                                     {
                                         id = p.id_proveedores,
                                         nombre = p.nombre_proveedores,
                                         telefono = p.telefono_proveedor
                                     };  


            if (ProveedorExistente.FirstOrDefault() != null)
            {

                lblDatosProveedorExistente.Text = $"ID: {ProveedorExistente.ToList()[0].id.ToString()} <br /> " +
                                        $"Nombre: {ProveedorExistente.ToList()[0].nombre.ToString()}, Telefono: {ProveedorExistente.ToList()[0].telefono.ToString()}";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowproveedorExistenteModal", "var myModal = new bootstrap.Modal(document.getElementById('proveedorExistenteModal')); myModal.show();", true);

                limpiar();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowcuentaModal", "var myModal = new bootstrap.Modal(document.getElementById('cuentaModal')); myModal.show();", true);
            }
        }
        protected void btnSiCuenta_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowagregarCuentaModal", "var myModal = new bootstrap.Modal(document.getElementById('agregarCuentaModal')); myModal.show();", true);
        }
        protected void btnNoCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                guardarProveedor();
                Swal.Fire("El proveedor ha sido agregado exitosamente.", "Proveedor Agregado",SwalIcon.Success);
                limpiar();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ha ocurrido un error al intentar guardar al proveedor: " + ex.Message;
                Swal.Fire(errorMessage, "Error", SwalIcon.Error);
                limpiar();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            using (TransactionScope transa = new TransactionScope())
            {
                try
                {
                    int codigoProveedor = GenerarCodigoUnico();

                        Proveedore nuevoProveedor = new Proveedore()
                        {
                            id_proveedores = codigoProveedor,
                            nombre_proveedores = txtPrimerNombre.Text.Trim(),
                            direccion = txtDireccion.Text.Trim(),
                            telefono_proveedor = txtTelefono.Text.Trim(),
                            telefono_alterno_proveedor = txtTelefonoAlt.Text.Trim(),
                            descripcion = txtDesc.Text.Trim(),
                            id_muni = Convert.ToInt32(ddlMunicipio.SelectedValue),
                            id_credito = chkCredito.Checked ? 1 : 0,
                            estado = true
                        };
                        mibd.Proveedores.InsertOnSubmit(nuevoProveedor);
                        mibd.SubmitChanges();

                    int codigoCuenta = GenerarCodigoUnico();


                    Cuentas_Bancarias_Proveedore nuevaCuenta = new Cuentas_Bancarias_Proveedore
                    {
                        id_cuenta_provee = codigoCuenta,
                        banco = TxtNBanco.Text.Trim(),
                        no_cuenta = TxtBoxNoCuenta.Text.Trim(),
                        descripcion_cuenta = TxtBoxDes.Text.Trim(),
                        nombre_propietario_cuenta = TxtBoxPropietario.Text.Trim(),
                        id_tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue),
                        id_proveedores = nuevoProveedor.id_proveedores,
                        estado = true,
                    };

                    mibd.Cuentas_Bancarias_Proveedores.InsertOnSubmit(nuevaCuenta);
                    mibd.SubmitChanges();

                    transa.Complete();
                    limpiar();
                    Swal.Fire("El proveedor y la cuenta han sido agregados exitosamente.", "Proveedor y Cuenta Agregados",SwalIcon.Success);
                }
                catch (Exception ex)
                {
                    transa.Dispose();
                    string errorMessage = "Ha ocurrido un error al intentar guardar al proveedor: " + ex.Message;
                    Swal.Fire(errorMessage, "Error", SwalIcon.Error);
                }
            }
        }
        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            string nombreProv = txtPrimerNombre.Text.Trim();
            var ProveedorExistente = from p in mibd.Proveedores
                                     where p.nombre_proveedores == nombreProv && p.estado == true
                                     select new
                                     {
                                         id = p.id_proveedores,
                                         nombre = p.nombre_proveedores,
                                         apellido = p.apellido_proveedor
                                     };

            int id = ProveedorExistente.ToList()[0].id;
            Response.Redirect("/Proveedores/BuscarProveedor.aspx?id=" + id);
        }
    }
}