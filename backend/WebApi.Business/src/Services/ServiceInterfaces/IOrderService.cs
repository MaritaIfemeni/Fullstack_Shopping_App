using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServiceInterfaces
{
    public interface IOrderService : IBaseService<Order, OrderDto>
    {
        OrderDto UpdateOrderStatus(string id, string newStatus); // for admin user
        OrderDto GetOrderStatus(string id); // for admin user
    }
}