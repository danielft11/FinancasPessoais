using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class CreditCardRepository : BaseRepository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(ApplicationDbContext context) : base(context) {}

        public async Task<IEnumerable<CreditCard>> GetCardsByCardNameAsync(string cardName)
        {
            return await _context.CreditCards.Where(c => c.CardName.Contains(cardName)).ToListAsync();
        }

        public async Task<CreditCard> GetCardByNumberAsync(string number)
        {
            return await _context.CreditCards.Where(c => c.CardNumber == number).FirstOrDefaultAsync();
        }

        public async Task CreateCreditCardAsync(CreditCard creditCard)
        {
            _context.CreditCards.Add(creditCard);
            await _context.SaveChangesAsync();
        }

    }
}
