using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Database;
using WebApi.Domain.src.Shared;

namespace WebApi.Infrastructure.src.RepoImplimentations
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {

        private readonly DbSet<T> _dbSet;
        private readonly DatabaseContext _context;
        public BaseRepo(DatabaseContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _context = dbContext;
        }
        public virtual async Task<T> CreateOne(T entityToCreate)
        {
            await _dbSet.AddAsync(entityToCreate);
            await _context.SaveChangesAsync();
            return entityToCreate;
        }

        public virtual async Task<bool> DeleteOneById(T entityToDelete)
        {
            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll(QueryOptions queryOptions)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(queryOptions.Search))
            {
                if (typeof(T) == typeof(Product))
                {
                    query = query.Where(e => ((Product)(object)e).ProductName.Contains(queryOptions.Search));
                }
                else if (typeof(T) == typeof(User))
                {
                    query = query.Where(e =>
                        ((User)(object)e).FirstName.Contains(queryOptions.Search) ||
                        ((User)(object)e).LastName.Contains(queryOptions.Search)
                    );
                }
                else if (typeof(T) == typeof(Order))
                {
                    query = query.Where(e => ((Order)(object)e).OrderStatus.ToString().Contains(queryOptions.Search));
                }
            }
            if (queryOptions.Descending)
            {
                if (typeof(T) == typeof(Product))
                {
                    query = query.OrderByDescending(e => EF.Property<DateTime>((Product)(object)e, queryOptions.Order));
                }
                else if (typeof(T) == typeof(User))
                {
                    query = query.OrderByDescending(e => EF.Property<DateTime>((User)(object)e, queryOptions.Order));
                }
                else if (typeof(T) == typeof(Order))
                {
                    query = query.OrderByDescending(e => EF.Property<DateTime>((Order)(object)e, queryOptions.Order));
                }
            }
            else
            {
                if (typeof(T) == typeof(Product))
                {
                    query = query.OrderBy(e => EF.Property<DateTime>((Product)(object)e, queryOptions.Order));
                }
                else if (typeof(T) == typeof(User))
                {
                    query = query.OrderBy(e => EF.Property<DateTime>((User)(object)e, queryOptions.Order));
                }
                else if (typeof(T) == typeof(Order))
                {
                    query = query.OrderBy(e => EF.Property<DateTime>((Order)(object)e, queryOptions.Order));
                }
            }
            query = query.Skip((queryOptions.PageNumber - 1) * queryOptions.PageSize)
                        .Take(queryOptions.PageSize);

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetOneById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> UpdateOneById(T updatedEntity)
        {
            _dbSet.Update(updatedEntity);
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
    }
}