using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class CatalystResult : DBModel
    {
        int _catalyst_id;
        string _type;
        string _number;

        public CatalystResult() { 
        }

        public CatalystResult(int id, string type, string number) {
            CatalystID = id;
            CatalystType = type;
            Number = number;
        }

        public int CatalystID {
            get
            {
                return _catalyst_id;
            }
            set
            {
                _catalyst_id = value;
            }
        }

        public string Number {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }

        public string CatalystType {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

    }
}
