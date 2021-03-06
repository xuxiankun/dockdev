using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using eCommerceApiProducts.Models;
using AspNetCoreRateLimit;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace eCommerceApiProducts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // needed to load configuration from appsettings.json
	        services.AddOptions();
            services.AddMemoryCache();
            
           
            services.AddScoped<SomeRepository>();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimitPolices"));
            services.AddControllers();
             services.AddApiVersioning(config=>{
                config.DefaultApiVersion = new ApiVersion(1,0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                //config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
	        services.AddInMemoryRateLimiting();
            services.AddMediatR(typeof(Startup));

            //services.AddMediatR(Assembly.GetExecutingAsse());

            services.AddDbContextPool<ProductsDbContext>(optionsBuilder =>
            {

                optionsBuilder.UseMySQL(Configuration.GetConnectionString("DBContext"));

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerceApiProducts", Version = "v1.0" });
            });
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIpRateLimiting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                SwaggerOptions swaggerOptions = new SwaggerOptions();
                Configuration.GetSection("Swagger").Bind(swaggerOptions);
                //app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerOptions.UiEndpoint, "eCommerceApiProducts v1"));
                                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eCommerceApiProducts v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
