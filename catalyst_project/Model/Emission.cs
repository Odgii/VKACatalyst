using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class Emission 
    {
        private int _Id;
        private string _Name;

        public Emission()
        { 
        
        }
        public Emission(int id, string name)
        {
            Id = id;
            Name = name;
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

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

    }
}
