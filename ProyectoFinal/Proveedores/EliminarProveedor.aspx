<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="EliminarProveedor.aspx.cs" Inherits="ProyectoFinal.Proveedores.EliminarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 15px;">
        <div class="card card-custom">
            <div class="card-header text-center">
                <h5 class="card-title">Eliminación de de Proveedor</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h5 class="card-title">Buscar Proveedor:</h5>
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
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombres" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                                    <asp:ButtonField Text="Eliminar" CommandName="Select" ButtonType="Button">
                                        <ItemStyle CssClass="text-center" />
                                        <ControlStyle CssClass="btn btn-outline-danger" />
                                    </asp:ButtonField>
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

    <!-- Modal para preguntar si desea eliminar al proveedor-->
    <div class="modal fade" id="eliminarModal" tabindex="-1" aria-labelledby="eliminarModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title w-100 text-center" id="eliminarModallLabel">Elimnar Proveedor</h5>
                </div>
                <div class="modal-body text-center">
                    ¿Deseas eliminar a este proveedor?
   
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <asp:Button ID="btnSi" runat="server" Text="Sí" CssClass="btn btn-outline-success flex-fill mx-1" OnClick="btnSi_Click" />
                    <asp:Button ID="btnNo" runat="server" Text="Más tarde" CssClass="btn btn-outline-secondary flex-fill mx-1" OnClick="btnNo_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
