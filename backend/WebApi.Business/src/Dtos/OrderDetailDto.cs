using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderDetailDto
    {
        public Order OrderId { get; set; }
        public Product ProductId { get; set; }
        public int Quantity { get; set; }
    }
}