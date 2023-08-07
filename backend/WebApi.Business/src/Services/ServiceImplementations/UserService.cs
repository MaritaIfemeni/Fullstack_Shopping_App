using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {

        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _userRepo = userRepo;
        }

        public UserDto UpadatePassword(string id, string newPassword)
        {
            var foundUser = _userRepo.GetOneById(id);
            if (foundUser is null)
            {
                throw new Exception("Not Found"); // change this to a custom exception
            }
            return _mapper.Map<UserDto>(_userRepo.UpdatePassword(foundUser, newPassword));
        }
    }
}