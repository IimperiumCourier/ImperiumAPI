using ImperiumLogistics.API.Diagnostics;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Implementation;
using ImperiumLogistics.Infrastructure.Repository;
using ImperiumLogistics.Infrastructure.Repository.Context;
using ImperiumLogistics.SharedKernel.Setting;
using Microsoft.AspNetCore.MiddlewareAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ImperiumLogistics.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // insert the AnalysisStartupFilter as the first IStartupFilter in the container
            builder.Services.Insert(0, ServiceDescriptor.Transient<IStartupFilter, AnalysisStartupFilter>());

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<ICompanyOnboardingService, CompanyOnboardingService>();
            builder.Services.AddHttpClient<IEmailService, EmailService>();

            builder.Services.AddTransient<ICompanyOnboardingService, CompanyOnboardingService>();
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            var connectionString = builder.Configuration.GetConnectionString("ImperiumDbString");
            builder.Services.AddDbContext<ImperiumDbContext>(x => x.UseSqlServer(connectionString));
            builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));


            //services cors
            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            var app = builder.Build();


            // Grab the "Microsoft.AspNetCore" DiagnosticListener from DI
            var listener = app.Services.GetRequiredService<DiagnosticListener>();

            // Create an instance of the AnalysisDiagnosticAdapter using the IServiceProvider
            // so that the ILogger is injected from DI
            var observer = ActivatorUtilities.CreateInstance<AnalysisDiagnosticAdapter>(app.Services);

            // Subscribe to the listener with the SubscribeWithAdapter() extension method
            using var disposable = listener.SubscribeWithAdapter(observer);

                app.UseSwagger();
                app.UseSwaggerUI();
            

            //app cors
            app.UseCors("corsapp");

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}