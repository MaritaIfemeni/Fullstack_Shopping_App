using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Domain.src.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
    }
    
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Processing = 1,
        Shipped = 2
    }
}