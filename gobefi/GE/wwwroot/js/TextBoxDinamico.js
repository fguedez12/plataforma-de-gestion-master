//**************************************
// Libreria de funciones JavaScript
// --------------------------------

// 1 = Numeros
// 2 = Letras
// 3 = Letras y Numeros
// 4 = correo
// 5 = Mayusculas
// 6 = Minusculas
// 7 = Digito Rut
// 8 = Especial
// 9 = Fecha

//**************************************                                        
// Valida que solo se acepten caracteres
//**************************************

function ValidaCaracteres(valor) {
    var parsed = true;
    var validchars = "abcdefghijklmnñopqrstuvwxyz";

    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//**************************************                                        
// Valida que solo se acepten mayusculas
//**************************************
function ValidaMayusculas(valor) {
    var parsed = true;
    var validchars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i);
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//**************************************                                        
// Valida que solo se acepten minusculas
//**************************************
function ValidaMinusculas(valor) {
    var parsed = true;
    var validchars = "abcdefghijklmnñopqrstuvwxyz";
    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i);
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//**************************************                                        
// Valida que solo se acepten numeros
//**************************************
function ValidaNumeros(valor) {
    var parsed = true;
    var validchars = "0123456789";
    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//**************************************                                        
// Valida que solo se acepten digitos y K 
//**************************************
function ValidaDigitoRut(valor) {
    var parsed = true;
    var validchars = "0123456789k";
    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//**************************************                                        
// Valida que solo se acepten caracteres y numeros
//**************************************
function ValidaAlfanumerico(valor) {
    var parsed = true;
    var validchars = "abcdefghijklmnñopqrstuvwxyz0123456789";
    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//*************************************************************
// Valida que solo se acepten caracteres validos para un correo
//*************************************************************
function ValidaCorreo(email) {
    var parsed = true;
    var validchars = "abcdefghijklmnñopqrstuvwxyz0123456789@.-_";
    for (var i = 0; i < email.length; i++) {
        var letter = email.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//*************************************************************
// Valida que solo se acepten caracteres validos segun juego
//*************************************************************
function ValidaEspeciales(juego, valor) {
    var parsed = true;
    var validchars = juego;
    for (var i = 0; i < valor.length; i++) {
        var letter = valor.charAt(i).toLowerCase();
        if (validchars.indexOf(letter) != -1)
            continue;
        parsed = false;
        break;
    }
    return parsed;
}

//**********************************************************
// Pega desde el portapapeles y que sea valido segun el tipo
//**********************************************************
function doPaste(accion) {

    var valor = window.clipboardData.getData("Text");

    var ret = true;

    if (accion == 1) {
        ret = ValidaNumeros(valor);
    } else if (accion == 2) {
        ret = ValidaCaracteres(valor);
    } else if (accion == 3) {
        ret = ValidaAlfanumerico(valor);
    } else if (accion == 4) {
        ret = ValidaCorreo(valor);
    } else if (accion == 5) {
        ret = ValidaMayusculas(valor);
    } else if (accion == 6) {
        ret = ValidaMinusculas(valor);
        if (ret == true) { ret } else { alert('Rut Incorrecto'); }
    } else if (accion == 7) {
        ret = ValidaDigitoRut(valor);
    }

    if (!ret) window.event.returnValue = false;
}

//***************************************************************************
// Pega desde el portapapeles y que sea valido solo si estan dentro del juego
//***************************************************************************
function doPasteEsp(juego) {

    var valor = window.clipboardData.getData("Text");

    var ret = true;

    ret = ValidaEspeciales(juego, valor);

    if (!ret) window.event.returnValue = false;
}

//**********************************************************
// Valida que solo se acepten numeros
//**********************************************************
function SoloNumeros(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;

    return true;
}

//**********************************************************
// Valida que solo se acepten mayusculas
//**********************************************************
function SoloMayusculas(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode <= 90) || charCode == 209) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten minusculas
//**********************************************************				
function SoloMinusculas(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 97 && charCode <= 122) || charCode == 241) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten letras
//**********************************************************				
function SoloLetras(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 32) || (charCode == 08)) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten caracteres validos para correo
//**********************************************************								
function SoloCorreo(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (SoloLetras(evt) || SoloNumeros(evt) || charCode == 46 || charCode == 64 || charCode == 45 || charCode == 95) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten numeros y letras
//**********************************************************								
function SoloLetrasNumeros(evt) {
    if (SoloLetras(evt) || SoloNumeros(evt)) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten numeros y la K
//**********************************************************												
function SoloDigitoRut(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (SoloNumeros(evt) || charCode == 107 || charCode == 75) return true;

    return false;
}

//**********************************************************
// Valida que solo se acepten caracteres dentro de juego
//**********************************************************
function SoloEspeciales(evt, juego) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    for (var i = 0; i < juego.length; i++) {
        var letter = juego.charCodeAt(i);

        if (charCode == letter) return true;
    }

    return false;
}

function validaFechaDDMMAAAA(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 47 || charCode > 57))
        return false;
    if (evt.value.length > 1)
        return evt.value + '/';
}

function validar(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode > 31 && (charCode < 47 || charCode > 57))
        return false;

    return true;
}

function chars_restantes(textbox, largomax) {

    var chars_restantes = largomax - textbox.value.length;

    if (chars_restantes < 0) {
        alert('El numero maximo de caracteres permitidos es : ' + largomax);
        textbox.value = textbox.value.substring(0, largomax);
    }
}


function FormatearRut(obj) {
    if (SoloDigitoRut(obj)) {
        var rut = obj.value;
        if (!/\./.test(rut)) {
            var nuevo = "";
            for (var i = 0; i < rut.length; i++) {
                if (i == 1 || i == 4) {
                    nuevo += rut[i] + '.';
                } else if (i == (rut.length - 1)) {
                    nuevo += '-' + rut[i];
                } else {
                    nuevo += rut[i];
                }
            }
            obj.value = nuevo;
        }
    }
}

//Escrive un div con resultado de la validacion 
function obligatorio(idCampo) {
    if ($('*[data-id="' + idCampo + '"]').length) {
        if (!$('*[data-id="' + idCampo + '"]').next().hasClass('caja_controles_msgerror'))
            $('*[data-id="' + idCampo + '"]').after(
             $('<div />').text('Requerido').addClass('caja_controles_msgerror')
             );//close after
    }//close lenght
}




