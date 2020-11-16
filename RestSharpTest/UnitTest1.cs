using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookProblem;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestSharpTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client = new RestClient("http://localhost:3000");
        /// <summary>
        /// Interface to get list of contacts in the json server
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetEmployeeList()
        {
            RestRequest request = new RestRequest("/Address", Method.GET);
            //act
            IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// Test method to check the employee list retrieved from json server
        /// </summary>
        [TestMethod]
        public void OnCallingGetApi_ReturnAddressList()
        {
            IRestResponse response = GetEmployeeList();
            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Contact> dataResponse = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(2, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + " Name: " + item.name + " Address: " + item.Address);
            }
        }
    }
}
