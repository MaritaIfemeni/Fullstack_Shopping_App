using WebApi.Business.src.Dtos;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public interface IAuthService
    {
         Task<string> VerifyCredentials(UserCredentialsDto credentials);
    }
}