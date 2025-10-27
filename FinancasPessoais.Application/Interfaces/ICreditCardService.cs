using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Interfaces
{
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCardResponseDTO>> GetAsync();
        Task<CreditCardResponseDTO> GetCreditCardByIdAsync(Guid id);
        Task<CreditCard> GetCreditCardAsync(Guid id);
        Task<CreditCard> GetCreditCardModelByNumberAsync(string number);
        Task<IEnumerable<CreditCard>> GetCreditCardsByCardNameAsync(string cardName);
        Task CreateCreditCardAsync(CreditCardRequestDTO request);
        Task<CreditCardResponseDTO> UpdateCreditCardAsync(CreditCard creditCard);
        Task RemoveCreditCardAsync(CreditCard creditCard);
        Task<FinancialRelease> GetCreditCardReleaseById(Guid id);
        Task<IEnumerable<FinancialRelease>> GetCreditCardReleasesByCreditCardId(Guid CreditCardId, DateTime closingDate);
    }
}
