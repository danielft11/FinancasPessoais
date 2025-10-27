using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Domain.Utils;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAsync(int? releaseType, bool withSubcategories = false) 
        {
            IQueryable<Category> categories = _context.Categories;

            if (withSubcategories)
                categories = categories.Include(s => s.Subcategories);

            if (releaseType != null)
                categories = releaseType == 0 ? 
                    categories.Where(c => c.Type == ReleaseTypes.Income) : 
                    categories.Where(c => c.Type == ReleaseTypes.Expense);
            
            return await categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByCodeAsync(string code)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Category> GetCategoryByCodeWitchSubcategoriesAsync(string code)
        {
            return await _context.Categories
                .Include(s => s.Subcategories)
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Category>> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

    }
}
