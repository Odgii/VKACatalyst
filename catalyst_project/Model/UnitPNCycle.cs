using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitPNCycle: DBModel
    {
        private string _CycleUnit;

        public UnitPNCycle()
        { 
        }

        public UnitPNCycle( string cycle_unit)
        {
            CycleUnit = cycle_unit;
        }
        public string CycleUnit
        {
            get
            {
                return _CycleUnit;
            }
            set
            {
                _CycleUnit = value;
            }
        }
    }
}
