using AutoMapper;
using Moq;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceImplementations;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Configuration;
namespace WebApi.Business.Tests
{
    public class OrderServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IOrderRepo> _orderRepoMock;
        private readonly Mock<IProductRepo> _productRepoMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _orderRepoMock = new Mock<IOrderRepo>();
            _productRepoMock = new Mock<IProductRepo>();

            _orderService = new OrderService(_orderRepoMock.Object, _productRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateOrder()
        {
            // Arrange
            var orderCreateDto = new OrderCreateDto
            {
                UserId = Guid.NewGuid(),
                OrderDetails = new List<OrderDetailCreateDto>
        {
            new OrderDetailCreateDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = 2
            },
            new OrderDetailCreateDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = 3
            }
        }
            };

            var product1 = new Product
            {
                Id = orderCreateDto.OrderDetails[0].ProductId,
                ProductName = "Product 1",
                Price = 10
            };

            var product2 = new Product
            {
                Id = orderCreateDto.OrderDetails[1].ProductId,
                ProductName = "Product 2",
                Price = 20
            };

            _productRepoMock.Setup(repo => repo.GetOneById(product1.Id)).ReturnsAsync(product1);
            _productRepoMock.Setup(repo => repo.GetOneById(product2.Id)).ReturnsAsync(product2);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = orderCreateDto.UserId,
                OrderDetails = new List<OrderDetail>
        {
            new OrderDetail
            {
                Product = product1,
                Quantity = 2,
                ProductId = product1.Id
            },
            new OrderDetail
            {
                Product = product2,
                Quantity = 3,
                ProductId = product2.Id
            }
        }
            };

            _orderRepoMock.Setup(repo => repo.CreateOne(It.IsAny<Order>())).ReturnsAsync(order);

            // Act
            var result = await _orderService.CreateOne(orderCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.UserId, result.UserId);
            Assert.Equal(order.OrderDetails.Count, result.OrderDetails.Count);
            Assert.Equal(order.OrderDetails[0].Product.Id, result.OrderDetails[0].ProductId);
            Assert.Equal(order.OrderDetails[0].Quantity, result.OrderDetails[0].Quantity);
            Assert.Equal(order.OrderDetails[1].Product.Id, result.OrderDetails[1].ProductId);
            Assert.Equal(order.OrderDetails[1].Quantity, result.OrderDetails[1].Quantity);
        }


        [Fact]
        public async Task CreateOne_InvalidProductId_ThrowsException()
        {
            // Arrange
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Product 1",
                Price = 10
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Product 2",
                Price = 20
            };

            var orderCreateDto = new OrderCreateDto
            {
                UserId = Guid.NewGuid(),
                OrderDetails = new List<OrderDetailCreateDto>
        {
            new OrderDetailCreateDto
            {
                ProductId = product1.Id,
                Quantity = 2
            },
            new OrderDetailCreateDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = 3
            }
        }
            };

            _productRepoMock.Setup(repo => repo.GetOneById(product1.Id)).ReturnsAsync(product1);
            _productRepoMock.Setup(repo => repo.GetOneById(Guid.NewGuid())).ReturnsAsync((Product)null);

            // Act
            var exception = await Record.ExceptionAsync(() => _orderService.CreateOne(orderCreateDto));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("Product with id " + orderCreateDto.OrderDetails[1].ProductId + " does not exist", exception.Message);
        }

        [Fact]
        public async Task CreateOrder_RollbackOnOrderDetailCreationFailure()
        {
            // Arrange
            var orderCreateDto = new OrderCreateDto
            {
                UserId = Guid.NewGuid(),
                OrderDetails = new List<OrderDetailCreateDto>
        {
            new OrderDetailCreateDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = 2
            },
            new OrderDetailCreateDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = 3
            }
        }
            };

            var product1 = new Product
            {
                Id = orderCreateDto.OrderDetails[0].ProductId,
                ProductName = "Product 1",
                Price = 10
            };

            var product2 = new Product
            {
                Id = orderCreateDto.OrderDetails[1].ProductId,
                ProductName = "Product 2",
                Price = 20
            };

            _productRepoMock.Setup(repo => repo.GetOneById(product1.Id)).ReturnsAsync(product1);
            _productRepoMock.Setup(repo => repo.GetOneById(product2.Id)).ReturnsAsync(product2);

            // Make the orderDetail creation to fail
            _orderRepoMock.Setup(repo => repo.CreateOne(It.IsAny<Order>())).Callback<Order>(order =>
            {
                if (order.OrderDetails.Count == 2)
                {
                    throw new Exception("Failed to create OrderDetail object");
                }
            }).ReturnsAsync((Order)null);

            // Act
            var exception = await Record.ExceptionAsync(() => _orderService.CreateOne(orderCreateDto));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("Failed to create OrderDetail object", exception.Message);
            _orderRepoMock.Verify(repo => repo.CreateOne(It.IsAny<Order>()), Times.Once);
            _orderRepoMock.Verify(repo => repo.CreateOne(It.Is<Order>(o => o.Id == Guid.Empty)), Times.Once);
        }
    }
}