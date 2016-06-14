using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class CatalystApproval
    {
        int _Catalyst_id;
        bool _Is_Approved;
        string _Review_Comment;

        public CatalystApproval() 
        {

        }

        public CatalystApproval(int catalyst_id, bool is_approved, string review_comment)
        {
            Catalyst_id = catalyst_id;
            Is_Approved = is_approved;
            Review_Comment = review_comment;
        }

        public int Catalyst_id
        {
            get
            {
                return _Catalyst_id;
            }

            set
            {
                _Catalyst_id = value;
            }
        }

        public bool Is_Approved
        {
            get
            {
                return _Is_Approved;
            }

            set
            {
                _Is_Approved = value;
            }
        }

        public string Review_Comment
        {
            get
            {
                return _Review_Comment;
            }

            set
            {
                _Review_Comment = value;
            }
        }
    }
}
