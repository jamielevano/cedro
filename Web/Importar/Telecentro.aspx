<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Telecentro.aspx.cs" Inherits="Web.Importar.Telecentro" %>

<!doctype html>

<html>
<head>
<meta charset="utf-8" />
<title>Demo - CSV-to-Table</title>
<script src="../Scripts/jquery-1.9.0.min.js"></script>
<script src="../Scripts/jquery.csv.js"></script>
<script src="../Scripts/util.js"></script>
<link href="../Content/Site.css" rel="stylesheet" />
<script>
    $(document).ready(function () {
        if (isAPIAvailable()) {
            $('#files').bind('change', handleFileSelect);
        }

        var usuarioid = getJson('../api/General/GetUsuarioIdByLogin');
        var telecentroid = getJson('../api/General/GetTelecentroIdByLogin');

        LoadSelect(getJson('../api/General/Telecentros'), 'cbotelecentro');
        LoadSelect(getJson('../api/General/ListUsuarios'), 'cboresponsable');

        $('#cboresponsable option[value=' + usuarioid + ']').attr('selected', 'selected');
        $('#cbotelecentro option[value=' + telecentroid + ']').attr('selected', 'selected');
        
    });

    function isAPIAvailable() {
        // Check for the various File API support.
        if (window.File && window.FileReader && window.FileList && window.Blob) {
            // Great success! All the File APIs are supported.
            return true;
        } else {
            // source: File API availability - http://caniuse.com/#feat=fileapi
            // source: <output> availability - http://html5doctor.com/the-output-element/
            document.writeln('The HTML5 APIs used in this form are only available in the following browsers:<br />');
            // 6.0 File API & 13.0 <output>
            document.writeln(' - Google Chrome: 13.0 or later<br />');
            // 3.6 File API & 6.0 <output>
            document.writeln(' - Mozilla Firefox: 6.0 or later<br />');
            // 10.0 File API & 10.0 <output>
            document.writeln(' - Internet Explorer: Not supported (partial support expected in 10.0)<br />');
            // ? File API & 5.1 <output>
            document.writeln(' - Safari: Not supported<br />');
            // ? File API & 9.2 <output>
            document.writeln(' - Opera: Not supported');
            return false;
        }
    }

    function handleFileSelect(evt) {
        var files = evt.target.files; // FileList object
        var file = files[0];

        // read the file metadata
        //var output = ''
        //output += '<span style="font-weight:bold;">' + escape(file.name) + '</span><br />\n';
        //output += ' - FileType: ' + (file.type || 'n/a') + '<br />\n';
        //output += ' - FileSize: ' + file.size + ' bytes<br />\n';
        //output += ' - LastModified: ' + (file.lastModifiedDate ? file.lastModifiedDate.toLocaleDateString() : 'n/a') + '<br />\n';

        // read the file contents
        printTable(file);

        // post the results
        //$('#list').append(output);
    }

    function printTable(file) {
        var reader = new FileReader();
        reader.readAsText(file);
        reader.onload = function (event) {
            var csv = event.target.result;
            var data = $.csv.toArrays(csv);
            var html = '';
            var header = 1;



            for (var row in data) {

                if (header == 1)
                {
                    html = '';
                    html += '<tr>\r\n';
                    for (var item in data[row]) {
                        html += '<th>' + data[row][item] + '</th>\r\n';
                    }
                    html += '<th>Proceso</th>\r\n';
                    html += '</tr>\r\n';
                    $('#contents thead').append(html);
                    header = 0;
                }
                else
                {
                    html = '';
                    html += '<tr>\r\n';
                    var col = 1;
                    var dataObj = {};

                    for (var item in data[row])
                    {
                        html += '<td>' + data[row][item] + '</td>\r\n';
                        var valor = data[row][item];

                        switch(col)
                        {
                            case 1:
                                dataObj.fila = valor;
                                break;
                            case 2:
                                dataObj.fecha = valor;
                                break;
                            case 3:
                                dataObj.nombre = valor;
                                break;
                            case 4:
                                dataObj.apellidos = valor;
                                break;
                            case 5:
                                dataObj.sexo = valor;
                                break;
                            case 6:
                                dataObj.institucion = valor;
                                break;
                            case 7:
                                dataObj.localidad = valor;
                                break;
                            case 8:
                                dataObj.dni = valor;
                                break;
                            case 9:
                                dataObj.fechanac = valor;
                                break;
                            case 10:
                                dataObj.email = valor;
                                break;
                            case 11:
                                dataObj.telefono = valor;
                                break;
                            case 12:
                                dataObj.actividad = valor;
                                break;
                            case 13:
                                dataObj.horaini = valor;
                                break;
                            case 14:
                                dataObj.horafin = valor;
                                break;


                        }
                        col++;
                    }

                    var resultado = '';

                    dataObj.telecentro = $("#cbotelecentro").val();
                    dataObj.responsable = $("#cboresponsable").val();
                    //console.log(JSON.stringify(dataObj));

                    postJson('../api/Asistencia/Telecentro/Importar', JSON.parse(JSON.stringify(dataObj)),
                        function (data) {

                            resultado = data;
                           
                            if (data == 0)
                                resultado = 'ERROR';

                            if (data == 1)
                                resultado = 'OK';

                            if (data == 2)
                                resultado = 'EXISTENTE';

                            //alert('¡Se realizo el proceso satisfactoriamente!');
                        },
                        function () {
                            //alert('Error');
                        });



                    html += '<td style="text-align:center">' + resultado + '</td>\r\n';
                    html += '</tr>\r\n';

                    $('#contents tbody').append(html);
                }
            }
        };
        reader.onerror = function () { alert('Unable to read ' + file.fileName); };
    }
</script>
</head>
<body>

<table style="width:95%; margin: 10px auto;">
<tr>
    <td>
        <fieldset>
            <legend>Buscar</legend>
            <form id="fbusqueda" >
                <table>
                    <tr>
                        <td>
                            Telecentro
                        </td>
                        <td>
                            <select id="cbotelecentro"></select>
                        </td>
                        <td>
                            Responsable
                        </td>
                        <td>
                            <select id="cboresponsable"></select>
                        </td>
                    </tr>
                </table>
            </form>
        </fieldset>
    </td>
</tr>
</table>

<hr />
<div id=inputs class=clearfix>
  <input type=file id=files name=files[] multiple />
</div>
<%--<hr />
<output id=list>
</output>--%>
<hr />
<table id=contents style="width:100%; height:200px;" border class="grid">
    <thead>

    </thead>
    <tbody>

    </tbody>
</table>
</body>
</html>