using BankMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        CustomerClass c = new CustomerClass();

        public ActionResult Index()
        {
            //used in the view to actually define the Title property.
            ViewBag.Title = "FED Bank";

            return View();
        }

        [HttpGet]
        //actionresult basically a return type 
        public ActionResult GetCustomer()
        {
            //list which is initialised to store the data 
            List<BankCustomer> CustomerList = new List<BankCustomer>();
            CustomerList = c.GetCustomer();
            //all the data is viewed in the View using the cshtml and css file 
            return View(CustomerList);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            //create an empty object
            BankCustomer ast = new BankCustomer();

            //sending the object to the view
            return View(ast);
        }

        //post operation of the object after fetching the data to be added 
        [HttpPost]
        public ActionResult AddCustomer(BankCustomer ev)
        {
            if (ModelState.IsValid)
            {
                //check if the entered data is valid as specified in the object class in the models 
                c.CustomerAdd(ev);
                //returns to the all data list 
                return RedirectToAction("GetCustomer");

            }
            else
            {
                //if not returns the view 
                return View();
            }

        }

        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            
            BankCustomer CustomerUpdate = new BankCustomer();
            CustomerUpdate = c.UpdateCustomer(id);
            return View(CustomerUpdate);
        }



        [HttpPost]
        //specifies the event to take place after the getting of the data 
        public ActionResult UpdateCustomer(BankCustomer ev)
        {
            //checks if the data entered is valid or not 
            if (ModelState.IsValid)
            {
                c.UpdateCustomers(ev);
                return RedirectToAction("GetCustomer");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult RemoveCustomer(int id)
        {
            BankCustomer CustomerRemove = new BankCustomer();
            CustomerRemove = c.RemoveCust(id);
            return View(CustomerRemove);
        }

        //used to delete the items from the list when ID is matched in the list 
        [HttpPost, ActionName("RemoveCustomer")]
        public ActionResult RemoveCustomers(int id)
        {
            TempData["SuccessMessage"] = "Successfull";
            //list is initialise 
            c.RemoveCusts(id);
            return RedirectToAction("GetCustomer");
        }
    }

}