using catalyst_project.Database;
using catalyst_project.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace catalyst_project.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        DBConnection dbconnection;
        private PopulateAdminPanel populateAdminFromDb;
        private PopulateDropdownsFromDB populateDropdownsFromDb;

        private ObservableCollection<History> histories;
        private ObservableCollection<User> users;
        private ObservableCollection<Manufacturer> manufacs;
        private ObservableCollection<MonolithMaterial> monoliths;
        private ObservableCollection<BoundaryShape> shapes;
        private ObservableCollection<UserRole> roles;
        public AdminWindow()
        {
            dbconnection = new DBConnection();
            InitializeComponent();
            populateAdminFromDb = new PopulateAdminPanel();
            populateDropdownsFromDb = new PopulateDropdownsFromDB();
            InitHistory();
            InitUser();
            InitManufacturer();
            InitMononlith();
            InitShape();
            InitUserRoles();
        }
        /*
         * History Section
         */ 

        private void InitHistory()
        { 
            histories = new ObservableCollection<History>();
            histories = populateAdminFromDb.LoadHistory();
            grid_history.ItemsSource = histories;
        }


        private void grid_history_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_history.SelectedItems.Count > 0)
            {
                int index = grid_history.SelectedIndex;
            }
        }
        //History section ends here

        /*
        * User Section
        */ 
        private void InitUser()
        {
            users = new ObservableCollection<User>();
            users = populateAdminFromDb.LoadUser("select * from user");
            grid_user.ItemsSource = users;
        }

        private void InitUserRoles()
        {
            roles = new ObservableCollection<UserRole>();
            roles = populateDropdownsFromDb.populateUserRoles();
            cmb_userrole.ItemsSource = roles;
        }

        private void grid_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_user.SelectedItems.Count > 0)
            {
                int index = grid_user.SelectedIndex;
                User selectedUser = new User();
                selectedUser =  users[grid_user.SelectedIndex];
                txb_userid.Text = selectedUser.Id.ToString();
                cmb_userrole.Text = selectedUser.Role;
                txb_usercode.Text = selectedUser.Code;
                txb_userfirst.Text = selectedUser.First_name;
                txb_userlast.Text = selectedUser.Last_name;
                txb_userinstitute.Text = selectedUser.Institute;
                txb_userpass.Text = selectedUser.Password;
                txb_userdate.Text = selectedUser.Registered_date;
            }
        }


        private void btns_user_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btn_user_add" && txb_userfirst.Text != "" && cmb_userrole.Text != "" )
            {
                MySqlCommand cmd_user_add = new MySqlCommand();
                cmd_user_add.CommandText = "insert user (user_role_id, user_code, user_firstname, user_lastname, user_password, user_institute, user_registered) values (@user_role_id, @user_code, @user_firstname, @user_lastname, @user_password, @user_institute, @user_registered)";
                cmd_user_add.Parameters.AddWithValue("@user_role_id", cmb_userrole.Text == "" ? 0 : cmb_userrole.SelectedValue);
                cmd_user_add.Parameters.AddWithValue("@user_code", txb_usercode.Text);
                cmd_user_add.Parameters.AddWithValue("@user_firstname", txb_userfirst.Text);
                cmd_user_add.Parameters.AddWithValue("@user_lastname", txb_userlast.Text);
                cmd_user_add.Parameters.AddWithValue("@user_password", txb_userpass.Text);
                cmd_user_add.Parameters.AddWithValue("@user_institute", txb_userinstitute.Text);
                cmd_user_add.Parameters.AddWithValue("@user_registered", DateTime.Now.ToString("dd/MM/yyyy"));
                dbconnection.Insert(cmd_user_add);
                users = populateAdminFromDb.LoadUser("select * from user");
                grid_user.ItemsSource = users;
                ClearUser();
            }
            if (btn.Name == "btn_user_update" && txb_userid.Text != "")
            {
                 MySqlCommand cmd_user_update = new MySqlCommand();
                 cmd_user_update.CommandText = "update user set user_role_id = @user_role, user_password = @user_password, user_code = @user_code, user_firstname = @user_firstname, user_lastname = @user_lastname, user_institute = @user_institute where user_id = @user_id";
                 cmd_user_update.Parameters.AddWithValue("@user_role", cmb_userrole.Text == "" ? 0 : cmb_userrole.SelectedValue);
                 cmd_user_update.Parameters.AddWithValue("@user_code", txb_usercode.Text);
                 cmd_user_update.Parameters.AddWithValue("@user_firstname", txb_userfirst.Text);
                 cmd_user_update.Parameters.AddWithValue("@user_lastname", txb_userlast.Text);
                 cmd_user_update.Parameters.AddWithValue("@user_institute", txb_userinstitute.Text);
                 cmd_user_update.Parameters.AddWithValue("@user_password", txb_userpass.Text);
                 cmd_user_update.Parameters.AddWithValue("@user_id", Convert.ToInt32(txb_userid.Text));
                 dbconnection.Update(cmd_user_update);
                 users = populateAdminFromDb.LoadUser("select * from user");
                 grid_user.ItemsSource = users;
                 ClearUser();
            }
            if (btn.Name == "btn_user_clear")
            {
                ClearUser();
            }
            if (btn.Name == "btn_user_delete" && txb_userid.Text != "")
            {
                MySqlCommand cmd_user_delete = new MySqlCommand();
                cmd_user_delete.CommandText = "delete from user where user_id = @user_id";
                cmd_user_delete.Parameters.AddWithValue("@user_id", Convert.ToInt32(txb_userid.Text));
                dbconnection.Delete(cmd_user_delete);
                users = populateAdminFromDb.LoadUser("select * from user");
                grid_user.ItemsSource = users;
                ClearUser();
            }
            if (btn.Name == "btn_user_search")
            {
                string search_type = cmb_search_user.Text;
                ObservableCollection<User> result = new ObservableCollection<User>();
                switch (search_type)
                {
                    case "Role":
                        for (int i = 0; i < users.Count(); i++)
                        {
                            if (users[i].Role == txb_user_search_value.Text.Trim())
                            {
                                result.Add(users[i]);
                            }
                        }
                        grid_user.ItemsSource = result;
                        break;
                    case "Code":
                        for (int i = 0; i < users.Count(); i++)
                        {
                            if (users[i].Code.Contains(txb_user_search_value.Text.Trim()))
                            {
                                result.Add(users[i]);
                            }
                        }
                        grid_user.ItemsSource = result;
                        break;
                    case "Firstname":
                        for (int i = 0; i < users.Count(); i++)
                        {
                            if (users[i].First_name.Contains(txb_user_search_value.Text.Trim()))
                            {
                                result.Add(users[i]);
                            }
                        }
                        grid_user.ItemsSource = result;
                        break;
                    case "Lastname":
                        for (int i = 0; i < users.Count(); i++)
                        {
                            if (users[i].Last_name.Contains(txb_user_search_value.Text.Trim()))
                            {
                                result.Add(users[i]);
                            }
                        }
                        grid_user.ItemsSource = result;
                        break;
                    case "Institute":
                        for (int i = 0; i < users.Count(); i++)
                        {
                            if (users[i].Institute == txb_user_search_value.Text.Trim())
                            {
                                result.Add(users[i]);
                            }
                        }
                        grid_user.ItemsSource = result;
                        break;
                    case "Show All":
                        grid_user.ItemsSource = users;
                        break;
                }

            }
        }

        private void ClearUser() 
        {
            txb_userid.Clear();
            cmb_userrole.Text = "";
            txb_usercode.Clear();
            txb_userfirst.Clear();
            txb_userlast.Clear();
            txb_userpass.Clear();
            txb_userinstitute.Clear();
            txb_userdate.Clear();
        
        }
        //User section ends here

        /*
         * Manufacturer section
         */ 
        private void InitManufacturer()
        {
            manufacs = new ObservableCollection<Manufacturer>();
            manufacs = populateDropdownsFromDb.populateManufacturers();
            grid_manufac.ItemsSource = manufacs;
        }

        private void grid_manufac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_manufac.SelectedItems.Count > 0)
            {
                int index = grid_manufac.SelectedIndex;
                Manufacturer selectec_manu = new Manufacturer();
                selectec_manu = manufacs[grid_manufac.SelectedIndex];
                txb_manufac_id.Text = selectec_manu.Id.ToString();
                txb_manufac.Text = selectec_manu.Name;
            }
        }

        private void btns_manu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btn_manu_add" && txb_manufac.Text != "")
            {
                MySqlCommand cmd_manu_add = new MySqlCommand();
                cmd_manu_add.CommandText = "insert into catalystmanufacturer(manufacturer_name) values (@name)";
                cmd_manu_add.Parameters.AddWithValue("@name", txb_manufac.Text);
                dbconnection.Insert(cmd_manu_add);

                manufacs = populateDropdownsFromDb.populateManufacturers();
                grid_manufac.ItemsSource = manufacs;
                ClearManu();

            }
            if (btn.Name == "btn_manu_update" && txb_manufac_id.Text != "")
            {
                MySqlCommand cmd_manu_update = new MySqlCommand();
                cmd_manu_update.CommandText = "update catalystmanufacturer set manufacturer_name = @name where manufacturer_id = @id";
                cmd_manu_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_manufac_id.Text));
                cmd_manu_update.Parameters.AddWithValue("@name", txb_manufac.Text);
                dbconnection.Update(cmd_manu_update);

                manufacs = populateDropdownsFromDb.populateManufacturers();
                grid_manufac.ItemsSource = manufacs;
                ClearManu();
            }
            if (btn.Name == "btn_manu_clear")
            {
                ClearManu();

            }
            if (btn.Name == "btn_manu_delete" && txb_manufac_id.Text != "")
            {
                MySqlCommand cmd_manu_delete = new MySqlCommand();
                cmd_manu_delete.CommandText = "delete from catalystmanufacturer where manufacturer_id = @id";
                cmd_manu_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_manufac_id.Text));
                dbconnection.Delete(cmd_manu_delete);

                manufacs = populateDropdownsFromDb.populateManufacturers();
                grid_manufac.ItemsSource = manufacs;
                ClearManu();
            }
        }

        private void ClearManu()
        {
            txb_manufac.Clear();
            txb_manufac_id.Clear();
        }

        //Manufacturer section ends here


        /*
         * Monolith Section
         */ 
        private void InitMononlith()
        {
            monoliths = new ObservableCollection<MonolithMaterial>();
            monoliths = populateDropdownsFromDb.populateMonolithMaterials();
            grid_monolith.ItemsSource = monoliths;
        }

        private void grid_monolith_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_monolith.SelectedItems.Count > 0)
            {
                int index = grid_monolith.SelectedIndex;
                MonolithMaterial a = new MonolithMaterial();
                a = (MonolithMaterial)grid_monolith.SelectedItems[0];
                txb_monolith_id.Text = a.Id.ToString();
                txb_monolith.Text = a.Material;
            }
        }

        private void btns_mono_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_mono_add" && txb_monolith.Text != "" && txb_monolith.Text != null)
            {
                MySqlCommand cmd_mon_add = new MySqlCommand();
                cmd_mon_add.CommandText = "insert into monolithmaterial (monolith_material) values (@material)";
                cmd_mon_add.Parameters.AddWithValue("@material", txb_monolith.Text);
                dbconnection.Insert(cmd_mon_add);

                monoliths = populateDropdownsFromDb.populateMonolithMaterials();
                grid_monolith.ItemsSource = monoliths;
                ClearMono();
            }

            if (btn.Name == "btn_mono_update" && txb_monolith_id.Text != "")
            {
                MySqlCommand cmd_mono_update = new MySqlCommand();
                cmd_mono_update.CommandText = "update monolithmaterial set monolithmaterial = @material where monolith_material_id = @id";
                cmd_mono_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_monolith_id.Text));
                cmd_mono_update.Parameters.AddWithValue("@material", txb_monolith.Text);
                dbconnection.Update(cmd_mono_update);

                monoliths = populateDropdownsFromDb.populateMonolithMaterials();
                grid_monolith.ItemsSource = monoliths;
                ClearMono();
            }

            if (btn.Name == "btn_mono_clear")
            {
                ClearMono();
            }
            if (btn.Name == "btn_mono_delete")
            {
                MySqlCommand cmd_mono_delete = new MySqlCommand();
                cmd_mono_delete.CommandText = "delete from monolithmaterial where monolith_material_id = @id";
                cmd_mono_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_monolith_id.Text));
                dbconnection.Delete(cmd_mono_delete);

                monoliths = populateDropdownsFromDb.populateMonolithMaterials();
                grid_monolith.ItemsSource = monoliths;
                ClearMono();
            }
        }

        private void ClearMono()
        {
            txb_monolith_id.Clear();
            txb_monolith.Clear();
        }

        private void InitShape()
        {
            shapes = new ObservableCollection<BoundaryShape>();
            shapes = populateDropdownsFromDb.populateBoundaryShapes();
            grid_boundary.ItemsSource = shapes;
        }


        private void grid_boundary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_boundary.SelectedItems.Count > 0)
            {
                int index = grid_boundary.SelectedIndex;
                BoundaryShape selectedShape = new BoundaryShape();
                selectedShape = shapes[grid_boundary.SelectedIndex];
                txb_shape_id.Text = selectedShape.Id.ToString();
                txb_shape.Text = selectedShape.Shape;
            }
        }

        private void btns_shape_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_shape_add" && txb_monolith.Text != "" && txb_monolith.Text != null)
            {
                MySqlCommand cmd_mon_add = new MySqlCommand();
                cmd_mon_add.CommandText = "insert into monolithmaterial (monolith_material) values (@material)";
                cmd_mon_add.Parameters.AddWithValue("@material", txb_monolith.Text);
                dbconnection.Insert(cmd_mon_add);

                monoliths = populateDropdownsFromDb.populateMonolithMaterials();
                grid_monolith.ItemsSource = monoliths;
                ClearShape();
            }

            if (btn.Name == "btn_shape_update" && txb_monolith_id.Text != "")
            {
                MySqlCommand cmd_mono_update = new MySqlCommand();
                cmd_mono_update.CommandText = "update monolithmaterial set monolithmaterial = @material where monolith_material_id = @id";
                cmd_mono_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_monolith_id.Text));
                cmd_mono_update.Parameters.AddWithValue("@material", txb_monolith.Text);
                dbconnection.Update(cmd_mono_update);

                monoliths = populateDropdownsFromDb.populateMonolithMaterials();
                grid_monolith.ItemsSource = monoliths;
                ClearShape();
            }

            if (btn.Name == "btn_shape_clear")
            {
                ClearShape();
            }
            if (btn.Name == "btn_shape_delete")
            {
                MySqlCommand cmd_mono_delete = new MySqlCommand();
                cmd_mono_delete.CommandText = "delete from monolithmaterial where monolith_material_id = @id";
                cmd_mono_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_monolith_id.Text));
                dbconnection.Delete(cmd_mono_delete);

                monoliths = populateDropdownsFromDb.populateMonolithMaterials();
                grid_monolith.ItemsSource = monoliths;
                ClearShape();
            }

        }

        private void ClearShape()
        {
            txb_shape_id.Clear();
            txb_shape.Clear();
        }

    }
}
