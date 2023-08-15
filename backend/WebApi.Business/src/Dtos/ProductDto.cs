using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class ProductReadDto
    {

        public string ProductName { get; set; } = string.Empty;
        public float Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Image> ProductImages { get; set; } 
    }

    public class ProductCreateDto
    {
        public string ProductName { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Image> ProductImages { get; set; }
    }

    public class ProductUpdateDto
    {
        public string ProductName { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Image> ProductImages { get; set; }
    }
}