using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderDetailReadDto
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailCreateDto
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderDetailUpdateDto
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}