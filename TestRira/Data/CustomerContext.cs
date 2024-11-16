using Microsoft.EntityFrameworkCore;
using System;
using TestRira.Entities;

namespace TestRira.Data
{
    public class CustomerContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public CustomerContext()
        {
        }
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}
