using Microsoft.AspNetCore.Mvc;
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
    }
}