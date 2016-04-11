using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitMaxOSC : DBModel
    {
        private int _Id;
        private string _Unit;

        public UnitMaxOSC() {
        }

        public UnitMaxOSC(int id, string unit) {
            Unit = unit;
            Id = id;
            
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
