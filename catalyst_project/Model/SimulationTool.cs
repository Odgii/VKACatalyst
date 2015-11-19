using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class SimulationTool
    {
        private int _Id;
        private string _Tool;

        public SimulationTool(int id, string tool)
        {
            Id = id;
            Tool = tool;

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

        public string Tool
        {
            get
            {
                return _Tool;
            }
            set
            {
                _Tool = value;
            }
        }

    }
}
