using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder
              .HasKey(k => k.Id);

            builder
               .Property(p => p.CreationDate)
               .IsRequired();

            builder
                .Property(p => p.CardName)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(p => p.CardNumber)
                .IsRequired()
                .HasMaxLength(16);

            builder
                .Property(p => p.CardLimit)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .Property(p => p.InvoiceClosingDate)
                .IsRequired();

            builder
                .Property(p => p.InvoiceDueDate)
                .IsRequired();

            SeedCreditCard(builder);
        }

        private static void SeedCreditCard(EntityTypeBuilder builder)
        {
            builder.HasData(new CreditCard(InitialGuids.GUID_CREDIT_CARD_ITAUCARD, "Itaucard Click Final 9289", "5316805324229289", 15000, 2, 9));
        }

    }
}
