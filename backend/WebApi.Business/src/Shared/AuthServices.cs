using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Business.src.Services.ServiceImplementations;

namespace WebApi.Business.src.Shared
{
    public class AuthServices : IAuthService
    {
        private readonly IUserRepo _userRepo;

        public AuthServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> VerifyCredentials(UserCredentialsDto credentials)
        {
            var foundUserEmail = await _userRepo.FindByEmail(credentials.Email);
            var isAuthenticated = PasswordService.VerifyPassword(credentials.Password, foundUserEmail.Password, foundUserEmail.Salt);
            if (!isAuthenticated)
            {
                throw new Exception("Invalid credentials");
            }
            return GenerateToken(foundUserEmail);
        }


// this information should be stored infrastructure layer and hide 
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString() )
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("prackey-backend-jsdguyfsdgcjsdbchjsdb jdhscjysdcsdj"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "prac-backend",
                Expires = DateTime.Now.AddMinutes(10),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}