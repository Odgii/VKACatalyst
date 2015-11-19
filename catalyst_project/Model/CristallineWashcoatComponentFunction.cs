using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class CristallineWashcoatComponentFunction
    {
        
        private int _Id;
        private string _Function;

        public CristallineWashcoatComponentFunction(int id, string function)
        {
            Id = id;
            Function = function;    
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

        public string Function
        {
            get
            {
                return _Function;
            }

            set
            {
                _Function = value;
            }
        }
    }
}
