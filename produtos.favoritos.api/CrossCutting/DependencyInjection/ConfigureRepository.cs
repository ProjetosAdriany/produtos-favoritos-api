using Data.Context;
using Data.Repository;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            string mySqlConnectionStr = "server = localhost; port = 3306; database = csharpBasico; user = root; password = 123456; Persist Security Info = False; Connect Timeout = 300";
            serviceCollection.AddScoped<IClientRepository, ClientRepository>();
            serviceCollection.AddScoped<IClientProductRepository, ClientProductRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IMagaluProvider, MagaluProvider>();
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr))
            );
        }
    }
}
