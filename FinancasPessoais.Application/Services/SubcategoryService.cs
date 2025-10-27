using AutoMapper;
using FinancasPessoais.Application.DTOs;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        #region Properties
        private ISubcategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<SubcategoryResponseDTO>> GetAsync()
        {
            var subcategories = await _subcategoryRepository.GetAsync();
            return _mapper.Map<IEnumerable<SubcategoryResponseDTO>>(subcategories);
        }

        public async Task<SubcategoryResponseDTO> GetByIdAsync(Guid id)
        {
            var subcategory = await _subcategoryRepository.GetByIdAsync(id);
            return _mapper.Map<SubcategoryResponseDTO>(subcategory);
        }

        public async Task<Subcategory> GetSubcategoryByIdAsync(Guid id)
        {
            return await _subcategoryRepository.GetByIdAsync(id);
        }

        public async Task<Subcategory> GetSubcategoryByCodeAsync(string code)
        {
            return await _subcategoryRepository.GetSubcategoryByCodeAsync(code);
        }

        public async Task<IEnumerable<SubcategoryResponseDTO>> GetSubcategoriesByCategoryId(Guid categoryId)
        {
            var subcategories = await _subcategoryRepository.GetSubcategoriesByCategoryId(categoryId);
            return _mapper.Map<IEnumerable<SubcategoryResponseDTO>>(subcategories);
        }

        public async Task<SubcategoryResponseDTO> CreateAsync(SubcategoryRequestDTO subcategoryDTO)
        {
            var category = await _subcategoryRepository.CreateAsync(_mapper.Map<Subcategory>(subcategoryDTO));
            return _mapper.Map<SubcategoryResponseDTO>(category);
        }

        public async Task<SubcategoryResponseDTO> UpdateAsync(Subcategory subcategory)
        {
            var subcategoryUpdated = await _subcategoryRepository.UpdateAsync(subcategory);
            return _mapper.Map<SubcategoryResponseDTO>(subcategoryUpdated);
        }

        public async Task RemoveAsync(Subcategory subcategory)
        {
            await _subcategoryRepository.RemoveAsync(subcategory);
        }

        #endregion

    }
}
