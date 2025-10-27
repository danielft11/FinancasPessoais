using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
              .HasKey(k => k.Id);

            builder
               .Property(p => p.CreationDate)
               .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(p => p.BankBranch)
                .HasMaxLength(30);

            builder
                .Property(p => p.AccountNumber)
                .HasMaxLength(30);

            SeedAccounts(builder);
        }

        private static void SeedAccounts(EntityTypeBuilder builder)
        {
            builder.HasData(
                new Account(InitialGuids.GUID_ACCOUNT_ITAU, "Itaú", "5611", "278499"),
                new Account(InitialGuids.GUID_ACCOUNT_CAIXA, "Caixa", "0081", "000007181")
                );
        }

    }
}
