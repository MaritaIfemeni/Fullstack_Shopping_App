using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Dtos;

namespace WebApi.Controller.src.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> VerifyCredentials([FromBody] UserCredentialsDto credentials)
        {
            var token = await _authService.VerifyCredentials(credentials);
            return Ok(token);
        }

        // [HttpGet("profile")]
        // public IActionResult GetUserProfile()
        // {
        //     // Get the user's ID from the authenticated user's identity
        //     var userId = User.FindFirst(ClaimTypes.NameIdentifier);

        //     // Fetch the user's profile from the database using the user's ID
        //     var userProfile = _authService.User.FirstOrDefault(u => u.Id == userId);

        //     if (userProfile == null)
        //     {
        //         return NotFound("User profile not found.");
        //     }

        //     return Ok(new
        //     {
        //         Id = userProfile.Id,
        //         Email = userProfile.Email,
        //         Name = userProfile.Name,
        //         Role = User.FindFirst(ClaimTypes.Role), // Get the user's role from claims
        //         Avatar = userProfile.Avatar
        //     });
      // }
    }
}