using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Shared;
using WebApi.Domain.src.RepoInterfaces;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto> : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        private readonly IBaseRepo<T> _baseRepo;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepo<T> baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }

        public async Task<bool> DeleteOneById(string id)
        {
            var foundItem = await _baseRepo.GetOneById(id);
            if (foundItem is not null)
            {
                await _baseRepo.DeleteOneById(foundItem);
                return true;
            }
            return false;
        }

        public virtual async Task<IEnumerable<TReadDto>> GetAll(QueryOptions queryOptions)
        {
            return _mapper.Map<IEnumerable<TReadDto>>(await _baseRepo.GetAll(queryOptions));
        }

        public virtual async Task<TReadDto> GetOneById(string id)
        {
            return _mapper.Map<TReadDto>(await _baseRepo.GetOneById(id));
        }

        public virtual async Task<TReadDto> UpdateOneById(string id, TUpdateDto updated)
        {
            var foundItem = await _baseRepo.GetOneById(id);
            if (foundItem is null)
            {
                await _baseRepo.DeleteOneById(foundItem);
                throw new Exception("Not Found"); // change this to a custom exception
            }

            var updatedEntity = _baseRepo.UpdateOneById(foundItem, _mapper.Map<T>(updated));
            return _mapper.Map<TReadDto>(updatedEntity);
        }

        public virtual async Task<TReadDto> CreateOne(TCreateDto newEntity)
        {
            var createdEntity = await _baseRepo.CreateOne(_mapper.Map<T>(newEntity));
            return _mapper.Map<TReadDto>(createdEntity);
        }
    }
}