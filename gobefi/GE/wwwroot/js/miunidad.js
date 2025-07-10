
$("body").on('click', ".clickable-row", function () {
    // GetDataByDivision($(this).attr('data-id'))

    localStorage.setItem("divisionId", $(this).attr('division-id'));
    localStorage.setItem("servicioId", $(this).attr('servicio-id'));
    localStorage.setItem("institucionId", $(this).attr('institucion-id')); 


    localStorage.setItem("menuActual", $('#myTabContent .nav-link:first').parent().attr('id')); 
    localStorage.setItem('divisionName', $(this).children().html());
    

    window.location = $(this).data('href');
    return false;
});

function GetDataByDivision(divisionId) {
    $.ajax({
        type: 'GET',
        url: apiDivision + "/" + divisionId,
        async: false,
        success: function (data) {
            localStorage.setItem("servicioId", data.servicioId); 
            localStorage.setItem("institucionId", data.servicio.institucionId); 
        }
    });
}