using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class SourceOfMeasurement
    {
        private int _Id;
        private string _Source;

        public SourceOfMeasurement(int id, string source)
        {
            Id = id;
            Source = source;

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

        public string Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
            }
        }

    }
}
