// Archivo JScript

function MM_findObj(n, d) { //v3.0
    var p, i, x;
    if (!d)
        d = document;
    if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document;
        n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all)
        x = d.all[n];
    for (i = 0; !x && i < d.forms.length; i++)
        x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++)
        x = MM_findObj(n, d.layers[i].document);
    return x;
}
function FechaMenorActual(s, e) {
    var Valido = EsFechaValida(e.Value)
    if (Valido == 0) {
        var FechaHoy = new Date();
        var Fecha_AUX = e.Value.split("/")[2] + e.Value.split("/")[1] + e.Value.split("/")[0];
        var mes = FechaHoy.getMonth();
        var num = FechaHoy.getDate();
        var ano = FechaHoy.getFullYear();
        if (parseFloat(num) < 10) {
            num = "0" + num;
        }
        mes++;
        if (parseFloat(mes) < 10) {
            mes = "0" + mes;
        }

        FechaHoy = new String(ano) + new String(mes) + new String(num);


        e.IsValid = false;
        //alert(Fecha_AUX + " " + FechaHoy);
        if (parseFloat(Fecha_AUX) >= parseFloat(FechaHoy)) {
            e.IsValid = true;
        }
    } else {
        e.IsValid = true;
    }
}
function MayorACero(s, e) {
    e.IsValid = true;
    var monto = "";
    for (j = 0; j < e.Value.length; j++) {
        if (e.Value.charAt(j) != ".") {
            monto = monto + e.Value.charAt(j);
        }
    }

    if (monto != "") {
        if (parseFloat(monto) <= 0) {
            e.IsValid = false;
        }
    }
    else {
        e.IsValid = false;
    }
}

function validaEmail(valor) {
    if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(valor)) {
        return true;
    } else {
        return false;
    }
}

function validarRut(s, e) {
    if (check_rut(e.Value.substring(0, e.Value.length - 1), e.Value.substring(e.Value.length - 1, e.Value.length))) {
        e.IsValid = true;
    }
    else {
        e.IsValid = false;
    }
}
function check_rut(rut, dv) {

    console.log("Validaciones: Validacion rut" + rut + dv);

    dig_ver = dv;
    var monto = ""
    for (j = 0; j < rut.length; j++) {
        if (rut.charAt(j) != "." && rut.charAt(j) != "-") {
            monto = monto + rut.charAt(j)
        }
    }
    rut = monto;

    if (isNaN(rut)) {
        return (false)
    }

    if (rut.length < 6 || dig_ver == "") {
        console.log("Menor a 6");
        return (false)
    }

    if (rut.length == 6)
        numero_rut = "00" + rut
    else if (rut.length < 8)
        numero_rut = "0" + rut
    else
        numero_rut = rut;

    console.log("rut es" + numero_rut);

    v8 = numero_rut.substring(7, 8) * 2;
    v7 = numero_rut.substring(6, 7) * 3;
    v6 = numero_rut.substring(5, 6) * 4;
    v5 = numero_rut.substring(4, 5) * 5;
    v4 = numero_rut.substring(3, 4) * 6;
    v3 = numero_rut.substring(2, 3) * 7;
    v2 = numero_rut.substring(1, 2) * 2;
    v1 = numero_rut.substring(0, 1) * 3;

    suma_rut = v1 + v2 + v3 + v4 + v5 + v6 + v7 + v8;
    resto_rut = suma_rut % 11;

    digito_verificador = 11 - resto_rut;

    console.log("Calculo DV es " + digito_verificador);

    if (digito_verificador == 10)
        digito_verificador = "K";
    if (digito_verificador == 11)
        digito_verificador = 0;

    if (digito_verificador == "K") {
        if (digito_verificador != dig_ver.toUpperCase()) {
            return (false)
        }
        else
            return (true);
    }
    else {
        if (digito_verificador != dig_ver) {
            console.log("dig no es valido");
            return (false)
        }
        else {
            console.log("dig es valido");
            return (true);
        }
    }

}

function validarFecha(s, e) {
    var Valido;
    Valido = EsFechaValida(e.Value)
    if (Valido == 0) {
        e.IsValid = true;
    }
    else {
        e.IsValid = false;
    }
}

