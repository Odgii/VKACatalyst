using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UnitPreciousMetalLoading : DBModel
    {
        private int _Id;
        private string _LoadingUnit;

        public UnitPreciousMetalLoading()
        { 
        }

        public UnitPreciousMetalLoading(int id, string loading_unit)
        {
            Id = id;
            LoadingUnit = loading_unit;
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

        public string LoadingUnit
        {
            get
            {
                return _LoadingUnit;
            }

            set
            {
                _LoadingUnit = value;
            }
        }
    }
}
