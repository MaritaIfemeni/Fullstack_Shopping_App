using AutoMapper;
using Moq;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceImplementations;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Configuration;
namespace WebApi.Business.Tests
{
    public class ProductServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepo> _productRepoMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _productRepoMock = new Mock<IProductRepo>();

            _productService = new ProductService(_productRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateProduct()
        {
            // Arrange
            var productCreateDto = new ProductCreateDto
            {
                ProductName = "Product 1",
                Price = 10,
                Stock = 10,
                Description = "Description",
                ProductImages = new List<ImageDto>
        {
            new ImageDto
            {
                Link = "https://example.com/image1.jpg"
            },
        }
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = productCreateDto.ProductName,
                Price = productCreateDto.Price,
                Stock = productCreateDto.Stock,
                Description = productCreateDto.Description,
                ProductImages = productCreateDto.ProductImages.Select(imageDto => new Image
                {
                    Id = Guid.NewGuid(),
                    Link = imageDto.Link
                }).ToList()
            };
            
            _productRepoMock.Setup(repo => repo.CreateOne(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _productService.CreateOne(productCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.ProductName, result.ProductName);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.ProductImages.Count, result.ProductImages.Count);
        }
    }
}