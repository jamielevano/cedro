using System;
using DatabaseContext;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class General
    {
        public DataTable ExecuteStoredProcedure(DbContext db, string storedProcedureName, SqlParameter[] parameters)
        {
            var connectionString = db.Database.Connection.ConnectionString;
            var ds = new DataSet();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 500;
                    cmd.CommandText = storedProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                       adapter.Fill(ds);
                    }
                }
            }

            return ds.Tables[0];
        }

        public DataTable ExecuteStoredProcedure(DbContext db, string storedProcedureName)
        {
            var connectionString = db.Database.Connection.ConnectionString;
            var ds = new DataSet();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 500;
                    cmd.CommandText = storedProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds.Tables[0];
        }


        public void ExecuteStoredProcedure_Single(DbContext db, string storedProcedureName)
        {
            var connectionString = db.Database.Connection.ConnectionString;
            
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 500;
                    cmd.CommandText = storedProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteScalar();
                    
                }
            }
        }

        public string ExecuteStoredProcedure_Single(DbContext db, string storedProcedureName, SqlParameter[] parameters)
        {
            var connectionString = db.Database.Connection.ConnectionString;
            string returnvalue = string.Empty; 

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 500;
                    cmd.CommandText = storedProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }

                    returnvalue = cmd.ExecuteScalar().ToString();

                }
            }

            return returnvalue;
        }


    }
}
