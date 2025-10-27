using FinancasPessoais.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IAccountPayableRepository : IBaseRepository<AccountPayable>
    {
        Task<IEnumerable<AccountPayable>> GetScheduledAccountsPayable(DateTime scheduledDate);
    }
}
