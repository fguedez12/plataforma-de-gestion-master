using AutoMapper;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using GobEfi.Web.Helper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    public class CustomController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        protected async Task<List<TDTO>> Get<TEntity,TDTO>()  where TEntity :class
        {
            var entities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
            return mapper.Map<List<TDTO>>(entities);
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>(PaginationDTO paginationDTO, IQueryable<TEntity> queryable) 
            where TEntity : class
        {
            await HttpContext.PaginationParams(queryable, paginationDTO.PerPage);
            var entidades = await queryable.Pager(paginationDTO).ToListAsync();
            return mapper.Map<List<TDTO>>(entidades);

        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>(IQueryable<TEntity> queryable)
            where TEntity : class
        {
            var entities = await queryable.AsNoTracking().ToListAsync();
            return mapper.Map<List<TDTO>>(entities);
        }

        protected async Task<ActionResult> Patch<TEntidad, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument)
            where TDTO : class
            where TEntidad : class, IId
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entidadDB = await context.Set<TEntidad>().FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<TDTO>(entidadDB);

            patchDocument.ApplyTo(dto, ModelState);

            var isValid = TryValidateModel(dto);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(dto, entidadDB);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
