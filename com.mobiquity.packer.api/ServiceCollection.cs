using com.mobiquity.packer.repository;
using Microsoft.Extensions.DependencyInjection;

namespace com.mobiquity.packer.Packer
{
    /// <summary>
    /// Dependency injection server collection extention class for
    /// the Packer interfaces
    /// </summary>
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection PackerLibrary(this IServiceCollection services)
        {
            services.AddScoped<IPackerService, PackerService>();                            // Packer repository
            services.AddScoped<IPackerRepository, PackerRepository>();                      // Packer repository
            services.AddScoped<IPacker, Packer>();                                          // com.mobiquity.packer service
            return services;
        }
    }
}
