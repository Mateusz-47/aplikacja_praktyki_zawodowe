using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Projekt_Praktyki_Zawodowe.Helpers
{
    public static class DbHelper
    {
        private const string ConnectionString =
            "Server=tcp:zst-radom2p2.database.windows.net,1433;" +
            "Database=Baza_Praktyki_Zawodowe;" +
            "User ID=baza;Password=Qwerty12345;Encrypt=True;";

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
