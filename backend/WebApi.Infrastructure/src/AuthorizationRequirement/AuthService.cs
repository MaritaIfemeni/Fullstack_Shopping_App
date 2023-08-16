using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Shared;

namespace WebApi.Infrastructure.src.AuthorizationRequirement
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepo userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<string> VerifyCredentials(UserCredentialsDto credentials)
        {
            var foundUserEmail = await _userRepo.FindByEmail(credentials.Email) ?? throw new Exception("Email not found");
            var isAuthenticated = PasswordService.VerifyPassword(credentials.Password, foundUserEmail.Password, foundUserEmail.Salt);
            if (!isAuthenticated)
            {
                throw ServiceExeption.UnAuthAexeption();
            }
            return GenerateToken(foundUserEmail);
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString() )
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSettings:SecurityKey")));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("TokenSettings:Issuer"),
                Expires = DateTime.Now.AddMinutes(_configuration.GetValue<int>("TokenSettings:ExpirationMinutes")),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}