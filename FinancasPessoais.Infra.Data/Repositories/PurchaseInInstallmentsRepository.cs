using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class PurchaseInInstallmentsRepository : BaseRepository<PurchaseInInstallments>, IPurchaseInInstallmentsRepository
    {
        public PurchaseInInstallmentsRepository(ApplicationDbContext context) : base(context) {}
    }
}
