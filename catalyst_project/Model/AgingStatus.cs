using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class AgingStatus
    {
        private int _Id;
        private string _Status;

        public AgingStatus (int id, string status)
        {
            Id = id;
            Status = status;
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

        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }
    }
}
