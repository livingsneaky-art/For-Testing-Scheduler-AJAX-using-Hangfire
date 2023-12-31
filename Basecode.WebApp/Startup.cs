﻿using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Basecode.WebApp
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureDependencies(services);       // Configuration for dependency injections           
            this.ConfigureDatabase(services);           // Configuration for database connections
            this.ConfigureMapper(services);             // Configuration for entity model and view model mapping
            this.ConfigureCors(services);               // Configuration for CORS
            this.ConfigureAuth(services);               // Configuration for Authentication logic
            this.ConfigureMVC(services);                // Configuration for MVC                  

            // Add services to the container.
            services.AddControllersWithViews();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHrScheduler, HRScheduler>();
            // Use the correct SQL Server connection string here
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BaseCodeDB;Trusted_Connection=True;MultipleActiveResultSets=true";

            // Add Hangfire services with SQL Server storage
            services.AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString));

            // Add Hangfire server to process background jobs
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");     // Enables site to redirect to page when an exception occurs
                app.UseHsts();                              // Enables the Strict-Transport-Security header.
            }

            app.UseStaticFiles();           // Enables the use of static files
            app.UseHttpsRedirection();      // Enables redirection of HTTP to HTTPS requests.
            app.UseCors("CorsPolicy");      // Enables CORS                              
            app.UseAuthentication();        // Enables the ConfigureAuth service.

            app.UseRouting();
            this.ConfigureRoutes(app);      // Configuration for API controller routing

            app.UseAuthorization();
            this.ConfigureAuth(app);        // Configuration for Token Authentication

            // Use Hangfire dashboard
            app.UseHangfireDashboard("/hangfire");

        }
    }
}
