using AutoMapper;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using System;
using System.Globalization;

namespace FinancasPessoais.Application.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AccountRequestDTO, Account>()
                .ConstructUsing(src => new Account(Guid.NewGuid(), src.Name, src.BankBranch, src.AccountNumber));

            CreateMap<Account, AccountResponseDTO>().ReverseMap();
            CreateMap<Account, DetailedAccountResponseDTO>().ReverseMap();

            CreateMap<CategoryRequestDTO, Category>()
                .ConstructUsing(src => new Category(Guid.NewGuid(), src.Code, src.Name, src.Type, src.ParentCategoryId, src.Description));

            CreateMap<Category, CategoryResponseDTO>().ReverseMap();
            CreateMap<Category, CategoryResponseDTO>()
               .ForMember(dest => dest.ReleaseType, opt => opt.MapFrom(src => Enum.GetName(src.Type)));

            CreateMap<CreditCardRequestDTO, CreditCard>()
                .ConstructUsing(src => new CreditCard(Guid.NewGuid(), src.CardName, src.CardNumber, src.CardLimit, src.InvoiceClosingDate, src.InvoiceDueDate));

            CreateMap<CreditCard, CreditCardResponseDTO>().ReverseMap();

            CreateMap<FinancialRelease, CreditCardReleaseRequestDTO>().ReverseMap();
            CreateMap<FinancialRelease, CreditCardReleaseResponseDTO>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => FormatValue(src.Value)))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
                //.ForMember(dest => dest.CreditCardName, opt => opt.MapFrom(src => src.CreditCard.CardName));

            CreateMap<FinancialRelease, FinancialReleaseRequestDTO>().ReverseMap();

            CreateMap<FinancialRelease, FinancialReleaseResponseDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => FormatEnumType(src.Type)))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CreditCard.CardName));

            CreateMap<PurchaseInInstallments, PurchaseInInstallmentsRequestDTO>().ReverseMap();
            CreateMap<PurchaseInInstallments, PurchaseInInstallmentsResponseDTO>()
                .ForMember(dest => dest.PurchaseValue, opt => opt.MapFrom(src => FormatValue(src.PurchaseValue)))
                .ForMember(dest => dest.CreditCardName, opt => opt.MapFrom(src => src.CreditCard.CardName));

            CreateMap<AccountPayable, AccountPayableResponseDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
  
        }

        private static string FormatEnumType(ReleaseTypes type) => type.ToString().Equals("Income") ? "Receita" : "Despesa";

        private static string FormatValue(decimal value) => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", value);
      
    }
}
