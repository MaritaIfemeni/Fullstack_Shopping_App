using WebApi.Domain.src.Entities;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;

namespace WebApi.Controller.src.Controllers
{
    public class ProductController : CrudController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {

        private readonly IProductService _productService;
        public ProductController(IProductService baseService) : base(baseService)
        {
            _productService = baseService;
        }
    }
}