using AutoMapper;
using Moq;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceImplementations;
using WebApi.Business.src.Shared;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Infrastructure.src.Configuration;

namespace WebApi.Business.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepo> _userRepoMock;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepo>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateOne_WithValidDto_ReturnsCreatedUser()
        {
            // Arrange
            var createDto = new UserCreateDto { Email = "test@example.com", Password = "password" };
            var expectedUser = new User { Id = Guid.NewGuid(), Email = createDto.Email, Password = "hashedPassword", Salt = new byte[] { 0x01, 0x02, 0x03 } };
            _userRepoMock.Setup(repo => repo.CreateOne(It.IsAny<User>())).ReturnsAsync(expectedUser);

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act
            var result = await userService.CreateOne(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public async Task CreateOne_WithInvalidEmail_ThrowsFieldRequirementsException()
        {
            // Arrange
            var createDto = new UserCreateDto { Email = "test", Password = "password" };

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ServiceExeption>(() => userService.CreateOne(createDto));
        }

        [Fact]
        public async Task GreateAdmin_WithInvalidEmail_ThrowsFieldRequirementsException()
        {
            // Arrange
            var createDto = new UserCreateDto { Email = "test", Password = "password" };

            var userService = new UserService(_userRepoMock.Object, _mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ServiceExeption>(() => userService.GreateAdmin(createDto));
        }
    }
}