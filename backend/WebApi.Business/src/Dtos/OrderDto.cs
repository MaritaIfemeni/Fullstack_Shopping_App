using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderDto
    {
        public User User { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}