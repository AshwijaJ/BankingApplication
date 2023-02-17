using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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
                        AccountBank.ATMpin = s1.ATMpin;
                        AccountBank.WithdrawAmount = s1.WithdrawAmount;
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
        public int AmountTransfer(Transfer AccTransfer)
        {
            BankAccount Acc1 = new BankAccount();
            BankAccount Acc2 = new BankAccount();

            int retValue = 0;
            try
            {
                using (var ctx = new BankDbContext())
                {

                    Acc1 = ctx.Account.Find(AccTransfer.AccountNumber);
                    Acc1.AccountBalance -= AccTransfer.amount;
                    ctx.Entry(Acc1).State = System.Data.Entity.EntityState.Modified;

                    Acc2 = ctx.Account.Find(AccTransfer.ToAccountNumber);
                    Acc2.AccountBalance += AccTransfer.amount;
                    ctx.Entry(Acc2).State = System.Data.Entity.EntityState.Modified;

                    retValue = ctx.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public bool CreateXml(List<BankCustomer> CustDetails)
        {
            try
            {
                using (var ctx = new BankDbContext())
                {

                    XDocument XmlDoc = new XDocument(
                         new XElement("Bank", from customer in CustDetails
                                                          select new XElement("Customer", 
                                                          new XAttribute("CustomerId", customer.CustomerId), 
                                                          new XAttribute("CustomerName", customer.CustomerName), 
                                                          new XAttribute("CustomerPhone", customer.CustomerPhone), 
                                                          new XAttribute("CustomerAddress", customer.CustomerAddress)
                                                            //from account in AccDetails
                                                            //select new XElement("Account",
                                                            // new XAttribute("AccountNumber", account.AccountNumber),
                                                            //new XAttribute("AccountBalance", account.AccountBalance),
                                                            //new XAttribute("AccountType", account.AccountType),
                                                            //new XAttribute("WithdrawAmount", account.WithdrawAmount),
                                                            // new XAttribute("ATMpin", account.ATMpin)


                          )));
                    Console.WriteLine(XmlDoc);
                    XmlDoc.Save(@"D:\NewRepo\bank.xml");
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}