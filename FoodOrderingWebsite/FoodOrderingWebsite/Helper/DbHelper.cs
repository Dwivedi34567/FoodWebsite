using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace FoodOrderingWebsite.Helper
{
    public class DbHelper
    {
        private readonly string _connectionString;

        /// <summary>
        ///  Private constructor to enforce the Singleton pattern
        /// </summary>
        /// <param name="configuration"></param>
        public DbHelper(IConfiguration configuration)
        {
            // Initialize the connection string from configuration
            _connectionString = configuration.GetConnectionString("FoodDB");
        }

        /// <summary>
        /// Method to execute a stored procedure and return a DataTable
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>Return result in the form of DataTable</returns>
        public DataTable ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            foreach (var kvp in parameters)
                            {
                                command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                            }
                        }
                        connection.Open();
                        DataTable results = new DataTable();
                        SqlDataReader reader = command.ExecuteReader();
                        results.Load(reader);
                        return results;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                throw new Exception("Error on executing stored Procedure" + ex.Message, ex);
            }
        }
    }
}
