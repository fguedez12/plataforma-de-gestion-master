using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GobEfi.Web.Data.Repositories
{
    public class MedidorRepository : BaseRepository<Medidor, long>, IMedidorRepository
    {

        public MedidorRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public int CambiaEstadoCompartido(long nClienteId, bool swap)
        {
            List<Medidor> medidores = this.ctx.Medidores.Where(a => a.NumeroClienteId == nClienteId).ToList();

            foreach (Medidor item in medidores)
            {
                item.Compartido = swap;
                item.UpdatedAt = DateTime.Now;

                Update(item);
                SaveChanges();
            }
            return 1; 
        }


        //public IQueryable<Medidor> Get(long divisionId, long EnergeticoDivisionId)
        //{
        //    var ret = (from a in this.dbContext.Medidores
        //               where a.DivisionId == divisionId &&
        //               a.EnergeticoDivisionId == EnergeticoDivisionId &&
        //               a.Active == true
        //               select a).AsNoTracking().AsQueryable();


        //    return ret;
        //}

        public IQueryable<Medidor> GetByNumClienteId(long id)
        {
            IQueryable<Medidor> ret = null;

            ret = (from a in this.ctx.Medidores
                        where a.NumeroClienteId == id && a.Active == true
                        select a).AsNoTracking().AsQueryable();

            return ret;
        }

        //public IQueryable<Medidor> GetByNumClientesIds(List<long> NumClienteIds)
        //{
        //    IQueryable<Medidor> ret = null;

        //    ret = (from a in this.ctx.Medidores
        //           where NumClienteIds.Contains(a.NumeroClienteId) &&
        //           a.Active == true
        //           select a).AsNoTracking().AsQueryable();

        //    return ret;
        //}

        public IQueryable<Medidor> GetByNumMedidor(long numMedidor)
        {
            IQueryable<Medidor> ret = null;

            ret = (from a in this.ctx.Medidores
                   where a.Numero == numMedidor.ToString()
                   select a).AsNoTracking().AsQueryable();

            return ret;
        }
    }
}
