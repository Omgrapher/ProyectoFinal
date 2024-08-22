<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProyectoFinal._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Inicio de Sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="CSS/EstilosLogin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate>
        <asp:ScriptManager runat="server" />
        <div class="container"> 
        <div class="login-container">
            <h2 class="text-center">Librería San Bartolomé</h2>
            <div class="mb-3">
                <label for="email" class="form-label">Usuario:</label>
                <asp:TextBox ID="UserTextBox" CssClass="form-control" placeholder="Introduce tu usuario" runat="server" required/>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Contraseña</label>
                <asp:TextBox ID="PasswordTextBox" CssClass="form-control" TextMode="Password" placeholder="Contraseña" runat="server" required/>
            </div>
            <div class="d-flex justify-content-center">
                <asp:Button ID="LoginButton" CssClass="btn btn-light" type="submit" Text="Iniciar Sesión" OnClick="LoginButton_Click" runat="server" />
            </div>
        </div>
        </div>
        <!-- Modal BIENVENIDA -->
        <div class="modal fade" id="welcomeModal" tabindex="-1" aria-labelledby="welcomeModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="welcomeModalLabel">Bienvenido</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ¡Has iniciado sesión correctamente!
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal ERROR-->
        <div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="errorModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="errorModalLabel">Error</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Las credenciales son incorrectas. Inténtalo de nuevo.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script src="JS/JSLogin.js"></script>
    </form>
</body>
</html>

