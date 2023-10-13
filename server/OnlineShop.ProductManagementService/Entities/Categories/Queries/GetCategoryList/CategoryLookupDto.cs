using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Entities.Categories.Queries.GetCategoryList
{
    public class CategoryLookupDto : IMapWith<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryLookupDto>();
        }
    }
}
