using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Domain.Service.Services;
using MarketEngine.Model.Models.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace MarketEngine.Web
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        private IServiceCollection services;

        public Startup(IHostingEnvironment env)
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection _services)
        {
            services = _services;
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

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            ConfigureConfigFiles();
            ConfigureAppParams();
            ConfigureDI();
        }

        private void ConfigureConfigFiles()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("MongoDBCredentials.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

        }

        private void ConfigureAppParams()
        {
            services.Configure<MongoSettings>(Configuration.GetSection("MongoDB"));
            DependencyInjection.Configuration.ConfigurationProvider.BuildProvider(Configuration);
        }

        private void ConfigureDI()
        {
            services.AddSingleton<IStatusService, StatusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

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
