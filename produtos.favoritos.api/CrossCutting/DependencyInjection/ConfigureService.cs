using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IClientService, ClientService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}
