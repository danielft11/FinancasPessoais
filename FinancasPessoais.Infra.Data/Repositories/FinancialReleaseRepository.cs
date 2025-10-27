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
    public class FinancialReleaseRepository : BaseRepository<FinancialRelease>, IFinancialReleaseRepository
    {
        public FinancialReleaseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<FinancialRelease>> GetFinancialReleasesByAccountIdAsync(Guid accountId)
        {
            return await _context.FinancialReleases
                .Where(f => f.AccountId == accountId)
                .Include(f => f.Subcategory)
                .ThenInclude(f => f.Category)
                .OrderBy(f => f.ReleaseDate)
                .ToListAsync();   
        }

        public async Task<IEnumerable<FinancialRelease>> GetCreditCardReleasesByCreditCardIdAsync(Guid creditCardId, DateTime closingDate)
        {
            return await _context.FinancialReleases
                .Where(c => c.CreditCardId == creditCardId && c.ReleaseDate >= closingDate && c.PaymentDate == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<FinancialRelease>> GetMonthlyExtractByAccountIdAsync(Guid accountId, DateTime firstDay, DateTime lastDay)
        {
            return await _context.FinancialReleases
                .Where(f => f.AccountId == accountId && f.ReleaseDate >= firstDay && f.ReleaseDate <= lastDay.AddHours(23).AddMinutes(55))
                .Include(f => f.Subcategory)
                .ThenInclude(f => f.Category)
                .OrderByDescending(f => f.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<FinancialRelease>> GetInvoiceByCreditCardId(Guid creditCardId, DateTime firstDay, DateTime lastDay)
        {
            return await _context.FinancialReleases
                .Where(f => f.CreditCardId == creditCardId && f.ReleaseDate >= firstDay && f.ReleaseDate < lastDay && f.PaymentDate == null && f.AccountId == null)
                .Include(f => f.Subcategory)
                .ThenInclude(f => f.Category)
                .OrderByDescending(f => f.ReleaseDate)
                .ToListAsync();
        }

        public async override Task<IEnumerable<FinancialRelease>> GetAsync()
        {
            return await _context.FinancialReleases
                .Include(f => f.Subcategory)
                .Include(f => f.Account)
                .ToListAsync();
        }

        public async override Task<FinancialRelease> GetByIdAsync(Guid id)
        {
            return await _context.FinancialReleases
                .Include(f => f.Subcategory)
                .Include(f => f.Account)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<decimal> GetTotalIncomeExpenseByAccountIdAsync(Guid accountId, ReleaseTypes type)
        {
            return await _context.FinancialReleases
                .Where(f => f.AccountId == accountId && f.Type == type)
                .SumAsync(f => f.Value);
        }

        public async Task PayCreditCardRelease(Guid id, DateTime paymentDate, Guid accountId)
        {
            var creditCardRelease = await _context
                .FinancialReleases
                .FirstOrDefaultAsync(c => c.Id == id);

            creditCardRelease.PaymentDate = paymentDate;
            creditCardRelease.AccountId = accountId;

            _context.Update(creditCardRelease);

            await _context.SaveChangesAsync();
        }

    }

}
