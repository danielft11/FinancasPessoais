using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Factories.Abstract;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Factories
{
    public class FinancialReleaseFactory : IFinancialReleaseFactory
    {
        #region Injected Services

        private readonly IFinancialReleaseService _financialReleaseService;
        private readonly IAccountService _accountService;
        private readonly ICreditCardService _creditCardService;

        public FinancialReleaseFactory(IFinancialReleaseService financialReleaseService, IAccountService accountService, ICreditCardService creditCardService)
        {
            _financialReleaseService = financialReleaseService;
            _accountService = accountService;
            _creditCardService = creditCardService;
        }

        #endregion

        public async Task<FinancialReleaseResponseDTO> CreateExpenseOnAccount(FinancialReleaseRequestDTO request)
        {
            var account = await _accountService.GetAccountByIdAsync(request.AccountId.GetValueOrDefault());
            if (account == null)
                throw new Exception(Constants.EntityNotExistError(Operations.Insert, "lançamento financeiro", "conta"));

            if (ReleaseAmountGreaterThanAccountBalance(request, account.Balance))
                throw new Exception(Constants.AMOUNT_GREATHER_THAN_ACCOUNT_BALANCE);

            return await _financialReleaseService.CreateFinancialReleaseAsync(request);

        }

        public async Task<FinancialReleaseResponseDTO> CreateExpenseOnCreditCard(FinancialReleaseRequestDTO request)
        {
            var creditCard = await _creditCardService.GetCreditCardByIdAsync(request.CreditCardId.GetValueOrDefault());
            if (creditCard == null)
                throw new Exception(Constants.EntityNotExistError(Operations.Insert, "despesa em cartão de crédito", "Cartão de Crédito"));

            return await _financialReleaseService.CreateFinancialReleaseAsync(request);
        }

        public async Task<PurchaseInInstallmentsResponseDTO> CreateInstallmentPurchaseOnCreditCard(PurchaseInInstallmentsRequestDTO request)
        {
            var creditCard = await _creditCardService.GetCreditCardAsync(request.CreditCardId);
            if (creditCard == null)
                throw new Exception(Constants.EntityNotExistError(Operations.Insert, "credit card release", "credit card"));

            return await _financialReleaseService.CreatePurchaseInInstallmentsAsync(request, creditCard);
        }

        public async Task<FinancialReleaseResponseDTO> GetFinancialReleaseByIdAsync(Guid id) => await _financialReleaseService.GetFinancialReleaseByIdAsync(id);

        public async Task<IEnumerable<CreditCardReleaseResponseDTO>> GetInvoiceByCreditCardId(CreditCard creditCard) 
        {
            DateTime lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, creditCard.InvoiceClosingDate);
            DateTime firstDay = lastDay.AddMonths(-1);

            return await _financialReleaseService.GetInvoiceByCreditCardId(creditCard.Id, firstDay, lastDay);
        }

        public async Task PayCreditCardRelease(CreditCardReleasePaymentRequestDTO request) => await _financialReleaseService.PayCreditCardRelease(request);

        private static bool ReleaseAmountGreaterThanAccountBalance(FinancialReleaseRequestDTO request, decimal balance)
        {
            return request.Type == ReleaseTypes.Expense && request.Value > balance;
        }

      
    }
}
