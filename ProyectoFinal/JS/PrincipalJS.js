
//////BUSCADOR
//$(document).ready(function () {
//    $('#txtBuscar').on('input', function () {
//        var query = $(this).val();
//        $.ajax({
//            type: 'POST',
//            url: 'Buscar.aspx/GetDatos',
//            data: JSON.stringify({ searchQuery: query }),
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'json',
//            success: function (response) {
//                var results = response.d;
//                var html = '';
//                $.each(results, function (index, item) {
//                    html += '<tr><td>' + item + '</td></tr>';
//                });
//                $('#gvResultados tbody').html(html);
//            },
//            error: function (xhr, status, error) {
//                console.error('Error al realizar la búsqueda:', error);
//            }
//        });
//    });
//});