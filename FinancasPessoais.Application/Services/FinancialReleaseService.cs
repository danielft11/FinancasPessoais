using AutoMapper;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Services
{
    public class FinancialReleaseService : IFinancialReleaseService
    {
        private IFinancialReleaseRepository _financialReleaseRepository;
        private IPurchaseInInstallmentsRepository _purchaseInInstallmentsRepository;
        private readonly IMapper _mapper;

        CultureInfo culture;

        public FinancialReleaseService(IFinancialReleaseRepository financialReleaseRepository, IPurchaseInInstallmentsRepository purchaseInInstallmentsRepository, IMapper mapper)
        {
            _financialReleaseRepository = financialReleaseRepository;
            _purchaseInInstallmentsRepository = purchaseInInstallmentsRepository;
            _mapper = mapper;

            culture = new CultureInfo("pt-BR");
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
                CategoryName = financialRelease.Category.Name
            });
        }

        public async Task<FinancialReleaseResponseDTO> CreateFinancialReleaseAsync(FinancialReleaseRequestDTO financialReleaseDTO, string userID)
        {
            var financialRelease = _mapper.Map<FinancialRelease>(financialReleaseDTO);
            financialRelease.UserId = userID;

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

        public async Task DeleteFinancialReleaseAsync(Guid id, string userID)
        {
            var financialRelease = await _financialReleaseRepository.GetByIdAsync(id);
            if (financialRelease == null)
                throw new KeyNotFoundException();

            if (financialRelease.UserId != userID)
                throw new UnauthorizedAccessException();

            await _financialReleaseRepository.RemoveAsync(financialRelease);
        }

        public async Task<ExpenseChartResponseDTO> GetExpensesGroupedByCategoryAsync(ChartRequest request)
        {
            var categoryIds = request.Categories;
            var result = await _financialReleaseRepository.GetExpensesGroupedByCategoryAsync(categoryIds);

            var periods = result
                .Select(r => new YearMonth(r.Year, r.Month))
                .Distinct()
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToList();

            var datasets = result
                .GroupBy(x => x.Category)
                .Select(category => new ExpenseChartDatasetDTO
                {
                    Label = category.Key,
                    CategoryId = category.First().CategoryId,
                    Data = periods.Select(period => category
                        .FirstOrDefault(x => x.Year == period.Year && x.Month == period.Month)?.Amount ?? 0)
                        .ToList()
                })
                .ToList();

            var labels = periods
                .Select(x => $"{culture.DateTimeFormat.GetAbbreviatedMonthName(x.Month).Replace(".", "")}/{x.Year}")
                .ToList();

            var chart = new ExpenseChartResponseDTO
            {
                Labels = labels,
                Datasets = datasets
            };

            return chart;
        }

    }
}
