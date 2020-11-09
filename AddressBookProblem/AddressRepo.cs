using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookProblem
{
    class AddressRepo
    {
        public static string connectionString = @"Data Source=DESKTOP-6S6I6GO\SQLEXPRESS;Initial Catalog=Address_Book_Service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllContacts()
        {
            try
            {
                Contact contact = new Contact();
                using (this.connection)
                {
                    string query = @"Select * from (Contact_Info ci inner join Contact_Type ct on ci.ContactId = ct.ContactId) inner join Address ad on ci.ContactId = ad.ContactId;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            contact.FirstName = !dr.IsDBNull(1)? dr.GetString(1): "";
                            contact.LastName = !dr.IsDBNull(2)? dr.GetString(2): "";
                            contact.PhoneNumber = !dr.IsDBNull(3)? dr.GetString(3): "";
                            contact.Email = !dr.IsDBNull(4)? dr.GetString(4): "";
                            contact.RelationType = !dr.IsDBNull(6)? dr.GetString(6): "";
                            contact.Address = !dr.IsDBNull(8)? dr.GetString(8): "";
                            contact.City = !dr.IsDBNull(9)? dr.GetString(9): "";
                            contact.State = !dr.IsDBNull(10)? dr.GetString(10): "";
                            contact.Zipcode = !dr.IsDBNull(11)? dr.GetString(11): "";
                            System.Console.WriteLine(contact.FirstName + "," + contact.LastName + "," + contact.PhoneNumber + "," + contact.Email + "," + contact.RelationType + "," + contact.Address + "," + contact.City +
                                "," + contact.State + "," + contact.Zipcode);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
