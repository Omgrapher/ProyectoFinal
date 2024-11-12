<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="BuscarProducto.aspx.cs" Inherits="ProyectoFinal.Productos.BuscarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 15px;">
    <div class="card card-custom">
        <div class="card-header text-center">
            <h5 class="card-title">Busqueda de Producto</h5>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <h5 class="card-title">Buscar Producto:</h5>
                    <div class="input-group mb-3">
                        <asp:TextBox ID="TextBoxBuscar" CssClass="form-control" runat="server" placeholder="Nombre / ID" />
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
                        <asp:GridView ID="GridView1Producto" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                            AllowPaging="true" PageSize="10" Visible="false" OnPageIndexChanging="GridView1_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="IdProduct" HeaderText="id" Visible="false" />
                                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                <asp:BoundField DataField="Materiales" HeaderText="Materiales" />
                                <asp:BoundField DataField="Precio_Venta" HeaderText="Precio Venta" />
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSeleccionar" runat="server" Text="Seleccionar" CssClass="btn btn-outline-info btn-sm">
                                            <i class="bi bi-plus-circle"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" FirstPageText="Primero" LastPageText="Ultimo" />
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
