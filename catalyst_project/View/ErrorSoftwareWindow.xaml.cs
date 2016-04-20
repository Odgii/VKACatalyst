using catalyst_project.Database;
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
using System.Windows.Shapes;
using System.Data.SQLite;

namespace catalyst_project.View
{
    /// <summary>
    /// Interaction logic for ErrorSoftwareWindow.xaml
    /// </summary>
    public partial class ErrorSoftwareWindow : Window
    {
        SqliteDBConnection db;
        public ErrorSoftwareWindow()
        {
            InitializeComponent();
            db = new SqliteDBConnection();
        }

        private void btn_soft_submit_Click(object sender, RoutedEventArgs e)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "insert into bugs (bug_catalyst_id,bug_data_group,bug_detected_date,error_field_name,error_current_value,error_correct_value,error_comment,bug_type_id,isFixed) values (null,null,@bug_detected_date,null,null,null,@error_comment,2,0) ";
            cmd.Parameters.AddWithValue("@bug_detected_date", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@error_comment", txb_soft_comment.Text);
            int n = db.Insert(cmd);
            MessageBox.Show(n.ToString());
        }
    }
}
