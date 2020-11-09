using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookProblem
{
    public class AddressRepo
    {
        public static string connectionString = @"Data Source=DESKTOP-6S6I6GO\SQLEXPRESS;Initial Catalog=Address_Book_Service;Integrated Security=True";
        /// <summary>
        /// Retrieves all contacts
        /// </summary>
        public void GetAllContacts()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                Contact contact = new Contact();
                using (connection)
                {
                    string query = @"Select * from (Contact_Info ci inner join Contact_Type ct on ci.ContactId = ct.ContactId) inner join Address ad on ci.ContactId = ad.ContactId;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            contact.FirstName = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                            contact.LastName = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            contact.PhoneNumber = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                            contact.Email = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                            contact.DateAdded = !dr.IsDBNull(5) ? dr.GetDateTime(5) : Convert.ToDateTime("01/01/0001");
                            contact.RelationType = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                            contact.Address = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                            contact.City = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                            contact.State = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                            contact.Zipcode = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                            Console.WriteLine(contact.FirstName + "," + contact.LastName + "," + contact.PhoneNumber + "," + contact.Email + "," + contact.DateAdded + "," + contact.RelationType + "," + contact.Address + "," + contact.City +
                                "," + contact.State + "," + contact.Zipcode);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Updates phone number and email address of a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>true if contact updated successfully else false</returns>
        public bool UpdateContact(Contact contact)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"Update Contact_Info set PhoneNumber = '" + contact.PhoneNumber + "', Email = '" + contact.Email + 
                        "' where FirstName = '" + contact.FirstName + "' and LastName = '" + contact.LastName + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        /// <summary>
        /// Deletes contact added in the given date range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>number of contacts deleted</returns>
        public int DeleteContactsAddedInADateRange(string startDate, string endDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            int contactsDeleted = 0;
            try
            {
                using (connection)
                {
                    string query = @"delete from Contact_Info where DateAdded between '" + startDate + "' and '" + endDate + "';";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    contactsDeleted = cmd.ExecuteNonQuery();
                    if (contactsDeleted > 0)
                    {
                        Console.WriteLine(contactsDeleted + " contacts affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally 
            {
                connection.Close();
            }
            return contactsDeleted;
        }
        /// <summary>
        /// Retrieves contacts by city and state
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        public void RetrieveContactByCityOrState(string city, string state)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"select * from (Contact_Info ci inner join Contact_Type ct on ci.ContactId = ct.ContactId)" +
                        "inner join Address ad on ad.ContactId = ci.ContactId where ad.City = '" + city + "' and ad.State = '" + state + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Contact contact = new Contact();
                            contact.FirstName = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                            contact.LastName = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            contact.PhoneNumber = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                            contact.Email = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                            contact.DateAdded = !dr.IsDBNull(5) ? dr.GetDateTime(5) : Convert.ToDateTime("01/01/0001");
                            contact.RelationType = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                            contact.Address = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                            contact.City = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                            contact.State = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                            contact.Zipcode = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                            Console.WriteLine(contact.FirstName + "," + contact.LastName + "," + contact.PhoneNumber + "," + contact.Email + "," + contact.DateAdded + "," + contact.RelationType + "," + contact.Address + "," + contact.City +
                                "," + contact.State + "," + contact.Zipcode);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Adds New Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>True if contact added successfully else false</returns>
        public bool AddContact(Contact contact)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpAddContactDetails", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", contact.Email);
                    command.Parameters.AddWithValue("@DateAdded", contact.DateAdded);
                    command.Parameters.AddWithValue("@Contact_Type", contact.RelationType);
                    command.Parameters.AddWithValue("@Address", contact.Address);
                    command.Parameters.AddWithValue("@City", contact.City);
                    command.Parameters.AddWithValue("@State", contact.State);
                    command.Parameters.AddWithValue("@Zipcode", contact.Zipcode);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
