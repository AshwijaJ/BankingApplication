﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class BankCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Fill the Name space")]
        public string CustomerName { get; set; }
        //[Phone]
        [RegularExpression(@"^(\d{10})$", ErrorMessage ="Invalid length of phone number")]
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
        //public DateTime TransactionTime { get; set; }

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


}