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
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<FinancialRelease>> GetFinancialReleasesByAccountIdAsync(Guid AcountId) 
        {
            return await _context.FinancialReleases.Where(f => f.AccountId == AcountId).ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsByNameAsync(string name)
        {
            IQueryable<Account> accounts = _context.Accounts.Where(a => a.Name.Contains(name));
            return await accounts.ToListAsync();
        }

        public async Task<Account> GetAccountByNameAndNumberAsync(string name, string accountNumber)
        {
            IQueryable<Account> accounts = _context.Accounts.Include(a => a.FinancialReleases);
            return await accounts.Where(a => a.Name.Equals(name) && a.AccountNumber.Equals(accountNumber)).FirstOrDefaultAsync();

            #region IQueryable vs IEnumerable
            /* Fontes: 
             * https://www.youtube.com/watch?v=lU2I8q5nmzs&t=116s
             * https://www.macoratti.net/20/06/c_iqueryb1.htm
             * IQueryable: o filtro é realizado no banco de dados e os dados são devolvidos já filtrados para aplicação. 
             * IEnumerable: o banco de dados retorna todos os dados e o filtro é realizado na memória da aplicação.
            */
            #endregion

        }

        public async Task<Account> GetByIdWithFinancialReleases(Guid id)
        {
            return await _context.Accounts
               .Include(a => a.FinancialReleases)
               .ThenInclude(f => f.Subcategory)
               .Where(a => a.Id == id)
               .FirstOrDefaultAsync();
        }

    }
}
