using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.RepoInterfaces;
using AutoMapper;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class OrderDetailService : BaseService<OrderDetail, OrderDetailReadDto, OrderDetailCreateDto, OrderDetailUpdateDto>, IOrderDetailService
    {
       
       private readonly IOrderDetailRepo _orderDetailRepo;

       public OrderDetailService(IOrderDetailRepo orderDetailRepo, IMapper mapper) : base(orderDetailRepo, mapper)
       {
           _orderDetailRepo = orderDetailRepo;
       }
    }
}
