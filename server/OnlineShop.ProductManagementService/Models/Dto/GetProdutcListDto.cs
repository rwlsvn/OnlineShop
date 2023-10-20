using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList;

namespace OnlineShop.ProductManagementService.Models.Dto
{
    public class GetProdutcListDto : IMapWith<GetProductListQuery>
    {
        public string? CategotyName { get; set; }
        public string? Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int Page { get; set; } = 1;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetProdutcListDto, GetProductListQuery>();
        }
    }
}
