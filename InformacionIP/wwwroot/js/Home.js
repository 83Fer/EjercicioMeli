

$(document).ready(function () {
    $("#btnEnviar").click(enviarIP);

    function enviarIP() {
        var ip = $("#ip").val();
        var data = { ip : ip };

        $.get("Home/GetInfoIp", data)
            .done((result) => {
                console.log(result);
                $("#ipInfoTable").empty();
                showTable(result);
            })
            .fail(() => {
                console.log("Termino con error");
                $("#ipInfoTable").empty();
                $("#ipInfoTable").append('<h2>Termino con error</h2>');

            })
    }

    function showTable(result) {
        var tableContent =
            '<thead>' +
            '< tr >' +
            '<th class="empid">IP</th>' +
            '<th class="fname">' + $("#ip").val() + '</th>' +
            '</tr >' +
            '</thead >' +
            '<tbody>' +
            '    <tr>' +
            '        <td class="empid"><strong>Pais</strong></td>' +
            '        <td class="fname">' + result.countryName + '</td>' +
            '    </tr>' +
            '    <tr>' +
            '        <td class="empid"><strong>ISO Code</strong></td>' +
            '        <td class="fname">' + result.countryCode + '</td>' +
            '    </tr>' +
            '    <tr>' +
            '  <td class="empid"><strong>Idiomas</strong></td>' +
            '   <td class="fname">'
            for (var i = 0; i < result.lenguages.length; i++) {
                tableContent += result.lenguages[i].native  + ';  ' ;
            }
            tableContent += '</td></tr>' +
            '    <tr>' +
            '        <td class="empid"><strong>Moneda</strong></td>' +
            '        <td class="fname">' + result.nameCurrency +'</td>' +
            '    </tr>' +
            '    <tr>' +
            '        <td class="empid"><strong>Hora</strong></td>' +
            '        <td class="fname">' + result.currentTimeFormatted + '</td>' +
            '    </tr>' +
            '    <tr>' +
            '        <td class="empid"><strong>Distancia Estimada</strong></td>' +
            '        <td class="fname">' + result.estimatedDistance + '</td>' +
            '    </tr>' +
            '   <tr>' +
            '        <td class="empid"><strong>Distancia mas lejana</strong></td>' +
            '        <td class="fname">' + result.farDistance + '</td>' +
            '    </tr>' +
            '   <tr>' +
            '        <td class="empid"><strong>Distancia mas cercana</strong></td>' +
            '        <td class="fname">' + result.closeDistance + '</td>' +
            '    </tr>' +
            '   <tr>' +
            '        <td class="empid"><strong>Distancia promedio</strong></td>' +
            '        <td class="fname">' + result.averageDistance + '</td>' +
            '    </tr>' +
            '</tbody>';

        $("#ipInfoTable").append(tableContent);
        
    }
});