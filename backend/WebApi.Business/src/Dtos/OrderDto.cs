using AutoMapper.Configuration.Annotations;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderReadDto
    {
        public OrderStatus OrderStatus { get; set; }
        public string FullName { get; set; }
        public string DeliveryAddress { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetailReadDto> OrderDetails { get; set; }
    }

    public class OrderCreateDto
    {
        public string FullName { get; set; }
        public string DeliveryAddress { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
    }

    public class OrderUpdateDto
    {
        public OrderStatus Status { get; set; }
    }
}