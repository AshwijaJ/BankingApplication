using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;


namespace WebApi.Controllers
{
    public class AccountController : ApiController
    {
        
        DataAccess Da = new DataAccess();
        List<BankAccount> ListAccount = new List<BankAccount>();
        IEnumerable<AccountDetails> AccDet;
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllAccounts()
        {
            AccDet = Da.GetAccount();
            return Ok(AccDet);

        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllAccByCstId(int id)
        {
            ListAccount = Da.GetAccByCustId(id);
            return Ok(ListAccount);

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult AccountAdd(BankAccount s2)
        {
            int retValue = Da.AddAccount(s2);
            return Ok(retValue);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult UpdateAccount(int aid)
        {

            BankAccount e1 = new BankAccount();
            e1 = Da.GetAccountbyId(aid);
            return Ok(e1);
        }

        [System.Web.Http.HttpPut]

        public IHttpActionResult UpdateAccount(BankAccount s1)
        {
            int result = 0;
            result = Da.AccountUpdate(s1);
            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult RemoveAccount(int aaid)
        {

            BankAccount e1 = new BankAccount();
            e1 = Da.GetAccountbyId(aaid);
            return Ok(e1);
        }


        [System.Web.Http.HttpDelete]
        public IHttpActionResult RemoveAccounts(int aid)
        {
            int retValue = 0;
            retValue = Da.RemoveAccount(aid);
            return Ok(retValue);
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}