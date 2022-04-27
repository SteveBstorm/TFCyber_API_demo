using GestionContact_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact_DAL.Services
{
    public class ContactService
    {
        private string _connectionString = @"Data Source=DESKTOP-56GOFPS\DEVPERSO;Initial Catalog=CyberContactDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public IEnumerable<Contact> GetAll()
        {
            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;

                using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Contact";
                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Contact { 
                                Id = (int)reader["Id"],
                                Lastname = (string)reader["Lastname"],
                                Firstname = (string)reader["Firstname"],
                                Email = (string)reader["Email"],
                                Telephone = (string)reader["Telephone"]
                            };
                        }
                    }
                }
            }
        }

        public Contact GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Contact WHERE Id = @id";
                    command.Parameters.AddWithValue("id", Id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Contact
                            {
                                Id = (int)reader["Id"],
                                Lastname = (string)reader["Lastname"],
                                Firstname = (string)reader["Firstname"],
                                Email = (string)reader["Email"],
                                Telephone = (string)reader["Telephone"]
                            };
                        }
                        else throw new Exception("Contact non trouvé");
                    }
                }
            }
        }

        public bool Create(Contact c)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Contact (Lastname, Firstname, Email, Telephone) VALUES (@ln, @fn, @mail, @tel)";
                    command.Parameters.AddWithValue("ln", c.Lastname);
                    command.Parameters.AddWithValue("fn", c.Firstname);
                    command.Parameters.AddWithValue("mail", c.Email);
                    command.Parameters.AddWithValue("tel", c.Telephone);

                    int response = command.ExecuteNonQuery();
                    connection.Close();
                    return response == 1;
                }
                
            }
        }
        public bool Update(Contact c)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Contact SET Lastname = @ln, Firstname = @fn, Email = @mail, Telephone = @tel WHERE Id = @id";
                    command.Parameters.AddWithValue("ln", c.Lastname);
                    command.Parameters.AddWithValue("fn", c.Firstname);
                    command.Parameters.AddWithValue("mail", c.Email);
                    command.Parameters.AddWithValue("tel", c.Telephone);
                    command.Parameters.AddWithValue("id", c.Id);

                    int response = command.ExecuteNonQuery();
                    connection.Close();
                    return response == 1;
                }
            }
        }

        public bool Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Contact WHERE Id = @id";
                    
                    command.Parameters.AddWithValue("id", Id);

                    int response = command.ExecuteNonQuery();
                    connection.Close();
                    return response == 1;
                }
            }
        }
    }
}
