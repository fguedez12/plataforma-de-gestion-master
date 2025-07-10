using GobEfi.Web.Core;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDbContext ctx;
        protected readonly UserManager<Usuario> manager;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly Usuario usuario;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(
            ApplicationDbContext context,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.ctx = context;
            this.manager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.usuario = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<TEntity> Datasource => ctx.Set<TEntity>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query() => Datasource.AsNoTracking().AsQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            Datasource.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            Datasource.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            Datasource.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> All()
        {
            return Datasource.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(TKey id)
        {
            return Datasource.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="query"></param>
        /// <param name="currenPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<TDto> Paged<TDto>(IQueryable<TDto> query, int currenPage, int pageSize)
        {
            return new PagedList<TDto>(query, currenPage, pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return ctx.SaveChanges();
        }
    }
}
