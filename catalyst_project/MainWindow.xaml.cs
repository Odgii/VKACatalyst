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

namespace catalyst_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginClicked(object sender, RoutedEventArgs e)
        {
             MainApplication new_app = new MainApplication();
        //   AdminWindow new_app = new AdminWindow();
         //    SearchWindow new_app = new SearchWindow();
             new_app.Show();
             this.Close();
        }
    }
}
