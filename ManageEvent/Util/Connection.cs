using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Util
{
    public class Connection
    {
        static private IConfiguration _configuration;

        public static IConfiguration Configuration {set => _configuration = value; }

        public static SqlConnection createConnection()
        {
            Console.WriteLine(_configuration["ConnectionStrings:DefaultConnection"]);
            return new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }
    }
}
