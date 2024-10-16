<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProyectoFinal._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Inicio de Sesión</title>
    <link href="CSS/bootstrap-5.3.3-dist(3)/bootstrap-5.3.3-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="CSS/bootstrap-icons-1.11.3/bootstrap-icons-1.11.3/font/bootstrap-icons.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" />
    <link href="CSS/EstilosLogin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate>
        <asp:ScriptManager runat="server" />
        <div class="container">
            <div class="login-container">
                <div class="d-flex justify-content-center align-items-center mb-3">
                    <i class="bi bi-shop me-2" style="font-size: 4rem;"></i>
                </div>

                <div class="input-group mb-3 has-validation">
                    <span class="input-group-text" id="basic-addon1"><i class="bi bi-person-fill"></i></span>
                    <div class="form-floating flex-grow-1">
                        <asp:TextBox ID="UserTextBox" CssClass="form-control" placeholder="Introduce tu usuario" runat="server" required />
                        <label for="UserTextBox" class="form-label">Usuario:</label>
                    </div>

                </div>

                <div class="input-group mb-3 has-validation">
                    <span class="input-group-text" id="basic-addon2"><i class="bi bi-lock-fill"></i></span>
                    <div class="form-floating flex-grow-1">
                        <asp:TextBox ID="PasswordTextBox" CssClass="form-control" TextMode="Password" placeholder="Contraseña" runat="server" required />
                        <label for="PasswordTextBox" class="form-label">Contraseña:</label>
                    </div>
                </div>

                <!-- Botón de inicio de sesión -->
                <div class="d-flex justify-content-center" style="padding-top:10px;">
                    <asp:Button ID="LoginButton" CssClass="btn btn-light" Text="Iniciar Sesión" OnClick="LoginButton_Click" runat="server"/>
                </div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"></script>
        <script src="CSS/bootstrap-5.3.3-dist(3)/bootstrap-5.3.3-dist/js/bootstrap.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <script src="JS/Login.js"></script>
    </form>
</body>
</html>

