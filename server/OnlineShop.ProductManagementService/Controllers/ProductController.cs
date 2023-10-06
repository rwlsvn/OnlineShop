using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ProductManagementService.Controllers.Base;
using OnlineShop.ProductManagementService.Entities.Products.Commands.CreateProduct;
using OnlineShop.ProductManagementService.Entities.Products.Commands.DeleteProduct;
using OnlineShop.ProductManagementService.Entities.Products.Commands.UpdateProduct;
using OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductDetails;
using OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList;
using OnlineShop.ProductManagementService.Models.Dto;

namespace OnlineShop.ProductManagementService.Controllers
{
    public class ProductController : BaseController
    {
        readonly IMapper _mapper;

        public ProductController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ProductLookupDto>> AllProducts()
        {
            var query = new GetProductListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Guid>> GetProduct(Guid id)
        {
            var query = new GetProductDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Guid>> AddProduct([FromForm] CreateProductDto productDto)
        {
            var command = _mapper.Map<CreateProductCommand>(productDto);
            var productId = await Mediator.Send(command);
            return Ok(productId);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductDto productDto)
        {
            var command = _mapper.Map<UpdateProductCommand>(productDto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
