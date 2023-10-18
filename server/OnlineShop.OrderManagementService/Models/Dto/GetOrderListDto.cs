using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderList;

namespace OnlineShop.OrderManagementService.Models.Dto
{
    public class GetOrderListDto : IMapWith<GetOrderListQuery>
    {
        public Guid? UserId { get; set; }
        public string? RecipientFirstName { get; set; }
        public string? RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientPhone { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetOrderListDto, GetOrderListQuery>();
        }
    }
}
