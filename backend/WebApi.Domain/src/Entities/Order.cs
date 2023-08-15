namespace WebApi.Domain.src.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus OrderStatus { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
    
    public enum OrderStatus
    {
        Processing,
        Shipped
    }
}