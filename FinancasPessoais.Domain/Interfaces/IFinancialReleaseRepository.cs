using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IFinancialReleaseRepository : IBaseRepository<FinancialRelease>
    {
        Task<IEnumerable<FinancialRelease>> GetFinancialReleasesByAccountIdAsync(Guid accountId, string userID);
        Task<IEnumerable<FinancialRelease>> GetCreditCardReleasesByCreditCardIdAsync(Guid creditCardId, DateTime closingDate);
        Task<decimal> GetTotalIncomeExpenseByAccountIdAsync(Guid accountId, ReleaseTypes type);
        Task<IEnumerable<FinancialRelease>> GetMonthlyExtractByAccountIdAsync(Guid accountId, DateTime firstDay, DateTime lastDay, string userID);
        Task<IEnumerable<FinancialRelease>> GetInvoiceByCreditCardId(Guid creditCardId, DateTime firstDay, DateTime lastDay);
        Task PayCreditCardRelease(Guid id, DateTime paymentDate, Guid accountId);
    }

}
