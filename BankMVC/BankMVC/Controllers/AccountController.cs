using BankMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace BankMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Customer
        AccountClass ac = new AccountClass();
        IEnumerable<AccountDetails> AccDetail;
        [HttpGet]
        //actionresult basically a return type 
        public ActionResult GetAccounts()
        {
            //list which is initialised to store the data 
            List<BankAccount> AccountList = new List<BankAccount>();
            AccDetail = ac.GetAccount();
            //all the data is viewed in the View using the cshtml and css file 
            return View(AccDetail);
        }

        [HttpGet]
        //actionresult basically a return type 
        public ActionResult GetAllAccByCstId(int id)
        {
            //list which is initialised to store the data 
            List<BankAccount> AccountList = new List<BankAccount>();
            AccountList = ac.GetAllAccByCstId(id);
            //all the data is viewed in the View using the cshtml and css file 
            return View(AccountList);
        }

        [HttpGet]
        public ActionResult AddAccount(int id)
        {
            //create an empty object
            //List<string> AccTypeList = new List<string> { "Savings", "Current", "Salary" };
            //var AccListType = new SelectList(AccTypeList);
            //ViewBag.AccountList = AccTypeList;
            BankAccount ast = new BankAccount();
            ast.CustomerId = id;
            //sending the object to the view
            return View(ast);
        }

        //post operation of the object after fetching the data to be added 
        [HttpPost]
        public ActionResult AddAccount(BankAccount ev)
        {
            if (ModelState.IsValid)
            {
                
                ac.AddAccount(ev);
                //returns to the all data list 
                return RedirectToAction("GetAccounts");

            }
            else
            {
                //if not returns the view 
                return View();
            }

        }

        [HttpGet]
        public ActionResult UpdateAccount(int id)
        {

            BankAccount AccUpdate = new BankAccount();
            AccUpdate = ac.AccountUpdate(id);
            return View(AccUpdate);
        }

        [HttpPost]
        //specifies the event to take place after the getting of the data 
        public ActionResult UpdateAccount(BankAccount ev)
        {
            //checks if the data entered is valid or not 
            if (ModelState.IsValid)
            {
                ac.UpdateAccounts(ev);
                return RedirectToAction("GetAccounts");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult RemoveAccount(int id)
        {
            BankAccount AccountRemove = new BankAccount();
            AccountRemove = ac.RemoveAct(id);
            return View(AccountRemove);
        }

        //used to delete the items from the list when ID is matched in the list 
        [HttpPost, ActionName("RemoveAccount")]
        public ActionResult RemoveAccounts(int id)
        {
            TempData["SuccessMessage"] = "Successfull";
            //list is initialise 
            ac.RemoveActs(id);
            return RedirectToAction("GetAccounts");
        }

        [HttpGet]
        //actionresult basically a return type 
        public ActionResult WithdrawlGetAllAccByCstId(int id)
        {
            //list which is initialised to store the data 
            BankAccount AccountList = new BankAccount();
            AccountList = ac.AccountUpdate(id);
            //all the data is viewed in the View using the cshtml and css file.
            return View(AccountList);
        }

        [HttpPost]
        public ActionResult WithdrawlGetAllAccByCstId(BankAccount amt)
        {
            //checks if the data entered is valid or not 
            if (ModelState.IsValid)
            {
                ac.AmountWithdraw(amt);
                return RedirectToAction("GetAccounts");
            }
            else
            {
                return View();
            }

        }


        [HttpGet]
        public ActionResult GetATMpin(int id)
        {
            BankAccount AccUpdate = new BankAccount();
            AccUpdate = ac.AccountUpdate(id);
            AccUpdate.ATMpin = 0;
            return View(AccUpdate);
        }

        [HttpPost]
        public ActionResult GetATMpin(BankAccount AccBank)
        {
            int retvalue = 0;
            retvalue = ac.ATMpindel(AccBank);
            if(retvalue==1)
            {
            return RedirectToAction("WithdrawlGetAllAccByCstId", "Account", new { @id = AccBank.AccountNumber });
            }
            else
            {
                TempData["ErrorMessage"] = "Incorrect pin";
                //FlashMessage.Equals("You have entered incorrect pin");
                return RedirectToAction("GetATMpin");
                //alertify.error("Entered Wrong pin");
                //return RedirectToAction("GetATMpin", "Account", new AlertifyMessageModel { Message = "Lorem ipsum" });
            }
        }

        [HttpGet]
        public ActionResult TransferAmount()
        {
            Transfer ast = new Transfer();
            return View(ast);
        }
        [HttpPost]
        public ActionResult TransferAmount(Transfer AmountTransfer)
        {
            ac.Transaction(AmountTransfer);
            return RedirectToAction("GetAccounts");
        }


    }
}