using Microsoft.Extensions.DependencyInjection;

namespace HomeBird.DataBase.Logic.Extensions
{
    public static class ServeiceCollectionExtensions
    {
        public static IServiceCollection AddUnits(this IServiceCollection services)
        {
            services.AddScoped<IBroodsUnit, BroodsUnit>();
            services.AddScoped<IIncubatorsUnit, IncubatorsUnit>();
            services.AddScoped<ILayingsUnit, LayingsUnit>();
            services.AddScoped<ILotsUnit, LotsUnit>();
            services.AddScoped<IOverheadsUnit, OverheadsUnit>();
            services.AddScoped<IPurchasesUnit, PurchasesUnit>();
            services.AddScoped<ISalesUnit, SalesUnit>();

            return services;
        }
    }
}
