using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class AgingProcedure
    {
        private int _Id;
        private string _Procedure;

        public AgingProcedure()
        { 
        
        }
        public AgingProcedure(int id, string procedure)
        {
            Id = id;
            Procedure = procedure;
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
    }
}
