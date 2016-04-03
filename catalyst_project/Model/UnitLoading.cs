using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitLoading :DBModel
    {
        private int _Id;
        private string _Unit;

        public UnitLoading() { 
        }

        public UnitLoading(int id, string unit)
        {
        }

        public int Id {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public string Unit {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
            }
        }
    }
}