function validarFechaA2D(s, e) {
    var arrFecha;
    var Dia;
    var Mes;
    var Ano;
    var Fecha;
    var FecOk;
    var Valido;

    Valido = 0;
    arrFecha = e.Value.split("/");
    if (arrFecha.length != 3) {
        Valido = 1;
    } else {
        if (arrFecha.length != 3) {
            Valido = 1;
        }
        if (arrFecha[0].length != 2) {
            Valido = 1;
        }
        if (arrFecha[1].length != 2) {
            Valido = 1;
        }
        if (arrFecha[2].length != 2) {
            Valido = 1;
        }
    }
    if (Valido == 0) {
        Dia = arrFecha[0];
        Mes = arrFecha[1];
        Ano = arrFecha[2];

        Fecha = Dia + Mes + Ano
        FecOk = parseFloat(Fecha)

        if (isNaN(FecOk) == true) {
            Valido = 1;
        }

        if (!((Dia >= 1) && (Dia <= 31))) {
            Valido = 1;
        }

        if (!((Mes >= 1) && (Mes <= 12))) {
            Valido = 1;
        }
        if (!((Ano >= 00) && (Ano <= 100))) {
            Valido = 1;
        }
    }

    if (Valido == 0) {
        if (((parseFloat(Ano) % 4) != 0) && ((parseFloat(Dia)) > 28) && ((parseFloat(Mes)) == 2)) {
            Valido = 1;
        }
        if (((parseFloat(Ano) % 4) == 0) && ((parseFloat(Dia)) > 29) && ((parseFloat(Mes)) == 2)) {
            Valido = 1;
        }
        if ((parseFloat(Mes) == 4 || parseFloat(Mes) == 6 || parseFloat(Mes) == 9 || parseFloat(Mes) == 11) && Dia > 30) {
            Valido = 1;
        }
    }
    if (Valido == 0) {
        e.IsValid = true;
    }
    else {
        e.IsValid = false;
    }
}

