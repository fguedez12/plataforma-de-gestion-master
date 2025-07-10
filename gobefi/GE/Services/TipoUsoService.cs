using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoUsoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoUsoService : ITipoUsoService
    {
        private ITipoUsoRepository _repoTipoUso;
        private readonly IDivisionRepository _repoDivision;
        private ILogger logger;
        private IMapper mapper;

        public TipoUsoService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ITipoUsoRepository repoTipoUso,
            IDivisionRepository repoDivision)
        {
            this.logger = loggerFactory.CreateLogger<TipoTarifaService>();
            this.mapper = mapper;
            _repoTipoUso = repoTipoUso;
            _repoDivision = repoDivision;
        }

        public IEnumerable<TipoUsoModel> All()
        {
            return _repoTipoUso
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<TipoUsoModel>(mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TipoUsoModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoUsoModel> getByEdificioId(long edificioId)
        {
            var divisiones = _repoDivision.Query().Where(d => d.EdificioId == edificioId);

            divisiones = divisiones.Include(tu => tu.TipoUso);

            var tiposDeUso = divisiones.Select(tu => tu.TipoUso).Distinct().ToList();

            return mapper.Map<IEnumerable<TipoUsoModel>>(tiposDeUso);
        }

        public long Insert(TipoUsoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoUsoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
