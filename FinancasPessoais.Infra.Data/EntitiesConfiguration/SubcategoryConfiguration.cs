using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder
               .HasKey(k => k.Id);

            builder
                .Property(p => p.CreationDate)
                .IsRequired();

            builder
              .Property(p => p.Name)
              .IsRequired()
              .HasMaxLength(50);

            builder
                .Property(p => p.Description)
                .HasMaxLength(250);

            builder
                .Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(5);

            builder
                .HasOne(c => c.Category)
                .WithMany(s => s.Subcategories)
                .HasForeignKey(c => c.CategoryId);

            SeedSubcategory(builder);
        }

        private static void SeedSubcategory(EntityTypeBuilder builder)
        {
            builder.HasData(new Subcategory(Guid.NewGuid(), "Salário", "R1001", "Salário", InitialGuids.GUID_INCOME_CATEGORY_SALARY));
            builder.HasData(new Subcategory(Guid.NewGuid(), "13º salário", "R1002", "13º salário", InitialGuids.GUID_INCOME_CATEGORY_SALARY));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Mão-de-obra", "R2001", "Mão-de-obra", InitialGuids.GUID_INCOME_CATEGORY_SERVICES));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Venda de peças", "R2002", "Venda de peças", InitialGuids.GUID_INCOME_CATEGORY_SERVICES));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Revenda de peças", "R2003", "Revenda de peças", InitialGuids.GUID_INCOME_CATEGORY_SERVICES));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Empréstimo - Recebimento", "R3001", "Empréstimo - Recebimento", InitialGuids.GUID_INCOME_CATEGORY_LOANS));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Proventos de investimentos", "R4001", "Proventos de investimentos", InitialGuids.GUID_INCOME_OTHERS_INCOMES));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Transferência entre contas", "R4002", "Transferência entre contas", InitialGuids.GUID_INCOME_OTHERS_INCOMES));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Compra em supermercado", "A1001", "Compra em supermercado", InitialGuids.GUID_EXPENSE_CATEGORY_FOOD));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Feira: frutas e verduras", "A1002", "Feira: frutas e verduras", InitialGuids.GUID_EXPENSE_CATEGORY_FOOD));
            builder.HasData(new Subcategory(Guid.NewGuid(), "Padaria", "A1003", "Padaria", InitialGuids.GUID_EXPENSE_CATEGORY_FOOD));
        }

    }
}
