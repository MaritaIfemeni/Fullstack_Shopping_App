using WebApi.Domain.src.Entities;
using static WebApi.Domain.src.Entities.Order;

namespace WebApi.Business.src.Dtos
{
    public class OrderDto
    {
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderDetailDto> OrderDetailDto { get; set; }
    }
}