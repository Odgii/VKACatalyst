using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class BoundaryShape
    {
        private int _Id;
        private string _Shape;

        public BoundaryShape(int id, string shape)
        {
            Id = id;
            Shape = shape;    
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

        public string Shape
        {
            get
            {
                return _Shape;
            }

            set
            {
                _Shape = value;
            }
        }
    }
    
}
