using catalyst_project.Model;
using catalyst_project.View;
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
    ///     <MyNamespace:CustomComboBoxItem/>
    ///
    /// </summary>
    public class CustomComboBoxItem 
    {
        private String _Name;
        private String _TableName;
        private String _FieldName;
        private bool _isCombobox;
        private bool _isTextBox;
        private bool _isNumber;
        private String _Unit;
        private bool _isDateTimePicker;
        private bool _MultipleChoice;
        private String _DisplayMemberPath;
        private String _SelectedValuePath;
        private String _SourceCollection;

        private Array _DBModelCollection;

        public CustomComboBoxItem(String name, String table_name, String field_name, bool is_TextBox,bool is_Number, String unit, bool is_DateTimePicker, bool is_Combobox, bool is_MultipleChoice, String selected_valuepath, String displayed_memberPath)
        {
            Name = name;
            TableName = table_name;
            FieldName = field_name;
            IsComboBox = is_Combobox;
            IsDateTimePicker = is_DateTimePicker;
      //      SourceCollection = sourceCollection;
            IsTextBox = is_TextBox;
            IsNumber = is_Number;
            Unit = unit;
            DisplayMemberPath = displayed_memberPath;
            SelectedValuePath = selected_valuepath;
        }


        public CustomComboBoxItem(String name, String table_name, String field_name, bool is_TextBox, bool is_Number, String unit, bool is_DateTimePicker, bool is_Combobox, bool is_MultipleChoice, String selected_valuepath, String displayed_memberPath, Array sourceCollection)
        {
            Name = name;
            TableName = table_name;
            FieldName = field_name;
            IsComboBox = is_Combobox;
            IsDateTimePicker = is_DateTimePicker;
            DBModelCollection = sourceCollection;
            IsTextBox = is_TextBox;
            IsNumber = is_Number;
            Unit = unit;
            DisplayMemberPath = displayed_memberPath;
            SelectedValuePath = selected_valuepath;
        }


        public String Name
        {
            get 
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        
        }

        public String Unit
        {
            get
            {
                return _Unit;
            }

            set
            {
                _Unit = value;
            }

        }
     

        public String TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        public String FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }


        public bool IsDateTimePicker
        {
            get
            {
                return _isDateTimePicker;
            }
            set
            {
                _isDateTimePicker = value;
            }
        }

        public bool IsComboBox
        {
            get
            {
                return _isCombobox;
            }
            set
            {
                _isCombobox = value;
            }
        }

        public String DisplayMemberPath
        {
            get
            {
                return _DisplayMemberPath;
            }
            set
            {
                _DisplayMemberPath = value;
            }
        }


        public String SelectedValuePath
        {
            get
            {
                return _SelectedValuePath;
            }
            set
            {
                _SelectedValuePath = value;
            }
        }

        public bool IsTextBox
        {
            get
            {
                return _isTextBox;
            }
            set
            {
                _isTextBox = value;
            }
        }

        public bool MultipleChoice
        {
            get
            {
                return _MultipleChoice;
            }
            set
            {
                _MultipleChoice = value;
            }
        }

        public bool IsNumber
        {
            get
            {
                return _isNumber;
            }
            set
            {
                _isNumber = value;
            }
        }



        public String SourceCollection
        {
            get
            {
                return _SourceCollection;
            }
            set
            {
                _SourceCollection = value;
            }
        }


        public Array DBModelCollection
        {
            get
            {
                return _DBModelCollection;
            }
            set
            {
                _DBModelCollection = value;
            }
        }


        

        
    }

}