function EvaluateText(cadena, obj, e) {
    var Test_Alpha_Nomina = /[^a-zA-Z' .,ÑñáéíóúÁÉÍÓÚ0-9_-]+/   //[^a-zA-Z0-9_-]+/
    var Test_Nombres = /[^a-zA-Z' ÑñáéíóúÁÉÍÓÚ]+/
    var Test_Nombres_vale = /[^a-zA-Z' .,Ññ]+/
    var Test_Glosa_Vale = /[^a-zA-Z' .,Ññ0-9_-]+/
    var Test_Fecha = /[^\d{2}/\d{2}/\d{4}]+/
    opc = false;
    tecla = (document.all) ? e.keyCode : e.which;
    if ((tecla < 32) || (tecla > 254)) {
        opc = true;
        return opc;
    }
    if (cadena == "%d") {
        if (tecla > 47 && tecla < 58) {
            opc = true;
        }
    }
    if (cadena == "%r") {
        if (tecla > 47 && tecla < 58) {
            opc = true;
        } else {
            if (tecla == 75 || tecla == 107 || tecla == 46 || tecla == 45) {
                opc = true;
            }
        }
    }
    if (cadena == "%a") {
        if (!Test_Alpha_Nomina.test(obj.value)) {
            opc = true;
        }
    }
    if (cadena == "%n") {
        if (tecla < 47 || tecla > 58) {
            if (!Test_Nombres.test(obj.value)) {
                opc = true;
            }
        }
    }
    if (cadena == "%g") {
        if (!Test_Glosa_Vale.test(obj.value)) {
            opc = true;
        }
    }
    if (cadena == "%v") {
        if (!Test_Nombres_vale.test(obj.value)) {
            opc = true;
        }
    }
    if (cadena == "%f") {
        obj.value = obj.value.replace('-', '/');
        //Valores numericos
        if (tecla > 47 && tecla < 58) {
            opc = true;
        }
        //Caracteres Separación
        if (tecla == 47 || tecla == 45) {
            opc = true;
        }
    }
    return opc;
}

function DesFormateaMonto(obj) {
    var monto = "";
    for (j = 0; j < obj.value.length; j++) {
        if (obj.value.charAt(j) != ".") {
            monto = monto + obj.value.charAt(j)
        }
    }
    if (!isNaN(monto)) {
        obj.value = monto;
    } else {
        obj.value = obj.value;
    }
}

function FormateaMonto(obj) {
    if (obj.value == "" || obj.value == ".") {
        obj.value = "";
        return;
    }
    var monto = "";
    var monto_aux = "";
    var monto_aux_ = "";
    var son = 0;
    var son2 = 0;
    for (j = 0; j < obj.value.length; j++) {
        if (obj.value.charAt(j) != ".") {
            monto = monto + obj.value.charAt(j)
        }
    }
    if (monto != "") {
        monto_aux_ = parseFloat(monto);
        monto = new String(monto_aux_);
        for (j = monto.length - 1; j >= 0; j--) {
            monto_aux = monto.charAt(j) + monto_aux;
            son++
            son2++
            if (son == 3) {
                if (monto.length != son2) {
                    monto_aux = "." + monto_aux
                }
                son = 0;
            }
        }
        if (!isNaN(monto)) {
            obj.value = monto_aux;
        } else {
            obj.value = obj.value;
        }
    } else {
        obj.value = "";
    }
}

function DesFormateaRut(obj) {
    var rut = "";
    for (j = 0; j < obj.value.length; j++) {
        if (obj.value.charAt(j) != "." && obj.value.charAt(j) != "-") {
            rut = rut + obj.value.charAt(j)
        }
    }
    obj.value = rut;
}

function FormateaRut(obj) {
    var rut = "";
    var dv = "";
    if (obj.value == "" || obj.value == ".") {
        obj.value = "";
        return;
    }
    for (j = 0; j < obj.value.length; j++) {
        if (obj.value.charAt(j) != "." && obj.value.charAt(j) != "-") {
            rut = rut + obj.value.charAt(j)
        }
    }
    dv = rut.substring(rut.length - 1, rut.length);
    rut = rut.substring(0, rut.length - 1);

    if (rut != "") {
        if (!isNaN(rut)) {
            obj.value = FormateaMontoRut(rut) + "-" + dv.toUpperCase();
        } else {
            obj.value = "";
        }
    } else {
        obj.value = "";
    }
}

function FormateaMontoRut(vv) {
    var monto = "";
    var monto_aux = "";
    var monto_aux_ = "";
    var son = 0;
    var son2 = 0;
    for (j = 0; j < vv.length; j++) {
        if (vv.charAt(j) != ".") {
            monto = monto + vv.charAt(j)
        }
    }
    monto_aux_ = parseFloat(monto);
    monto = new String(monto_aux_);
    for (j = monto.length - 1; j >= 0; j--) {
        monto_aux = monto.charAt(j) + monto_aux;
        son++
        son2++
        if (son == 3) {
            if (monto.length != son2) {
                monto_aux = "." + monto_aux
            }
            son = 0;
        }
    }
    return monto_aux;
}

function SacaPuntos(vValor) {
    var rut = "";
    for (j = 0; j < vValor.length; j++) {
        if (vValor.charAt(j) != "." && vValor.charAt(j) != "-") {
            rut = rut + vValor.charAt(j)
        }
    }
    return rut;
}

function activaCompare(inicio, fin, idcompare, tipo) {
    if (tipo == 'F') {
        if (validarFechaCompare(inicio) == true && validarFechaCompare(fin) == true) {
            document.getElementById(idcompare).enabled = true;
        } else {
            document.getElementById(idcompare).enabled = false;
            document.getElementById(idcompare).style.display = 'none';
        }
    } else {
        if (IsNumeric(inicio) == true && IsNumeric(fin) == true) {
            document.getElementById(idcompare).enabled = true;
        } else {
            document.getElementById(idcompare).enabled = false;
            document.getElementById(idcompare).style.display = 'none';
        }
    }
}

function validarFechaCompare(fecha) {
    var arrFecha;
    var Dia;
    var Mes;
    var Ano;
    var Fecha;
    var FecOk;
    var Valido;

    Valido = 0;
    arrFecha = fecha.split("/");
    //alert(arrFecha.length);
    if (arrFecha.length < 3) {
        Valido = 1;
    }
    else {
        if (arrFecha.length != 3) {
            Valido = 1;
        }
        if (arrFecha[0].length != 2) {
            Valido = 1;
        }
        if (arrFecha[1].length != 2) {
            Valido = 1;
        }
        if (arrFecha[2].length != 4) {
            Valido = 1;
        }
    }
    if (Valido == 0) {
        Dia = arrFecha[0];
        Mes = arrFecha[1];
        Ano = arrFecha[2];

        Fecha = Dia + Mes + Ano
        FecOk = parseFloat(Fecha)

        if (isNaN(FecOk) == true) {
            Valido = 1;
        }

        if (!((Dia >= 1) && (Dia <= 31))) {
            Valido = 1;
        }

        if (!((Mes >= 1) && (Mes <= 12))) {
            Valido = 1;
        }
        if (!((Ano >= 1900) && (Ano <= 4000))) {
            Valido = 1;
        }
    }

    if (Valido == 0) {
        if (((parseFloat(Ano) % 4) != 0) && ((parseFloat(Dia)) > 28) && ((parseFloat(Mes)) == 2)) {
            Valido = 1;
        }
        if (((parseFloat(Ano) % 4) == 0) && ((parseFloat(Dia)) > 29) && ((parseFloat(Mes)) == 2)) {
            Valido = 1;
        }
        if ((parseFloat(Mes) == 4 || parseFloat(Mes) == 6 || parseFloat(Mes) == 9 || parseFloat(Mes) == 11) && Dia > 30) {
            Valido = 1;
        }
    }
    if (Valido == 0) {
        return true;
    }
    else {
        return false;
    }
}

function EsFechaValida(miFecha) {
    var arrFecha;
    var Dia;
    var Mes;
    var Ano;
    var Fecha;
    var FecOk;
    var Valido;

    Valido = 0;
    arrFecha = miFecha.split("/");
    if (arrFecha.length < 3) {
        Valido = 1;
    }
    else {
        if (arrFecha.length != 3) {
            Valido = 1;
        }
        if (arrFecha[0].length != 2) {
            Valido = 1;
        }
        if (arrFecha[1].length != 2) {
            Valido = 1;
        }
        if (arrFecha[2].length != 4) {
            Valido = 1;
        }
    }
    if (Valido == 0) {
        Dia = arrFecha[0];
        Mes = arrFecha[1];
        Ano = arrFecha[2];

        Fecha = Dia + Mes + Ano
        FecOk = parseFloat(Fecha)

        if (isNaN(FecOk) == true) {
            Valido = 1;
        }

        if (!((Dia >= 1) && (Dia <= 31))) {
            Valido = 1;
        }

        if (!((Mes >= 1) && (Mes <= 12))) {
            Valido = 1;
        }
        if (!((Ano >= 1900) && (Ano <= 4000))) {
            Valido = 1;
        }
    }

    if (Valido == 0) {
        if (((parseFloat(Ano) % 4) != 0) && ((parseFloat(Dia)) > 28) && ((parseFloat(Mes)) == 2)) {
            Valido = 1;
        }
        if (((parseFloat(Ano) % 4) == 0) && ((parseFloat(Dia)) > 29) && ((parseFloat(Mes)) == 2)) {
            Valido = 1;
        }
        if ((parseFloat(Mes) == 4 || parseFloat(Mes) == 6 || parseFloat(Mes) == 9 || parseFloat(Mes) == 11) && Dia > 30) {
            Valido = 1;
        }
    }

    return Valido;
}

function Letras_caracteres(e) {

    var evt = (e) ? e : window.event;
    //var key = (evt.keyCode) ? evt.keyCode : evt.which;
    var keyID = (evt.charCode) ? evt.charCode : ((evt.which) ? evt.which : evt.keyCode);

    if (keyID > 64 && keyID < 91) {
        return true;
    }
    if (keyID > 96 && keyID < 123) {
        return true;
    }
    if (keyID > 191 && keyID < 251) {
        return true;
    }
    if (keyID == 8 || keyID == 46 || keyID == 32) {
        return true;
    }
    return false;
}

function Letras_numeros(e) {
    var evt = (e) ? e : window.event;
    //var key = (evt.keyCode) ? evt.keyCode : evt.which;
    var keyID = (evt.charCode) ? evt.charCode : ((evt.which) ? evt.which : evt.keyCode);

    if (keyID > 64 && keyID < 91) {
        return true;
    }
    if (keyID > 96 && keyID < 123) {
        return true;
    }
    if (keyID > 47 && keyID < 58) {
        return true;
    }
    if (keyID == 8 || keyID == 32) {
        return true;
    }
    return false;
}

function Numeros(e) {
    var evt = (e) ? e : window.event;
    //var key = (evt.keyCode) ? evt.keyCode : evt.which;
    var keyID = (evt.charCode) ? evt.charCode : ((evt.which) ? evt.which : evt.keyCode);

    if (keyID > 47 && keyID < 58) {
        return true;
    }
    if (keyID == 46) {
        return true;
    }
    return false;
}

function TodasMayusculas(Obj) {
    if (Obj.value != '') {
        Obj.value = Obj.value.toUpperCase();
    }
    return Obj;
}

function format_num(id) {
    var number = document.getElementById(id).value;
    number += '';
    number = number.replace(".", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    document.getElementById(id).value = x1 + x2;
}

function format(field){
    var number = new String(field.value);
    number = number.replace(/\./g, ''); //quita todos los puntos de la cadena
    var result = '';
    while( number.length > 3 ){
        result = '.' + number.substr(number.length - 3) + result;
        number = number.substring(0, number.length - 3);
    }
    result = number + result;
    field.value = result;
}