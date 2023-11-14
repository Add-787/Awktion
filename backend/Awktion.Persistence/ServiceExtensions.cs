using System.ComponentModel.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Awktion.Persistence;
public static class ServiceExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        if(configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
        
            
        }

    }


}
