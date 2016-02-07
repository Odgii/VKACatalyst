using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
     class SourceOfData : DBModel
    {
        private int _Id;
        private string _Source;
        private bool _IsChecked;

        public SourceOfData(int id, string source, bool isChecked)
        {
            Id = id;
            Source = source;
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
