<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="IngresarProveedor.aspx.cs" Inherits="ProyectoFinal.Proveedores.IngresarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container needs-validation" style="margin-top: 15px;" novalidate>
        <div class="card card-custom">
            <div class="card-header text-center">
                <h5 class="card-title">Ingreso de Proveedor</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control" required />
                            <label for="txtPrimerNombre">Nombre</label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" required/>
                            <label for="txtDireccion">Dirección</label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" required/>
                            <label for="txtDireccion">Telefono Principal</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtTelefonoAlt" runat="server" CssClass="form-control" />
                            <label for="txtTelefono">Teléfono alterno</label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" required/>
                            <label for="txtDesc">Descripcion</label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating">
                            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="True" required></asp:DropDownList>
                            <label for="ddlDepartamento">Departamento</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating">
                            <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select" required></asp:DropDownList>
                            <label for="ddlMunicipio">Municipio</label>
                        </div>
                    </div>
                </div>

                <div class="form-check mb-3 text-center">
                    <asp:Label ID="Label1" runat="server" Text="¿El proveedor tendrá disponibilidad de crédito?" CssClass="form-label d-block mb-2" />
                    <div class="d-inline-flex align-items-center">
                        <asp:CheckBox ID="chkCredito" runat="server" CssClass="form-check-input me-2" required />
                        <label class="form-check-label" for="chkCredito">Crédito</label>
                    </div>
                </div>

                <div class="card-footer text-start">
                    <asp:Button ID="btnEnviar" runat="server" Text="Guardar" CssClass="btn btn-outline-primary" OnClick="btnEnviar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para preguntar si desea agregar una cuenta bancaria -->
    <div class="modal fade" id="cuentaModal" tabindex="-1" aria-labelledby="cuentaModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title w-100 text-center" id="cuentaModalLabel">Agregar Cuenta Bancaria</h5>
                </div>
                <div class="modal-body text-center">
                    ¿Deseas agregar una cuenta bancaria?
       
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <asp:Button ID="btnSiCuenta" runat="server" Text="Sí" CssClass="btn btn-outline-primary flex-fill mx-1" OnClick="btnSiCuenta_Click" />
                    <asp:Button ID="btnNoCuenta" runat="server" Text="Más tarde" CssClass="btn btn-outline-info flex-fill mx-1" OnClick="btnNoCuenta_Click" />
                </div>
            </div>
        </div>
    </div>


    <!-- Modal para agregar cuenta bancaria -->
    <div class="modal fade" id="agregarCuentaModal" tabindex="-1" aria-labelledby="agregarCuentaModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title w-100 text-center" id="agregarCuentaModalLabel">Agregar Cuenta Bancaria</h5>
                </div>
                <div class="modal-body">
                    <div class="card">
                        <div class="card-body">
                            <div class="mb-3 form-floating">
                                <asp:TextBox ID="TxtNBanco" runat="server" CssClass="form-control" placeholder="Banco"></asp:TextBox>
                                <label for="TxtNBanco">Banco</label>
                            </div>
                            <div class="mb-3 form-floating">
                                <asp:TextBox ID="TxtBoxNoCuenta" runat="server" CssClass="form-control" placeholder="Número de Cuenta"></asp:TextBox>
                                <label for="TxtBoxNoCuenta">Número de Cuenta</label>
                            </div>
                            <div class="mb-3 form-floating">
                                <asp:TextBox ID="TxtBoxDes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Descripción de Cuenta"></asp:TextBox>
                                <label for="TxtBoxDes">Descripción de Cuenta</label>
                            </div>
                            <div class="mb-3 form-floating">
                                <asp:TextBox ID="TxtBoxPropietario" runat="server" CssClass="form-control" placeholder="Nombre del Propietario"></asp:TextBox>
                                <label for="TxtBoxPropietario">Nombre del Propietario</label>
                            </div>
                            <div class="mb-3 form-floating">
                                <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="form-select"></asp:DropDownList>
                                <label for="ddlTipoCuenta">Tipo Cuenta</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-outline-success flex-fill mx-1" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btn btn-outline-info flex-fill mx-1" data-bs-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Cliente existente -->
    <div class="modal fade" id="proveedorExistenteModal" tabindex="-1" aria-labelledby="proveedorExistenteLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title w-100 text-center" id="proveedorExistenteLabel">Proveedor Existente</h5>
                </div>
                <div class="modal-body text-center">
                    <!-- Etiqueta para mostrar el NIT, nombres y apellidos -->
                    <asp:Label ID="lblDatosProveedorExistente" runat="server" Text=""></asp:Label>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <!-- Botón para Editar -->
                    <asp:Button ID="btnEditarProveedor" runat="server" Text="¿Deseas Editarlo?" CssClass="btn btn-outline-primary flex-fill mx-1" OnClick="btnEditarCliente_Click" />

                    <!-- Botón para Cerrar el modal -->
                    <button type="button" class="btn btn-outline-secondary flex-fill mx-1" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
