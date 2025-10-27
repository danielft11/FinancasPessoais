using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class PurchaseInInstallmentsConfiguration : IEntityTypeConfiguration<PurchaseInInstallments>
    {
        public void Configure(EntityTypeBuilder<PurchaseInInstallments> builder)
        {
            builder
            .HasKey(k => k.Id);

            builder
             .Property(p => p.PurchaseDate)
             .IsRequired();

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(p => p.PurchaseValue)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .Property(p => p.NumberOfInstallments)
                .IsRequired();

            builder
            .Property(p => p.ClosingDate);

            builder
              .HasOne(s => s.Subcategory)
              .WithMany(p => p.PurchaseInInstallments)
              .HasForeignKey(s => s.SubcategoryId);

            builder
              .HasOne(s => s.CreditCard)
              .WithMany(p => p.PurchaseInInstallments)
              .HasForeignKey(s => s.CreditCardId);
        }
    }

}
