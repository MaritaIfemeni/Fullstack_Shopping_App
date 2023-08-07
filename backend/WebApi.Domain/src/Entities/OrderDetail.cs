namespace WebApi.Domain.src.Entities
{
    public class OrderDetail
    {
        public Order OrderId { get; set; }
        public Product ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}