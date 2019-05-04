using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication5
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { }
        public void ConfigureServices(IServiceCollection services)
        {
            //SampleData.InitData();
            services.AddMvc();                   
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name:"default",
                    template: "{controller=Suggestion}/{action=Index}/{id?}");
            });
        }
    }
}
