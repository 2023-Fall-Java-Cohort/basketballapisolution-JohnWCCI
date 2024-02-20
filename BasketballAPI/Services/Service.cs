using BasketballAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace BasketballAPI.Services
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly BasketballContext dbContext;
        private readonly ILogger<TEntity> logger;
        protected readonly string className;

        public Service(BasketballContext dbContext, ILogger<TEntity> logger)
        {
            this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger)); ;
            className = typeof(TEntity).FullName ?? "TEntity";
            logger.LogInformation($"Service Constructor called for {className}");

        }
        public virtual async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
        {
            try
            {
                logger.LogInformation($"Add {className} to table {entity}");
                dbContext.Set<TEntity>()
                    .Add(entity);
                await dbContext.SaveChangesAsync(cancellation);
                dbContext.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add {className} using {entity}");
                throw;
            }
            return entity;
        }

        public virtual async ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
        {
            try
            {
                dbContext.Set<TEntity>()
                    .Update(entity)
                    //.Add(entity)
                    .State = EntityState.Modified;
                await dbContext.SaveChangesAsync(cancellation);
                dbContext.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error saving update failed {className} changes to the Database {entity}");
                throw;
            }
            return entity;
        }

        public virtual async ValueTask<List<TEntity>> GetAllAsync(CancellationToken cancellation = default(CancellationToken))
        {
            try
            {
                return await dbContext.Set<TEntity>()
                    .AsNoTracking()
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting all {className} from the Database");
                throw;
            }
        }

        public virtual async ValueTask<TEntity?> GetAsync(int id, CancellationToken cancellation = default(CancellationToken))
        {
            TEntity? entity;
            try
            {
                entity = await dbContext.Set<TEntity>()
                    .Where(w => EF.Property<int>(w, "Id") == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting {className} from the Database using Id = {id}");
                throw;
            }
            return entity;
        }

        public virtual async ValueTask<bool> DeleteAsync(TEntity entity, CancellationToken cancellation = default(CancellationToken))
        {
            bool result = false;
            try
            {
                dbContext.Set<TEntity>()
                    .Remove(entity);
                await dbContext.SaveChangesAsync(cancellation);
                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error deleting {className} from the Database  {entity}");
                throw;
            }
            return result;
        }
    }
}
