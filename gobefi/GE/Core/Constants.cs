using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core
{
    public class Constants
    {
        public static class Claims
        {
            public const string ES_ADMINISTRADOR = "ADMINISTRADOR";
            public const string ES_DESARROLLADOR = "DESARROLLADOR";
            public const string ES_GESTORSERVICIO = "GESTOR_SERVICIO";
            public const string ES_GESTORUNIDAD = "GESTOR_UNIDAD";
            public const string ES_GESTORCONSULTA = "GESTOR DE CONSULTA";
            public const string ES_AUDITOR = "AUDITOR";
            public const string ES_USUARIO = "USUARIO";
        }

        public static class Buttons
        {
            public const string VOLVER = "Atrás";
            public const string ACTUALIZAR = "Actualizar";
            public const string GUARDAR = "Guardar";
            public const string BUSCAR = "Buscar";
            public const string FILTRAR = "Filtrar";
            public const string CREAR = "Crear";
            public const string CANCELAR = "Cancelar";
            public const string EDITAR = "Editar";
            public const string ELIMINAR = "Eliminar";
        }

        public static class Icons
        {
            public const string DESCARGAR = "fa fa-download";
            public const string EDITAR = "fa fa-pencil-square-o";
            public const string VER = "fa fa-eye";
            public const string CREAR = "fa fa-file";
            public const string FILTRAR = "fa fa-filter";
            public const string ELIMINAR = "fa fa-trash-o";
            public const string IR = "fa fa-pencil-square-o";
            public const string VOLVER = "fa fa-backward";
            public const string GUARDAR = "fa fa-save";
        }

        public static class Titles
        {
            public const string SELECCIONE = "-- SELECCIONE --";
            public const string ACCION = "Acciones";
            public const string FILTRAR = "Filtrar Resultados";
            public const string EXPORTAR = "Exportar";
        }
    }
}
