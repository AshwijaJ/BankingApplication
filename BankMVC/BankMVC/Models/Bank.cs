using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankMVC.Models
{
    public class BankCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Fill the Name space")]
        public string CustomerName { get; set; }
        //[Phone]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid length of phone number")]
        public long CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }

        //public int AccountNumber { get; set; }
        public ICollection<BankAccount> AccountNumber { get; set; }

    }

    public class BankAccount
    {
        [Key]
        public int AccountNumber { get; set; }

        [Required(ErrorMessage = "Enter correct account balance")]
        [Range(1, 100000)]
        public long AccountBalance { get; set; }
      
        public string AccountType { get; set; }
        
        public long WithdrawAmount { get; set; }
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Pin should be of 4 digits")]
        public int ATMpin { get; set; }

        [ForeignKey("Cust")]
        public int CustomerId { get; set; }

        public BankCustomer Cust { get; set; }
        

    }
    public class AccountDetails
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AccountNumber { get; set; }
        public long AccountBalance { get; set; }
        public string AccountType { get; set; }

    }

    public class Transfer
    {
        [Key]
        public int TransactionId { get; set; }
        public int ToAccountNumber { get; set; }
        public DateTime TransactionTime { get; set; }

        public long amount { get; set; }

        [ForeignKey("Acc")]
        public int AccountNumber { get; set; }
        public BankAccount Acc { get; set; }
    }

}