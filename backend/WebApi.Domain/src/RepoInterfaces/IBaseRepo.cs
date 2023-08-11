using WebApi.Domain.src.Shared;

namespace WebApi.Domain.src.RepoInterfaces
{
    public interface IBaseRepo<T>
    {
        Task<IEnumerable<T>> GetAll(QueryOptions queryOptions);  // should consider the sorting, searching and pagination all from here
        Task<T> GetOneById(Guid id);
        Task<T> UpdateOneById(T orginalEntity, T updatedEntity);
        Task<bool> DeleteOneById(T entityToDelete);
        Task<T> CreateOne(T entityToCreate);
    }
}