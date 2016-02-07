using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class AgingStatus : DBModel, INotifyPropertyChanged
    {
        private int _Id;
        private string _Status;
        private bool _IsChecked;
        public event PropertyChangedEventHandler PropertyChanged;

        public AgingStatus (int id, string status, bool isChecked)
        {
            Id = id;
            Status = status;
            IsChecked = isChecked;
           
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

        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
