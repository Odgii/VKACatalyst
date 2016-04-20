using catalyst_project.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ErrorDataWindow.xaml
    /// </summary>
    public partial class ErrorDataWindow : Window
    {
        SqliteDBConnection db;
        public ErrorDataWindow()
        {
            InitializeComponent();
            db = new SqliteDBConnection();
        }

        private void btn_err_submit_Click(object sender, RoutedEventArgs e)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "insert into bugs (bug_catalyst_id,bug_data_group,bug_detected_date,error_field_name,error_current_value,error_correct_value,error_comment,bug_type_id,isFixed) values (@bug_catalyst_id,@bug_data_group,@bug_detected_date,@error_field_name,@error_current_value,@error_correct_value,@error_comment,1,0) ";
            cmd.Parameters.AddWithValue("@bug_catalyst_id", txb_err_catalyst_id.Text.Equals("")? 0 : Convert.ToInt32(txb_err_catalyst_id.Text));
            cmd.Parameters.AddWithValue("@bug_data_group", cmb_catalyst_datagroup.Text);
            cmd.Parameters.AddWithValue("@bug_detected_date", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@error_field_name", txb_err_field.Text);
            cmd.Parameters.AddWithValue("@error_current_value", txb_err_field_curr_value.Text);
            cmd.Parameters.AddWithValue("@error_correct_value", txb_err_field_corr_value.Text);
            cmd.Parameters.AddWithValue("@error_comment", txb_err_comment.Text);
            int n = db.Insert(cmd);
            MessageBox.Show(n.ToString());

        }
    }
}
