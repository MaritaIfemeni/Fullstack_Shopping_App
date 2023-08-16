using WebApi.Domain.src.Shared;

namespace WebApi.Domain.src.RepoInterfaces
{
    public interface IBaseRepo<T>
    {
        Task<IEnumerable<T>> GetAll(QueryOptions queryOptions);
        Task<T?> GetOneById(Guid id);
        Task<T> UpdateOneById(T updatedEntity);
        Task<bool> DeleteOneById(T entityToDelete);
        Task<T> CreateOne(T entityToCreate);
    }
}