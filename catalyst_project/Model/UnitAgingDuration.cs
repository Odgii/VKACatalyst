using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitAgingDuration: DBModel
    {
        private int _Id;
        private string _DurationUnit;

        public UnitAgingDuration()
        { 
        }

        public UnitAgingDuration(int id, string duration)
        {
            Id = id;
            DurationUnit = duration;
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

        public string DurationUnit
        {
            get
            {
                return _DurationUnit;
            }

            set
            {
                _DurationUnit = value;
            }
        }
    }
}
