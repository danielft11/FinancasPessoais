using FinancasPessoais.Domain.Exceptions;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;

namespace FinancasPessoais.Domain.Entities
{
    public sealed class Subcategory : BaseEntity
    {
        #region Properties
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Code { get; set; }
        #endregion

        #region Navigation Properties
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<FinancialRelease> FinancialReleases { get; set; }
        public IEnumerable<PurchaseInInstallments> PurchaseInInstallments { get; set; }
        public IEnumerable<AccountPayable> AccountsPayable { get; set; }
        #endregion

        #region Constructor

        public Subcategory(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }

        public Subcategory(Guid id, string name, string code, string description, Guid categoryId)
        {
            ValidateDomain(name, code, description, categoryId);
            Id = id;
        }
        #endregion

        #region Public Methods
        public void UpdateModel(string name, string code, string description, Guid categoryId)
        {
            ValidateDomain(name, code, description, categoryId);
        }
        #endregion

        #region Private Methods
        private void ValidateDomain(string name, string code, string description, Guid categoryId)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), Constants.FieldRequired(name));
            DomainExceptionValidation.When(name.Length < 2, Constants.InvalidFieldMinimumCharacters(name, 2));
            DomainExceptionValidation.When(name.Length > 50, Constants.InvalidFieldMaximumCharacters(name, 50));
            DomainExceptionValidation.When(string.IsNullOrEmpty(code), Constants.FieldRequired(code));
            DomainExceptionValidation.When(code.Length != 5, Constants.InvalidFieldNumberCharacters(code, 5));
            DomainExceptionValidation.When(categoryId == Guid.Empty, Constants.SUBCATEGORY_WITHOUT_CATEGORY);

            if (!string.IsNullOrEmpty(description))
            {
                DomainExceptionValidation.When(description.Length < 2, Constants.InvalidFieldMinimumCharacters(description, 2));
                DomainExceptionValidation.When(description.Length > 250, Constants.InvalidFieldMaximumCharacters(description, 250));
            }
            Name = name;
            Code = code;
            Description = description;
            CategoryId = categoryId;
        }
        #endregion
    }

}
