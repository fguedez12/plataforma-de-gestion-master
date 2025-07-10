using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class NumeroClienteRepository : BaseRepository<NumeroCliente, long>, INumeroClienteRepository
    {

        public NumeroClienteRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        {
        }

        public IQueryable<NumeroCliente> Get(long divisionId, long energeticoDivisionId)
        {
            var ret = (from a in this.ctx.NumeroClientes
                       where a.Active == true
                       select a).AsNoTracking().AsQueryable();

            return ret;
        }

        public NumeroCliente GetByNumero(string numeroCliente)
        {
            var ret = ctx.NumeroClientes.AsNoTracking().FirstOrDefault(a => a.Numero == numeroCliente && a.Active == true);


            return ret;
        }

        public bool NumClientExist(string numeroCliente, long divisionId, long energeticoDivisionId)
        {
            if (ctx.NumeroClientes.Any(x => x.Numero == numeroCliente))
                return true;

            return false;
        }
    }
}
