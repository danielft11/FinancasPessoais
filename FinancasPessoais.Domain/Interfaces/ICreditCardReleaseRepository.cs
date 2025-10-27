using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface ICreditCardReleaseRepository : IBaseRepository<FinancialRelease>
    {
        Task<IEnumerable<FinancialRelease>> GetCreditCardReleasesByCreditCardId(Guid creditCardId, DateTime closingDate);
    }
}
