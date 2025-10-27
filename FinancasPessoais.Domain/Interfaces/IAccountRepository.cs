using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<IEnumerable<Account>> GetAccountsByNameAsync(string name);
        Task<Account> GetAccountByNameAndNumberAsync(string name, string accountNumber);
        Task<Account> GetByIdWithFinancialReleases(Guid id);
    }
}
