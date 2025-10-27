using AutoMapper;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Services
{
    public class CategoryService : ICategoryService
    {
        #region Properties
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<CategoryResponseDTO>> GetAsync(int? releaseType, bool withSubcategories = false)
        {
            try
            {
                var categories = await _categoryRepository.GetAsync(releaseType, withSubcategories);
                return _mapper.Map<List<CategoryResponseDTO>>(categories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        public async Task<CategoryResponseDTO> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            
            return category == null ? throw new Exception(Constants.EntityNotFound("Category")) : _mapper.Map<CategoryResponseDTO>(category);
        }

        public async Task<Category> GetCategoryByCodeAsync(string code)
        {
            return await _categoryRepository.GetCategoryByCodeAsync(code);
        }

        public async Task<Category> GetCategoryByCodeWitchSubcategoriesAsync(string code)
        {
            return await _categoryRepository.GetCategoryByCodeWitchSubcategoriesAsync(code);
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetCategoryByNameAsync(string name)
        {
            try
            {
                var categories = await _categoryRepository.GetCategoryByNameAsync(name);
                return _mapper.Map<List<CategoryResponseDTO>>(categories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<CategoryResponseDTO> CreateAsync(CategoryRequestDTO categoryDTO)
        {
            if (await GetCategoryByCodeAsync(categoryDTO.Code) != null)
                throw new Exception(Constants.EntityAlreadyExist("category", Operations.Insert));

            var categoryCreated = await _categoryRepository.CreateAsync(new Category(Guid.NewGuid(), categoryDTO.Code, categoryDTO.Name, categoryDTO.Type, categoryDTO.Description));

            return _mapper.Map<CategoryResponseDTO>(categoryCreated);
        }

        public async Task<CategoryResponseDTO> UpdateAsync(Guid id, CategoryRequestDTO categoryDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(id) ?? throw new Exception(Constants.EntityNotExistError(Operations.Update, "category", "category"));
            
            category.UpdateModel(categoryDTO.Code, categoryDTO.Name, categoryDTO.Type, categoryDTO.Description);

            var categoryUpdated = await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryResponseDTO>(categoryUpdated);
        }

        public async Task RemoveAsync(string code)
        {
            var category = await GetCategoryByCodeWitchSubcategoriesAsync(code) ?? 
                throw new Exception(Constants.EntityNotExistError(Operations.Remove, "category", "category"));
            
            if (category.HasSubcategories())
                throw new Exception(Constants.CATEGORY_WITH_SUBCATEGORIES);

            await _categoryRepository.RemoveAsync(category);
        }

        #endregion

    }
}
