using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class OrderService : BaseService<Order, OrderDto>, IOrderService
    {

        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo, IMapper mapper) : base(orderRepo, mapper)
        {
            _orderRepo = orderRepo;
        }

        // these methods are for admin user, authorization will be implemented later
        public OrderDto UpdateOrderStatus(string id, string newStatus)
        {
            var foundOrder = _orderRepo.GetOneById(id);
            if (foundOrder is null)
            {
                throw new Exception("Not Found"); // change this to a custom exception
            }
            return _mapper.Map<OrderDto>(_orderRepo.UpdateOrderStatus(foundOrder, newStatus));
        }

        public OrderDto GetOrderStatus(string id)
        {
            var foundOrder = _orderRepo.GetOneById(id);
            if (foundOrder is null)
            {
                throw new Exception("Not Found"); // change this to a custom exception
            }
            return _mapper.Map<OrderDto>(_orderRepo.GetOrderStatus(foundOrder));
        }
    }

}