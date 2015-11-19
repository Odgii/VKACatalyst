using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class SteadyStateLegislation : INotifyPropertyChanged
    {
        private int _Id;
        private string _Legislation;
        private bool _IsChecked;


        public event PropertyChangedEventHandler PropertyChanged;

        public SteadyStateLegislation(bool isChecked, int id, string steadyStateLeg)
        {
            IsChecked = isChecked;
            Id = id;
            Legislation = steadyStateLeg;
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
