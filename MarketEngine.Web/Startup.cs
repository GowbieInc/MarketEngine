using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MarketEngine.Web
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger => 
            {
                swagger.SwaggerDoc("V1", new Info
                {
                    Title = "Gowbie Market Engine",
                    Version = "v1",
                    Description = "Project for educational purposes",
                    Contact = new Contact { Name = "Gabriel Silva", Url = "https://github.com/gabrielbpc" }
                });

                swagger.DescribeAllEnumsAsStrings();
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapHttpRoute(
            //        name: "default", routeTemplate: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("Swagger/V1/swagger.json", "Documentation V1");
                c.DocumentTitle = "Documentation";
            });
            var rewriteOptions = new RewriteOptions();
            rewriteOptions.AddRedirect("^$", "swagger");
            app.UseRewriter(rewriteOptions);
        }
    }
}
