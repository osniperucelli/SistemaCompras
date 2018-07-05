using System;
using System.Data;
using System.Data.SqlClient;

namespace PersistenceProject
{
    public class DatabaseConnection
    {
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        private string connectionString;

        public DatabaseConnection()
        {
            connectionString = "Data Source=VAIO-VAIO;Initial Catalog=SistemaCompras;Integrated Security=true;";
        }

        public SqlConnection GetConnection()
        {
            conn = new SqlConnection();
            adapter = new SqlDataAdapter();
            conn.ConnectionString = connectionString;
            conn.Open();

            return conn;
        }

        public DataTable ExecuteSelectQuery(String query)
        {
            return ExecuteSelectQuery(query, null);
        }

        public int ExecuteNonQuery(String query)
        {
            return ExecuteNonQuery(query, null);
        }

        public int ExecuteProcedure(String query)
        {
            return ExecuteProcedure(query, null);
        }

        public int ExecuteProcedure(String query, SqlParameter[] sqlParameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            using (SqlConnection connection = GetConnection())
            {
                string transactionName = ("Alunos_Trans_" + Guid.NewGuid().ToString()).Substring(0, 32);
                SqlTransaction sqlTransaction = connection.BeginTransaction(transactionName);

                try
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.Transaction = sqlTransaction;
                    sqlCommand.CommandText = query;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    if (sqlParameter != null)
                    {
                        sqlCommand.Parameters.AddRange(sqlParameter);
                    }

                    var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    sqlCommand.ExecuteNonQuery();

                    var result = returnParameter.Value;
                    returnValue = (int)result;
                    sqlTransaction.Commit();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Commit Exception Type: {0}", e.GetType());
                    Console.WriteLine("  Message: {0}", e.Message);
                    System.Diagnostics.Trace.WriteLine(e.Message);

                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);
                        System.Diagnostics.Trace.WriteLine(e.Message);
                    }

                    return 0;
                }
                finally
                {
                }
            }

            return returnValue;
        }

        public DataTable ExecuteSelectQuery(String query, SqlParameter[] sqlParameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = query;

                    if (sqlParameter != null)
                    {
                        //sqlCommand.Parameters.AddRange(sqlParameter);
                        foreach (SqlParameter p in sqlParameter)
                        {
                            sqlCommand.Parameters.AddWithValue(p.ParameterName, p.SqlDbType).Value = p.Value;
                        }
                    }

                    sqlCommand.ExecuteNonQuery();
                    adapter.SelectCommand = sqlCommand;
                    adapter.Fill(ds);
                    dataTable = ds.Tables[0];
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Commit Exception Type: {0}", e.GetType());
                Console.WriteLine("  Message: {0}", e.Message);
                //TODO do something better than this
                System.Diagnostics.Trace.WriteLine(e.Message);

                return dataTable;
            }
            finally
            {
            }

            return dataTable;
        }

        public int ExecuteNonQuery(String query, SqlParameter[] sqlParameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            int rows = 0;

            using (SqlConnection connection = GetConnection())
            {
                string transactionName = ("Alunos_Trans_" + Guid.NewGuid().ToString()).Substring(0, 32);
                SqlTransaction sqlTransaction = connection.BeginTransaction(transactionName);

                try
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.Transaction = sqlTransaction;
                    sqlCommand.CommandText = query;

                    if (sqlParameter != null)
                    {
                        sqlCommand.Parameters.AddRange(sqlParameter);
                    }

                    rows = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Commit Exception Type: {0}", e.GetType());
                    Console.WriteLine("  Message: {0}", e.Message);
                    System.Diagnostics.Trace.WriteLine(e.Message);

                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);
                        System.Diagnostics.Trace.WriteLine(e.Message);
                    }

                    return 0;
                }
                finally
                {
                }
            }

            return rows;
        }

        public int ExecuteInsert(string query, SqlParameter[] sqlParameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            int id = 0;

            using (SqlConnection connection = GetConnection())
            {
                string transactionName = ("Compras_Trans_" + Guid.NewGuid().ToString()).Substring(0, 32);
                SqlTransaction sqlTransaction = connection.BeginTransaction(transactionName);

                try
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.Transaction = sqlTransaction;
                    sqlCommand.CommandText = query;

                    if (sqlParameter != null)
                    {
                        sqlCommand.Parameters.AddRange(sqlParameter);
                    }

                    id = (int)(decimal)sqlCommand.ExecuteScalar();
                    sqlTransaction.Commit();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Commit Exception Type: {0}", e.GetType());
                    Console.WriteLine("  Message: {0}", e.Message);
                    System.Diagnostics.Trace.WriteLine(e.Message);

                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);
                        System.Diagnostics.Trace.WriteLine(e.Message);
                    }

                    return 0;
                }
                finally
                {
                }
            }

            return id;
        }
    }
}
