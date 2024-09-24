using ProyectoFinal.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Transactions;

namespace ProyectoFinal
{
    public partial class IngresarCliente : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);
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
            ddlTipoCuenta.DataValueField= "id";
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

            // Generar un número aleatorio de 4 dígitos
            int codigoUnico = random.Next(1000, 9999);

            return codigoUnico;
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
            string nitCliente = txtNIT.Text.Trim();

            var clienteExistente = (from c in mibd.Clientes
                                    where c.nit_cliente == nitCliente
                                    select c).FirstOrDefault();

            if (clienteExistente != null)
            {
                string script = SweetAlertUtils.ShowAlert("Cliente Existente", "El cliente con este NIT ya existe.");
                ClientScript.RegisterStartupScript(this.GetType(), "ClienteExistente", script, true);
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
                int codigoAlumno = GenerarCodigoUnico();

                Cliente nuevoCliente = new Cliente
                {
                    id_cliente = codigoAlumno,
                    nombre1_cliente = txtPrimerNombre.Text.Trim(),
                    nombre2_cliente = txtSegundoNombre.Text.Trim(),
                    apellido1_cliente = txtPrimerApellido.Text.Trim(),
                    apellido2_cliente = txtSegundoApellido.Text.Trim(),
                    direccion = txtDireccion.Text.Trim(),
                    telefono = txtTelefono.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    nit_cliente = txtNIT.Text.Trim(),
                    id_muni = Convert.ToInt32(ddlMunicipio.SelectedValue),
                    id_credito = chkCredito.Checked ? 1 : 0,
                    estado = true
                };

                mibd.Clientes.InsertOnSubmit(nuevoCliente);
                mibd.SubmitChanges();

                string script = SweetAlertUtils.ShowSuccess("Cliente Agregado", "El cliente ha sido agregado exitosamente.");
                ClientScript.RegisterStartupScript(this.GetType(), "ClienteAgregado", script, true);

                // Redireccionar a la página de Clientes o a la página que desees
                // Response.Redirect("Clientes.aspx");
            }
            catch (Exception ex)
            {
                string errorMessage = "Ha ocurrido un error al intentar guardar el cliente: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('" + errorMessage + "');", true);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            using (TransactionScope transa = new TransactionScope())
            {
                try
                {
                    // Crear el nuevo cliente
                    int codigoCliente = GenerarCodigoUnico();

                    Cliente nuevoCliente = new Cliente
                    {
                        id_cliente = codigoCliente,
                        nombre1_cliente = txtPrimerNombre.Text.Trim(),
                        nombre2_cliente = txtSegundoNombre.Text.Trim(),
                        apellido1_cliente = txtPrimerApellido.Text.Trim(),
                        apellido2_cliente = txtSegundoApellido.Text.Trim(),
                        direccion = txtDireccion.Text.Trim(),
                        telefono = txtTelefono.Text.Trim(),
                        email = txtEmail.Text.Trim(),
                        nit_cliente = txtNIT.Text.Trim(),
                        id_muni = Convert.ToInt32(ddlMunicipio.SelectedValue),
                        id_credito = chkCredito.Checked ? 1 : 0,
                        estado = true
                    };

                    // Insertar el cliente en la base de datos
                    mibd.Clientes.InsertOnSubmit(nuevoCliente);
                    mibd.SubmitChanges();

                    int codigoCuenta = GenerarCodigoUnico();
                    // Crear la cuenta bancaria
                    Cuenta_Bancaria_Cliente nuevaCuenta = new Cuenta_Bancaria_Cliente
                    {
                        id_cuenta_cliente = codigoCuenta,
                        banco = TxtNBanco.Text.Trim(),
                        no_cuenta = TxtBoxNoCuenta.Text.Trim(),
                        descripcion_cuenta = TxtBoxDes.Text.Trim(),
                        nombre_propietario_cuenta = TxtBoxPropietario.Text.Trim(),
                        id_tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue),
                        id_cliente = nuevoCliente.id_cliente,
                        estado = true// Vincular la cuenta al cliente recién creado
                    };

                    // Insertar la cuenta bancaria en la base de datos
                    mibd.Cuenta_Bancaria_Clientes.InsertOnSubmit(nuevaCuenta);
                    mibd.SubmitChanges();

                    // Si todo va bien, confirmar la transacción
                    transa.Complete();

                    string script = SweetAlertUtils.ShowSuccess("Cliente y Cuenta Agregados", "El cliente y la cuenta han sido agregados exitosamente.");
                    ClientScript.RegisterStartupScript(this.GetType(), "ClienteCuentaAgregados", script, true);
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertir la transacción
                    transa.Dispose();
                    string errorMessage = "Ha ocurrido un error al intentar guardar el cliente y la cuenta: " + ex.Message;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('" + errorMessage + "');", true);
                }
            }
        }
    }
}