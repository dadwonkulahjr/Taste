using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private static string ConnectionStrings = "";
        public SP_Call(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            ConnectionStrings = _applicationDbContext.Database.GetDbConnection().ConnectionString;
        }

        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            {
                conn.Open();
                return conn.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void ExecuteWithOutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            {
                conn.Open();
                conn.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            {
                conn.Open();
                return (T)Convert.ChangeType(conn.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
      
    }
}
