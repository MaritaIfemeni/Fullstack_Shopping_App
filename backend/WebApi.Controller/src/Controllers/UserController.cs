using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.src.Controllers
{
    public class UserController : CrudController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _userService;

        public UserController(IUserService baseService) : base(baseService)
        {
            _userService = baseService;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<UserReadDto>> CreateOne([FromBody] UserCreateDto dto)
        {
            var createdObject = await base.CreateOne(dto);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<UserReadDto>> GetOneById([FromRoute] Guid id)
        {
            var foundUser = await base.GetOneById(id);
            return foundUser;
        }
    }
}