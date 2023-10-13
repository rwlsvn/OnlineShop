using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Entities.Categories.Commands.CreateCategory;

namespace OnlineShop.ProductManagementService.Models.Dto
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        }
    }
}
