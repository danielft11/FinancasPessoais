using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface ISubcategoryRepository : IBaseRepository<Subcategory>
    {
        Task<IEnumerable<Subcategory>> GetSubcategoriesByCategoryId(Guid categoryId);
        Task<Subcategory> GetSubcategoryByCodeAsync(string code);
    }
}
