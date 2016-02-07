using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class CatalystType : DBModel, INotifyPropertyChanged
    {
        private int _Id;
        private string _Type;
        private bool _IsChecked;
        public event PropertyChangedEventHandler PropertyChanged;

        public CatalystType(int id, string type, bool isChecked) 
        {
            Id = id;
            Type = type;
            IsChecked = isChecked;
    
        }

        public  bool IsChecked
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

        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
