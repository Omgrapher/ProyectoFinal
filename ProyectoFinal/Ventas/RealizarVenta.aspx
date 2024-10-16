<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="RealizarVenta.aspx.cs" Inherits="ProyectoFinal.Ventas.RealizarVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-12 col-md-10 col-lg-8 form-container">
            <div class="card shadow">
                <div class="card-header text-center">
                    <h4>Realizar Venta</h4>
                </div>
                <div class="card-body">
                    <!-- Buscador de Cliente -->
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtBuscarCliente" CssClass="form-control" runat="server" placeholder="Nombre / NIT" />
                        <asp:LinkButton ID="btnBuscarCliente" runat="server" CssClass="btn btn-outline-secondary">
                        <i class="bi bi-search"></i>
                    </asp:LinkButton>
                    </div>

                    <!-- MODAL PARA SELECCIONAR CLIENTE -->
                    <div class="modal fade" id="editarModal" tabindex="-1" aria-labelledby="editarModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title w-100 text-center" id="editarModalLabel">Selección de Cliente</h5>
                                </div>
                                <div class="modal-body text-center">
                                    <div class="mb-3 row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="ClienteId" HeaderText="ID" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                                <asp:LinkButton ID="btnSeleccionarCliente" runat="server" CommandName="SeleccionarCliente" CssClass="btn btn-outline-success">
                                                                     <i class="bi bi-check2-circle"></i>
                                                                </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer d-flex justify-content-center">
                                    <asp:Button ID="btnNo" runat="server" Text="Más tarde" CssClass="btn btn-outline-info flex-fill mx-1" />
                                </div>
                            </div>
                        </div>
                    </div>


                    <!-- Mostrar Cliente Seleccionado -->
                    <div class="form-floating mb-3">
                        <asp:Label ID="lblClienteSeleccionado" runat="server" CssClass="form-control"></asp:Label>
                        <label for="lblClienteSeleccionado">Cliente Seleccionado</label>
                    </div>

                    <!-- Buscador de Producto -->
                    <div class="input-group mb-3">
                        <asp:TextBox ID="txtBuscarProducto" CssClass="form-control" runat="server" placeholder="Nombre / Código de Producto" />
                        <asp:LinkButton ID="btnBuscarProducto" runat="server" CssClass="btn btn-outline-secondary">
                        <i class="bi bi-search"></i>
                    </asp:LinkButton>
                    </div>

                    <!-- Tabla de previsualización de productos -->
                    <div class="mb-3 row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvProductos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="ProductoId" HeaderText="ID" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                    <asp:BoundField DataField="CantidadDisponible" HeaderText="Cantidad Disponible" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSeleccionarProducto" runat="server" CommandName="SeleccionarProducto" Text="Seleccionar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <!-- Detalles del producto seleccionado -->
                    <div class="row">
                        <div class="form-floating mb-3 col-sm-6">
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            <label for="txtPrecio">Precio</label>
                        </div>
                        <div class="form-floating mb-3 col-sm-6">
                            <asp:TextBox ID="txtCantidadDisponible" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            <label for="txtCantidadDisponible">Cantidad Disponible</label>
                        </div>
                    </div>

                    <!-- Cantidad a agregar -->
                    <div class="row">
                        <div class="form-floating mb-3 col-sm-6">
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control"></asp:TextBox>
                            <label for="txtCantidad">Cantidad</label>
                        </div>
                        <div class="col-sm-6 mb-3 d-grid">
                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success btn-sm" Text="Agregar Producto" />
                        </div>
                    </div>

                    <!-- Dropdown para seleccionar forma de pago -->
                    <div class="form-floating mb-3">
                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione forma de pago" Value="" />
                            <asp:ListItem Text="Efectivo" Value="Efectivo" />
                            <asp:ListItem Text="Tarjeta de Crédito" Value="Tarjeta" />
                            <asp:ListItem Text="Transferencia" Value="Transferencia" />
                        </asp:DropDownList>
                        <label for="ddlFormaPago">Forma de Pago</label>
                    </div>

                    <!-- Descripción de la venta -->
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        <label for="txtDescripcion">Descripción de la Venta</label>
                    </div>

                    <!-- Grid para mostrar productos agregados -->
                    <div class="mb-3">
                        <asp:GridView ID="gvCarrito" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" EmptyDataText="No hay productos agregados.">
                            <Columns>
                                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Botón para finalizar venta -->
                <div class="card-footer">
                    <asp:Button ID="btnFinalizarVenta" runat="server" CssClass="btn btn-primary" Text="Finalizar Venta" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
