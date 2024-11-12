<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="ReporteGeneral.aspx.cs" Inherits="ProyectoFinal.Reportes.ReporteGeneral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hover-card {
            transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
        }

            .hover-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 0.5rem 1rem rgba(0,0,0,0.15) !important;
            }

        .icon-wrapper {
            background-color: rgba(13, 110, 253, 0.1);
            padding: 1.5rem;
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
        }

        .pointer-cursor {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row mb-4">
            <div class="col text-center">
                <h1 class="display-4">Reportes</h1>
            </div>
        </div>
        <div class="row row-cols-1 row-cols-md-3 g-4">
            <!-- Card 1 -->
            <div class="col">
                <!-- Primero el botón ASP.NET -->
                <asp:Button ID="btnReporteClientes" runat="server"
                    CssClass="d-none"
                    OnClick="btnReporteClientes_Click" />

                <!-- Luego el card que actuará como botón visual -->
                <div class="card h-100 shadow-sm hover-card pointer-cursor"
                    onclick="document.getElementById('<%= btnReporteClientes.ClientID %>').click();">
                    <div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
                        <div class="icon-wrapper mb-3">
                            <i class="bi bi-people-fill display-4 text-primary"></i>
                        </div>
                        <h5 class="card-title fw-bold mb-2">Reporte de Clientes</h5>
                        <p class="card-text text-muted mb-3">Genera informes detallados de tu cartera de clientes</p>
                        <%--<div class="mt-auto">
                            <span class="btn btn-outline-primary btn-sm">
                                <i class="bi bi-file-earmark-text me-2"></i>Generar Reporte
                            </span>
                        </div>--%>
                    </div>
                </div>
            </div>
            <!-- Card Proveedores -->
            <div class="col">
                <!-- Botón ASP.NET de Proveedores -->
                <asp:Button ID="btnReporteProveedores" runat="server"
                    CssClass="d-none"
                    OnClick="btnReporteProveedores_Click" />

                <!-- Card que actúa como botón visual -->
                <div class="card h-100 shadow-sm hover-card pointer-cursor"
                    onclick="document.getElementById('<%= btnReporteProveedores.ClientID %>').click();">
                    <div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
                        <div class="icon-wrapper mb-3">
                            <i class="bi bi-people-fill display-4 text-primary"></i>
                        </div>
                        <h5 class="card-title fw-bold mb-2">Reporte de Proveedores</h5>
                        <p class="card-text text-muted mb-3">Genera informes detallados de tus proveedores</p>
                        <%--<div class="mt-auto">
                            <span class="btn btn-outline-primary btn-sm">
                                <i class="bi bi-file-earmark-text me-2"></i>Generar Reporte
                            </span>
                        </div>--%>
                    </div>
                </div>
            </div>


            <!-- Card Ventas -->
            <div class="col">
                <!-- Botón ASP.NET de Proveedores -->
                <asp:Button ID="btnReporteVentas" runat="server"
                    CssClass="d-none"
                    OnClick="btnReporteVentas_Click" />

                <!-- Card que actúa como botón visual -->
                <div class="card h-100 shadow-sm hover-card pointer-cursor"
                    onclick="document.getElementById('<%= btnReporteVentas.ClientID %>').click();">
                    <div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
                        <div class="icon-wrapper mb-3">
                            <i class="bi bi-people-fill display-4 text-primary"></i>
                        </div>
                        <h5 class="card-title fw-bold mb-2">Reporte de Ventas</h5>
                        <p class="card-text text-muted mb-3">Genera informes detallados de tus ventas</p>
                        <%--<div class="mt-auto">
                            <span class="btn btn-outline-primary btn-sm">
                                <i class="bi bi-file-earmark-text me-2"></i>Ver Filtros
                            </span>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Reporte Cliente -->
 <div class="modal fade" id="reporteClienteModal" runat="server" tabindex="-1" aria-labelledby="reporteClienteModalLabel" aria-hidden="true">
     <div class="modal-dialog modal-dialog-centered">
         <div class="modal-content">
             <div class="modal-header">
                 <h5 class="modal-title" id="reporteClienteModalLabel">Generar Reporte de Clientes</h5>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <div class="mb-3 form-floating">
                     <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" placeholder="Nombre del Cliente"></asp:TextBox>
                     <asp:Label AssociatedControlID="txtNombreCliente" runat="server">Nombre del Cliente</asp:Label>
                 </div>
                 <div class="mb-3 form-floating">
                     <asp:TextBox ID="txtApellidoCliente" runat="server" CssClass="form-control" placeholder="Apellido del Cliente"></asp:TextBox>
                     <asp:Label AssociatedControlID="txtApellidoCliente" runat="server">Apellido del Cliente</asp:Label>
                 </div>
                 <div class="mb-3 form-floating">
                     <asp:TextBox ID="txtNitCliente" runat="server" CssClass="form-control" placeholder="NIT del Cliente"></asp:TextBox>
                     <asp:Label AssociatedControlID="txtNitCliente" runat="server">NIT del Cliente</asp:Label>
                 </div>
                 <div class="mb-3 form-floating">
                     <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select">
                     </asp:DropDownList>
                     <asp:Label AssociatedControlID="ddlMunicipio" runat="server">Municipio</asp:Label>
                 </div>
                 <div class="mb-3 form-floating">
                     <asp:DropDownList ID="ddlCredito" runat="server" CssClass="form-select">
                     </asp:DropDownList>
                     <asp:Label AssociatedControlID="ddlCredito" runat="server">Crédito</asp:Label>
                 </div>
             </div>
             <div class="modal-footer justify-content-between">
                 <!-- Botón de Generar Reporte a la derecha -->
                 <asp:Button ID="btnGenerarReporte" runat="server" CssClass="btn btn-primary"
                     Text="Generar Reporte" OnClick="btnGenerarReporte_Click" />
                 <!-- Botón de Cerrar a la izquierda -->
                 <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                     <i class="bi bi-x-circle me-2"></i>Cerrar
                 </button>

             </div>
         </div>
     </div>
 </div>
    <!-- Reporte Ventas -->
    <div class="modal fade" id="reporteVentasModal" runat="server" tabindex="-1" aria-labelledby="reporteVentasModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="reporteVentasModalLabel">Generar Reporte de Ventas</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <!-- Filtro de Fecha Inicio -->
                <div class="mb-3 form-floating">
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" placeholder="Fecha de Inicio" TextMode="Date"></asp:TextBox>
                    <asp:Label AssociatedControlID="txtFechaInicio" runat="server">Fecha de Inicio</asp:Label>
                </div>
                
                <!-- Filtro de Fecha Fin -->
                <div class="mb-3 form-floating">
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" placeholder="Fecha de Fin" TextMode="Date"></asp:TextBox>
                    <asp:Label AssociatedControlID="txtFechaFin" runat="server">Fecha de Fin</asp:Label>
                </div>
            </div>
            <div class="modal-footer justify-content-between">
                <!-- Botón de Generar Reporte a la derecha -->
                <asp:Button ID="btnGenerarReporteVentas" runat="server" CssClass="btn btn-primary"
                    Text="Generar Reporte" OnClick="btnGenerarReporteVentas_Click"/>
                <!-- Botón de Cerrar a la izquierda -->
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    <i class="bi bi-x-circle me-2"></i>Cerrar
                </button>
            </div>
        </div>
    </div>
</div>
</asp:Content>
