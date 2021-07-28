using Application.Interfaces;
using Infrastructure.Clients;
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
            services.AddDbContext<ITransactionContext, TransactionContext>(options =>
            {
                options.UseInMemoryDatabase("TransactionsDb");
            });

            services.AddHttpClient<IAccountClient, AccountClient>(client =>
            {
                //TODO: pull this from the config.
                client.BaseAddress = new Uri("https://localhost:44394/accounts/");
            });

            return services;
        }
    }
}
