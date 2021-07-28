using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBankContext
    {
        //TODO: We can actually extract this out more so that the Application Layer
        // does not have a dependancy on EF.
        DbSet<Bank> Banks { get; set; }
        DbSet<BankAccount> BankAccounts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
