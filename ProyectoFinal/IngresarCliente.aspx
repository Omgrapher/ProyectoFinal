<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="IngresarCliente.aspx.cs" Inherits="ProyectoFinal.IngresarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card card-custom">
            <div class="card-header text-center">
                <h5 class="card-title">Formulario de Información</h5>
            </div>
            <div class="card-body">
                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control" />
                    <label for="txtPrimerNombre">Primer Nombre</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control" />
                    <label for="txtSegundoNombre">Segundo Nombre</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control" />
                    <label for="txtPrimerApellido">Primer Apellido</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control" />
                    <label for="txtSegundoApellido">Segundo Apellido</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                    <label for="txtDireccion">Dirección</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    <label for="txtTelefono">Teléfono</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    <label for="txtEmail">Email</label>
                </div>

                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtNIT" runat="server" CssClass="form-control" TextMode="Number" />
                    <label for="txtNIT">NIT</label>
                </div>

                <div class="mb-3">
                    <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                    <label for="ddlMunicipio">Municipio</label>
                </div>

                <div class="mb-3">
                    <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                    <label for="ddlDepartamento">Departamento</label>
                </div>

                <div class="form-check mb-3">
                    <asp:CheckBox ID="chkCredito" runat="server" CssClass="form-check-input" />
                    <label class="form-check-label" for="chkCredito">Crédito</label>
                </div>

                <div class="card-footer">
                    <button type="submit" class="btn btn-primary">Enviar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
