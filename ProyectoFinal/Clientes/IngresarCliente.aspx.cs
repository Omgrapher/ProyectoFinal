﻿using ProyectoFinal.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Transactions;
using Parcial2.Util;

namespace ProyectoFinal
{
    public partial class IngresarCliente : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);
        protected void limpiar()
        {
            txtPrimerNombre.Text = txtSegundoNombre.Text = txtPrimerApellido.Text = txtSegundoApellido.Text =
                txtDireccion.Text = txtTelefono.Text = txtNIT.Text = txtEmail.Text = "";
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

        //protected int GenerarCodigoUnico()
        //{
        //    Random random = new Random();
        //    int codigoUnico;
        //    bool codigoExiste;
        //    do
        //    {
        //        codigoUnico = random.Next(1000, 9999);
        //        codigoExiste = mibd.Clientes.Any(c => c.id_cliente == codigoUnico);
        //    } while (codigoExiste);

        //    return codigoUnico;
        //}

        protected void guardarCliente()
        {

            Cliente nuevoCliente = new Cliente
            {
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

            var clienteExistente = from c in mibd.Clientes
                                   where c.nit_cliente == nitCliente && c.estado == true
                                   select new 
                                   {
                                        Nit = c.nit_cliente,
                                        Nombre = c.nombre1_cliente + " " + c.nombre2_cliente,
                                        Apellido = c.apellido1_cliente + " " + c.apellido2_cliente,
                                   };


            if (clienteExistente.FirstOrDefault() != null)
            {
                lblDatosClienteExistente.Text = $"NIT: {clienteExistente.ToList()[0].Nit.ToString()} <br /> " +
                                        $"Nombre: {clienteExistente.ToList()[0].Nombre.ToString()} {clienteExistente.ToList()[0].Apellido.ToString()}";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowclienteExistenteModal", "var myModal = new bootstrap.Modal(document.getElementById('clienteExistenteModal')); myModal.show();", true);
                
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
                guardarCliente();

                Swal.Fire("El cliente ha sido agregado exitosamente","Cliente Agregado",SwalIcon.Success);
                limpiar();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ha ocurrido un error al intentar guardar el cliente: " + ex.Message;

                Swal.Fire(errorMessage,"Error",SwalIcon.Error);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            using (TransactionScope transa = new TransactionScope())
            {
                try
                {

                    Cliente nuevoCliente = new Cliente
                    {
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

                    // Crear la cuenta bancaria
                    Cuenta_Bancaria_Cliente nuevaCuenta = new Cuenta_Bancaria_Cliente
                    {
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
                    limpiar();

                    Swal.Fire("El cliente y la cuenta han sido agregados exitosamente.", "Cliente y Cuenta Agregados",SwalIcon.Success);
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertir la transacción
                    transa.Dispose();
                    string errorMessage = "Ha ocurrido un error al intentar guardar el cliente: " + ex.Message;
                    Swal.Fire(errorMessage,"Error",SwalIcon.Error);
                }
            }
        }

        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            string nitCliente = txtNIT.Text; // Obtén el valor del NIT del cliente
            Response.Redirect("BuscarCliente.aspx?nit=" + nitCliente);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}