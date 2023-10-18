using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderManagementService.Controllers.Base;
using OnlineShop.OrderManagementService.Entities.OrderItems.Commands.DeleteOrderItem;
using OnlineShop.OrderManagementService.Entities.OrderItems.Commands.UpdateOrderItem;
using OnlineShop.OrderManagementService.Models.Dto;

namespace OnlineShop.OrderManagementService.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/orderitem")]
    public class OrderItemAdminController : BaseController
    {
        readonly IMapper _mapper;

        public OrderItemAdminController(IMapper mapper)
        {
            _mapper = mapper;
        }
     
        [HttpPut("update")]
        public async Task<ActionResult> Update(UpdateOrderItemDto orderItemDto)
        {
            var command = _mapper.Map<UpdateOrderItemCommand>(orderItemDto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteOrderItemCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
