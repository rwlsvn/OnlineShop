using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Entities.Categories.Commands.UpdateCategory;

namespace OnlineShop.ProductManagementService.Models.Dto
{
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
        }
    }
}
