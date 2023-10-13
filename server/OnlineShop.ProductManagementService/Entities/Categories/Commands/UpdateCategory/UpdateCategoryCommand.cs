﻿using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
