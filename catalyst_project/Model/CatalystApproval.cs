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
        string _Modified_Date;
        string _Review_Comment;

        public CatalystApproval() 
        {

        }

        public CatalystApproval(int catalyst_id, bool is_approved, string review_comment, string modified_date)
        {
            Catalyst_id = catalyst_id;
            Is_Approved = is_approved;
            Review_Comment = review_comment;
            Modified_Date = modified_date;
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

        public string Modified_Date
        {
            get
            {
                return _Modified_Date;
            }

            set
            {
                _Modified_Date = value;
            }
        }
    }
}
