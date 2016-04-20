using catalyst_project.Database;
using catalyst_project.Model;
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

namespace catalyst_project.View
{
    /// <summary>
    /// Interaction logic for CombineCatalystWindow.xaml
    /// </summary>
    public partial class CombineCatalystWindow : Window
    {
        public CombineCatalystWindow()
        {
            InitializeComponent();
        }

        private void btn_search_catalyst_Click(object sender, RoutedEventArgs e)
        {
            SqliteDBConnection db = new SqliteDBConnection();
            if (cmb_search_by.Text.Equals("") || cmb_search_by.SelectedItem.Equals(null) || txb_search_value.Text.Equals("") || txb_search_value.Text.Equals(null))
            {
                MessageBox.Show("Please enter the search value!");
            }
            else if (!cmb_search_by.Text.Equals("") && !txb_search_value.Text.Equals(""))
            {
                if (cmb_search_by.SelectedItem.ToString().Contains("ID"))
                {
                    ObservableCollection<CatalystResult> result = new ObservableCollection<CatalystResult>();
                    List<string>[] list = db.Select("select Catalyst.catalyst_id, CatalystType.catalyst_type_name, GeneralInformation.catalyst_number from Catalyst join GeneralInformation on GeneralInformation.catalyst_id = Catalyst.catalyst_id join CatalystType on CatalystType.catalyst_type_id = Catalyst.catalyst_type_id where Catalyst.catalyst_id = " + txb_search_value.Text);
                    if (list.Count() > 0)
                    {
                        for (int i = 0; i < list[0].Count(); i++)
                        {
                            int id = Convert.ToInt32(list[0][i]);
                            string type = list[1][i];
                            string num = list[2][i];
                            CatalystResult c = new CatalystResult(id, type, num);
                            result.Add(c);
                        }
                    }
                    grid_result.ItemsSource = result;
                }
                if (cmb_search_by.SelectedItem.ToString().Contains("Catalyst"))
                {
                    ObservableCollection<CatalystResult> result = new ObservableCollection<CatalystResult>();
                    List<string>[] list = db.Select("select Catalyst.catalyst_id, CatalystType.catalyst_type_name, GeneralInformation.catalyst_number from Catalyst join GeneralInformation on GeneralInformation.catalyst_id = Catalyst.catalyst_id join CatalystType on CatalystType.catalyst_type_id = Catalyst.catalyst_type_id where GeneralInformation.catalyst_number like '%" + txb_search_value.Text + "%'");
                    if (list.Count() > 0)
                    {
                        for (int i = 0; i < list[0].Count(); i++)
                        {
                            int id = Convert.ToInt32(list[0][i]);
                            string type = list[1][i];
                            string num = list[2][i];
                            CatalystResult c = new CatalystResult(id, type, num);
                            result.Add(c);
                        }
                    }
                    grid_result.ItemsSource = result;
                }
            }
        }

        private void grid_result_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (grid.SelectedItem != null)
            {
                CatalystResult s = (CatalystResult)grid.SelectedItem;
                MainApplication.chosenID = s.CatalystID.ToString();
                this.Close();
            }

        }
    }
}
