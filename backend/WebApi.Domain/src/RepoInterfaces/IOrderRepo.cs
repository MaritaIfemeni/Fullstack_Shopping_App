using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.RepoInterfaces
{
    public interface IOrderRepo : IBaseRepo<Order>
    {
       
        public Order UpdateOrderStatus(Order order, string newStatus); // for admin user
        public Order GetOrderStatus(Order order); // for admin user

    }
}