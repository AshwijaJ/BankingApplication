using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace BankMVC.Models
{
    public class CustomerClass
    {
        public List<BankCustomer> GetCustomer()
        {
            List<BankCustomer> CustomerList = new List<BankCustomer>();

            
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseTask = client.GetAsync("Customer/GetAllCustomer");
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<List<BankCustomer>>();
                    readTask.Wait();
                    CustomerList = readTask.Result;


                }

            }
            return CustomerList;

        }

        public void CustomerAdd(BankCustomer ev)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //helps in returning the task that will yield an object of that type 
                var responseTask = client.PostAsJsonAsync(String.Format("Customer/CustomerAdd"), ev);
                //reponse is waited to be got 
                responseTask.Wait();
                //returning a message/data from your action
                HttpResponseMessage response = responseTask.Result;

            }
        }
       

        public BankCustomer UpdateCustomer(int id)
        {
            //empty object is initialised for the id passed to be got with the data from the webAPI response 
            BankCustomer CustomerUpdate = new BankCustomer();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //gets the data of the particular ID and all its information that can be editted 
                var EditresponseTask = client.GetAsync(String.Format("Customer/UpdateCustomer?aid=") + id);
                //var EditresponseTask = client.GetAsync(String.Format("Employee/UpdateEmp?aid=") + id);
                //var responseTask = client.GetAsync("Review/GetTaskReviewbytheirID?ToDoId=" + ToDoId.ToString());
                EditresponseTask.Wait();

                HttpResponseMessage response = EditresponseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<BankCustomer>();
                    readTask.Wait();
                    CustomerUpdate = readTask.Result;
                }
            }
            return CustomerUpdate;
        }

        public void UpdateCustomers(BankCustomer ev)
        {
           
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Send a PUT request to the specified Uri containing the value serialized as JSON in the request body.
                var responseTask = client.PutAsJsonAsync(String.Format("Customer/UpdateCustomer/"), ev);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
            }

        }

        public BankCustomer RemoveCust(int id)
        {
            //fetchs the data of the particular item/ asset that needs to be deleted 
            BankCustomer CustomerRemove = new BankCustomer();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //getasync helps in fetching the data from the list in the database to be deleted 
                var responseTask = client.GetAsync("Customer/RemoveCustomer/?aaid=" + id);
                //var responseTask = client.GetAsync("Review/GetTaskReviewbytheirID?ToDoId=" + ToDoId.ToString());
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object
                    var readTask = response.Content.ReadAsAsync<BankCustomer>();
                    readTask.Wait();
                    CustomerRemove = readTask.Result;
                }
            }
            return CustomerRemove;
        }

        public void RemoveCusts(int id)
        {
            //list is initialise 


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8044/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Send a DELETE request to the specified Uri as an asynchronous operation.
                var responseTask = client.DeleteAsync(String.Format("Customer/RemoveCustomers/?aid=" + id));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

            }
        }
    }
}