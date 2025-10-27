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
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Services
{
    public class FinancialReleaseService : IFinancialReleaseService
    {
        private IFinancialReleaseRepository _financialReleaseRepository;
        private IPurchaseInInstallmentsRepository _purchaseInInstallmentsRepository;
        private readonly IMapper _mapper;

        public FinancialReleaseService(IFinancialReleaseRepository financialReleaseRepository, IPurchaseInInstallmentsRepository purchaseInInstallmentsRepository, IMapper mapper)
        {
            _financialReleaseRepository = financialReleaseRepository;
            _purchaseInInstallmentsRepository = purchaseInInstallmentsRepository;
            _mapper = mapper;
        }

        public async Task<FinancialReleaseResponseDTO> GetFinancialReleaseByIdAsync(Guid id)
        {
            var financialRelease = await _financialReleaseRepository.GetByIdAsync(id);
            return _mapper.Map<FinancialReleaseResponseDTO>(financialRelease);
        }

        public async Task<IEnumerable<CreditCardReleaseResponseDTO>> GetInvoiceByCreditCardId(Guid creditCardId, DateTime firstDay, DateTime lastDay) 
        {
            var financialReleases = await _financialReleaseRepository.GetInvoiceByCreditCardId(creditCardId, firstDay, lastDay);
            return financialReleases.Select(financialRelease => new CreditCardReleaseResponseDTO
            {
                ReleaseDate = financialRelease.ReleaseDate,
                PaymentDate = financialRelease.PaymentDate,
                Value = financialRelease.Value.ToString(),
                Description = financialRelease.Description,
                CategoryName = financialRelease.Subcategory.Category.Name,
                SubcategoryName = financialRelease.Subcategory.Name
            });
        }

        public async Task<FinancialReleaseResponseDTO> CreateFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO)
        {
            var financialRelease = _mapper.Map<FinancialRelease>(financialReleaseDTO);

            if (financialReleaseDTO.Type == ReleaseTypes.Expense && financialReleaseDTO.AccountId != null)
                financialRelease.PaymentDate = financialReleaseDTO.ReleaseDate;

            await _financialReleaseRepository.CreateAsync(financialRelease);

            return _mapper.Map<FinancialReleaseResponseDTO>(financialRelease);
        }

        public async Task<PurchaseInInstallmentsResponseDTO> CreatePurchaseInInstallmentsAsync(PurchaseInInstallmentsRequestDTO request, CreditCard creditCard)
        {
            PurchaseInInstallments purchase = _mapper.Map<PurchaseInInstallments>(request);

            purchase.CreateParcels(creditCard.InvoiceClosingDate, creditCard.InvoiceDueDate);

            return _mapper.Map<PurchaseInInstallmentsResponseDTO>(await _purchaseInInstallmentsRepository.CreateAsync(purchase));
        }

        public async Task PayCreditCardRelease(CreditCardReleasePaymentRequestDTO request)
        {
            await _financialReleaseRepository.PayCreditCardRelease(request.Id, request.PaymentDate, request.AccountId);
        }

    }
}
