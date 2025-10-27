using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.CreationDate)
                .IsRequired();

            builder
               .Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(2);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Description)
                .HasMaxLength(250);

            builder
                .Property(p => p.Type)
                .HasConversion<int>()
                .HasMaxLength(7);

            SeedCategory(builder);
        }

        private static void SeedCategory(EntityTypeBuilder builder)
        { 
            builder.HasData(new Category(InitialGuids.GUID_INCOME_CATEGORY_SALARY, "R1", "Salário", ReleaseTypes.Income, "Salário"));
            builder.HasData(new Category(InitialGuids.GUID_INCOME_CATEGORY_SERVICES, "R2", "Serviços", ReleaseTypes.Income, "Serviços"));
            builder.HasData(new Category(InitialGuids.GUID_INCOME_CATEGORY_LOANS, "R3", "Empréstimo - Recebimento", ReleaseTypes.Income, "Empréstimo - Recebimento"));
            builder.HasData(new Category(InitialGuids.GUID_INCOME_OTHERS_INCOMES, "R4", "Outras Receitas", ReleaseTypes.Income, "Outras Receitas"));
            builder.HasData(new Category(InitialGuids.GUID_EXPENSE_CATEGORY_FOOD, "A1", "Alimentação", ReleaseTypes.Expense, "Alimentação"));
        }

    }
}
