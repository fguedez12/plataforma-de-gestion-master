async function GetUnidades() {
    let response;

    try {
        $("#loader").show();

        response = await $.get({
            url: "/obtenerUnidades" + "/" + $("#Id").val()
        });

        $("#Unidades").html(response);

        $("#Unidades input[id='UserId']").val($("#Id").val());

    } catch (e) {
        alert(e);
    } finally {
        $("#loader").hide();
    }

}

async function GetUnidadesv2() {
    let response;

    try {
        $("#loaderv2").show();

        response = await $.get({
            url: "/obtenerUnidadesv2" + "/" + $("#Id").val()
        });

        $("#Unidadesv2").html(response);

        $("#Unidadesv2 input[id='UserId']").val($("#Id").val());

    } catch (e) {
        alert(e);
    } finally {
        $("#loaderv2").hide();
    }

}

async function CargarSelectMultiples(element, url, parentName, compareElement){
    let data;
    try {

        data = await $.get({
            url: url,
        });

        //console.log(compareElement);

        $.each(data, function (i, item){

            if (compareElement !== undefined && (compareElement.find("option[value=" + item.id + "]").length > 0) ) {
                //console.log("ya existe");
                return;
            }
            //console.log("agregando");

            element.append($("<option>", {
                value: item.id,
                title: item.nombre,
                parentId: item[parentName]
            }).html(item.nombre));

        });

        
    } catch (e) {
        console.log(e);
    } finally {
        MySort(element.attr("id"))
    }

}

