using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class MonolithMaterial
    {
        public int _Id;
        public string _Material;

        public MonolithMaterial(int id, string material)
        {
            _Id = id;
            _Material = material;
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

        public string Material
        {
            get
            {
                return _Material;
            }

            set
            {
                _Material = value;
            }
        }

    }
}
