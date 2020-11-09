using NUnit.Framework;
using AddressBookProblem;
using System;

namespace AddressBookTest
{
    public class Tests
    {
        AddressRepo repo = new AddressRepo();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenNewContactDetails_WhenUpdated_ShouldSyncWithDataBase()
        {
            Contact contact = new Contact()
            {
                FirstName = "Siddhi",
                LastName = "Seth",
                PhoneNumber = "951527534",
                Email = "siddhi@gmail.com"
            };
            bool result = repo.UpdateContact(contact);
            Assert.AreEqual(true, result);
        }
        [Test]
        public void GivenADateRange_WhenAnalysed_ShouldDeleteContactsAddedInThatRange()
        {
            int expected = 1;
            int actual = repo.DeleteContactsAddedInADateRange("2020-01-01", "2020-12-31");
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GivenContactInformation_WhenAdded_ShouldBeAddedInTheDB()
        {
            Contact contact = new Contact()
            {
                FirstName = "Siddhi",
                LastName = "Seth",
                PhoneNumber = "951527534",
                Email = "siddhi@gmail.com",
                Address = "Street 6",
                City = "Bangalore",
                State = "Karnataka",
                DateAdded = Convert.ToDateTime("08-06-2020"),
                RelationType = "Friend",
                Zipcode = "852456"
            };
            bool result = repo.AddContact(contact);
            Assert.AreEqual(true, result);
        }
    }
}