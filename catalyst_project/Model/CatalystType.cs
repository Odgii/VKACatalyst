using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class CatalystType
    {
        private int _Id;
        private string _Type;

        public CatalystType(int id, string type) 
        {
            Id = id;
            Type = type;
    
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
