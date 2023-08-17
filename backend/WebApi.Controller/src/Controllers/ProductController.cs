using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Shared;

namespace WebApi.Controller.src.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : CrudController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        private readonly IProductService _productService;
        public ProductController(IProductService baseService) : base(baseService)
        {
            _productService = baseService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _productService.GetAll(queryOptions));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        public override async Task<ActionResult<ProductReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _productService.GetOneById(id));
        }
    }
}