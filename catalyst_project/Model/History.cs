using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.View
{
    class History : INotifyPropertyChanged
    {
        private string _Id;
        private string _Catalyst_id;
        private string _Group_name;
        private string _Field_name;
        private string _User_name;
        private string _User_action;
        private string _Old_value;
        private string _New_value;
        private string _ModifiedDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public History(string id, string catalyst_id, string group_name, string field_name, string user_name, string user_action,string old_value, string new_value, string modifiedDate)
        { 
            Id = id;
            Catalyst_id = catalyst_id;
            Group_name = group_name;
            Field_name = field_name;
            User_name = user_name;
            Old_value = old_value;
            New_value = new_value;
            ModifiedDate = modifiedDate;
            User_action = user_action;
        }

        public string User_action
        {
            get
            {
                return _User_action;
            }

            set
            {
                _User_action = value;
                OnPropertyChanged("User_action");
            }
        }
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Catalyst_id
        {
            get
            {
                return _Catalyst_id;
            }

            set
            {
                _Catalyst_id = value;
                OnPropertyChanged("Catalyst_id");
            }
        }


        public string Group_name
        {
            get
            {
                return _Group_name;
            }

            set
            {
                _Group_name = value;
                OnPropertyChanged("Group_name");
            }
        }

        public string Field_name
        {
            get
            {
                return _Field_name;
            }

            set
            {
                _Field_name = value;
                OnPropertyChanged("Field_name");
            }
        }

        public string User_name
        {
            get
            {
                return _User_name;
            }

            set
            {
                _User_name = value;
                OnPropertyChanged("User_name");
            }
        }

        public string Old_value
        {
            get
            {
                return _Old_value;
            }

            set
            {
                _Old_value = value;
                OnPropertyChanged("Old_value");
            }
        }

        public string New_value
        {
            get
            {
                return _New_value;
            }

            set
            {
                _New_value = value;
                OnPropertyChanged("New_value");
            }
        }

        public string ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }

            set
            {
                _ModifiedDate = value;
                OnPropertyChanged("ModifiedDate");
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
