using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionContext
    {
        //TODO: We can actually extract this out more so that the Application Layer
        // does not have a dependancy on EF.
        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
