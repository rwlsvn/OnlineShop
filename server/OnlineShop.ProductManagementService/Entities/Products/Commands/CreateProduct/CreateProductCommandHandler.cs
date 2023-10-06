using MediatR;
using Microsoft.Extensions.FileProviders;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;
using OnlineShop.ProductManagementService.Services;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler
        : IRequestHandler<CreateProductCommand, Guid>
    {
        readonly IProductsDbContext _context;
        readonly IStaticFileProvider _fileProvider;

        public CreateProductCommandHandler
            (IProductsDbContext context, IStaticFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        public async Task<Guid> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            string fileExtension = Path.GetExtension(request.Image.FileName);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageName = $"{Guid.NewGuid()}{fileExtension}"
            };
            await _fileProvider.WriteFileAsync(request.Image, product.ImageName);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}
