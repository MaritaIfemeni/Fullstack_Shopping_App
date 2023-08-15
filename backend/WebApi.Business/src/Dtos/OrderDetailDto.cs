using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderDetailReadDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderDetailUpdateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}