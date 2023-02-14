using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApi.Models
{
    public class BankDbContext : DbContext
    {
        public BankDbContext() : base("name=BankDbConnectionString")
        {

            Database.SetInitializer<BankDbContext>(new BankInitializer());
        }

        public DbSet<BankCustomer> Customer { get; set; }
        public DbSet<BankAccount> Account { get; set; }
    }
}