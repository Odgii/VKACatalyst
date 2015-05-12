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

        private void btn_choose_catalyst_Click(object sender, RoutedEventArgs e)
        {
            string id = txb_search_value.Text;
            MainApplication.chosenID = id;
            this.Close();
        }
    }
}
