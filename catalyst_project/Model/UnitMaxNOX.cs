using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitMaxNOX: DBModel
    {
        private int _Id;
        private string _Unit;

        public UnitMaxNOX() {
        }

        public UnitMaxNOX(int id, string unit)
        {
            Id = id;
            Unit = unit;
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
