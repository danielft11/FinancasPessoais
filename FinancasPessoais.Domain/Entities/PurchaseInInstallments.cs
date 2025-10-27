using FinancasPessoais.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancasPessoais.Domain.Entities
{
    public sealed class PurchaseInInstallments : BaseEntity
    {
        #region Properties
        
        public DateTime PurchaseDate { get; private set; }
        public string Description { get; private set; }
        public decimal PurchaseValue { get; private set; }
        public int NumberOfInstallments { get; set; }
        public DateTime? ClosingDate { get; private set; }
        
        #endregion

        #region Navigation Properties
        
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public Guid CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
        
        
        public IList<FinancialRelease> FinancialReleases { get; set; } = new List<FinancialRelease>();
        #endregion

        #region Public Methods
        
        public void CreateParcels(int invoiceClosingDay, int invoiceDueDay)
        {
            DateTime invoiceClosingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, invoiceClosingDay);
            DateTime invoiceDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, invoiceDueDay);

            if (PurchaseDate <= invoiceClosingDate)
            {
                for (int i = 1; i <= NumberOfInstallments; i++)
                {
                    DateTime parcelDueDate;
                    
                    if (i == 1)
                        parcelDueDate = CheckParcelDueDateWeekend(invoiceDueDate);
                    else
                        parcelDueDate = CheckParcelDueDateWeekend(invoiceDueDate.AddMonths(i - 1));

                    FinancialReleases.Add(new FinancialRelease(parcelDueDate, GetValueOfParcel(), SetDescription(i), SubcategoryId, CreditCardId));
                }
            }
            else
            {
                for (int i = 1; i <= NumberOfInstallments; i++)
                {
                    DateTime parcelDueDate = CheckParcelDueDateWeekend(invoiceDueDate.AddMonths(i));
                    FinancialReleases.Add(new FinancialRelease(parcelDueDate, GetValueOfParcel(), SetDescription(i), SubcategoryId, CreditCardId));
                }
            }

        }

        public decimal GetValueOfParcel() => PurchaseValue / NumberOfInstallments;
        
        #endregion

        #region Private Methods
        
        private static DateTime CheckParcelDueDateWeekend(DateTime dueDate)
        {
            dueDate = dueDate.DayOfWeek == DayOfWeek.Saturday ? dueDate.AddDays(2) : dueDate;
            dueDate = dueDate.DayOfWeek == DayOfWeek.Sunday ? dueDate.AddDays(1) : dueDate;

            return dueDate;
        }

        private string SetDescription(int i)
        {
            StringBuilder descriptionBuilder = new StringBuilder(string.Empty);

            descriptionBuilder.Append(Description);
            descriptionBuilder.Append(" - ");
            descriptionBuilder.Append(@$"parcela {i} de {NumberOfInstallments} - {PurchaseDate}.");

            return descriptionBuilder.ToString();
        }
       
        #endregion

    }
}
