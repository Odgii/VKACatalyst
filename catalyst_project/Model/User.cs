using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class User : INotifyPropertyChanged
    {
        private int _Id;
        private string _Role;
        private string _Code;
        private string _First_name;
        private string _Last_name;
        private string _Password;
        private string _Institute;
        private string _Registered_date;

        public event PropertyChangedEventHandler PropertyChanged;

        public User(int user_id, string user_role, string user_code, string first_name, string last_name, string password)
        {
            Id = user_id;
            Role = user_role;
            Code = user_code;
            First_name = first_name;
            Last_name = last_name;
            Password = password;
        }

        public User()
        { 
        
        }

        public int Id
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
        

        public string Role
        {
            get
            {
                return _Role;
            }

            set
            {
                _Role = value;
                OnPropertyChanged("Role");
            }
        }
        
        
        public string Code
        {
            get
            {
                return _Code;
            }

            set
            {
                _Code = value;
                OnPropertyChanged("Code");
            }
        }
        
        
        public string First_name
        {
            get
            {
                return _First_name;
            }

            set
            {
                _First_name = value;
                OnPropertyChanged("First");
            }
        }
       
        
        
        public string Last_name
        {
            get
            {
                return _Last_name;
            }

            set
            {
                _Last_name = value;
                OnPropertyChanged("Last");
            }
        }


        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }
        
        
        
        
        public string Institute
        {
            get
            {
                return _Institute;
            }

            set
            {
                _Institute = value;
                OnPropertyChanged("Institute");
            }
        }


        public string Registered_date
        {
            get
            {
                return _Registered_date;
            }

            set
            {
                _Registered_date = value;
                OnPropertyChanged("Registered_date");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
