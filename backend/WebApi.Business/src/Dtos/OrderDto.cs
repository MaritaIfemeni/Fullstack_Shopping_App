using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
 public class OrderReadDto
    {
        public UserReadDto UserDto { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderDetailReadDto> OrderDetailReadDto { get; set; }
    }

    public class OrderCreateDto
    {
        public UserReadDto UserDto { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderDetailCreateDto> OrderDetailCreateDto { get; set; }
    }

    public class OrderUpdateDto
    {
        public UserReadDto UserDto { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderDetailUpdateDto> OrderDetailUpdateDto { get; set; }
    }
}