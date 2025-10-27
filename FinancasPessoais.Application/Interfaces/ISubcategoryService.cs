using FinancasPessoais.Application.DTOs;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryResponseDTO>> GetAsync();
        Task<SubcategoryResponseDTO> GetByIdAsync(Guid id);
        Task<Subcategory> GetSubcategoryByIdAsync(Guid id);
        Task<IEnumerable<SubcategoryResponseDTO>> GetSubcategoriesByCategoryId(Guid categoryId);
        Task<Subcategory> GetSubcategoryByCodeAsync(string code);
        Task<SubcategoryResponseDTO> CreateAsync(SubcategoryRequestDTO subcategoryDTO);
        Task<SubcategoryResponseDTO> UpdateAsync(Subcategory subcategory);
        Task RemoveAsync(Subcategory subcategory);
    }
}
