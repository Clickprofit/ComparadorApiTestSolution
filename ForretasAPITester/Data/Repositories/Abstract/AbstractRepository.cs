using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ForretasAPITester.Data.Repositories.Abstract
{
    public abstract class AbstractRepository<T>
    {
        protected IConfiguration Configuration { get; set; }
        protected string ConnectionString = string.Empty;

        public AbstractRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = GetConnectionString();
        }

        protected virtual string GetConnectionString()
        {
            return Configuration.GetSection("ConnectionStrings")?.GetSection("ForretasDBContext").Value ?? string.Empty;
        }

        protected bool ExecuteNonQuery(string query, DynamicParameters parameters)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Execute(query, parameters);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public object ExecuteScalar(string query, DynamicParameters parameters)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    return con.ExecuteScalar(query, parameters);
                }
            }
            catch
            {
                return null;
            }
        }

        protected T GetSingleMappedEntity(string query, DynamicParameters parameters)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    var result = con.QueryAsync<T>(query, parameters).Result;
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                return default(T);
            }
        }

        protected IEnumerable<T> GetMultipleMappedEntities(string query, DynamicParameters parameters = null)
        {
            try
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    return con.QueryAsync<T>(query, parameters).Result;
                }
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
