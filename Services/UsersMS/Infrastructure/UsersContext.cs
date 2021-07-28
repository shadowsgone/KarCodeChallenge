using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class UsersContext : DbContext, IUsersContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
