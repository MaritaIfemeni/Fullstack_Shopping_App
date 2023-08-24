using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.src.Entities
{
    public class Product : BaseEntity
    {
        [Required] 
        public required string ProductName { get; set; }
        [Required] 
        public float Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Image> ProductImages { get; set; } = new List<Image>();
    }
}