using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class ApplicationFieldTestbench : INotifyPropertyChanged
    {
        private int _Id;
        private string _Name;
        private bool _IsChecked;


        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationFieldTestbench(bool isChecked, int id, string name)
        {
            IsChecked = isChecked;
            Id = id;
            Name = name;
        }

        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }

            set
            {
                _IsChecked = value;
                OnPropertyChanged("IsChecked");
            }
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

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
