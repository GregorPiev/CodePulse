using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Implementation;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {        
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {            
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            var category = new Category
            {                
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await _categoryRepository.CreateAsync(category);

            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        //Get All Categories
        [HttpGet]        
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDTO
                  { 
                    Id = category.Id, 
                    Name = category.Name, 
                    UrlHandle = category.UrlHandle 
                   }
                );
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryId([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if(existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDTO 
            { 
                Id = existingCategory.Id ,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, [FromBody] UpdateCategoryRequestDTO request)
        {
            var category = new Category 
            { 
                Id= id ,
                Name = request.Name ,
                UrlHandle = request.UrlHandle            
            };

            category = await _categoryRepository.UpdateAsync(category);

            if(category is null)
            {
                return NotFound();
            }

            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DeleteCategory([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid id value");
            }

            var category = await _categoryRepository.DeleteAsync(id);

            if(category is null)
            {
                return NotFound();
            }

            var reponse = new CategoryDTO { Id = category.Id, Name = category.Name, UrlHandle = category.UrlHandle };
            
            return Ok(reponse);
            
        }

    }
}
