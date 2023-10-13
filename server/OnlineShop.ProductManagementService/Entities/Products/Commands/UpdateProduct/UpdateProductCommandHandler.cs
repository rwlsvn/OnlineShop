using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;
using OnlineShop.ProductManagementService.Services;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand>
    {
        readonly IProductsDbContext _context;
        readonly IStaticFileProvider _fileProvider;

        public UpdateProductCommandHandler
            (IProductsDbContext context, IStaticFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        public async Task Handle(UpdateProductCommand request,
            CancellationToken cancellationToken)
        {  
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(nameof(Product), request.Id);
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.CategoryId);

            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request.CategoryId);
            }

            product.CategoryId = request.CategoryId;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            if (request.Image != null)
            {
                string fileExtension = Path.GetExtension(request.Image.FileName);
                string imageName = $"{Guid.NewGuid()}{fileExtension}";
                await _fileProvider.WriteFileAsync(request.Image, imageName);

                if (product.ImageName != null)
                {
                    _fileProvider.DeleteFile(product.ImageName);
                } 

                product.ImageName = imageName;
            }

            await _context.SaveChangesAsync();
        }
    }
}
