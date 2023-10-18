using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderManagementService.Controllers.Base;
using OnlineShop.OrderManagementService.Entities.OrderItems.Commands.CreateOrderItem;
using OnlineShop.OrderManagementService.Models.Dto;

namespace OnlineShop.OrderManagementService.Controllers
{
    [Authorize]
    [Route("api/user/orderitem")]
    public class OrderItemUserController : BaseController
    {
        readonly IMapper _mapper;

        public OrderItemUserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Guid>> Add(CreateOrderItemDto orderItemDto)
        {
            var command = _mapper.Map<CreateOrderItemCommand>(orderItemDto);
            command.UserId = UserId;
            var orderId = await Mediator.Send(command);

            return Ok(orderId);
        }
    }
}
