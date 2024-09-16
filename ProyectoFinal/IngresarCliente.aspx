<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="IngresarCliente.aspx.cs" Inherits="ProyectoFinal.IngresarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top:5px;">
    <div class="card card-custom">
        <div class="card-header text-center">
            <h5 class="card-title">Ingreso de Cliente</h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control" />
                        <label for="txtPrimerNombre">Primer Nombre</label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control" />
                        <label for="txtSegundoNombre">Segundo Nombre</label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control" />
                        <label for="txtPrimerApellido">Primer Apellido</label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control" />
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
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                        <label for="txtTelefono">Teléfono</label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <label for="txtEmail">Email</label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtNIT" runat="server" CssClass="form-control" TextMode="Number" />
                        <label for="txtNIT">NIT</label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select" aria-label="Municipio">
                        </asp:DropDownList>
                        <label for="ddlMunicipio">Municipio</label>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-select" aria-label="Departamento">
                        </asp:DropDownList>
                        <label for="ddlDepartamento">Departamento</label>
                    </div>
                </div>
            </div>

            <div class="form-check mb-3 text-center">
                <asp:Label ID="Label1" runat="server" Text="¿El cliente tendrá disponibilidad de crédito?" CssClass="form-label d-block mb-2" />
                <div class="d-inline-flex align-items-center">
                    <asp:CheckBox ID="chkCredito" runat="server" CssClass="form-check-input me-2" />
                    <label class="form-check-label" for="chkCredito">
                        Crédito
                    </label>
                </div>
            </div>

            <div class="card-footer text-start">
                <asp:Button ID="btnEnviar" type="submit" runat="server" Text="Guardar" CssClass="btn btn-outline-primary" OnClick="btnEnviar_Click" />
            </div>
        </div>
    </div>
    </div>
</asp:Content>
