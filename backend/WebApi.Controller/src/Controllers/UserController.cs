using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Domain.src.Shared;

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
        [HttpPost]
        public override async Task<ActionResult<UserReadDto>> CreateOne([FromBody] UserCreateDto dto)
        {
            return CreatedAtAction(nameof(CreateOne), await _userService.CreateOne(dto));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto dto)
        {
            return CreatedAtAction(nameof(CreateAdmin), await _userService.GreateAdmin(dto));
        }

        //[Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _userService.GetAll(queryOptions));
        }

        [AllowAnonymous]  //add resourse base auth if time
        public override async Task<ActionResult<UserReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _userService.GetOneById(id));
        }

    }
}