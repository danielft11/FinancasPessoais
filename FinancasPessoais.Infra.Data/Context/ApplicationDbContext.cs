using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Infra.Data.EntitiesConfiguration;
using FinancasPessoais.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinancasPessoais.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FinancialRelease> FinancialReleases { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<PurchaseInInstallments> PurchaseInInstallments { get; set; }
        public DbSet<AccountPayable> AccountsPayable { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialReleaseConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseInInstallmentsConfiguration());
            modelBuilder.ApplyConfiguration(new AccountPayableConfiguration());    
        }

    }
}
