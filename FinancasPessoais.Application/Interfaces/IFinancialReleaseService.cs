using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Interfaces
{
    public interface IFinancialReleaseService
    {
        Task<FinancialReleaseResponseDTO> GetFinancialReleaseByIdAsync(Guid id);
        Task<IEnumerable<CreditCardReleaseResponseDTO>> GetInvoiceByCreditCardId(Guid creditCardId, DateTime firstDay, DateTime lastDay);
        Task<FinancialReleaseResponseDTO> CreateFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO);
        Task<PurchaseInInstallmentsResponseDTO> CreatePurchaseInInstallmentsAsync(PurchaseInInstallmentsRequestDTO request, CreditCard creditCard);
        Task PayCreditCardRelease(CreditCardReleasePaymentRequestDTO request);
    }
}
