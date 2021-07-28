using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection SetupInfrastructure(this IServiceCollection services, 
            Action<InfrastructureOptions> options = null)
        {
            services.AddDbContext<IUsersContext, UsersContext>(options =>
            {
                options.UseInMemoryDatabase("UsersDb");
            });

            return services;
        }
    }
}
