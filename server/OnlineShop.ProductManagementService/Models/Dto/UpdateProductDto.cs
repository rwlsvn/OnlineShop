using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Entities.Products.Commands.UpdateProduct;

namespace OnlineShop.ProductManagementService.Models.Dto
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>();
        }
    }
}
