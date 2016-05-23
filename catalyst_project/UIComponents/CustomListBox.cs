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

namespace catalyst_project.UIComponents
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:catalyst_project.UIComponents"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:catalyst_project.UIComponents;assembly=catalyst_project.UIComponents"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomListBox/>
    ///
    /// </summary>
    public class CustomListBox : ListBox
    {
        public CustomListBox()
        {
            
        }

        public static DependencyProperty LabelTitleProperty =
        DependencyProperty.Register("LabelTitle", typeof(String), typeof(CustomListBox), new PropertyMetadata(default(string)));

        public static DependencyProperty GroupTitleProperty =
        DependencyProperty.Register("GroupTitle", typeof(String), typeof(CustomListBox), new PropertyMetadata(default(string)));

        public String GroupTitle
        {
            get
            {
                return (String)GetValue(GroupTitleProperty); ;
            }
            set
            {
                SetValue(GroupTitleProperty, value);
            }
        }

        public String LabelTitle
        {
            get
            {
                return (String)GetValue(LabelTitleProperty); ;
            }
            set
            {
                SetValue(LabelTitleProperty, value);
            }
        }

    }
}
