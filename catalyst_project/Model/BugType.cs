using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class BugType
    {
        private int _Id;
        private string _Type;

        public BugType()
        { 
        
        }

        public BugType(int id, string bug_type)
        {
            Id = id;
            Type = bug_type;
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
