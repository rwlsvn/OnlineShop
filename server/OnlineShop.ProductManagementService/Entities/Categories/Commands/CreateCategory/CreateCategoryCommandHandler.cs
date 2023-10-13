using MediatR;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, Guid>
    {
        readonly IProductsDbContext _context;

        public CreateCategoryCommandHandler
            (IProductsDbContext context)
        {
            _context = context; 
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }
    }
}
