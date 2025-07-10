
/**
 * It Loads a list of Instituciones and Servicios for the Usuario that it is currently being edited.
 * 
 * @param {Object} institucionContainer - The object that will contain all Instituciones list
 * @param {Object} servicioContainer - The object that will contain all Servicios list
 * @param {Object} divisionContainer - The object that will contain all Divisiones list
 * @param {string} institucionUrl The url to request the list of Instituciones
 * @param {string} servicioUrl - The url to request the list of Servicios
 * @param {string} divisionUrl - The url to request the list of Divisiones
 * @param {string} selected - To check if the key value of Servicio or Instituciones match
 * @param {string} [field] - The field to check the value of selected
 * @param {string} [containerType] - The type of container for fill the information obtained
 * @param {string} [styleClass] - The class for the container
 */
function loadInstitucionesServiciosAndDivisiones(
    institucionContainer,
    servicioContainer,
    divisionContainer,
    institucionUrl,
    servicioUrl,
    divisionUrl,
    selected,
    field,
    containerType,
    styleClass) {

    if (!field) {
        field = 'id';
    }

    if (!containerType) {
        containerType = 'select';
    }

    // Instituciones
    $.ajax({
        url: institucionUrl,
        type: "GET",
        success: function (data) {
            var contentInstitucion = "";
            var contentServicio = "";
            var contentDivision = "";
            var items = new Object;
            if ('result' in data) {
                items = data.result;
            } else {
                items = data;
            }

            $.each(items, function (index, value) {
                var options = 'value="' + value['id'] + '"' + (value[field] === selected ? 'selected="selected"' : '');
                var ot = getOpenTagForContainer(
                    containerType,
                    options,
                    styleClass
                );
                var ct = getClosingTagForContainer(containerType);

                contentInstitucion += ot + value.nombre + ct;

                if (value[field] === selected) {
                    servicioUrl = servicioUrl + "/" + value['id'];
                    // Servicios
                    $.ajax({
                        url: servicioUrl,
                        type: "GET",
                        success: function (data) {
                            var items = new Object;
                            if ('result' in data) {
                                items = data.result;
                            } else {
                                items = data;
                            }

                            $.each(items, function (index, value) {
                                var options = 'value="' + value['id'] + '"' + (value[field] === selected ? 'selected="selected"' : '');
                                var ot = getOpenTagForContainer(
                                    containerType,
                                    options,
                                    styleClass
                                );
                                var ct = getClosingTagForContainer(containerType);

                                contentServicio += ot + value.nombre + ct;

                                if (value[field] === selected) {
                                    divisionUrl = divisionUrl + "/" + value['id'];
                                    // Divisiones
                                    $.ajax({
                                        url: divisionUrl,
                                        type: "GET",
                                        success: function (data) {
                                            var items = new Object;
                                            if ('result' in data) {
                                                items = data.result;
                                            } else {
                                                items = data;
                                            }

                                            $.each(items, function (index, value) {
                                                var options = 'value="' + value['id'] + '"' + (value[field] === selected ? 'selected="selected"' : '');
                                                var ot = getOpenTagForContainer(
                                                    containerType,
                                                    options,
                                                    styleClass
                                                );
                                                var ct = getClosingTagForContainer(containerType);
                                                contentDivision += ot + value.nombre + ct;
                                            });
                                            divisionContainer.append(contentDivision);
                                        },
                                        error: function (data) {
                                            console.log("Nope!");
                                        }
                                    });
                                }
                            });
                            servicioContainer.append(contentServicio);
                        },
                        error: function (data) {
                            console.log("Nope!");
                        }
                    });
                }
            });
            institucionContainer.append(contentInstitucion);
        },
        error: function (data) {
            console.log("Nope!");
        }
    });
}

/**
 * 
 * @param {string} containerType - The type of container for opening tag
 * @param {string} [options] - Options to add to the tag
 * @param {string} [styleClass] - The class for the container
 * @returns {string} - The opening tag for the given container type
 */
function getOpenTagForContainer(containerType, options, styleClass) {
    if (!styleClass) {
        styleClass = '';
    } else {
        styleClass = 'class="' + styleClass + '"';
    }
    switch (containerType) {
        case "select":
            return '<option ' + styleClass + options + '>';
        case "ul":
            return '<li class="' + styleClass + '" ' + options + '>';
    }
}

/**
 * 
 * @param {string} containerType - The type of container for closing tag
 * @returns {string} - The closing tag for the given container type
 */
function getClosingTagForContainer(containerType) {
    switch (containerType) {
        case 'select':
            return '</option>';
        case 'ul':
            return '</li>';
    }
}