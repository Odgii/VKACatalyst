using catalyst_project.Database;
using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace catalyst_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqliteDBConnection db;
        public static int userID = 0;
        public static string userCode = null;
        public static int userRole = 0;
        public MainWindow()
        {
            InitializeComponent();
            db = new SqliteDBConnection();
        }

        private void LoginClicked(object sender, RoutedEventArgs e)
        {
            SQLiteCommand cmd = new SQLiteCommand("select * from user where user_code = @user_code and user_password = @user_password");
            cmd.Parameters.AddWithValue("@user_code", txb_username.Text);
            cmd.Parameters.AddWithValue("@user_password", txb_password.Text);
            int user_id = db.Exist(cmd);
            if (user_id > 0)
            {
                userID = user_id;
                userCode = txb_username.Text;
                string query = "select * from user where user_code = '" + txb_username.Text + "' and user_password = '" +txb_password.Text + "'";
                List<string>[] userData = db.Select(query);
                userRole = Convert.ToInt32(userData[1][0]);
             //  SearchWindow new_app = new SearchWindow();
                MainApplication new_app = new MainApplication();
                new_app.Show();
                this.Close();
            }
            else {
                MessageBox.Show("Invalid Username or Password.");
            }
             
        }

    }
}
