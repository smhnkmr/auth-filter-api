using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace OpenAPI.Common
{
    public class UserValidation
    {
        public static bool Validate(string username, string password, bool testing = false)
        {
            if(testing)
            {
                return true;
            }
            var connectionString = @"Server=.\SQLEXPRESS;Database=;User Id=sa;Password=password123;";

            var query = $@"select 1 from OpenUsersTable where username = '{username}' and password = '{password}'";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    try
                    {
                        var value = Convert.ToInt32(command.ExecuteScalar());

                        return value == 1 ? true : false;
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        connection.Close();
                    }
                }
            }
        }
    }
}