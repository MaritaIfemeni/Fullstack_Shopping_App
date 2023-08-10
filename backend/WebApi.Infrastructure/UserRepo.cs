using WebApi.Domain.src.RepoInterfaces;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Shared;

namespace WebApi.Infrastructure
{
    public class UserRepo : IUserRepo
    {
        public UserRepo()
        {
        }
        public Task<User> CreateAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateOne(User entityToCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneById(User entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll(QueryOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetOneById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateOneById(User orginalEntity, User updatedEntity)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdatePassword(User user, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}