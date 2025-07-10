using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class EnergeticoRepository : BaseRepository<Energetico, long>, IEnergeticoRepository
    {

        public EnergeticoRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public Energetico CheckFirstEnergetico(long numClienteId, long energeticoIdActual)
        {
            var item = (from a in this.ctx.EnergeticoDivision
                        join b in this.ctx.Energeticos on a.EnergeticoId equals b.Id
                        where a.NumeroClienteId == numClienteId &&
                        b.Id == energeticoIdActual
                        orderby a.CreatedAt
                        select b).FirstOrDefault();

            return item;
        }

        public ICollection<Energetico> GetByDivisionId(long divisionId)
        {
            var energeticos = ctx.EnergeticoDivision.Where(edn => edn.DivisionId == divisionId && edn.Active == true && edn.NumeroClienteId == null).Select(e => e.EnergeticoId);

            var toRet = ctx.Energeticos.Where(e => energeticos.Contains(e.Id)).ToList();

            return toRet;
        }
    }
}
