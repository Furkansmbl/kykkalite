using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KykKaliteApi.Models.DapperContext
{
    public class Context
    {
        private readonly string _connectionString;

        public Context(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("connection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
