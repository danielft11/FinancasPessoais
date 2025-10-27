using FinancasPessoais.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface ICreditCardRepository : IBaseRepository<CreditCard>
    {
        Task<IEnumerable<CreditCard>> GetCardsByCardNameAsync(string cardName);
        Task<CreditCard> GetCardByNumberAsync(string number);
        Task CreateCreditCardAsync(CreditCard creditCard);
    }
}
