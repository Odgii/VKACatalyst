using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class BoundaryShape : DBModel, INotifyPropertyChanged
    {
        private int _Id;
        private string _Shape;
        private bool _IsChecked;
        public event PropertyChangedEventHandler PropertyChanged;

        public BoundaryShape(int id, string shape, bool isChecked)
        {
            Id = id;
            Shape = shape;    
        }

        public BoundaryShape()
        { 
        
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

        public string Shape
        {
            get
            {
                return _Shape;
            }

            set
            {
                _Shape = value;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
