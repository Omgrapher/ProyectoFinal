<%@ Page Title="" Language="C#" MasterPageFile="~/bienvenida.Master" AutoEventWireup="true" CodeBehind="ReporteGeneral.aspx.cs" Inherits="ProyectoFinal.Reportes.ReporteGeneral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <a href="#" class="text-decoration-none" data-bs-toggle="modal" data-bs-target="#modalReporter1">
                    <div class="card h-100 text-center card_custom1">
                        <div class="card-body">
                            <i class="bi bi-person-circle display-4 mb-3"></i>
                            <h5 class="card-title">Clientes</h5>
                            <p class="card-text">Descripcion</p>
                        </div>
                    </div>
                </a>
            </div>

            <!-- Card 2 -->
            <div class="col">
                <a href="#" class="text-decoration-none" data-bs-toggle="modal" data-bs-target="#modalReporter2">
                    <div class="card h-100 text-center card_custom1">
                        <div class="card-body">
                            <i class="bi bi-person-circle display-4 mb-3"></i>
                            <h5 class="card-title">Proveedores</h5>
                            <p class="card-text">Descripcion</p>
                        </div>
                    </div>
                </a>
            </div>

            <!-- Card 3 -->
            <div class="col">
                <a href="#" class="text-decoration-none" data-bs-toggle="modal" data-bs-target="#modalReporter3">
                    <div class="card h-100 text-center card_custom1">
                        <div class="card-body">
                            <i class="bi bi-person-circle display-4 mb-3"></i>
                            <h5 class="card-title">Ventas</h5>
                            <p class="card-text">Descripcion</p>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>

</asp:Content>
