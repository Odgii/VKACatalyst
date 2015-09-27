using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;
using System.Data;

namespace catalyst_project.Database
{
    class SqliteDBConnection
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public SqliteDBConnection()
        {
            openConnection();
        }

        public void openConnection() 
        {
            sql_con = new SQLiteConnection("Data Source=db_catalyst.db;Version=3;New=False;Compress=True;");
            sql_con.Open();
        }

        public void closeConnection() 
        {
        
        }

    }
}
