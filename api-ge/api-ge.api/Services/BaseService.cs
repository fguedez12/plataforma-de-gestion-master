using api_gestiona.Entities;

namespace api_gestiona.Services
{
    public abstract class BaseService
    {
        protected TEntity SetAuditableSave<TEntity>(TEntity entity, string userId) where TEntity : BaseEntity
        {
            entity.Active = true;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = userId;
            entity.Version = 1;
            return entity;
        }

        protected TEntity SetAuditableUpdate<TEntity>(TEntity entity, string userId, TEntity entityDb) where TEntity : BaseEntity
        {
            entity.CreatedAt = entityDb.CreatedAt;
            entity.CreatedBy = entityDb.CreatedBy;
            entity.Active = true;
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = userId;
            entity.Version = entityDb.Version + 1;
            return entity;
        }
        protected TEntity SetAuditableDelete<TEntity>(TEntity entity, string userId) where TEntity : BaseEntity
        {
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = userId;
            entity.Version = entity.Version + 1;
            return entity;
        }
    }
}
