using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Connection
    {
        private static SqlConnection db = null;

        public static SqlConnection Connect()
        {
            db = new SqlConnection("server=DESKTOP-EM3U44N; Initial Catalog=AracKiralamaRezervasyon;User Id=utku;password=10021998ud");
            db.Open();

            return db;
        }
    }
}
