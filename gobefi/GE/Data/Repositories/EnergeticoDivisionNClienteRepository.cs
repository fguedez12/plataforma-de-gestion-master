using System.Collections.Generic;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace GobEfi.Web.Data.Repositories
{
    public class EnergeticoDivisionRepository : BaseRepository<EnergeticoDivision, long>, IEnergeticoDivisionRepository
    {

        public EnergeticoDivisionRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public List<long> Get(long DivisionId, long EnergeticoId)
        {
            List<long?> retFromTable = ctx.EnergeticoDivision
                .Where(a => 
                a.DivisionId == DivisionId && 
                a.EnergeticoId == EnergeticoId && 
                a.NumeroClienteId != null && 
                a.Active == true)
                .Select(b => b.NumeroClienteId).ToList();

            List<long> toRet = new List<long>();

            for (int i = 0; i < retFromTable.Count; i++)
            {
                toRet.Add(retFromTable[i] ?? 0);
            }

            return toRet;
        }

        public EnergeticoDivision Get(long nClienteId, long divisionId, long energeticoId)
        {
            EnergeticoDivision data = this.ctx.EnergeticoDivision
                .Where(a =>
                a.DivisionId == divisionId &&
                a.EnergeticoId == energeticoId &&
                a.NumeroClienteId == nClienteId &&
                a.Active == true).AsNoTracking()
                .FirstOrDefault();

            return data;
        }

        public List<EnergeticoDivision> Get(long divisionId, long energeticoId, bool active = true)
        {
            var ret = this.ctx.EnergeticoDivision
                .Where(a =>
                a.DivisionId == divisionId &&
                a.EnergeticoId == energeticoId &&
                a.Active == active).AsNoTracking().ToList();

            return ret;
        }

        public IList<EnergeticoDivision> GetByDivisionId(long id)
        {

            var ret = (from item in ctx.EnergeticoDivision
                       where item.DivisionId == id
                       select item).AsNoTracking().ToList();


            return ret;
        }

        public bool TieneMasAsociado(long nClienteId)
        {
            var ret = this.ctx.EnergeticoDivision.Where(a => a.NumeroClienteId == nClienteId && a.Active == true);


            return (ret.Count() <= 1);
        }
    }
}
