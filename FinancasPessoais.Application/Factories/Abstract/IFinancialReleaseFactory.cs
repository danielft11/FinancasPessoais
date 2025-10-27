using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Factories.Abstract
{
    public interface IFinancialReleaseFactory
    {
        Task<FinancialReleaseResponseDTO> CreateExpenseOnAccount(FinancialReleaseRequestDTO request);
        Task<FinancialReleaseResponseDTO> CreateExpenseOnCreditCard(FinancialReleaseRequestDTO request);
        Task<PurchaseInInstallmentsResponseDTO> CreateInstallmentPurchaseOnCreditCard(PurchaseInInstallmentsRequestDTO request);
        Task<FinancialReleaseResponseDTO> GetFinancialReleaseByIdAsync(Guid id);
        Task<IEnumerable<CreditCardReleaseResponseDTO>> GetInvoiceByCreditCardId(CreditCard creditCard);
        Task PayCreditCardRelease(CreditCardReleasePaymentRequestDTO request);
    }
}
