using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class CheckBoxValues : DBModel
    {
        string _BoxValue;

        public CheckBoxValues(string val)
        {
            BoxValue = val;
        }

        public string BoxValue
        {
            get
            {
                return _BoxValue;
            }

            set
            {
                _BoxValue = value;
            }
        }
    }
}
