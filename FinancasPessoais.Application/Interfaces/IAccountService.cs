using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<DetailedAccountResponseDTO>> GetDetailedAccountsAsync();
        Task<IEnumerable<AccountResponseDTO>> GetAccountAsync();
        Task<DetailedAccountResponseDTO> GetAccountByIdAsync(Guid id);
        Task<Account> GetAccountModelByIdAsync(Guid id);
        Task<Account> GetAccoundModelByIdWithFinancialReleases(Guid id);
        Task<IEnumerable<Account>> GetAccountsByNameAsync(string name);
        Task<Account> GetAccountByNameAndNumberAsync(string name, string accountNumber);
        Task<AccountResponseDTO> CreateAccountAsync(AccountRequestDTO accountDTO);
        Task<AccountResponseDTO> UpdateAccountAsync(Account entity);
        Task RemoveAccountAsync(Account entity);
        Task<FinancialReleaseResponseDTO> UpdateFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO);
        Task<FinancialReleaseResponseDTO> RemoveFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO);
        Task<decimal> GetTotalIncomeExpenseByAccountIdAsync(Guid accountId, ReleaseTypes type);
        Task<IEnumerable<ExtractResponseDTO>> GetExtractByAccountId(ExtractRequestDTO extractRequest);
        Task<IEnumerable<ExtractResponseDTO>> GetMonthlyExtractByAccountId(MonthlyExtractRequestDTO request);
    }
}
