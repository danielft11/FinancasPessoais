using FinancasPessoais.Application.DTOs;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICategoryService _categoryService;

        public SubcategoryController(ISubcategoryService subcategoryService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSubcategories()
        {
            try
            {
                var subcategories = await _subcategoryService.GetAsync();
                if (!subcategories.Any())
                    return NotFound(Constants.EntityNotFound("Subcategories"));

                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpGet]
        [Route("getSubcategoriesByCategoryId/{categoryId:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSubcategoriesByCategoryId(Guid categoryId)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(categoryId);
                if (category == null)
                    return NotFound(Constants.EntityNotFound("Category"));

                var subcategories = await _subcategoryService.GetSubcategoriesByCategoryId(categoryId);
                if (!subcategories.Any())
                    return NotFound(Constants.EntityNotFound("Subcategories"));

                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSubcategoryById(Guid id)
        {
            try
            {
                var subcategory = await _subcategoryService.GetByIdAsync(id);
                if (subcategory == null)
                    return NotFound(Constants.EntityNotFound("Subcategory"));

                return Ok(subcategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubcategoryRequestDTO request)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(request.CategoryId);
                if (category == null)
                    return BadRequest(Constants.EntityNotExistError(Operations.Insert, "subcategory", "category"));

                var subcategory = await _subcategoryService.GetSubcategoryByCodeAsync(request.Code);
                if (subcategory != null)
                    return BadRequest(Constants.EntityAlreadyExist("subcategory", Operations.Insert));

                var subcategoryInserted = await _subcategoryService.CreateAsync(request);

                return Created(string.Empty, subcategoryInserted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromQuery] Guid Id, [FromBody] SubcategoryRequestDTO request)
        {
            try
            {
                var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(Id);
                if (subcategory == null)
                    return NotFound(Constants.EntityNotExistError(Operations.Update, "subcategory", "subcategory"));

                subcategory.UpdateModel(request.Name, request.Code, request.Description, request.CategoryId);

                var subcategoryUpdated = await _subcategoryService.UpdateAsync(subcategory);

                return Ok(subcategoryUpdated);

            }
            catch (Exception ex)
            {
                return BadRequest(Constants.GenericFailure(Operations.Update, "subcategory", ex.Message));
            }
        }

        [HttpDelete("{code}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveSubcategory(string code)
        {
            try
            {
                var subcategory = await _subcategoryService.GetSubcategoryByCodeAsync(code);
                if (subcategory == null)
                    return NotFound(Constants.EntityNotExistError(Operations.Remove, "subcategory", "subcategory"));

                await _subcategoryService.RemoveAsync(subcategory);

                return Ok(Constants.EntityOperationSucessfully("Subcategory", Operations.Remove));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }

}
