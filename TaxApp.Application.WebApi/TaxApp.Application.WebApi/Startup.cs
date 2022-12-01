using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaxApp.Domain.Service.Class;
using TaxApp.Domain.Service.Interface;
using TaxApp.Infrastructure.DataAccess.Class;
using TaxApp.Infrastructure.DataAccess.Interface;
using TaxApp.Infrastructure.DataAccess.Models;

namespace TaxApp.Application.WebApi
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
            services.AddMvc();
            // Connection to local database (MDF file)
            services.AddDbContext<TaxApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //DI
            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<IMunicipalityTaxDetailsRepository, MunicipalityTaxDetailsRepository>();
            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<ITaxSlabTypeRepository, TaxSlabTypeRepository>();
            services.AddScoped<ITaxSlabDetailsRepository, TaxSlabDetailsRepository>();
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{id}");
            });
        }
    }
}
