using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitSupport : DBModel
    {
        private int _id;
        private string _unit;

        public UnitSupport()
        { 
        }

        public UnitSupport(int id, string unit)
        {
            Unit = unit;
            Id = id;
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
            }
        }
    }
}
