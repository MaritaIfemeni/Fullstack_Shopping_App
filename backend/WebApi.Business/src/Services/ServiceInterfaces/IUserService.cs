using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServiceInterfaces
{
    public interface IUserService : IBaseService<User, UserDto>
    {
        UserDto UpadatePassword(string id, string password);
    }
}