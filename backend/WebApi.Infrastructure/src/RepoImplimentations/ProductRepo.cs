using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.RepoImplimentations
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {

        private readonly DbSet<Product> _products;
        private readonly DatabaseContext _context;

        public ProductRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _products = dbContext.Products;
            _context = dbContext;
        }

        public override async Task<Product?> GetOneById(Guid id)
        {
             return await _products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}