using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class CristallineWashcoatComponentFunction : DBModel, INotifyPropertyChanged
    {
        
        private int _Id;
        private string _Function;
        private bool _IsChecked;
        public event PropertyChangedEventHandler PropertyChanged;

        public CristallineWashcoatComponentFunction(int id, string function, bool isChecked)
        {
            Id = id;
            Function = function;
            IsChecked = isChecked;
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
            }
        }

        public string Function
        {
            get
            {
                return _Function;
            }

            set
            {
                _Function = value;
            }
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
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
