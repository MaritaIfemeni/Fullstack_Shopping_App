using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Shared;
using WebApi.Domain.src.RepoInterfaces;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class BaseService<T, TDto> : IBaseService<T, TDto>
    {
        private readonly IBaseRepo<T> _baseRepo;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepo<T> baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }
        public bool DeleteOneById(string id)
        {
            var foundItem = _baseRepo.GetOneById(id);
            if (foundItem is not null)
            {
                _baseRepo.DeleteOneById(foundItem);
                return true;
            }
            return false;
        }

        public IEnumerable<TDto> GetAll(QueryOptions queryOptions)
        {
            return _mapper.Map<IEnumerable<TDto>>(_baseRepo.GetAll(queryOptions));
        }

        public TDto GetOneById(string id)
        {
            return _mapper.Map<TDto>(_baseRepo.GetOneById(id));
        }

        public TDto UpdateOneById(string id, TDto updated)
        {
            var foundItem = _baseRepo.GetOneById(id);
            if (foundItem is null)
            {
                _baseRepo.DeleteOneById(foundItem);
                throw new Exception("Not Found"); // change this to a custom exception
            }

            var updatedEntity = _baseRepo.UpdateOneById(foundItem, _mapper.Map<T>(updated));
            return _mapper.Map<TDto>(updatedEntity);
        }
    }
}