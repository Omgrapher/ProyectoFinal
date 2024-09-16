//document.addEventListener('DOMContentLoaded', function () {
//    var form = document.getElementById('form1');

//    form.addEventListener('submit', function (event) {
//        var user = document.getElementById('<%= UserTextBox.ClientID %>');
//        var password = document.getElementById('<%= PasswordTextBox.ClientID %>');

//        var isValid = true;

//        if (user.value.trim() === '') {
//            user.classList.add('is-invalid');
//            isValid = false;
//        } else {
//            user.classList.remove('is-invalid');
//        }

//        if (password.value.trim() === '') {
//            password.classList.add('is-invalid');
//            isValid = false;
//        } else {
//            password.classList.remove('is-invalid');
//        }

//        if (!isValid) {
//            event.preventDefault(); // Evita el envío del formulario si hay errores
//        }
//    });
//});