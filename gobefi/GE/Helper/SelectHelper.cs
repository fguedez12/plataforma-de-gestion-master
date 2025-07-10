using GobEfi.Web.Core;
using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Models.ProvinciaModels;
using GobEfi.Web.Models.RegionModels;
using GobEfi.Web.Models.RolModels;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Models.SexoModels;
using GobEfi.Web.Models.TipoPropiedadModels;
using GobEfi.Web.Models.TipoUsoModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using GobEfi.Web.Core.Extensions;
using System.Threading.Tasks;
using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Models.NumeroClienteModels;

namespace GobEfi.Web.Helper
{
    public class SelectHelper
    {
        #region [INSTITUCION]
        public static List<SelectListItem> LlenarDDL(IEnumerable<InstitucionModel> instituciones, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in instituciones) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }

        public static List<SelectListItem> LlenarDDL(IEnumerable<InstitucionListModel> instituciones, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in instituciones) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [SERVICIO]
        public static List<SelectListItem> LlenarDDL(IEnumerable<ServicioModel> servicios, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in servicios) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [DIVISIONES]
        public static List<SelectListItem> LlenarDDL(IEnumerable<DivisionModel> divisiones, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in divisiones) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [COMUNAS]
        public static List<SelectListItem> LlenarDDL(IEnumerable<ComunaModel> comunas, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in comunas) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [ENERGETICO]
        public static List<SelectListItem> LlenarDDL(IEnumerable<EnergeticoModel> energeticos, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in energeticos) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }


        #endregion

        #region [REGIONES]
        public static List<SelectListItem> LlenarDDL(IEnumerable<RegionModel> regiones, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in regiones) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [PROVINCIAS]
        public static List<SelectListItem> LlenarDDL(IEnumerable<ProvinciaModel> provincias, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in provincias) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [TIPOUSO]
        public static List<SelectListItem> LlenarDDL(IEnumerable<TipoUsoModel> tiposUso, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in tiposUso) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [ROLE]
        public static List<SelectListItem> LlenarDDL(IEnumerable<RolModel> roles, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in roles) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [TIPOPROPIEDAD]
        public static List<SelectListItem> LlenarDDL(IEnumerable<TipoPropiedadModel> tiposPropiedad, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in tiposPropiedad) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [SEXO]
        public static List<SelectListItem> LlenarDDL(IEnumerable<SexoModel> sexos, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in sexos) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [EDIFICIO]
        public static List<SelectListItem> LlenarDDL(IEnumerable<EdificioModel> edificios, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in edificios) opciones.Add(new SelectListItem() { Text = $"{x.Direccion}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [EMPRESA DISTRIBUIDORA]
        public static List<SelectListItem> LlenarDDL(IEnumerable<EmpresaDistribuidoraModel> empresaDistribuidora, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in empresaDistribuidora) opciones.Add(new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [NUMERO DE CLIENTE]
        public static List<SelectListItem> LlenarDDL(IEnumerable<NumeroClienteModel> numClientes, bool agregarSeleccion = false)
        {
            var opciones = llenarOpciones(agregarSeleccion);
            foreach (var x in numClientes) opciones.Add(new SelectListItem() { Text = $"{x.Numero}", Value = $"{x.Id}" });

            return opciones;
        }
        #endregion

        #region [METODO PRIVADO]
        private static List<SelectListItem> llenarOpciones(bool agregarSeleccion)
        {
            var opciones = new List<SelectListItem>();
            if (agregarSeleccion) opciones.Add(new SelectListItem() { Text = Constants.Titles.SELECCIONE, Value = "" });
            return opciones;
        }
        #endregion

        public static List<SelectListItem> LlenarEnumTiene()
        {
            var opciones = new List<SelectListItem>();
            var valores = Enum.GetValues(typeof(EnumTiene))
                .Cast<EnumTiene>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToDescription(),
                    Value = Convert.ToInt32(x) == 0 ? "" : Convert.ToInt32(x) == 1 ? "true" : "false"
                })
                .ToList();

            opciones.AddRange(valores);
            return opciones;
        }
    }
}
