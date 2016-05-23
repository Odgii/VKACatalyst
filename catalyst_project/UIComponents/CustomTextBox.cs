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
    ///     <MyNamespace:CustomTextBox/>
    ///
    /// </summary>
    public class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {

        }

        public static  DependencyProperty TableProperty =
        DependencyProperty.Register("TableName", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty FieldProperty =
        DependencyProperty.Register("FieldName", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty UpdateProperty =
        DependencyProperty.Register("UpdateId", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty ControlTypeProperty =
        DependencyProperty.Register("ContentType", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty UpdateHelperProperty =
        DependencyProperty.Register("UpdateHelper", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty LabelTitleProperty =
        DependencyProperty.Register("LabelTitle", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty GroupTitleProperty =
        DependencyProperty.Register("GroupTitle", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public static DependencyProperty DefaultUnitProperty =
        DependencyProperty.Register("DefaultUnit", typeof(String), typeof(CustomTextBox), new PropertyMetadata(default(string)));

        public String DefaultUnit
        {
            get
            {
                return (String)GetValue(DefaultUnitProperty); ;
            }
            set
            {
                SetValue(DefaultUnitProperty, value);
            }
        }

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
        public String UpdateHelper
        {
            get
            {
                return (String)GetValue(UpdateHelperProperty); ;
            }
            set
            {
                SetValue(UpdateHelperProperty, value);
            }
        }
        public String TableName
        {
            get
            {
                return (String)GetValue(TableProperty); ;
            }
            set
            {
                SetValue(TableProperty, value);
            }
        }

        public String FieldName
        {
            get
            {
                return (String)GetValue(FieldProperty); ;
            }
            set
            {
                SetValue(FieldProperty, value);
            }
        }

        public String UpdateId
        {
            get
            {
                return (String)GetValue(UpdateProperty); ;
            }
            set
            {
                SetValue(UpdateProperty, value);
            }
        }

        public String ContentType
        {
            get
            {
                return (String)GetValue(ControlTypeProperty); ;
            }
            set
            {
                SetValue(ControlTypeProperty, value);
            }
        }
    }
}
