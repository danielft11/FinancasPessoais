using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class AccountPayableRepository : BaseRepository<AccountPayable>, IAccountPayableRepository
    {
        public AccountPayableRepository(ApplicationDbContext context) : base(context) { }

        public async override Task<IEnumerable<AccountPayable>> GetAsync()
        {
            return await _context.AccountsPayable
                .Include(c => c.Subcategory)
                .ToListAsync();
        }

        public override async Task<AccountPayable> GetByIdAsync(Guid id)
        {
            return await _context
                .AccountsPayable
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AccountPayable>> GetScheduledAccountsPayable(DateTime scheduledDate) 
        {
            return await _context
                .AccountsPayable
                .Where(a => a.ScheduleDate == scheduledDate)
                .ToListAsync();
        }

    }

}
