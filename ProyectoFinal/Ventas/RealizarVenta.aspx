<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="RealizarVenta.aspx.cs" Inherits="ProyectoFinal.Ventas.RealizarVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">VENTAS</h2>
        <!-- Título de Ventas -->

        <div class="accordion" id="ventasAccordion">

            <!-- Acordeón para el Buscador de Cliente -->
            <div class="accordion-item">
                <h2 class="accordion-header" id="clienteAccordionHeader">
                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#clienteAccordion" aria-expanded="true" aria-controls="clienteAccordion">
                        Buscador de Cliente
                   
                    </button>
                </h2>
                <div id="clienteAccordion" class="accordion-collapse collapse show" aria-labelledby="clienteAccordionHeader" data-bs-parent="#ventasAccordion">
                    <div class="accordion-body">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtBuscarCliente" CssClass="form-control" runat="server" placeholder="Nombre / NIT" />
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click">
                                <i class="bi bi-search"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="mb-3 row">
                            <div class="col-sm-12">
                                <asp:GridView ID="GridViewResultado" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="2" Visible="false" OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnRowCommand="GridViewResultado_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="NIT" HeaderText="NIT" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombres" />
                                        <asp:BoundField DataField="Apellido" HeaderText="Apellidos" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnSeleccionar" runat="server" CommandName="SeleccionarCliente" CommandArgument='<%# Eval("Nit") %>' Text="Seleccionar" CssClass="btn btn-outline-info btn-sm" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" FirstPageText="Primero" LastPageText="Ultimo" />
                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                        </div>
                        <!-- Mostrar Cliente Seleccionado -->
                        <div class="mb-3 text-center">
                            <asp:Label ID="Label1" runat="server" Text="Cliente Seleccionado:" CssClass="fs-5"></asp:Label>
                            <asp:Label ID="lblClienteSeleccionado" runat="server" CssClass="d-none fs-5 fw-bold"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Acordeón para el Buscador de Producto -->
            <div class="accordion-item">
                <h2 class="accordion-header" id="productoAccordionHeader">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#productoAccordion" aria-expanded="false" aria-controls="productoAccordion">
                        Buscador de Producto
                    </button>
                </h2>
                <div id="productoAccordion" class="accordion-collapse collapse" aria-labelledby="productoAccordionHeader" data-bs-parent="#ventasAccordion">
                    <div class="accordion-body">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtBuscarProducto" CssClass="form-control" runat="server" placeholder="Nombre / Código de Producto" />
                            <asp:LinkButton ID="btnBuscarProducto" runat="server" CssClass="btn btn-outline-secondary">
                            <i class="bi bi-search"></i>
                            </asp:LinkButton>
                        </div>
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
                        <div class="input-group mb-3">
                            <!-- TextBox para la cantidad -->
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Cantidad"></asp:TextBox>

                            <!-- Botón para agregar producto -->
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-success" Text="Agregar Producto" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Acordeón para el Carrito de Productos -->
            <div class="accordion-item">
                <h2 class="accordion-header" id="carritoProductosAccordionHeader">
                    <button class="accordion-button" type="button" aria-expanded="true">
                        Detalle de Venta
       
                    </button>
                </h2>
                <div id="carritoProductosAccordion" class="accordion-collapse show" aria-labelledby="carritoProductosAccordionHeader">
                    <div class="accordion-body">
                        <div class="form-floating mb-3">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Seleccione forma de pago" Value="" />
                                <asp:ListItem Text="Efectivo" Value="Efectivo" />
                                <asp:ListItem Text="Tarjeta de Crédito" Value="Tarjeta" />
                                <asp:ListItem Text="Transferencia" Value="Transferencia" />
                            </asp:DropDownList>
                            <label for="ddlFormaPago">Forma de Pago</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                            <label for="txtDescripcion">Descripción de la Venta</label>
                        </div>
                        <asp:GridView ID="gvProductosSeleccionados" runat="server" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" />
                                <asp:BoundField DataField="Total" HeaderText="Total" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>


        <!-- Toast Cliente Seleccionado -->
        <div class="toast-container position-fixed bottom-0 end-0 p-3">
            <div id="toastClienteSeleccionado" class="toast hide" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="toast-body bg-success text-white text-center">
                    Cliente seleccionado.
                </div>
            </div>
        </div>
</asp:Content>
