using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Infra.Data.Context;
using AssetVariation.Infra.Data.Repository;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Implementation;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Interfaces;
using AssetVariation.Infra.Mapper;
using AssetVariation.Infra.Settings;
using AssetVariation.Service.Interfaces;
using AssetVariation.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AssetVariationService.Api
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
            //httpClientFactory
            services.AddHttpClient();

            //settings
            const string appSettings = "AppSettings";
            services.Configure<AppSettings>(Configuration.GetSection(appSettings));

            // automapper          
            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); });
            services.AddSingleton(mapperConfig.CreateMapper());

            //DI
            SetIoc(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Title = "AssetVariationService.Api",
                    Version = "v1",
                    Description = "API construída para processar as variações do ativo PETR4.SA nos últimos 30 dias.",
                    Contact = new OpenApiContact
                    {
                        Name = "Jefferson Marques Quirino da Silva",
                        Email = "jeftsilva@gmail.com"
                    }
                });
            });
        }

        private void SetIoc(IServiceCollection services)
        {
            //context
            services.AddSingleton(d => Configuration);
            services.AddDbContext<AssetVariationContext>(options => options.UseSqlServer(Configuration["SqlConnection:SqlConnectionString"]), ServiceLifetime.Transient);

            //financeYahooChart       
            services.AddTransient<IFinanceYahooChart, FinanceYahooChart>();

            //repositories
            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<ITraddingFloorRepository, TraddingFloorRepository>();

            //services
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<ITraddingFloorService, TraddingFloorService>();
            services.AddTransient<IProcessService, ProcessMasterDataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssetVariationService.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}