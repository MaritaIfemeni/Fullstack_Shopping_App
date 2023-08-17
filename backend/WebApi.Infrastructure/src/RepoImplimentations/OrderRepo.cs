using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Database;
namespace WebApi.Infrastructure.src.RepoImplimentations
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DatabaseContext _context;

        public OrderRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _orders = dbContext.Orders;
            _context = dbContext;

        }
        public override async Task<Order> CreateOne(Order order)
        {
            order.OrderStatus = OrderStatus.Processing;
            return await base.CreateOne(order);
        }
        public override async Task<Order?> GetOneById(Guid id)
        {
             return await _orders.Include(p => p.OrderDetails).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}