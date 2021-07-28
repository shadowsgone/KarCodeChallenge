using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BankContext : DbContext, IBankContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
