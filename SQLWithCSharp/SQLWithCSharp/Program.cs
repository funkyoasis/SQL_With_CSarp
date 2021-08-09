using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace SQLWithCSharp
{
	class Program
	{
		static void Main(string[] args)
		{
			var customers = new List<Customer>();
			
			using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{ 
				connection.Open();
				Console.WriteLine(connection.State);
				string sqlUpdateString = $"UPDATE CUSTOMERS SET CITY = 'Barcelona' WHERE CustomerID = 'BEARD'";

				using (var updateCustomerCommand = new SqlCommand("UpdateCustomer", connection))
				{
					updateCustomerCommand.CommandType = System.Data.CommandType.StoredProcedure;
					updateCustomerCommand.Parameters.AddWithValue("ID", "BEARD");
					updateCustomerCommand.Parameters.AddWithValue("NewName", "Eric Smith");
					int affected = updateCustomerCommand.ExecuteNonQuery();
				}


				/*
				using (var command3 = new SqlCommand(sqlUpdateString, connection))

				{
					int affected = command3.ExecuteNonQuery();
				}

				string sqlDeleteString = $"DELETE FROM CUSTOMERS WHERE CustomerID = 'BEARD'";
				using (var command4 = new SqlCommand(sqlUpdateString, connection))

				{
					int affected = command4.ExecuteNonQuery();
				}
				
				var newCustomer = new Customer()
				{
					CustomerID = "BEARD",
					ContactName = "Martin Beard",
					City = "London",
					CompanyName = "Sparta Global"

				};
				string sqlString = $"INSERT INTO CUSTOMERS(CustomerID, "+
					$"ContactName, CompanyName, City)" + 
					$"VALUES('{newCustomer.CustomerID}'," +
					$"'{newCustomer.ContactName}', '{newCustomer.CompanyName}','{newCustomer.City}')";

				using (var command2 = new SqlCommand(sqlString, connection))
				{
					int affected = command2.ExecuteNonQuery();
				}

				connection.Close();
				/*
				using (var command = new SqlCommand("Select * from customers", connection))
				{
					SqlDataReader sqlReader = command.ExecuteReader();
					while (sqlReader.Read())
					{
						var customerID = sqlReader["CustomerID"].ToString();
						var contactName = sqlReader["ContactName"].ToString();
						var companyName = sqlReader["CompanyName"].ToString();
						var city = sqlReader["City"].ToString();
						var contactTitle = sqlReader["ContactTitle"].ToString();

						var customer = new Customer()
						{
							ContactTitle = contactTitle,
							CustomerID = customerID,
							City = city,
							CompanyName = companyName

						};
						customers.Add(customer);
					}

					foreach (var c in customers)
					{
						Console.WriteLine($"Customer {c.GetFullName()} has ID {c.CustomerID} and lives in a {c.City}");
					}
					sqlReader.Close();

				}
				*/

			}

		}
	}
	public class Customer
	{
		public string CustomerID { get; set; }
		public string CompanyName { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string City { get; set; }

		public string GetFullName()
		{
			return $"{ContactTitle} - {ContactName}";
		}
	}
}
