using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Services.ServiceInterfaces;

namespace WebApi.Controller.src.Controllers
{
    public class UserController : CrudController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _userService;
        public UserController(IUserService baseService) : base(baseService)
        {
            _userService = baseService;
        }
    }
}