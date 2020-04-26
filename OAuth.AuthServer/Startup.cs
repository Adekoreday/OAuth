using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OAuth.AuthServer
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           string certificatePath = _configuration["Secrets:KeyPath"];
           string certificatePassword = _configuration["Secrets:Password"];

          services.AddIdentityServer()
         .AddSigningCredential(new X509Certificate2(certificatePath, certificatePassword))
         .AddInMemoryIdentityResources(Config.IdentityResources())
         .AddInMemoryApiResources(Config.ApiResources())
          .AddInMemoryClients(Config.Clients(_configuration))
         .AddTestUsers(Config.TestUsers().ToList());
         services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
