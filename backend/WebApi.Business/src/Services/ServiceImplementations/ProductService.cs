using AutoMapper;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.RepoInterfaces;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Services.ServiceImplementations
{
    public class ProductService : BaseService<Product, ProductDto>, IProductService
    {

        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo, IMapper mapper) : base(productRepo, mapper)
        {
            _productRepo = productRepo;
        }
   
    }
}