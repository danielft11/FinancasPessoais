using FinancasPessoais.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;
using Hangfire;
using FinancasPessoais.Application.Services;

namespace FinancasPessoais.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7167", "http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddInfraStructure(Configuration);

            services.AddLogging();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinancasPessoais.Api", Version = "v1" });
                //c.ResolveConflictingActions(x => x.First());
            });
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinancasPessoais.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policyName: "CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configura o dashboard do Hangfire
            //app.UseHangfireDashboard();

            // Exemplo de job recorrente
            //RecurringJob.AddOrUpdate<AccountPayableService>(
            //    "check-and-send-reminders", 
            //    accountsPayableService => accountsPayableService.CheckAndSendReminders(), 
            //    Cron.Minutely // Executa a cada hora
            //);

        }

    }
}
