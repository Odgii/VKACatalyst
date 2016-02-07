using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class PreciousMetalLoading: DBModel, INotifyPropertyChanged
    {
        private bool _IsChecked;
        private int _Id;
        private string _Name;

        public event PropertyChangedEventHandler PropertyChanged;

        public PreciousMetalLoading(bool isChecked, int id, string name)
        {
             _Id = id;
             _IsChecked = isChecked;
             _Name = name;
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

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("WashcoatValue");
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


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
                
        }
    }
}
