using WebApi.Business.src.Dtos;

namespace WebApi.Business.src.Services.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<string> VerifyCredentials(UserCredentialsDto credentials);
    }
}