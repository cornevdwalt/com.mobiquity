using com.mobiquity.packer.repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.api
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection PackerLibrary(this IServiceCollection services)
        {
            services.AddScoped<IPackerRepository, PackerRepository>();                      // Packer repository
            services.AddScoped<IPacker, Packer>();                                          // com.mobiquity.packer service
            return services;
        }
    }
}
