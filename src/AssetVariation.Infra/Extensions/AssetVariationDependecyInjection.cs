using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Infra.Data.Repository;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Implementation;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AssetVariation.Infra.Extensions
{
    public static class AssetVariationDependecyInjection
    {
        public static IServiceCollection AddAssetVariationDependencyInjection(this IServiceCollection services)
        {
            //FinanceYahooChart
            services.AddTransient<IFinanceYahooChart, FinanceYahooChart>();

            //Repositories
            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<ITraddingFloorRepository, TraddingFloorRepository>();

      

            return services;
        }
    }
}
