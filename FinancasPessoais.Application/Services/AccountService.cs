using AutoMapper;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancasPessoais.Application.Services
{
    public class AccountService : IAccountService
    {
        #region Properties
        private IAccountRepository _accountRepository;
        private IFinancialReleaseRepository _financialReleaseRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Construtor
        
        public AccountService(IAccountRepository accountRepository, IFinancialReleaseRepository financialReleaseRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _financialReleaseRepository = financialReleaseRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public methods
        public async Task<IEnumerable<AccountResponseDTO>> GetAccountAsync()
        {
            var accounts = await _accountRepository.GetAsync();
            return _mapper.Map<List<AccountResponseDTO>>(accounts);
        }

        public async Task<IEnumerable<DetailedAccountResponseDTO>> GetDetailedAccountsAsync()
        {
            var accounts =  await _accountRepository.GetAsync();
            if (accounts != null && accounts.Any()) 
            {
                var detailedAccounts = _mapper.Map<List<DetailedAccountResponseDTO>>(accounts);
                foreach (var detailedAccount in detailedAccounts)
                {
                    detailedAccount.TotalIncome = await GetTotalIncomeExpenseByAccountIdAsync(detailedAccount.Id, ReleaseTypes.Income);
                    detailedAccount.TotalExpenses = await GetTotalIncomeExpenseByAccountIdAsync(detailedAccount.Id, ReleaseTypes.Expense);
                    detailedAccount.Balance = detailedAccount.TotalIncome - detailedAccount.TotalExpenses;
                }
                return detailedAccounts;
            }

            return null;
        }

        public async Task<DetailedAccountResponseDTO> GetAccountByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdWithFinancialReleases(id);

            if (account != null) 
            {
                var detailedAccount = _mapper.Map<DetailedAccountResponseDTO>(account);
                
                detailedAccount.TotalIncome = await GetTotalIncomeExpenseByAccountIdAsync(account.Id, ReleaseTypes.Income);
                detailedAccount.TotalExpenses = await GetTotalIncomeExpenseByAccountIdAsync(account.Id, ReleaseTypes.Expense);
                detailedAccount.Balance = detailedAccount.TotalIncome - detailedAccount.TotalExpenses;

                return detailedAccount;
            }

            return null;

        }

        public async Task<Account> GetAccountModelByIdAsync(Guid id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ExtractResponseDTO>> GetExtractByAccountId(ExtractRequestDTO extractRequest) 
        {
            var extracts = new List<ExtractResponseDTO>();
            
            DateTime cutoffDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-extractRequest.Period);

            var financialReleases = await _financialReleaseRepository.GetFinancialReleasesByAccountIdAsync(extractRequest.AccountID);
            if (financialReleases != null && financialReleases.Any()) 
            {
                var balanceBeforePeriod = GetBalanceBeforePeriod(financialReleases, cutoffDate);
                extracts.Add(balanceBeforePeriod);

                var releases = financialReleases.Where(f => f.ReleaseDate >= cutoffDate && f.ReleaseDate <= DateTime.Now);

                GetReleasesInsidePeriod(releases, cutoffDate, extracts);

                GetBalanceOfTheDay(balanceBeforePeriod, releases, extracts);
                
                return extracts;
            }

            return null;
          
        }

        public async Task<IEnumerable<ExtractResponseDTO>> GetMonthlyExtractByAccountId(MonthlyExtractRequestDTO request)
        {
            var financialReleases = await _financialReleaseRepository.GetMonthlyExtractByAccountIdAsync(request.AccountID, request.firstDay, request.lastDay);
             return financialReleases.Select(financialRelease => new ExtractResponseDTO
             {
                 Type = financialRelease.Type == ReleaseTypes.Income ? "Receita" : "Despesa",
                 ReleaseDate = financialRelease.ReleaseDate,
                 Value = financialRelease.Value,
                 Description = financialRelease.Description,
                 Category = financialRelease.Subcategory.Category.Name,
                 Subcategory = financialRelease.Subcategory.Name
             });
        }

        public async Task<IEnumerable<Account>> GetAccountsByNameAsync(string name)
        {
            return await _accountRepository.GetAccountsByNameAsync(name);
        }

        public async Task<Account> GetAccountByNameAndNumberAsync(string name, string accountNumber)
        {
            return await _accountRepository.GetAccountByNameAndNumberAsync(name, accountNumber);
        }

        public async Task<Account> GetAccoundModelByIdWithFinancialReleases(Guid id)
        {
            return await _accountRepository.GetByIdWithFinancialReleases(id);
        }

        public async Task<AccountResponseDTO> CreateAccountAsync(AccountRequestDTO accountDTO)
        {
            var account = _mapper.Map<Account>(accountDTO);
            
            var accountInserted = await _accountRepository.CreateAsync(account);

            return _mapper.Map<AccountResponseDTO>(accountInserted);
        }

        public async Task<AccountResponseDTO> UpdateAccountAsync(Account account)
        {
            var accountUpdated = await _accountRepository.UpdateAsync(account);
            return _mapper.Map<AccountResponseDTO>(accountUpdated);
        }

        public async Task RemoveAccountAsync(Account account)
        {
            await _accountRepository.RemoveAsync(account);
        }

        public async Task<FinancialReleaseResponseDTO> UpdateFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<FinancialReleaseResponseDTO> RemoveFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTotalIncomeExpenseByAccountIdAsync(Guid accountId, ReleaseTypes type)
        {
            return await _financialReleaseRepository.GetTotalIncomeExpenseByAccountIdAsync(accountId, type);
        }

        #endregion

        #region Private Methods

        private static void GetReleasesInsidePeriod(IEnumerable<FinancialRelease> releases, DateTime cutoffDate, List<ExtractResponseDTO> extracts)
        {
            foreach (var release in releases)
            {
                extracts.Add(new ExtractResponseDTO
                {
                    Type = release.Type == ReleaseTypes.Income ? "Receita" : "Despesa",
                    ReleaseDate = release.ReleaseDate,
                    Value = release.Value,
                    Description = release.Description,
                    Category = release.Subcategory.Category.Name
                });
            }
        }

        private static ExtractResponseDTO GetBalanceBeforePeriod(IEnumerable<FinancialRelease> financialReleases, DateTime cutoffDate)
        {
            var incomeBeforePeriod = financialReleases
                .Where(f => f.Type == ReleaseTypes.Income && f.ReleaseDate < cutoffDate)
                .Sum(f => f.Value);

            var expenseBeforePeriodo = financialReleases
                .Where(f => f.Type == ReleaseTypes.Expense && f.ReleaseDate < cutoffDate)
                .Sum(f => f.Value);

            return new ExtractResponseDTO
            {
                Type = "Receita",
                ReleaseDate = cutoffDate,
                Value = incomeBeforePeriod - expenseBeforePeriodo,
                Description = "SALDO ANTERIOR",
                Category = "Receita"
            };
        }

        private static void GetBalanceOfTheDay(ExtractResponseDTO balanceBeforePeriod, IEnumerable<FinancialRelease> releases, List<ExtractResponseDTO> extracts)
        {
            var incomes = balanceBeforePeriod.Value + releases.Where(f => f.Type == ReleaseTypes.Income).Sum(f => f.Value);
            var expenses = releases.Where(f => f.Type == ReleaseTypes.Expense).Sum(f => f.Value);

            extracts.Add(new ExtractResponseDTO
            {
                Type = "Receita",
                ReleaseDate = DateTime.Now,
                Value = incomes - expenses,
                Description = "SALDO DO DIA",
                Category = "Receita"
            });
           
        }

        #endregion
    }
}
