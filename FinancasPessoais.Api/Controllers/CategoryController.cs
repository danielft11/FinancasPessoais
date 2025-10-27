using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinancasPessoais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("getAllCategories")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategories([FromQuery] int? releaseType = null, bool withSubcategories = false)
        {
            try
            {
                return Ok(await _categoryService.GetAsync(releaseType, withSubcategories));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet()]
        [Route("getCategoryById/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                return Ok(await _categoryService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("createCategory")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDTO categoryDTO)
        {
            try
            {
                var categoryCreated = await _categoryService.CreateAsync(categoryDTO);

                return Created(string.Empty, categoryCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
  
        }

        [HttpPut]
        [Route("updateCategory")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromQuery] Guid Id, [FromBody] CategoryRequestDTO categoryDTO) 
        {
            try
            {
                return Ok(await _categoryService.UpdateAsync(Id, categoryDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete()]
        [Route("removeCategory/{code}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCategory(string code) 
        {
            try
            {
                await _categoryService.RemoveAsync(code);

                return Ok(Constants.EntityOperationSucessfully("Category", Operations.Remove));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
