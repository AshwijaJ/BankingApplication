using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class DataAccess
    {

        List<BankCustomer> CustomerList = new List<BankCustomer>();
        List<BankAccount> AccountList = new List<BankAccount>();
        
        public List<BankCustomer> GetCustomer()
        {

            using (var ctx = new BankDbContext())
            {

                CustomerList = ctx.Customer.ToList();

            }
            return CustomerList;
        }

        //public List<BankAccount> GetAccount()
        //{

        //    using (var ctx = new BankDbContext())
        //    {

        //        AccountList = ctx.Account.ToList();

        //    }
        //    return AccountList;
        //}

        public IEnumerable<AccountDetails> GetAccount()
        {

            using (var ctx = new BankDbContext())
            {

                //AccountList = ctx.Account.ToList();
                var query = (from Acc in ctx.Account
                             join Cus in ctx.Customer
                             on Acc.CustomerId equals Cus.CustomerId
                             select new AccountDetails()
                             {
                                 AccountNumber = Acc.AccountNumber,
                                 CustomerId = Acc.CustomerId,
                                 AccountBalance = Acc.AccountBalance,
                                 CustomerName = Cus.CustomerName,
                                 AccountType = Acc.AccountType
                             }).ToList();
                return query;
            }
            
        }

        public int AddCustomer(BankCustomer CustoId)
        {
            int retValue = 0;
            if (CustoId != null)
            {
                try
                {
                    using (var ctx = new BankDbContext())
                    {
                        ctx.Customer.Add(CustoId);
                        retValue = ctx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return retValue;

        }

        public int AddAccount(BankAccount AccountNo)
        {
            int retValue = 0;
            if (AccountNo != null)
            {
                try
                {
                    using (var ctx = new BankDbContext())
                    {
                        ctx.Account.Add(AccountNo);
                        retValue = ctx.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return retValue;

        }

        public BankCustomer GetCustomerbyId(int id)
        {
            BankCustomer Customerslist = new BankCustomer();
            try
            {
                using (var ctx = new BankDbContext())
                {
                    Customerslist = ctx.Customer.Where(s => s.CustomerId == id).Single();
                    ctx.SaveChanges();
                    //eventslist = ctx.Events.ToList();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //throw ex;
            }

            return Customerslist;
        }

        public BankAccount GetAccountbyId(int id)
        {
            BankAccount Accountlist = new BankAccount();
            try
            {
                using (var ctx = new BankDbContext())
                {
                    Accountlist = ctx.Account.Where(s => s.AccountNumber == id).SingleOrDefault();
                    ctx.SaveChanges();
                    //eventslist = ctx.Events.ToList();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //throw ex;
            }

            return Accountlist;
        }

        public int CustomerUpdate(BankCustomer s1)
        {
            int retValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {
                    BankCustomer CustomerBank = ctx.Customer.Where(s => s.CustomerId == s1.CustomerId).Single();
                    if (CustomerBank != null)
                    {
                        CustomerBank.CustomerId = s1.CustomerId;
                        CustomerBank.CustomerName = s1.CustomerName;
                        CustomerBank.CustomerPhone = s1.CustomerPhone;
                        CustomerBank.CustomerAddress = s1.CustomerAddress;
                        ctx.Entry(CustomerBank).State = System.Data.Entity.EntityState.Modified;

                        retValue = ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //throw(ex);
            }
            return retValue;
        }

        public int AccountUpdate(BankAccount s1)
        {
            int retValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {
                    BankAccount AccountBank = ctx.Account.Where(s => s.AccountNumber == s1.AccountNumber).SingleOrDefault();
                    if (AccountBank != null)
                    {
                        AccountBank.CustomerId = s1.CustomerId;
                        AccountBank.AccountNumber = s1.AccountNumber;
                        AccountBank.AccountBalance = s1.AccountBalance;
                        AccountBank.AccountType = s1.AccountType;
                        //AccountBank.TransactionTime = s1.TransactionTime;
                        ctx.Entry(AccountBank).State = System.Data.Entity.EntityState.Modified;

                        retValue = ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public int AmmountWithdrawal(BankAccount WithdrawalAccount)
        {
            int retValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {
                    
                    BankAccount AccountBank = ctx.Account.Where(s => s.AccountNumber == WithdrawalAccount.AccountNumber).SingleOrDefault();
                    AccountBank.AccountBalance -= WithdrawalAccount.WithdrawAmount;
                    ctx.Entry(AccountBank).State = System.Data.Entity.EntityState.Modified;
                    retValue = ctx.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public int RemoveCustomer(int s1)
        {
            int retValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {
                    BankCustomer std = ctx.Customer.Find(s1);

                    ctx.Entry(std).State = System.Data.Entity.EntityState.Deleted;

                    retValue = ctx.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public int RemoveAccount(int s1)
        {
            int retValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {
                    BankAccount std = ctx.Account.Find(s1);

                    ctx.Entry(std).State = System.Data.Entity.EntityState.Deleted;

                    retValue = ctx.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public List<BankAccount> GetAccByCustId(int id)
        {
            List<BankAccount> Accountlist = new List<BankAccount>();
            try
            {
                using (var ctx = new BankDbContext())
                {
                    Accountlist = ctx.Account.Where(s => s.CustomerId == id).ToList();
                    ctx.SaveChanges();
                    //eventslist = ctx.Events.ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Accountlist;
        }

        public int pinATM(int acc, int id)
        {
            BankAccount AccBank = new BankAccount();
            int ReturnValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {
                    AccBank = ctx.Account.Where(s => s.AccountNumber == acc && s.ATMpin == id).SingleOrDefault();
                    //ctx.Entry(AccBank).State = System.Data.Entity.EntityState.Unchanged;
                    //ctx.Account.Where(s => s.ATMpin == id).Single();
                    //ReturnValue = ctx.SaveChanges();
                    if (AccBank !=null)
                    {
                        
                        ReturnValue = 1;
                    }
                    else
                    {
                        ReturnValue = 0;
                    }

                    //eventslist = ctx.Events.ToList();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //throw ex;
            }

            return ReturnValue;
        }
    }
}