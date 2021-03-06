﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace catalyst_project.Database
{
    class DBConnection
    {
        private MySqlConnection connection;

        public DBConnection() {
            Initialize();
            
        }

        void Initialize() {
            string connectionString = "server=localhost;userid=root;password=rootpassword;database=catalyst_db;";
           // string connectionString = "Server=137.226.225.174;Database=catalyst_db;Uid=root;Pwd=catalyst-rootpass;";
            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        public bool CloseConnection() 
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UserExist(string username, string password) {
            return true;
        }

        public void Insert(MySqlCommand cmd)
        {
            if(this.OpenConnection() == true)
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Hurray");
                this.CloseConnection();

            }
           
        }

        public void Update(MySqlCommand cmd)
        {
            if(this.OpenConnection() == true)
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Hurray");
                this.CloseConnection();
            }
        
        }

        public void Delete(MySqlCommand cmd)
        {
            if (this.OpenConnection() == true)
            {
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Hurray");
                this.CloseConnection();
            }
        }

        public List<string>[] Select(string query)
        {
            int n = 0;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                n = data.FieldCount;
                this.CloseConnection();
            }
            List<string>[] list = new List<string>[n];
            for (int i = 0; i < n; i++)
            {
                list[i] = new List<string>();
            }
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    for (int i = 0; i < n; i++)
                    {
                        list[i].Add(dataReader[i] + "");
                    }
                }

                dataReader.Close();
                this.CloseConnection();
                return list;
            }
            else
            {
                return list;
            }            
        }

    }
}
