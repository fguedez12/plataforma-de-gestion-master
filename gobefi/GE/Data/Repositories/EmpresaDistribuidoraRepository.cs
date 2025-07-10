using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class EmpresaDistribuidoraRepository : BaseRepository<EmpresaDistribuidora, long>, IEmpresaDistribuidoraRepository
    {

        public EmpresaDistribuidoraRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public void AgregarComunas(ICollection<long> comunas, long empresaDistribuidoraId)
        {
            foreach (var item in comunas)
            {
                EmpresaDistribuidoraComuna nuevo = new EmpresaDistribuidoraComuna();
                nuevo.EmpresaDistribuidoraId = empresaDistribuidoraId;
                nuevo.ComunaId = item;

                ctx.EmpresasDistribuidoraComunas.Add(nuevo);
            }

            ctx.SaveChanges();
            
        }

        public void DeleteComunas(long empresaDistribuidoraId)
        {
            var relaciones = ctx.EmpresasDistribuidoraComunas.Where(e => e.EmpresaDistribuidoraId == empresaDistribuidoraId);

            foreach (var item in relaciones)
            {
                ctx.Remove(item);
            }

            ctx.SaveChanges();
        }

        public IQueryable<EmpresaDistribuidora> GetByEnergetico(long energeticoId)
        {
            return ctx.EmpresaDistribuidoras.Where(e => e.EnergeticoId == energeticoId && e.Active);
        }
    }
}
