using FinancasPessoais.Domain.Exceptions;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;

namespace FinancasPessoais.Domain.Entities
{
    public sealed class Account : BaseEntity
    {
        #region Properties
        public string Name { get; private set; }
        public string BankBranch { get; private set; }
        public string AccountNumber { get; private set; }
        #endregion

        #region Navigation Properties
        public IEnumerable<FinancialRelease> FinancialReleases { get; set; } = new List<FinancialRelease>();
        #endregion

        #region Constructor
        public Account(Guid id, string name, string bankBranch, string accountNumber)
        {
            ValidateDomain(name, bankBranch, accountNumber);
            Id = id;
        }
        #endregion

        #region Private Methods
        public void UpdateModel(string name, string bankBranch, string accountNumber)
        {
            ValidateDomain(name, bankBranch, accountNumber);
        }

        private void ValidateDomain(string name, string bankBranch, string accountNumber)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), Constants.FieldRequired(name));
            DomainExceptionValidation.When(name.Length < 3, Constants.InvalidFieldMinimumCharacters(name, 3));
            DomainExceptionValidation.When(name.Length > 250, Constants.InvalidFieldMaximumCharacters(name, 250));

            if (!string.IsNullOrEmpty(bankBranch))
            {
                DomainExceptionValidation.When(bankBranch.Length < 3, Constants.InvalidFieldMinimumCharacters(bankBranch, 3));
                DomainExceptionValidation.When(bankBranch.Length > 30, Constants.InvalidFieldMaximumCharacters(name, 30));
            }

            Name = name;
            BankBranch = bankBranch;
            AccountNumber = accountNumber;
        }
        #endregion
    }

}
