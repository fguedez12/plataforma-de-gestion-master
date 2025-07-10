using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.InstitucionModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class ComunaService : IComunaService
    {
        private readonly IComunaRepository _repoComuna;
        private readonly IEdificioRepository _repoEdificio;
        private readonly IMapper _mapper;

        public ComunaService(IComunaRepository repoComuna, IEdificioRepository repoEdificio, IMapper mapper)
        {
            _repoComuna = repoComuna;
            _repoEdificio = repoEdificio;
            _mapper = mapper;
        }

        public IEnumerable<ComunaModel> All()
        {
            return _repoComuna
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<ComunaModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public ComunaModel Get(long id)
        {
            return _mapper.Map<ComunaModel>(_repoComuna.Get(id));
        }

        public async Task<IEnumerable<ComunaModel>> GetAllAsync()
        {
            var result = _repoComuna.Query();


            return await result.ProjectTo<ComunaModel>(_mapper.ConfigurationProvider).OrderBy(n => n.Nombre).ToListAsync();
        }

        public async Task<ComunaModel> GetAsync(long comunaId)
        {
            var comuna = _repoComuna.Query().Where(c => c.Id == comunaId);

            return  _mapper.Map<ComunaModel>(await comuna.FirstOrDefaultAsync());

        }

        public async Task<IEnumerable<ComunaModel>> GetByEdificioId(long edificioId)
        {
            var comuna = _repoEdificio.Query().Where(e => e.Id == edificioId).Include(e => e.Comuna).Select(e => e.Comuna);

            return await comuna.ProjectTo<ComunaModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public IEnumerable<ComunaModel> GetByEmpresaDistribuidora(long idEmpresaDistribuidora)
        {
            var ret =_repoComuna.Query()
                .Include(c => c.EmpresaDistribuidoraComunas)
                .Where(ec => ec.EmpresaDistribuidoraComunas.Any(e => (e.EmpresaDistribuidoraId == idEmpresaDistribuidora) && e.Active))
                .OrderBy(c => c.Nombre);

            return ret.ProjectTo<ComunaModel>(_mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<ComunaModel> GetByEmpresaDistribuidoraNoAsociadas(long idEmpresaDistribuidora)
        {
            var siAsociados = GetByEmpresaDistribuidora(idEmpresaDistribuidora);

            
            var ret = _repoComuna.Query()                
                .Where(c => !siAsociados.Any(si => si.Id == c.Id))
                .OrderBy(c => c.Nombre);

            return ret.ProjectTo<ComunaModel>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<IEnumerable<ComunaModel>> GetByProvinciaId(long provinciaId)
        {
            var comunasByProvinciaId = await _repoComuna.Query().Where(c => c.ProvinciaId == provinciaId).ToListAsync();

            return _mapper.Map<IEnumerable<ComunaModel>>(comunasByProvinciaId);

        }

        public long Insert(ComunaModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(ComunaModel model)
        {
            throw new NotImplementedException();
        }
    }
}
