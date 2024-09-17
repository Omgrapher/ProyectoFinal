<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="BuscarCliente.aspx.cs" Inherits="ProyectoFinal.BuscarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 5px;">
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
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-outline-secondary">
                                <i class="bi bi-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- GridView Aquí -->
                            <asp:GridView ID="GridViewResultado" CssClass="table table-striped" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="2" OnPageIndexChanging="GridViewResultado_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombres" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellidos" />
                                    <asp:ButtonField Text="Seleccionar" CommandName="Select" ButtonType="Button">
                                        <ItemStyle CssClass="text-center" />
                                        <ControlStyle CssClass="btn btn-outline-primary" />
                                    </asp:ButtonField>
                                </Columns>
                                <PagerStyle CssClass="pagination"/>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
