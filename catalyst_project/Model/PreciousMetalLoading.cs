using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class PreciousMetalLoading: INotifyPropertyChanged
    {
        public bool _IsChecked;
        public string  _Name;

        public event PropertyChangedEventHandler PropertyChanged;

        public PreciousMetalLoading(bool isChecked, String name)
        {
             IsChecked = isChecked;
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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
                
        }
    }
}
