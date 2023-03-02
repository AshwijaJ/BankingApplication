using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace BankMVC.Models
{
    public class AccountClass
    {
        
        public List<AccountDetails> GetAccount()
        {
            List<AccountDetails> AccountList = new List<AccountDetails>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                //var url = ConfigurationManager.AppSettings["BaseAddress"];
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var path = ConfigurationManager.AppSettings["GetCustomer"];
                var responseTask = client.GetAsync("Account/GetAllAccounts");
                //var responseTask = client.GetAsync(ConfigurationManager.AppSettings["GetCustomer"]);
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<List<AccountDetails>>();
                    readTask.Wait();
                    AccountList = readTask.Result;


                }

            }
            return AccountList;

        }

        public List<BankAccount> GetAllAccByCstId(int id)
        {
            List<BankAccount> AccountList = new List<BankAccount>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                //var url = ConfigurationManager.AppSettings["BaseAddress"];
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseTask = client.GetAsync(String.Format("Account/GetAllAccByCstId?id=") + id);
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<List<BankAccount>>();
                    readTask.Wait();
                    AccountList = readTask.Result;
                }

            }
            return AccountList;

        }

        public void AddAccount(BankAccount ev)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //helps in returning the task that will yield an object of that type 
                var responseTask = client.PostAsJsonAsync(String.Format("Account/AccountAdd"), ev);
                //reponse is waited to be got 
                responseTask.Wait();
                //returning a message/data from your action
                HttpResponseMessage response = responseTask.Result;

            }
        }


        public BankAccount AccountUpdate(int id)
        {
            //empty object is initialised for the id passed to be got with the data from the webAPI response 
            BankAccount AccountUpdate = new BankAccount();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //gets the data of the particular ID and all its information that can be editted 
                var EditresponseTask = client.GetAsync(String.Format("Account/UpdateAccount?aid=") + id);
                //var EditresponseTask = client.GetAsync(String.Format("Employee/UpdateEmp?aid=") + id);
                //var responseTask = client.GetAsync("Review/GetTaskReviewbytheirID?ToDoId=" + ToDoId.ToString());
                EditresponseTask.Wait();

                HttpResponseMessage response = EditresponseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<BankAccount>();
                    readTask.Wait();
                    AccountUpdate = readTask.Result;
                }
            }
            return AccountUpdate;
        }

        public void UpdateAccounts(BankAccount ev)
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Send a PUT request to the specified Uri containing the value serialized as JSON in the request body.
                var responseTask = client.PutAsJsonAsync(String.Format("Account/UpdateAccount/"), ev);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
            }

        }

        public BankAccount RemoveAct(int id)
        {
            //fetchs the data of the particular item/ asset that needs to be deleted 
            BankAccount AccountRemove = new BankAccount();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //getasync helps in fetching the data from the list in the database to be deleted 
                var responseTask = client.GetAsync("Account/RemoveAccount/?aaid=" + id);
                //var responseTask = client.GetAsync("Review/GetTaskReviewbytheirID?ToDoId=" + ToDoId.ToString());
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<BankAccount>();
                    readTask.Wait();
                    AccountRemove = readTask.Result;
                }
            }
            return AccountRemove;
        }

        public void RemoveActs(int id)
        {
            //list is initialise 


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Send a DELETE request to the specified Uri as an asynchronous operation.
                var responseTask = client.DeleteAsync(String.Format("Account/RemoveAccounts/?aid=" + id));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

            }

        }

        public void AmountWithdraw(BankAccount ev)
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Send a PUT request to the specified Uri containing the value serialized as JSON in the request body.
                var responseTask = client.PutAsJsonAsync(String.Format("Account/Withdrawal/"), ev);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
            }

        }

        public int ATMpindel(BankAccount ev)
        {
            int ReturnValue = 0;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Send a PUT request to the specified Uri containing the value serialized as JSON in the request body.
                var responseTask = client.PutAsJsonAsync(String.Format("Account/ATMpin/"), ev);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<int>();
                    readTask.Wait();
                    ReturnValue = readTask.Result;
                }
            }
            return ReturnValue;
        }

        public void Transaction(Transfer AmtTrans)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //helps in returning the task that will yield an object of that type 
                var responseTask = client.PostAsJsonAsync(String.Format("Account/TransferAmount"), AmtTrans);
                //reponse is waited to be got 
                responseTask.Wait();
                //returning a message/data from your action
                HttpResponseMessage response = responseTask.Result;

            }
        }

    }
}