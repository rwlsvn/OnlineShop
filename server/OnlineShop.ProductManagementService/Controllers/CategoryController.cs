using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ProductManagementService.Controllers.Base;
using OnlineShop.ProductManagementService.Entities.Categories.Commands.CreateCategory;
using OnlineShop.ProductManagementService.Entities.Categories.Commands.DeleteCategory;
using OnlineShop.ProductManagementService.Entities.Categories.Commands.UpdateCategory;
using OnlineShop.ProductManagementService.Entities.Categories.Queries.GetCategoryList;
using OnlineShop.ProductManagementService.Models;
using OnlineShop.ProductManagementService.Models.Dto;

namespace OnlineShop.ProductManagementService.Controllers
{
    public class CategoryController : BaseController
    {
        readonly IMapper _mapper;

        public CategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IList<Category>>> AllCategories()
        {
            var query = new GetCategoryListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("add")]
        public async Task<ActionResult<Guid>> AddCategory(CreateCategoryDto categoryDto)
        {
            var command = _mapper.Map<CreateCategoryCommand>(categoryDto);
            var categoryId = await Mediator.Send(command);
            return Ok(categoryId);
        }

        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(categoryDto);
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
