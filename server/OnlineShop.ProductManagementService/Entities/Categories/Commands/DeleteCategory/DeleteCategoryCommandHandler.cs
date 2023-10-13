using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler
        : IRequestHandler<DeleteCategoryCommand>
    {
        readonly IProductsDbContext _context;

        public DeleteCategoryCommandHandler(IProductsDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request.Id);
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
