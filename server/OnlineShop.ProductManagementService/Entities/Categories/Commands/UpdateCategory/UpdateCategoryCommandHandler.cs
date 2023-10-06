using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommand>
    {
        readonly IProductsDbContext _context;

        public UpdateCategoryCommandHandler(IProductsDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.Name = request.Name;
            await _context.SaveChangesAsync();
        }
    }
}
