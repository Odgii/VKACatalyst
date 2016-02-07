using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class MonolithMaterial : DBModel
    {
        private int _Id;
        private string _Material;
        private bool _IsChecked;

        public MonolithMaterial(int id, string material, bool isChecked)
        {
            Id = id;
            Material = material;
            IsChecked = isChecked;
        }

        public MonolithMaterial()
        { 
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
