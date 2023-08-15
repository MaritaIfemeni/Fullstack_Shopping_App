using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderReadDto
    {
        public OrderStatus OrderStatus { get; set; }
        public UserReadDto User { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetailReadDto> OrderDetails { get; set; }
    }

    public class OrderCreateDto
    {
        public OrderStatus OrderStatus { get; set; }
        public UserReadDto User { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
    }

    public class OrderUpdateDto
    {
        public OrderStatus Status { get; set; }
    }
}