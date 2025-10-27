using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAsync(int? releaseType, bool withSubcategories = false);
        Task<CategoryResponseDTO> GetByIdAsync(Guid id);
        Task<Category> GetCategoryByCodeAsync(string code);
        Task<Category> GetCategoryByCodeWitchSubcategoriesAsync(string code);
        Task<IEnumerable<CategoryResponseDTO>> GetCategoryByNameAsync(string name);
        Task<CategoryResponseDTO> CreateAsync(CategoryRequestDTO categoryDTO);
        Task<CategoryResponseDTO> UpdateAsync(Guid id, CategoryRequestDTO categoryDTO);
        Task RemoveAsync(string code);
    }
}
