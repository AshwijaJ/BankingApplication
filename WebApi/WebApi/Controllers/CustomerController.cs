using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        // GET: Bank

        DataAccess Da = new DataAccess();

        List<BankCustomer> ListCustomer = new List<BankCustomer>();


        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllCustomer()
        {
            ListCustomer = Da.GetCustomer();
            return Ok(ListCustomer);

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult CustomerAdd(BankCustomer s1)
        {
            int retValue = Da.AddCustomer(s1);
            return Ok(retValue);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult UpdateCustomer(int aid)
        {

            BankCustomer e1 = new BankCustomer();
            e1 = Da.GetCustomerbyId(aid);
            return Ok(e1);
        }

        [System.Web.Http.HttpPut]

        public IHttpActionResult UpdateCustomer(BankCustomer s1)
        {
            int result = 0;

            result = Da.CustomerUpdate(s1);

            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult RemoveCustomer(int aaid)
        {

            //BankCustomer ast = new BankCustomer();

            //using (var ctx = new BankDbContext())
            //{

            //    ast = ctx.Customer.Where(s => s.CustomerId == aaid).Single();

            //}

            //return Ok(ast);
            BankCustomer e1 = new BankCustomer();
            e1 = Da.GetCustomerbyId(aaid);
            return Ok(e1);
        }


        [System.Web.Http.HttpDelete]
        public IHttpActionResult RemoveCustomers(int aid)
        {
            int retValue = 0;
            retValue = Da.RemoveCustomer(aid);
            return Ok(retValue);
        }

        


    }
}