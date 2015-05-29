using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    public class EngineManufacturer
    {
        private int _Id;
        private string _Manufacturer;

        public EngineManufacturer(int id, string manufacturer)
        {
            Id = id;
            Manufacturer = manufacturer;

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

        public string Manufacturer
        {
            get
            {
                return _Manufacturer;
            }
            set
            {
                _Manufacturer = value;
            }
        }

    }
}
