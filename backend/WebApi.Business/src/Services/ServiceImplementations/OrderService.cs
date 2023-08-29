using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Domain.src.Entities;
using System.Transactions;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class OrderService : BaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>, IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public OrderService(IOrderRepo orderRepo, IProductRepo productRepo, IMapper mapper) : base(orderRepo, mapper)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public override async Task<OrderReadDto> CreateOne(OrderCreateDto orderCreateDto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var order = _mapper.Map<Order>(orderCreateDto);
                    order.UserId = orderCreateDto.UserId;
                    order.OrderDetails = new List<OrderDetail>();

                    foreach (var orderDetailDto in orderCreateDto.OrderDetails)
                    {
                        var product = await _productRepo.GetOneById(orderDetailDto.ProductId);
                        if (product == null)
                        {
                            throw new Exception($"Product with id {orderDetailDto.ProductId} does not exist");
                        }

                        order.OrderDetails.Add(new OrderDetail
                        {
                            Product = product,
                            Quantity = orderDetailDto.Quantity,
                            ProductId = orderDetailDto.ProductId
                        });
                    }

                    var createdOrder = await _orderRepo.CreateOne(order);

                    scope.Complete(); // Commit the transaction
                    return _mapper.Map<OrderReadDto>(createdOrder);
                }
                catch (Exception)
                {
                    scope.Dispose(); // Rollback the transaction
                    throw;
                }
            }
        }
    }
}

