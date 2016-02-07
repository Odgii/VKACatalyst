using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class Bug
    {
        private int _Id;
        private int _CatalystID;
        private string _BugType;
        private string _DataGroup;
        private string _ErrorFieldName;
        private string _ErrorCurrentValue;
        private string _ErrorCorrectValue;
        private string _Comment;
        private bool _isFixed;
        private string _DetectedDate;

        public Bug()
        { 
        
        }

        public Bug(int id, int catalyst_id, string bug_type, string data_group, string error_fieldname, string current_value, string correct_value, string comment, bool is_fixed, string detected_date)
        {
            Id = id;
            CatalystID = catalyst_id;
            BugType = bug_type;
            DataGroup = data_group;
            ErrorFieldName = error_fieldname;
            ErrorCurrentValue = current_value;
            ErrorCorrectValue = correct_value;
            Comment = comment;
            isFixed = is_fixed;
            DetectedDate = detected_date;
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

        public int CatalystID
        {
            get
            {
                return _CatalystID;
            }
            set
            {
                _CatalystID = value;
            }
        }

        public string BugType
        {
            get
            {
                return _BugType;
            }
            set
            {
                _BugType = value;
            }
        }

        public string DataGroup
        {
            get
            {
                return _DataGroup;
            }
            set
            {
                _DataGroup = value;
            }
        }

        public string ErrorFieldName
        {
            get
            {
                return _ErrorFieldName;
            }
            set
            {
                _ErrorFieldName = value;
            }
        }

        public string ErrorCurrentValue
        {
            get
            {
                return _ErrorCurrentValue;
            }
            set
            {
                _ErrorCurrentValue = value;
            }
        }

        public string ErrorCorrectValue
        {
            get
            {
                return _ErrorCorrectValue;
            }
            set
            {
                _ErrorCorrectValue = value;
            }
        }

        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                _Comment = value;
            }
        }

        public bool isFixed
        {
            get
            {
                return _isFixed;
            }
            set
            {
                _isFixed = value;
            }
        }

        public string DetectedDate
        {
            get
            {
                return _DetectedDate;
            }
            set
            {
                _DetectedDate = value;
            }
        }
    }
}
