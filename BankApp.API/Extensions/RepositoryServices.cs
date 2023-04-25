using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Repositories;

namespace BankApp.API.Extensions
{
    public static class RepositoryServices
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
        }
    }
}
