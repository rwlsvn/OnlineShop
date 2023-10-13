using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;
using OnlineShop.ProductManagementService.Services;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler
        : IRequestHandler<DeleteProductCommand>
    {
        readonly IProductsDbContext _context;
        readonly IStaticFileProvider _fileProvider;

        public DeleteProductCommandHandler
            (IProductsDbContext context, IStaticFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        public async Task Handle(DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(nameof(Product), request.Id);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            if (product.ImageName != null)
            {
                _fileProvider.DeleteFile(product.ImageName);
            }
        }
    }
}
