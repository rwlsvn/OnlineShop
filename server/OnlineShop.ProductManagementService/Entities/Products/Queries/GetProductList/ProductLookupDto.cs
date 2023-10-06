using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Models;
using System.ComponentModel;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList
{
    public class ProductLookupDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductLookupDto>();
        }
    }
}
