using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class ModelType : DBModel
    {
        private int _Id;
        private string _Type;
        private bool _IsChecked;

        public ModelType()
        { 
        
        }
        public ModelType(int id, string type, bool isChecked)
        {
            Id = id;
            Type = type;
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

    }
}
