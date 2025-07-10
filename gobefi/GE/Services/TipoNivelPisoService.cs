using AutoMapper;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoNivelPisoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoNivelPisoService : ITipoNivelPisoService
    {
        public IEnumerable<TipoNivelPisoModel> All()
        {
            var tipoList = new List<TipoNivelPisoModel>();
            var values = Enum.GetValues(typeof(TipoNivel)).Cast<TipoNivel>().ToList();

            foreach (TipoNivel val in values)
            {
                var tipo = new TipoNivelPisoModel() {Id = (int)val , Nombre = Helper.EnumHelper.GetEnumDisplayName(val) };
                tipoList.Add(tipo);
            }

            return tipoList.OrderBy(a => a.Nombre).ToList();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public TipoNivelPisoModel Get(int id)
        {
            return new TipoNivelPisoModel() { Id = id, Nombre = Helper.EnumHelper.GetEnumDisplayName((TipoNivel)id) };
        }

        public TipoNivelPisoModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(TipoNivelPisoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoNivelPisoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
