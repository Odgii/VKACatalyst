using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class SimulationTool : DBModel
    {
        private int _Id;
        private string _Tool;
        private bool _IsChecked;

        public SimulationTool()
        { 
        
        }

        public SimulationTool(int id, string tool, bool isChecked)
        {
            Id = id;
            Tool = tool;
            IsChecked = isChecked;
        }

        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }

            set
            {
                _IsChecked = value;
            }
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
