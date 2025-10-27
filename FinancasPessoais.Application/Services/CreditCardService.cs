using AutoMapper;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        #region Properties

        private ICreditCardRepository _creditCardRepository;
        private IFinancialReleaseRepository _financialReleaseRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Construtor

        public CreditCardService(ICreditCardRepository creditCardRepository,
            IFinancialReleaseRepository financialReleaseRepository,
            IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _financialReleaseRepository = financialReleaseRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public methods

        public async Task<IEnumerable<CreditCardResponseDTO>> GetAsync()
        {
            var creditCards = await _creditCardRepository.GetAsync();
            return _mapper.Map<IEnumerable<CreditCardResponseDTO>>(creditCards);
        }

        public async Task<CreditCardResponseDTO> GetCreditCardByIdAsync(Guid id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);
            return _mapper.Map<CreditCardResponseDTO>(creditCard);
        }

        public async Task<CreditCard> GetCreditCardAsync(Guid id)
        {
            return await _creditCardRepository.GetByIdAsync(id);
        }

        public async Task<CreditCard> GetCreditCardModelByNumberAsync(string number)
        {
            return await _creditCardRepository.GetCardByNumberAsync(number);
        }

        public async Task<IEnumerable<CreditCard>> GetCreditCardsByCardNameAsync(string cardName)
        {
            return await _creditCardRepository.GetCardsByCardNameAsync(cardName);
        }

        public async Task CreateCreditCardAsync(CreditCardRequestDTO request)
        {
            var creditCard = _mapper.Map<CreditCard>(request);
            await _creditCardRepository.CreateCreditCardAsync(creditCard);
        }

        public async Task<CreditCardResponseDTO> UpdateCreditCardAsync(CreditCard creditCard)
        {
            return _mapper.Map<CreditCardResponseDTO>(await _creditCardRepository.UpdateAsync(creditCard));
        }

        public async Task RemoveCreditCardAsync(CreditCard creditCard)
        {
            await _creditCardRepository.RemoveAsync(creditCard);
        }

        public async Task<IEnumerable<FinancialRelease>> GetCreditCardReleasesByCreditCardId(Guid CreditCardId, DateTime closingDate)
        {
            return await _financialReleaseRepository.GetCreditCardReleasesByCreditCardIdAsync(CreditCardId, closingDate);
        }

        public async Task<FinancialRelease> GetCreditCardReleaseById(Guid id)
        {
            return await _financialReleaseRepository.GetByIdAsync(id);
        }

        #endregion

    }
}
