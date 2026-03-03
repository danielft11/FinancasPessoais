using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Infra.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class FinancialReleaseConfiguration : IEntityTypeConfiguration<FinancialRelease>
    {
        public void Configure(EntityTypeBuilder<FinancialRelease> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.CreationDate)
                .IsRequired();

            builder
                .Property(p => p.Type)
                .HasConversion<int>()
                .HasMaxLength(7);

            builder
                .Property(p => p.ReleaseDate)
                .IsRequired();

            builder
                .Property(p => p.PaymentDate);

            builder
                .Property(p => p.Value)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder
               .HasOne(s => s.Subcategory)
               .WithMany(f => f.FinancialReleases)
               .HasForeignKey(s => s.SubcategoryId);

            builder
                .HasOne(a => a.Account)
                .WithMany(f => f.FinancialReleases)
                .HasForeignKey(a => a.AccountId)
                .IsRequired(false);

            builder
               .HasOne(c => c.CreditCard)
               .WithMany(cr => cr.FinancialReleases)
               .HasForeignKey(cr => cr.CreditCardId);

            /* Shadow Navigation, uma propriedade que existe apenas no banco de dados, mas não existe na classe de domínio.
            /* Ela é usada para criar um relacionamento entre as entidades sem precisar adicionar uma propriedade de navegação 
            na classe de domínio. */
            builder
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(fr => fr.UserId)
                .IsRequired(true);
        }

    }
}



