using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;

namespace DMP_Regi_Adjustment_Program
{
    public class SQLiteManager
    {
        private SQLiteConnection connection;
        private String databaseFileName;

        public SQLiteManager(String databaseFileName)
        {
            this.databaseFileName = databaseFileName;
        }

        public void CreateSQLiteDB()
        {
            if (File.Exists(databaseFileName))
                return;

            SQLiteConnection.CreateFile(databaseFileName);
        }

        public void Connect()
        {
            connection = new SQLiteConnection(String.Format("Data Source={0};Version=3;NEW=False;Compress=True;UseUTF8Encoding=True;", databaseFileName));
            connection.Open();
        }

        public void Disconnect()
        {
            if (connection != null)
                connection.Close();
        }

        public void NonQuery(String sql)
        {
            Connect();
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            Disconnect();
        }

        public DataTable Query(String sql)
        {
            Connect();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            Disconnect();

            return dataTable;
        }
    }
}
