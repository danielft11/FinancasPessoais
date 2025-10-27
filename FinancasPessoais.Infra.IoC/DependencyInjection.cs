using FinancasPessoais.Application.AutoMapper;
using FinancasPessoais.Application.Factories;
using FinancasPessoais.Application.Factories.Abstract;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Application.Services;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using FinancasPessoais.Infra.Data.Identity;
using FinancasPessoais.Infra.Data.Identity.Interfaces;
using FinancasPessoais.Infra.Data.Repositories;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FinancasPessoais.Infra.IoC
{
    public static class DependencyInjection
    {
        private static IServiceCollection _services;
        private static IConfiguration _configuration;

        public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            AddContext();
            AddIdentity();
            AddInfrastructureJWT(configuration);
            AddRepositories();
            AddServices(configuration);
            AddAutoMapperService();
            AddHangfire();

            return services;
        }

        private static void AddContext()
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            ); //Exibindo logs do Entity Framework no console: https://carloscds.net/2021/03/entity-framework-inspecionando-a-consulta/

        }

        private static void AddIdentity()
        {
            _services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddInfrastructureJWT(IConfiguration configuration)
        {
            _services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private static void AddRepositories()
        {
            _services.AddScoped<IAccountRepository, AccountRepository>();
            _services.AddScoped<ICategoryRepository, CategoryRepository>();
            _services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            _services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            _services.AddScoped<ICreditCardReleaseRepository, CreditCardReleaseRepository>();
            _services.AddScoped<IFinancialReleaseRepository, FinancialReleaseRepository>();
            _services.AddScoped<IPurchaseInInstallmentsRepository, PurchaseInInstallmentsRepository>();
            _services.AddScoped<IAccountPayableRepository, AccountPayableRepository>();
        }

        private static void AddServices(IConfiguration configuration)
        {
            _services.AddScoped<IAccountService, AccountService>();
            _services.AddScoped<ICategoryService, CategoryService>();
            _services.AddScoped<ICreditCardService, CreditCardService>();
            _services.AddScoped<ISubcategoryService, SubcategoryService>();
            _services.AddScoped<IFinancialReleaseService, FinancialReleaseService>();
            _services.AddScoped<IFinancialReleaseFactory, FinancialReleaseFactory>();
            _services.AddScoped<IAccountPayableService, AccountPayableService>();
            _services.AddScoped<IFileService, FileService>();
            _services.AddScoped<IEmailService, EmailService>();
            _services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        private static void AddAutoMapperService()
        {
            _services.AddAutoMapper(typeof(MappingProfiles));
        }

        private static void AddHangfire() 
        {
            _services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            _services.AddHangfireServer();
        }

    }
}
