using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementattions;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Security.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using Serilog;
using Microsoft.OpenApi.Models;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Connection to database
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            //Adding Migrations Support
            MigrateDatabase(connectionString);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            //Content negociation - Support to XML and JSON
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));

            })
            .AddXmlSerializerFormatters();

            //HATEOAS filter definitions
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

            //Service inject
            services.AddSingleton(filterOptions);

            //Versioning
            services.AddApiVersioning(option => option.ReportApiVersions = true);

            //Add Swagger Service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "RESTful API's with ASP.NET Core 5 and Docker",
                        Version = "v1"
                    });

            });
            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
            services.AddScoped<IFileBusiness, FileBusinessImpl>();

            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();

            //Dependency Injection of GenericRepository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            //Enable Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //Starting our API in Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();
            //Adding map routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=Values}/{id?}");
            });
        }

        private void MigrateDatabase(string connectionString)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();

            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed.", ex);
                throw;
            }
        }  
    }
}
