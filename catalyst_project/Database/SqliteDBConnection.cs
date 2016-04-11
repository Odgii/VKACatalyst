﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace catalyst_project.Database
{
    class SqliteDBConnection
    {
        private SQLiteConnection connection;

        public SqliteDBConnection()
        {
            connection = new SQLiteConnection("URI=file:catalyst_db.db");
        }

        public int Insert(SQLiteCommand cmd)
        {
            int last_row_id =0;
            try
            {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                SQLiteCommand cm = new SQLiteCommand("select last_insert_rowid()", connection);
                last_row_id = Convert.ToInt32(cm.ExecuteScalar());
                MessageBox.Show("Insert Hurray");
                this.DisposeSQLite();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
               
            }
            return last_row_id;
        }

        public void Update(SQLiteCommand cmd)
        {
            try {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Hurray");
                this.DisposeSQLite();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Delete(SQLiteCommand cmd)
        {
            try
            {
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                this.DisposeSQLite();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public bool DisposeSQLite() 
        {
            try
            {
                connection.Close();
                SQLiteConnection.ClearAllPools();
                GC.Collect();
                return true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public List<string>[] Select( string query)
        {
            int tableColumnCount = 0;
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                connection.Open();
                SQLiteDataReader data = cmd.ExecuteReader();
                tableColumnCount = data.FieldCount;
                data.Close();

                List<string>[] list = new List<string>[tableColumnCount];
                for (int i = 0; i < tableColumnCount; i++)
                {
                    list[i] = new List<string>();
                }

                SQLiteCommand cmd1 = new SQLiteCommand(query, connection);
                SQLiteDataReader dataReader = cmd1.ExecuteReader();
                while (dataReader.Read())
                {
                    for (int i = 0; i < tableColumnCount; i++)
                    {
                        list[i].Add(dataReader[i] + "");
                    }
                }

                dataReader.Close();
                this.DisposeSQLite();
                return list;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
