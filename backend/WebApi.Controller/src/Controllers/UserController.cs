using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Services.ServiceInterfaces;

namespace WebApi.Controller.src.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : CrudController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;
        public UserController(IUserService baseService, IAuthorizationService authService) : base(baseService)
        {
            _authorizationService = authService;
            _userService = baseService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public override async Task<ActionResult<UserReadDto>> CreateOne([FromBody] UserCreateDto dto)
        {
            return CreatedAtAction(nameof(CreateOne), await _userService.CreateOne(dto));
        }

        [HttpPost("admin")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto dto)
        {
            return CreatedAtAction(nameof(CreateAdmin), await _userService.GreateAdmin(dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public override async Task<ActionResult<UserReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _userService.GetOneById(id));
        }

        // [Authorize]
        // [HttpGet("profile")]
        // [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        // [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        // public async Task<ActionResult<UserReadDto>> GetProfile([FromRoute] Guid id)
        // {
        //     var user = HttpContext.User;
        //     Console.WriteLine(user);
        //     Console.WriteLine("user: " + user.Identity?.Name);
        //     var userId = await _userService.GetOneById(id);
        //     Console.WriteLine("somethin" + userId);
        //     Console.WriteLine("userId: " + userId?.Id);
        //     var authorizeOwner = await _authorizationService.AuthorizeAsync(user, userId, "OwnerOnly");
        //     if (authorizeOwner.Succeeded)
        //     {
        //         return Ok(await _userService.GetOneById(id));
        //     }
        //     else
        //     {
        //         return new ForbidResult();
        //     }

        // }

        [HttpGet("profile")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public ActionResult<UserReadDto> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {


                return Ok(_userService.GetOneById(userId)); 
            }
            else
            {
                return BadRequest("Unable to obtain the user ID from claims.");
            }
        }
    }
}