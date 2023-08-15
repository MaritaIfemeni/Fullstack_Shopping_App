namespace WebApi.Domain.src.Entities
{
    public class OrderDetail
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}