using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.src.Entities;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Services.ServiceInterfaces;

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

        [HttpPost]
        public override async Task<ActionResult<OrderReadDto>> CreateOne(OrderCreateDto orderCreateDto)
        {

            var orderCreated = await _orderService.CreateOne(orderCreateDto);
            return CreatedAtAction(nameof(CreateOne), orderCreated);
            // return CreatedAtAction(nameof(GetOneById), new { id = orderReadDto.User }, orderReadDto);
        }

        [Authorize]
        public override async Task<ActionResult<OrderReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] OrderUpdateDto update)
        {
            var user = HttpContext.User;
            var order = await _orderService.GetOneById(id);
            /* resource based authorization  only owner can update their orders. This should have more rules etc...*/
            var authorizeOwner = await _authorizationService.AuthorizeAsync(user, order, "OwnerOnly");
            if (authorizeOwner.Succeeded)
            {
                return await base.UpdateOneById(id, update);
            }
            else
            {
                return new ForbidResult();
            }
        }
    }
}
