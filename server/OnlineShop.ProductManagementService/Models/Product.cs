﻿namespace OnlineShop.ProductManagementService.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public Category Category { get; set; }
    }
}
