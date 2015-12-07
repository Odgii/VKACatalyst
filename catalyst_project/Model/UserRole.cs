using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class UserRole
    {
        private int _Id;
        private string _Role;

        public UserRole()
        { 
        
        }

        public UserRole(int id, string role)
        {
            Id = id;
            Role = role;
        }

        public int Id
        {
            get {
                return _Id;
            }

            set
            {
                _Id = value; 
            }
        }

        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }

        }
    }
}
