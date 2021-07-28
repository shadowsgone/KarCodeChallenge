using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AccountsContext : DbContext, IAccountsContext
    {
        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
    }
}
