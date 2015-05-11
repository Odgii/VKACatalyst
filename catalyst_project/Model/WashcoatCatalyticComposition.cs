﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace catalyst_project.Model
{
    public class WashcoatCatalyticComposition : INotifyPropertyChanged
    {
        public bool _IsChecked;
        public string _WashcoatValue;
        public bool _NeedPreciousMetal;

        public event PropertyChangedEventHandler PropertyChanged;

        public WashcoatCatalyticComposition(bool isChecked, String washcoatValue, bool needPreciousMetal)
        {
             IsChecked = isChecked;
             WashcoatValue = washcoatValue;
             NeedPreciousMetal = needPreciousMetal;
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

        public bool NeedPreciousMetal
        {
            get
            {
                return _NeedPreciousMetal;
            }

            set
            {
                _NeedPreciousMetal = value;
                OnPropertyChanged("NeedPreciousMetal");
            }
        }

        public string WashcoatValue
        {
            get
            {
                return _WashcoatValue;
            }
            set
            {
                _WashcoatValue = value;
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
