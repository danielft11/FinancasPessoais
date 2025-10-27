using FinancasPessoais.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoryByNameAsync(string name);
        Task<Category> GetCategoryByCodeAsync(string code);
        Task<Category> GetCategoryByCodeWitchSubcategoriesAsync(string code);
        Task<IEnumerable<Category>> GetAsync(int? releaseType, bool withSubcategories = false);
    }
}
