using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderManagementService.Controllers.Base;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrderStatus;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderDetails;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderList;
using OnlineShop.OrderManagementService.Models;
using OnlineShop.OrderManagementService.Models.Dto;

namespace OnlineShop.OrderManagementService.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/admin/order")]
    public class OrderAdminController : BaseController
    {
        readonly IMapper _mapper;

        public OrderAdminController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<IList<Order>>> GetById(Guid id)
        {
            var query = new GetOrderDetailsQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("get")]
        public async Task<ActionResult<IList<Order>>> GetByQuery(GetOrderListDto orderDto)
        {
            var query = new GetOrderListQuery
            {
                UserId = orderDto.UserId,
                RecipientFirstName = orderDto.RecipientFirstName,
                RecipientLastName = orderDto.RecipientLastName,
                RecipientEmail = orderDto.RecipientEmail,
                RecipientPhone = orderDto.RecipientPhone
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
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
