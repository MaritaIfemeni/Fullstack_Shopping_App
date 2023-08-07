using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Shared;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class BaseService<T, TDto> : IBaseService<T, TDto>
    {
        public bool DeleteOneById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDto> GetAll(QueryOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public TDto GetOneById(string id)
        {
            throw new NotImplementedException();
        }

        public TDto UpdateOneById(string id, TDto updated)
        {
            throw new NotImplementedException();
        }
    }
}