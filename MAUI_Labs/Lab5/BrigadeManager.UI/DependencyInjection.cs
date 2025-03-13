using BrigadeManager.UI.Pages;
using BrigadeManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services) 
        {
            services
                .AddSingleton<Brigades>()
                .AddTransient<WorkDetails>()
                .AddTransient<AddOrEditBrigade>()
                .AddTransient<AddOrEditWork>();
            return services;
        }

        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services
                .AddSingleton<BrigadesViewModel>()
                .AddTransient<WorkDetailsViewModel>()
                .AddTransient<AddOrEditBrigadeViewModel>()
                .AddTransient<AddOrEditWorkViewModel>();
            return services;
        }
}
}
