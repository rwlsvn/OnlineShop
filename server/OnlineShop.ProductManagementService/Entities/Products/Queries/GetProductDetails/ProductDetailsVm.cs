using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductDetails
{
    public class ProductDetailsVm : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsVm>();
        }
    }
}
