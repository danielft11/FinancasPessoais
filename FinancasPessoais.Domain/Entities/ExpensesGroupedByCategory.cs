using System;

namespace FinancasPessoais.Domain.Entities
{
    public class ExpensesGroupedByCategory
    {
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
