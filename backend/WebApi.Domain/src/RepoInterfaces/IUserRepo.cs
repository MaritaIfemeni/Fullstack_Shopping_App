using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.RepoInterfaces
{
    public interface IUserRepo : IBaseRepo<User>
    {
        Task<User> CreateAdmin(User user);
        Task<User> UpdatePassword(User user);
        Task<User?> FindByEmail(string email);
    }
}