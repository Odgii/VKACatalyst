using catalyst_project.Model;
using catalyst_project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Database
{


    class PopulateAdminPanel
    {

        SqliteDBConnection database;

        public PopulateAdminPanel()
        {
            database = new SqliteDBConnection();
        }
        public ObservableCollection<History> LoadHistory()
        {
            ObservableCollection<History> histories = new ObservableCollection<History>();
            List<string>[] list = database.Select("select * from history");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string history_id = list[0][i];
                string catalyst_id = list[1][i];
                string group_name = list[2][i];
                string field_name = list[3][i];
                string user_name = list[4][i];
                string user_action = list[5][i];
                string old_value = list[6][i];
                string new_value = list[7][i];
                string modified_date = list[8][i];

                History h = new History(id: history_id, catalyst_id: catalyst_id, group_name: group_name, field_name: field_name, user_name: user_name, user_action: user_action, old_value: old_value, new_value: new_value, modifiedDate: modified_date);
                histories.Add(h);
            }
            return histories;
        }

        public ObservableCollection<History> searchHistoryResult(string query)
        {
            ObservableCollection<History> histories = new ObservableCollection<History>();
            List<string>[] list = database.Select(query);
            for (int i = 0; i < list[0].Count(); i++)
            {
                string history_id = list[0][i];
                string catalyst_id = list[1][i];
                string group_name = list[2][i];
                string field_name = list[3][i];
                string user_name = list[4][i];
                string user_action = list[5][i];
                string old_value = list[6][i];
                string new_value = list[7][i];
                string modified_date = list[8][i];

                History h = new History(id: history_id, catalyst_id: catalyst_id, group_name: group_name, field_name: field_name, user_name: user_name, user_action: user_action, old_value: old_value, new_value: new_value, modifiedDate: modified_date);
                histories.Add(h);
            }
            return histories;
        }

        public ObservableCollection<User> LoadUser()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            List<string>[] list = database.Select("select * from user");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                int role = Convert.ToInt32(list[1][i]);
                string code = list[2][i];
                string first_name = list[3][i];
                string last_name = list[4][i];
                string password = list[5][i];


                List<string>[] user_roles = database.Select("select * from userrole where user_role_id =" + role);
                int user_role_id = Convert.ToInt32(user_roles[0][0]);
                string user_role = user_roles[1][0];

                User u = new User(id, user_role, code, first_name, last_name, password);
                users.Add(u);
            }
            return users;
        }

        public int getUserRoleID(string query)
        {
            List<string>[] user_role = database.Select(query);
            int user_id = Convert.ToInt32(user_role[0][0]);
            return user_id;
        }

        public ObservableCollection<Bug> LoadBugs(string query)
        {
            ObservableCollection<Bug> bugs = new ObservableCollection<Bug>();
            List<string>[] list = database.Select(query);
            for (int i = 0; i < list[0].Count(); i++)
            {
                string id = list[0][i];
                string catalyst_id = list[1][i];
                string data_group = list[2][i];
                string detected_date = list[3][i].ToString();
                string error_field = list[4][i];
                string current_val = list[5][i];
                string correct_val = list[6][i];
                string comment = list[7][i];
                string bug_type_id = list[8][i];
                string is_fixed = list[9][i];


                List<string>[] bug_types = database.Select("select * from bugtype where bug_type_id =" + bug_type_id);
                string bug_type = bug_types[1][0];

                Bug b = new Bug(id, catalyst_id, bug_type, data_group, error_field, current_val, correct_val, comment, is_fixed, detected_date);
                bugs.Add(b);
            }

            return bugs;
        }

        public ObservableCollection<CatalystApproval> LoadApprovals()
        {
            ObservableCollection<CatalystApproval> approvals = new ObservableCollection<CatalystApproval>();
            List<string>[] list = database.Select("select * from catalyst");
            for (int i = 0; i < list[0].Count(); i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                bool is_approved = Convert.ToBoolean(list[4][i]);
                string comment = list[3][i];
                string modified_date = list[7][i];

                CatalystApproval c = new CatalystApproval(id, is_approved, comment, modified_date);
                approvals.Add(c);
            }

            return approvals;
        }

    }
}
