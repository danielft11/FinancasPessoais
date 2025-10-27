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
    public class CreditCardReleaseRepository : BaseRepository<FinancialRelease>, ICreditCardReleaseRepository
    {
        public CreditCardReleaseRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<FinancialRelease> GetByIdAsync(Guid id)
        {
            return await _context.FinancialReleases
                .Include(c => c.CreditCard)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<FinancialRelease>> GetCreditCardReleasesByCreditCardId(Guid creditCardId, DateTime closingDate)
        {
            return await _context.FinancialReleases
                .Where(c => c.CreditCardId == creditCardId && c.ReleaseDate >= closingDate && c.PaymentDate == null)
                .ToListAsync();
        }

    }

}
