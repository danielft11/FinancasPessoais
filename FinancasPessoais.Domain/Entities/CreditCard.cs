using FinancasPessoais.Domain.Exceptions;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;

namespace FinancasPessoais.Domain.Entities
{
    public sealed class CreditCard : BaseEntity
    {
        #region Properties
        
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public decimal CardLimit { get; private set; }
        public int InvoiceClosingDate { get; private set; }
        public int InvoiceDueDate { get; private set; }
        
        #endregion

        #region Navigation Properties
        
        public IEnumerable<PurchaseInInstallments> PurchaseInInstallments { get; set; }
        public IEnumerable<FinancialRelease> FinancialReleases { get; set; }
        
        #endregion

        #region Constructor
        
        public CreditCard(Guid id, string cardName, string cardNumber, decimal cardLimit, int invoiceClosingDate, int invoiceDueDate)
        {
            ValidateDomain(cardName, cardNumber, cardLimit, invoiceClosingDate, invoiceDueDate);
            Id = id;
        }
        
        #endregion

        #region Private Metdhos
        private void ValidateDomain(string cardName, string cardNumber, decimal cardLimit, int invoiceClosingDate, int invoiceDueDate)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(cardName), Constants.FieldRequired(cardName));
            DomainExceptionValidation.When(cardName.Length < 3, Constants.InvalidFieldMinimumCharacters(cardName, 3));
            DomainExceptionValidation.When(cardName.Length > 30, Constants.InvalidFieldMaximumCharacters(cardName, 30));
            DomainExceptionValidation.When(string.IsNullOrEmpty(cardNumber), Constants.FieldRequired(cardNumber));
            DomainExceptionValidation.When(cardNumber.Length != 16, Constants.InvalidFieldNumberCharacters(cardNumber, 16));
            DomainExceptionValidation.When(cardLimit < 0, Constants.InvalidFieldGreatherThan(cardLimit.ToString(), 0));
            DomainExceptionValidation.When(invoiceClosingDate <= 0 || invoiceClosingDate > 31, Constants.CreditCardInvalidDate("invoiceClosingDate"));
            DomainExceptionValidation.When(invoiceDueDate <= 0 || invoiceDueDate > 31, Constants.CreditCardInvalidDate("invoiceDueDate"));

            CardName = cardName;
            CardNumber = cardNumber;
            CardLimit = cardLimit;
            InvoiceClosingDate = invoiceClosingDate;
            InvoiceDueDate = invoiceDueDate;
        }
        #endregion
    }

}
