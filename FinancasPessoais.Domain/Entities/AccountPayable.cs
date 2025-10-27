using FinancasPessoais.Domain.Exceptions;
using FinancasPessoais.Domain.Utils;
using System;

namespace FinancasPessoais.Domain.Entities
{
    public class AccountPayable : BaseEntity
    {
        public DateTime DueDate { get; private set; }
        public decimal Value { get; private set; }
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public string Description { get; private set; }
        public string BarCode { get; private set; }
        public DateTime ScheduleDate { get; set; }
        public string Emails { get; set; }
        public string FilePath { get; set; }

        public AccountPayable(DateTime dueDate, decimal value, Guid subcategoryId, string description, string barCode, DateTime scheduleDate, string emails, string filePath)
        {
            ValidateDomain(dueDate, value, subcategoryId, description);
            BarCode = barCode;
            ScheduleDate = scheduleDate;
            Emails = emails;
            FilePath = filePath;
        }

        #region Private Methods

        private void ValidateDomain(DateTime dueDate, decimal value, Guid subcategoryId, string description)
        {
            DomainExceptionValidation.When(value <= 0, Constants.InvalidFieldGreatherThan(value.ToString(), 0));
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), Constants.FieldRequired(description));
            DomainExceptionValidation.When(description.Length < 2, Constants.InvalidFieldMinimumCharacters(description, 2));
            DomainExceptionValidation.When(description.Length > 250, Constants.InvalidFieldMaximumCharacters(description, 250));
            DomainExceptionValidation.When(subcategoryId == Guid.Empty, Constants.INVALID_SUBCATEGORY_ID);

            DueDate = dueDate;
            Value = value;
            SubcategoryId = subcategoryId;
            Description = description;
        }

        #endregion

    }

}
