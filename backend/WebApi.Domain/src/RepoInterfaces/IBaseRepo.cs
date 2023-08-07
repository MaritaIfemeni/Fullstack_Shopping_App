using WebApi.Domain.src.Shared;

namespace WebApi.Domain.src.RepoInterfaces
{
    public interface IBaseRepo<T>
    {
        IEnumerable<T> GetAll<T>(QueryOptions options);
        T GetOneById(string id);
        T UpdateOneById(T orginalEntity, T updatedEntity);
        bool DeleteOneByID(T entityToDelete);
    }
}