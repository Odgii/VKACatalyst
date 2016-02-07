using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class AgingProcedure : DBModel, INotifyPropertyChanged
    {
        private int _Id;
        private string _Procedure;
        private bool _IsChecked;
        public event PropertyChangedEventHandler PropertyChanged;

        public AgingProcedure()
        { 
        
        }
        public AgingProcedure(int id, string procedure, bool isChecked )
        {
            Id = id;
            Procedure = procedure;
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
            }
        }

        public string Procedure
        {
            get
            {
                return _Procedure;
            }

            set
            {
                _Procedure = value;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
