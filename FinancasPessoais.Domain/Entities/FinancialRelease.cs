using FinancasPessoais.Domain.Exceptions;
using FinancasPessoais.Domain.Utils;
using System;

namespace FinancasPessoais.Domain.Entities
{
    public class FinancialRelease : BaseEntity
    {
        public ReleaseTypes Type { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public DateTime? PaymentDate { get; set; }
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }

        public Guid? PurchaseInInstallmentsId { get; set; }
        public PurchaseInInstallments PurchaseInInstallments { get; set; }

        public FinancialRelease(DateTime releaseDate, decimal value, string description, Guid subcategoryId, Guid? creditCardId)
        {
            ValidateDomain(releaseDate, value, description, subcategoryId, creditCardId);
        }

        #region Private Methods

        private void ValidateDomain(DateTime releaseDate, decimal value, string description, Guid subcategoryId, Guid? creditCardId)
        {
            DomainExceptionValidation.When(value <= 0, Constants.InvalidFieldGreatherThan(value.ToString(), 0));
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), Constants.FieldRequired(description));
            DomainExceptionValidation.When(description.Length < 2, Constants.InvalidFieldMinimumCharacters(description, 2));
            DomainExceptionValidation.When(description.Length > 250, Constants.InvalidFieldMaximumCharacters(description, 250));
            DomainExceptionValidation.When(subcategoryId == Guid.Empty, Constants.INVALID_SUBCATEGORY_ID);
            DomainExceptionValidation.When(creditCardId == Guid.Empty, Constants.FAILED_INCLUDE_FINANCIALRELEASE_WITHOUT_ACCOUNT);

            Type = ReleaseTypes.Expense;
            ReleaseDate = releaseDate;
            Value = value;
            Description = description;
            SubcategoryId = subcategoryId;
            CreditCardId = creditCardId;
        }

        #endregion
    }

}
