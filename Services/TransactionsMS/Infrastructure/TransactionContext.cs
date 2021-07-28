using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class TransactionContext : DbContext, ITransactionContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
