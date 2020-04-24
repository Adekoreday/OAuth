using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OAuth.Data;
using OAuth.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



namespace OAuth.webApi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public string authority {get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            authority = _configuration["Authority"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        string oauthConnectionString = _configuration["ConnectionString:DefaultConnection"];
                Debug.Assert(!String.IsNullOrEmpty(authority), "Default authority cannot be empty");
                Debug.Assert(!String.IsNullOrEmpty(oauthConnectionString), "Default connection string cannot be empty");
        services.AddDbContext<OAuthDbContext>(options => options.UseSqlServer(oauthConnectionString)); 
        services.AddControllers();
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = authority;
                options.RequireHttpsMetadata = false;
                options.Audience = "oauthApi";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else{
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
