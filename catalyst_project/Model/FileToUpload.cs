using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class FileToUpload
    {
        string name;
        string location;

        public FileToUpload(string name, string location) {
            Name = name;
            Location = location;
        }

        public string Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Location { 
            get
            {
                return location;    
            }
            set
            {
                location = value;
            }
        }
    }
}
