using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class TransientLegislation : INotifyPropertyChanged
    {
        public int _Id;
        public string _Legislation;
        public bool _IsChecked;


        public event PropertyChangedEventHandler PropertyChanged;

        public TransientLegislation(bool isChecked, int id, string transient)
        {
            IsChecked = isChecked;
            Id = id;
            Legislation = transient;
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

        public string Legislation
        {
            get
            {
                return _Legislation;
            }

            set
            {
                _Legislation = value;
                OnPropertyChanged("SteadyState");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
