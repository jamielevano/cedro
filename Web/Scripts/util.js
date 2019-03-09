var msjGrabado = 'Grabado';
var msjError = 'Error';
var msjChkVal = 'Debe seleccionar al menos una alternativa.';
var msjChkNumeric = 'Los valores totales deben coincidir.';

$.validator.defaults.ignore = '';

$(function () {
    $.global.culture = $.global.cultures['default'] = $.global.cultures['es-PE'];
})

function queryString(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

$.prototype.refreshValidator = function () {
    this.removeData("validator");
    this.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(this);
    return this;
}

Number.prototype.padLeft = function (n, str) {
    return Array(n - String(this).length + 1).join(str || '0') + this;
}

function LoadSelect(datos, select) {
    $.each(datos, function (key, value) {

        $('#' + select)
             .append($('<option>', { value: value.id })
             .text(value.nombre));
    });
}


function getJson(url, data) {
    var result;
    $.ajax({
        url: url,
        data: data,
        type: 'GET',
        datatype: 'json',
        cache: false,
        async: false,
        success: function (data) { result = data }
    });
    return parseDatesInObject(result);
}

function postJson(url, data, success, error) {
    $('#loading').show();
    $.ajax({
        url: url,
        data: data,
        type: 'POST',
        dataType: 'json',
        cache: false,
        success: success,
        error: error,
        complete: function () {
            $('#loading').hide();
        }
    });
}


function postJson_async(url, data, success, error) {
    $('#loading').show();
    $.ajax({
        url: url,
        data: data,
        type: 'POST',
        dataType: 'json',
        async: true,
        cache: false,
        success: success,
        error: error,
        complete: function () {
            $('#loading').hide();
        }
    });
}

function AnioActual() {
    var today = new Date();
    var yyyy = today.getFullYear();
    return yyyy;
}

function MesActual() {
    var today = new Date();
    var mm = today.getMonth() + 1; //January is 0!
    return mm;
}

function DateNow()
{
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function DateNowVal() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear() - 12;
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function DateNowVal2() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear() - 90;
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function DateNow_Format2() {
    return Date.now();
}


function DateIni() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = '01/' + mm + '/' + yyyy;
    return today;
}

//function formatDate_YYYYMMDD(x)
//{
//    console.log(x);
//    var cadena = x.toString().split('/');
//    return cadena[2] +'-'+cadena[1] +'-'+cadena[0];
    
//}

function formatDate_DDMMYYYY(x) {
    x = ko.utils.unwrapObservable(x);
    if (typeof x === 'string') x = $.global.parseDate(x);
    return $.global.format(x, 'dd-MM-yyyy')
}


function formatDate_YYYYMMDD(x) {
    x = ko.utils.unwrapObservable(x);
    if (typeof x === 'string') x = $.global.parseDate(x);
    return $.global.format(x, 'yyyy-MM-dd')
}

function formatDateShort(x) {
    x = ko.utils.unwrapObservable(x);
    if (typeof x === 'string') x = $.global.parseDate(x);
    return $.global.format(x, 'dd/MM/yy')
}

function formatDate(x) {
    x = ko.utils.unwrapObservable(x);
    if (typeof x === 'string') x = $.global.parseDate(x);
    return $.global.format(x, 'dd/MM/yyyy')
}

function formatNumber(x) {
    x = ko.utils.unwrapObservable(x);
    if (typeof x === 'string') x = $.global.parseFloat(x);
    return $.global.format(x, 'n2')
}

function formatFileSize(x) {
    x = +ko.utils.unwrapObservable(x);
    if (x <= 1024) return x + ' B';
    x /= 1024;
    if (x <= 1024) return Math.round(x) + ' KB';
    x /= 1024;
    return Math.round(x) + ' MB';
}

function parseDatesInObject(x) {
    for (i in x) {
        var type = typeof x[i];
        if (type == 'string' && /\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/.test(x[i]))
            x[i] = $.global.parseDate(x[i]) || x[i];
        else if (type == 'object')
            x[i] = parseDatesInObject(x[i]);
    }
    return x;
}

Array.prototype.sum = function () {
    var total = 0;
    for (var i = 0; i < this.length; i++) {
        total += this[i];
    }
    return total;
}

listMeses = [
    { id: 1, nombre: 'Enero' },
    { id: 2, nombre: 'Febrero' },
    { id: 3, nombre: 'Marzo' },
    { id: 4, nombre: 'Abril' },
    { id: 5, nombre: 'Mayo' },
    { id: 6, nombre: 'Junio' },
    { id: 7, nombre: 'Julio' },
    { id: 8, nombre: 'Agosto' },
    { id: 9, nombre: 'Septiembre' },
    { id: 10, nombre: 'Octubre' },
    { id: 11, nombre: 'Noviembre' },
    { id: 12, nombre: 'Diciembre' }
];

listSiNo = [
    { id: 1, nombre: '1. Si' },
    { id: 2, nombre: '2. No' }
]

function ShowPopupArchivo(url, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    window.open(url, "Adjuntar archivo", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

}

function focusable(element, isTabIndexNotNaN) {
    var map, mapName, img,
        nodeName = element.nodeName.toLowerCase();
    if ("area" === nodeName) {
        map = element.parentNode;
        mapName = map.name;
        if (!element.href || !mapName || map.nodeName.toLowerCase() !== "map") {
            return false;
        }
        img = $("img[usemap=#" + mapName + "]")[0];
        return !!img && visible(img);
    }
    return (/input|select|textarea|button|object/.test(nodeName) ?
        !element.disabled :
        "a" === nodeName ?
            element.href || isTabIndexNotNaN :
            isTabIndexNotNaN) &&
        // the element and all of its ancestors must be visible
        visible(element);
}

function visible(element) {
    return $.expr.filters.visible(element) &&
        !$(element).parents().addBack().filter(function () {
            return $.css(this, "visibility") === "hidden";
        }).length;
}

$.fn.focusNext = function () {
    return this.each(function () {
        var fields = $(':tabbable');
        var index = fields.index(this);
        if (index > -1 && (index + 1) < fields.length) {
            fields.eq(index + 1).focus();
        }
        return false;
    });
};

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) +
      ((exdays == null) ? "" : ("; expires=" + exdate.toUTCString()));
    document.cookie = c_name + "=" + c_value;
}


function getCookie(name) {
    var cname = name + "=";
    var dc = document.cookie;
    if (dc.length > 0) {
        begin = dc.indexOf(cname);
        if (begin != -1) {
            begin += cname.length;
            end = dc.indexOf(";", begin);
            if (end == -1) end = dc.length;
            return unescape(dc.substring(begin, end));
        }
    }
    return null;
}