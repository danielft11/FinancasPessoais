using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.EntitiesConfiguration
{
    public class AccountPayableConfiguration : IEntityTypeConfiguration<AccountPayable>
    {
        void IEntityTypeConfiguration<AccountPayable>.Configure(EntityTypeBuilder<AccountPayable> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.DueDate)
                .IsRequired();

            builder
              .Property(p => p.Value)
              .IsRequired()
              .HasPrecision(10, 2);

            builder
               .HasOne(s => s.Subcategory)
               .WithMany(f => f.AccountsPayable)
               .HasForeignKey(s => s.SubcategoryId);

            builder
               .Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(250);

            builder
               .Property(p => p.BarCode);

            builder
               .Property(p => p.ScheduleDate);

            builder
               .Property(p => p.Emails);

            builder
               .Property(p => p.FilePath);
        }
    }
}
