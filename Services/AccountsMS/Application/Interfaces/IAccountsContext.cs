using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountsContext
    {
        //TODO: We can actually extract this out more so that the Application Layer
        // does not have a dependancy on EF.
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountUser> AccountUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
