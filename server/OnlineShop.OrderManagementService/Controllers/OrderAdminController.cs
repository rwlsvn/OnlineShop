using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderManagementService.Controllers.Base;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrderStatus;
using OnlineShop.OrderManagementService.Models.Dto;

namespace OnlineShop.OrderManagementService.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderAdminController : BaseController
    {
        readonly IMapper _mapper;

        public OrderAdminController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(UpdateOrderDto orderDto)
        {
            var command = _mapper.Map<UpdateOrderCommand>(orderDto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("updatestatus")]
        public async Task<ActionResult> UpdateStatus(UpdateOrderStatusDto orderDto)
        {
            var command = _mapper.Map<UpdateOrderStatusCommand>(orderDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
