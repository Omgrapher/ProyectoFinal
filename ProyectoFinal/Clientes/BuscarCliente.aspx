 <%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="BuscarCliente.aspx.cs" Inherits="ProyectoFinal.BuscarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 15px;">
        <div class="card card-custom">
            <div class="card-header text-center">
                <h5 class="card-title">Busqueda de Cliente</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="card-title">Buscar Cliente:</h5>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="TextBoxBuscar" CssClass="form-control" runat="server" placeholder="Nombre / NIT" />
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click">
                            <i class="bi bi-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- GridView Aquí -->
                            <asp:GridView ID="GridViewResultado" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="2" Visible="false" OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnSelectedIndexChanged="GridViewResultado_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="NIT" HeaderText="NIT" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombres" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellidos" />
                                    <asp:ButtonField Text="Seleccionar" CommandName="Select" ButtonType="Button">
                                        <ItemStyle CssClass="text-center" />
                                        <ControlStyle CssClass="btn btn-outline-info" />
                                    </asp:ButtonField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" FirstPageText="Primero" LastPageText="Ultimo"/>
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center"/>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container" style="margin-top: 5px;">
        <div class="card card-custom">
            <div class="card-header text-center">
                <h5 class="card-title">Edición de Cliente</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control" required/>
                            <label for="txtPrimerNombre">Primer Nombre</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control" required/>
                            <label for="txtSegundoNombre">Segundo Nombre</label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control" required/>
                            <label for="txtPrimerApellido">Primer Apellido</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control" required/>
                            <label for="txtSegundoApellido">Segundo Apellido</label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                            <label for="txtDireccion">Dirección</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" required/>
                            <label for="txtTelefono">Teléfono</label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" required/>
                            <label for="txtEmail">Email</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="txtNIT" runat="server" CssClass="form-control" TextMode="Number" required/>
                            <label for="txtNIT">NIT</label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-floating">
                            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <label for="ddlDepartamento">Departamento</label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating">
                            <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select"></asp:DropDownList>
                            <label for="ddlMunicipio">Municipio</label>
                        </div>
                    </div>
                </div>

                <div class="form-check mb-3 text-center">
                    <asp:Label ID="Label1" runat="server" Text="¿El cliente tendrá disponibilidad de crédito?" CssClass="form-label d-block mb-2" />
                    <div class="d-inline-flex align-items-center">
                        <asp:CheckBox ID="chkCredito" runat="server" CssClass="form-check-input me-2" />
                        <label class="form-check-label" for="chkCredito">Crédito</label>
                    </div>
                </div>

                <div class="card-footer text-start">
                    <asp:Button ID="btnEnviar" runat="server" Text="Editar" CssClass="btn btn-outline-primary" OnClick="btnEnviar_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para preguntar si desea editar al cliente-->
<div class="modal fade" id="editarModal" tabindex="-1" aria-labelledby="editarModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title w-100 text-center" id="editarModalLabel">Editar Cliente</h5>
            </div>
            <div class="modal-body text-center">
                ¿Deseas editar a este cliente?
       
            </div>
            <div class="modal-footer d-flex justify-content-center">
                <asp:Button ID="btnSi" runat="server" Text="Sí" CssClass="btn btn-outline-success flex-fill mx-1" OnClick="btnSi_Click" />
                <asp:Button ID="btnNo" runat="server" Text="Más tarde" CssClass="btn btn-outline-info flex-fill mx-1" OnClick="btnNo_Click"/>
            </div>
        </div>
    </div>
</div>
</asp:Content>
