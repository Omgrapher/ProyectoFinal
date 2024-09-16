<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ProyectoFinal._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Inicio de Sesión</title>
    <link href="CSS/bootstrap-5.3.3-dist(3)/bootstrap-5.3.3-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="CSS/bootstrap-icons-1.11.3/bootstrap-icons-1.11.3/font/bootstrap-icons.min.css"/>
    <link href="CSS/EstilosLogin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <div class="container">
            <div class="login-container">
                <div class="d-flex justify-content-center align-items-center mb-3">
                    <i class="bi bi-shop me-2" style="font-size: 4rem;""></i>
                </div>

                <div class="input-group mb-3">
                    <span class="input-group-text" id="basic-addon1"><i class="bi bi-person-fill"></i></span>
                    <div class="form-floating flex-grow-1">
                        <asp:TextBox ID="UserTextBox" CssClass="form-control" placeholder="Introduce tu usuario" runat="server" required />
                        <label for="UserTextBox" class="form-label">Usuario:</label>
                    </div>
                </div>


                <div class="input-group mb-3">
                    <span class="input-group-text" id="basic-addon2"><i class="bi bi-lock-fill"></i></span>
                    <div class="form-floating flex-grow-1">
                        <asp:TextBox ID="PasswordTextBox" CssClass="form-control" TextMode="Password" placeholder="Contraseña" runat="server" required />
                        <label for="PasswordTextBox" class="form-label">Contraseña:</label>
                    </div>
                </div>

                <!-- Botón de inicio de sesión -->
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
                    <div class="modal-body" id="welcomeModalBody">
                        ¡Has iniciado sesión correctamente!
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal ERROR -->
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
        <script src="CSS/bootstrap-5.3.3-dist(3)/bootstrap-5.3.3-dist/js/bootstrap.min.js"></script>
    </form>
</body>
</html>

