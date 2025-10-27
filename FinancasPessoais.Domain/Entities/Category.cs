using FinancasPessoais.Domain.Exceptions;
using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancasPessoais.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        #region Properties
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ReleaseTypes Type { get; set; }
        #endregion

        #region Navigation Properties
        public IEnumerable<Subcategory> Subcategories { get; private set; }
        #endregion

        #region Construtor

        public Category(Guid id, string code, string name, ReleaseTypes type, string description = "")
        {
            ValidateDomain(code, name, type, description);
            Id = id;
        }

        #endregion

        #region Public Methods

        public void UpdateModel(string code, string name, ReleaseTypes type, string description) 
        {
            ValidateDomain(code, name, type, description);
        }

        public bool HasSubcategories() => Subcategories.Any();

        #endregion

        #region Private Methods

        private void ValidateDomain(string code, string name, ReleaseTypes type, string description) 
        {
            DomainExceptionValidation.When(code.Length != 2, Constants.InvalidFieldNumberCharacters(code, 2));
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), Constants.FieldRequired(name));
            DomainExceptionValidation.When(name.Length < 2, Constants.InvalidFieldMinimumCharacters(name, 2));
            DomainExceptionValidation.When(name.Length > 50, Constants.InvalidFieldMaximumCharacters(name, 50));
            DomainExceptionValidation.When(type != ReleaseTypes.Income && type != ReleaseTypes.Expense, Constants.INVALID_RELEASE_TYPE);

            if (!string.IsNullOrEmpty(description)) 
            {
                DomainExceptionValidation.When(description.Length < 2, Constants.InvalidFieldMinimumCharacters(description, 2));
                DomainExceptionValidation.When(description.Length > 250, Constants.InvalidFieldMaximumCharacters(description, 250));
            }
            Code = code;
            Name = name;
            Type = type;
            Description = description;
        }

        #endregion

    }

}
