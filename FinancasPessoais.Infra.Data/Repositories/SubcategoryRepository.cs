using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class SubcategoryRepository : BaseRepository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Subcategory> GetSubcategoryByCodeAsync(string code)
        {
            return await _context.Subcategories
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async override Task<IEnumerable<Subcategory>> GetAsync()
        {
            return await _context.Subcategories
                .Include(c => c.Category)
                .ToListAsync();
        }

        public override async Task<Subcategory> GetByIdAsync(Guid id)
        {
            return await _context.Subcategories
                .Include(c => c.Category)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategoriesByCategoryId(Guid categoryId)
        {
            return await _context.Subcategories
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }

    }
}
