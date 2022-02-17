﻿using com.mobiquity.packer.repository;
using Microsoft.Extensions.DependencyInjection;

namespace com.mobiquity.packer.Packer
{
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
