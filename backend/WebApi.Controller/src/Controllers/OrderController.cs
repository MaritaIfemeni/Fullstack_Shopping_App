using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;
using WebApi.Domain.src.Shared;

namespace WebApi.Controller.src.Controllers
{
    public class OrderController : CrudController<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOrderService _orderService;
        public OrderController(IOrderService baseService, IAuthorizationService authService) : base(baseService)
        {
            _authorizationService = authService;
            _orderService = baseService;
        }

        [Authorize]
        [HttpPost]
        public override async Task<ActionResult<OrderReadDto>> CreateOne(OrderCreateDto orderCreateDto)
        {

            var orderCreated = await _orderService.CreateOne(orderCreateDto);
            return CreatedAtAction(nameof(CreateOne), orderCreated);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:Guid}")]
        public override async Task<ActionResult<OrderReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _orderService.GetOneById(id));
        }

        //wanted to make resourse based authorization so that user that is logges in can only see his own orders and admin can use it as well
        //but i could not make it work

        // [Authorize]   
        // public override async Task<ActionResult<OrderReadDto>> GetOneById([FromRoute] Guid id)
        // {
        //     var user = HttpContext.User;
        //     var order = await _orderService.GetOneById(id);

        //     // Check if the user is an admin
        //     if (user.IsInRole("Admin"))
        //     {
        //         return await base.GetOneById(id);
        //     }

        //     // Check if the user is the owner of the order
        //     var authorizeOwner = await _authorizationService.AuthorizeAsync(user, order, "OwnerOnly");
        //     if (authorizeOwner.Succeeded)
        //     {
        //         return await base.GetOneById(id);
        //     }
        //     else
        //     {
        //         return new ForbidResult();
        //     }
        // }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _orderService.GetAll(queryOptions));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:Guid}")]
        public override async Task<ActionResult<OrderReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] OrderUpdateDto update)
        {
            return Ok(await _orderService.UpdateOneById(id, update));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        public override async Task<ActionResult<bool>> DeleteOneById([FromRoute] Guid id)
        {
            return StatusCode(204, await _orderService.DeleteOneById(id));
        }



    }
}
