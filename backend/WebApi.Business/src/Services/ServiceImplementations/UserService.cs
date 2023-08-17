using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Shared;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class UserService : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _userRepo = userRepo;
        }

        public override async Task<UserReadDto> CreateOne(UserCreateDto dto)
        {
            if (!dto.Email.Contains("@"))
            {
                throw ServiceExeption.FieldRequirementsExeption("Email is not valid");
            }
            var entity = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            entity.Password = hashedPassword;
            entity.Salt = salt;
            var created = await _userRepo.CreateOne(entity);
            return _mapper.Map<UserReadDto>(created);
        }

        public async Task<UserReadDto> GreateAdmin(UserCreateDto CreatedDto)
        {
            if (!CreatedDto.Email.Contains("@"))
            {
                throw ServiceExeption.FieldRequirementsExeption("Email is not valid");
            }
            var entity = _mapper.Map<User>(CreatedDto);
            PasswordService.HashPassword(CreatedDto.Password, out var hashedPassword, out var salt);
            entity.Password = hashedPassword;
            entity.Salt = salt;
            var created = await _userRepo.CreateAdmin(entity);
            return _mapper.Map<UserReadDto>(created);
        }
    }
}