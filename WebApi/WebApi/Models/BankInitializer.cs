using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApi.Models
{
    public class BankInitializer : CreateDatabaseIfNotExists<BankDbContext>
    {
        protected override void Seed(BankDbContext context)
        {
            var Customers = new List<BankCustomer> {
                new BankCustomer{CustomerName="Srinidhi",CustomerPhone=9087452617, CustomerAddress="Bagaluru, Bengaluru"},
                new BankCustomer{CustomerName="Nilesh",CustomerPhone=8765432156,  CustomerAddress="Nagasandra, Bengaluru"},
                 new BankCustomer{CustomerName="Kavya",CustomerPhone=8765434567,  CustomerAddress="Thane, Mumbai"}
          
            };
            Customers.ForEach(g => context.Customer.Add(g));

            context.SaveChanges();

            var Accounts = new List<BankAccount> {
                new BankAccount{CustomerId=1,AccountBalance=100000,AccountType="Savings"}, 
                new BankAccount{CustomerId=2,AccountBalance=2000000,AccountType="Savings"},
                 new BankAccount{CustomerId=3,AccountBalance=100000000,AccountType="Savings"}

            };
            Accounts.ForEach(h => context.Account.Add(h));

            context.SaveChanges();

        }
    }
}