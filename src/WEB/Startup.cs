using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using DAL.SqlServer;
using DAL.Context;
using DAL.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using DAL.DatabaseProvider;
using DAL.ServiceCollectionExtension;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Emailing;
using Microsoft.Extensions.Logging;
using WEB.Extensions;
using WEB.Middlewares;
using WEB.Providers;

namespace WEB
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder();

            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddTransient<IDatabaseProvider, SqlServerProvider>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddEntityFramework();
            services.Configure<EmailCredential>(Configuration.GetSection("EmailCredential"));

            services.AddIdentity();

            services.AddAuthentication(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                //app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseErrorHandling();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
