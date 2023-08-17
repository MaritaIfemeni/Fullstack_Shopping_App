using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.RepoImplimentations
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        private readonly DbSet<User> _users;
        private readonly DatabaseContext _context;

        public UserRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _users = dbContext.Users;
            _context = dbContext;
        }

        public async Task<User> CreateAdmin(User user)
        {
            user.UserRole = UserRole.Admin;
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> FindByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public override async Task<User> CreateOne(User entity)
        {
            entity.UserRole = UserRole.User;
            return await base.CreateOne(entity);
        }
        public override async Task<User> UpdateOneById(User updatedEntity)
        {
            updatedEntity.UserRole = UserRole.User;
            return await base.UpdateOneById(updatedEntity);
        }

    }
}