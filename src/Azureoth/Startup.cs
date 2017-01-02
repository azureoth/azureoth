using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Azureoth.Database;
using Azureoth.RestfulDb;

namespace Azureoth
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseConnectionString = Configuration.GetConnectionString("DatabaseConnectionString");
            services.AddDbContext<AzureothDbContext>(options => options.UseSqlServer(databaseConnectionString));

            services.ConfigureRestfulDb(
                connectionString: Configuration.GetConnectionString("ProdDatabaseConnectionString"),   // Database connection string
                apiPrefix: "/api/db");                    // API path that the db will be exposed under

            services.AddMvc();

            // Add framework services.
            services.AddMvc();

            services.AddAuthentication(sharedOptions => sharedOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AzureothDbContext db)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            db.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Configure the OWIN pipeline to use cookie auth.
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            // Configure the OWIN pipeline to use OpenID Connect auth.
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                ClientId = Configuration["AzureAD:ClientId"],
                Authority = String.Format(Configuration["AzureAd:AadInstance"], Configuration["AzureAd:Tenant"]),
                ResponseType = OpenIdConnectResponseType.IdToken,
                PostLogoutRedirectUri = Configuration["AzureAd:PostLogoutRedirectUri"],
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false
                },
                Events = new OpenIdConnectEvents
                {
                    OnRemoteFailure = OnAuthenticationFailed,
                }
            });

            app.UseRestfulDb();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private Task OnAuthenticationFailed(FailureContext context)
        {
            context.HandleResponse();
            context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            return Task.FromResult(0);
        }
    }
}
