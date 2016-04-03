using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class MaxO2Unit : DBModel
    {
        private int _Id;
        private string _Unit;

        public MaxO2Unit() {
        }

        public MaxO2Unit(int id, string unit) { 
            
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
