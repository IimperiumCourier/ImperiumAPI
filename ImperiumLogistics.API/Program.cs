using ImperiumLogistics.API.Authorization;
using ImperiumLogistics.API.Diagnostics;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Implementation;
using ImperiumLogistics.Infrastructure.Repository;
using ImperiumLogistics.Infrastructure.Repository.Context;
using ImperiumLogistics.SharedKernel.Setting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.MiddlewareAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text;

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImperiumCourier.Api", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });

            builder.Services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; })
            .AddJwtBearer(opt => opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value,
                ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:SecretKey").Value))
            });


            builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();
            builder.Services.AddHttpClient<ICompanyService, CompanyService>();
            builder.Services.AddHttpClient<IEmailService, EmailService>();

            builder.Services.AddTransient<ICompanyService, CompanyService>();
            builder.Services.AddTransient<IPackageService, PackageService>();
            builder.Services.AddTransient<IPackageDescriptionService, PackageDescriptionService>();
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IPackageDescriptionRepo, PackageDescriptionRepository>();

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



            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            //app cors
            app.UseCors("corsapp");

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}