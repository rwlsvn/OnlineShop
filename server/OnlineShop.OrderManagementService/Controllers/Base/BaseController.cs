using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OnlineShop.OrderManagementService.Controllers.Base
{
    [ApiController]  
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices
            .GetService<IMediator>();

        public Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
