using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Entities.Products.Commands.CreateProduct;

namespace OnlineShop.ProductManagementService.Models.Dto
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>();
        }
    }
}
