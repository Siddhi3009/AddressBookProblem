using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookProblem;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// Test method to check the contact list retrieved from json server
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
        /// <summary>
        /// Test method to check multiple contacts added to json server
        /// </summary>
        [TestMethod]
        public void GivenMultipleContacts_WhenPosted_ShouldReturnContactListWithAddedContacts()
        {
            //arrange
            List<Contact> list = new List<Contact>();
            list.Add(new Contact { name = "John", Address = "California" });
            list.Add(new Contact { name = "Divya", Address = "Allahabad" });
            foreach (Contact contact in list)
            {
                //act
                RestRequest request = new RestRequest("/Address/create", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("Name", contact.name);
                jObject.Add("Address", contact.Address);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                Contact dataResponse = JsonConvert.DeserializeObject<Contact>(response.Content);
                Assert.AreEqual(contact.name, dataResponse.name);
                Assert.AreEqual(contact.Address, dataResponse.Address);
            }
        }
    }
}
