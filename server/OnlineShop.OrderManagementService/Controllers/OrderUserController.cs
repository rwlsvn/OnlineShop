using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.OrderManagementService.Controllers.Base;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.CreateOrder;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderDetails;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderList;
using OnlineShop.OrderManagementService.Models;
using OnlineShop.OrderManagementService.Models.Dto;

namespace OnlineShop.OrderManagementService.Controllers
{
    [Authorize]
    [Route("api/user/order")]
    public class OrderUserController : BaseController
    {
        readonly IMapper _mapper;

        public OrderUserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IList<OrderLookupDto>>> All()
        {
            var query = new GetUserOrderListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<IList<Order>>> Get(Guid id)
        {
            var query = new GetUserOrderDetailsQuery
            {
                Id = id,
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Guid>> Add(CreateOrderDto orderDto)
        {
            var command = _mapper.Map<CreateOrderCommand>(orderDto);
            command.UserId = UserId;
            var orderId = await Mediator.Send(command);
            return Ok(orderId);
        }
    }
}
