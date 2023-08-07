using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class UsersServices : BaseService<User, UserDto>, IUsersService
    {
        public UserDto UpadatePassword(string id, string password)
        {
            throw new NotImplementedException();
        }
    }
}