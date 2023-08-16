using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Shared;

namespace WebApi.Controller.src.Controllers
{
    public class ProductController : CrudController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {

        private readonly IProductService _productService;
        public ProductController(IProductService baseService) : base(baseService)
        {
            _productService = baseService;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _productService.GetAll(queryOptions));
        }
        
        [AllowAnonymous]
        public override async Task<ActionResult<ProductReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _productService.GetOneById(id));
        }
    }
}