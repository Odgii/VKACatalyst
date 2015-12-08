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
        private ObservableCollection<ApplicationField> appfields;
        private ObservableCollection<AgingProcedure> agingprocedures;
        private ObservableCollection<WashcoatCatalyticComposition> washcoats;
        private ObservableCollection<Emission> emissions;
        private ObservableCollection<TransientLegislation> transients;
        private ObservableCollection<SteadyStateLegislation> steadys;
        private ObservableCollection<ModelType> modeltypes;
        private ObservableCollection<SimulationTool> tools;
  //      private ObservableCollection<AgingProcedure> agingprocedures;
  //      private ObservableCollection<AgingProcedure> agingprocedures;


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
            InitApplicationFields();
            InitAgingProcedures();
            InitWashcoats();
            InitEmissions();
            InitTransients();
            InitSteadys();
            InitModelTypes();
            InitTools();
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

        /*
         * Shape Section
         */ 
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

            if (btn.Name == "btn_shape_add" && txb_shape.Text != "" )
            {
                MySqlCommand cmd_shape_add = new MySqlCommand();
                cmd_shape_add.CommandText = "insert into substrateboundaryshape (shape) values (@shape)";
                cmd_shape_add.Parameters.AddWithValue("@shape", txb_shape.Text);
                dbconnection.Insert(cmd_shape_add);

                shapes = populateDropdownsFromDb.populateBoundaryShapes();
                grid_boundary.ItemsSource = shapes;
                ClearShape();
            }

            if (btn.Name == "btn_shape_update" && txb_shape_id.Text != "")
            {
                MySqlCommand cmd_shape_update = new MySqlCommand();
                cmd_shape_update.CommandText = "update substrateboundaryshape set shape = @shape where shape_id = @id";
                cmd_shape_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_shape_id.Text));
                cmd_shape_update.Parameters.AddWithValue("@shape", txb_shape.Text);
                dbconnection.Update(cmd_shape_update);

                shapes = populateDropdownsFromDb.populateBoundaryShapes();
                grid_boundary.ItemsSource = shapes;
                ClearShape();
            }

            if (btn.Name == "btn_shape_clear")
            {
                ClearShape();
            }
            if (btn.Name == "btn_shape_delete" && txb_shape_id.Text != "")
            {
                MySqlCommand cmd_shape_delete = new MySqlCommand();
                cmd_shape_delete.CommandText = "delete from substrateboundaryshape where shape_id = @id";
                cmd_shape_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_shape_id.Text));
                dbconnection.Delete(cmd_shape_delete);

                shapes = populateDropdownsFromDb.populateBoundaryShapes();
                grid_boundary.ItemsSource = shapes;
                ClearShape();
            }

        }

        private void ClearShape()
        {
            txb_shape_id.Clear();
            txb_shape.Clear();
        }

        //shape section ends here

        /*
         * Application Field Section
         */ 

        private void InitApplicationFields()
        {
            appfields = new ObservableCollection<ApplicationField>();
            appfields = populateDropdownsFromDb.populateApplicationFields();
            grid_appfields.ItemsSource = appfields;
        }

        private void grid_appfields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_appfields.SelectedItems.Count > 0)
            {
                ApplicationField selectedApp = new ApplicationField();
                selectedApp = appfields[grid_appfields.SelectedIndex];
                txb_appfield_id.Text = selectedApp.Id.ToString();
                txb_appfield.Text = selectedApp.Name;
            }
        }

        private void btn_app_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_app_add" && txb_appfield.Text != "")
            {
                MySqlCommand cmd_app_add = new MySqlCommand();
                cmd_app_add.CommandText = "insert into applicationfield (app_field) values (@app_field)";
                cmd_app_add.Parameters.AddWithValue("@app_field", txb_appfield.Text);
                dbconnection.Insert(cmd_app_add);

                appfields = populateDropdownsFromDb.populateApplicationFields();
                grid_appfields.ItemsSource = appfields;
                ClearAppFields();
            }

            if (btn.Name == "btn_app_update" && txb_appfield_id.Text != "")
            {
                MySqlCommand cmd_app_update = new MySqlCommand();
                cmd_app_update.CommandText = "update applicationfield set app_field = @app_field where app_field_id = @id";
                cmd_app_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_appfield_id.Text));
                cmd_app_update.Parameters.AddWithValue("@app_field", txb_appfield.Text);
                dbconnection.Update(cmd_app_update);

                appfields = populateDropdownsFromDb.populateApplicationFields();
                grid_appfields.ItemsSource = appfields;
                ClearAppFields();
            }

            if (btn.Name == "btn_app_clear")
            {
                ClearAppFields();
            }
            if (btn.Name == "btn_app_delete" && txb_appfield_id.Text != "")
            {
                MySqlCommand cmd_app_delete = new MySqlCommand();
                cmd_app_delete.CommandText = "delete from applicationfield where app_field_id = @id";
                cmd_app_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_appfield_id.Text));
                dbconnection.Delete(cmd_app_delete);

                appfields = populateDropdownsFromDb.populateApplicationFields();
                grid_appfields.ItemsSource = appfields;
                ClearAppFields();
            }
        }

        private void ClearAppFields()
        {
            txb_appfield.Clear();
            txb_appfield_id.Clear();
        }

        //application field section ends here

        /*
         * Aging Procedure Section
         */ 
        private void InitAgingProcedures()
        {
            agingprocedures = new ObservableCollection<AgingProcedure>();
            agingprocedures = populateDropdownsFromDb.populateAgingProcedures();
            grid_procedures.ItemsSource = agingprocedures;
        }

        private void grid_procedures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_procedures.SelectedItems.Count > 0)
            {
                AgingProcedure selectedProcedure = new AgingProcedure();
                selectedProcedure = agingprocedures[grid_procedures.SelectedIndex];
                txb_proc_id.Text = selectedProcedure.Id.ToString();
                txb_proc.Text = selectedProcedure.Procedure;
            }
        }

        private void btns_proc_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_proc_add" && txb_proc.Text != "")
            {
                MySqlCommand cmd_proc_add = new MySqlCommand();
                cmd_proc_add.CommandText = "insert into agingprocedure (aging_procedure) values (@aging_procedure)";
                cmd_proc_add.Parameters.AddWithValue("@aging_procedure", txb_proc.Text);
                dbconnection.Insert(cmd_proc_add);

                agingprocedures = populateDropdownsFromDb.populateAgingProcedures();
                grid_procedures.ItemsSource = agingprocedures;
                ClearAging();
            }

            if (btn.Name == "btn_proc_update" && txb_proc_id.Text != "")
            {
                MySqlCommand cmd_proc_update = new MySqlCommand();
                cmd_proc_update.CommandText = "update agingprocedure set aging_procedure = @aging_procedure where aging_procedure_id = @id";
                cmd_proc_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_proc_id.Text));
                cmd_proc_update.Parameters.AddWithValue("@aging_procedure", txb_proc.Text);
                dbconnection.Update(cmd_proc_update);

                agingprocedures = populateDropdownsFromDb.populateAgingProcedures();
                grid_procedures.ItemsSource = agingprocedures;
                ClearAging();
            }

            if (btn.Name == "btn_proc_clear")
            {
                ClearAging();
            }
            if (btn.Name == "btn_proc_delete" && txb_proc_id.Text != "")
            {
                MySqlCommand cmd_proc_delete = new MySqlCommand();
                cmd_proc_delete.CommandText = "delete from agingprocedure where aging_procedure_id = @id";
                cmd_proc_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_proc_id.Text));
                dbconnection.Delete(cmd_proc_delete);

                agingprocedures = populateDropdownsFromDb.populateAgingProcedures();
                grid_procedures.ItemsSource = agingprocedures;
                ClearAging();
            }

        }

        private void ClearAging()
        {
            txb_proc.Clear();
            txb_proc_id.Clear();
        }
        //aging procedure section ends here

        /*
         * Washcoats Section
         */ 

        private void InitWashcoats()
        {
            washcoats = new ObservableCollection<WashcoatCatalyticComposition>();
            washcoats = populateDropdownsFromDb.populateWashcoats();
            grid_washcoats.ItemsSource = washcoats;
            
        }
        private void grid_washcoats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_washcoats.SelectedItems.Count > 0)
            {
                WashcoatCatalyticComposition selectedWashcoat = new WashcoatCatalyticComposition();
                selectedWashcoat = washcoats[grid_washcoats.SelectedIndex];
                txb_washcoat_id.Text = selectedWashcoat.Id.ToString();
                txb_washcoat.Text = selectedWashcoat.WashcoatValue;
                chk_washcoat_metal.IsChecked = selectedWashcoat.NeedPreciousMetal;
            }
        }

        private void btns_washcoat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_washcoat_add" && txb_washcoat.Text != "")
            {
                MySqlCommand cmd_washcoat_add = new MySqlCommand();
                cmd_washcoat_add.CommandText = "insert into washcoat_material (material, has_precious_metal) values (@material, @has_precious_metal)";
                cmd_washcoat_add.Parameters.AddWithValue("@material", txb_washcoat.Text);
                cmd_washcoat_add.Parameters.AddWithValue("@has_precious_metal", chk_washcoat_metal.IsChecked);
                dbconnection.Insert(cmd_washcoat_add);

                washcoats = populateDropdownsFromDb.populateWashcoats();
                grid_washcoats.ItemsSource = washcoats;
                ClearWashcoats();
            }

            if (btn.Name == "btn_washcoat_update" && txb_washcoat_id.Text != "")
            {
                MySqlCommand cmd_washcoat_update = new MySqlCommand();
                cmd_washcoat_update.CommandText = "update washcoat_material set material = @material, has_precious_metal = @has_precious_metal where material_id = @id";
                cmd_washcoat_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_washcoat_id.Text));
                cmd_washcoat_update.Parameters.AddWithValue("@material", txb_washcoat.Text);
                cmd_washcoat_update.Parameters.AddWithValue("@has_precious_metal", chk_washcoat_metal.IsChecked);
                dbconnection.Update(cmd_washcoat_update);

                washcoats = populateDropdownsFromDb.populateWashcoats();
                grid_washcoats.ItemsSource = washcoats;
                ClearWashcoats();
            }

            if (btn.Name == "btn_washcoat_clear")
            {
                ClearWashcoats();
            }
            if (btn.Name == "btn_washcoat_delete" && txb_washcoat_id.Text != "")
            {
                MySqlCommand cmd_washcoat_delete = new MySqlCommand();
                cmd_washcoat_delete.CommandText = "delete from washcoat_material where material_id = @id";
                cmd_washcoat_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_washcoat_id.Text));
                dbconnection.Delete(cmd_washcoat_delete);

                washcoats = populateDropdownsFromDb.populateWashcoats();
                grid_washcoats.ItemsSource = washcoats;
                ClearWashcoats();
            }
        }

        private void ClearWashcoats()
        {
            txb_washcoat_id.Clear();
            txb_washcoat.Clear();
            chk_washcoat_metal.IsChecked = false;
        
        }
        //washcoat section ends here

        /*
         * Emission Section
         */ 
        private void InitEmissions()
        {
            emissions = new ObservableCollection<Emission>();
            emissions = populateDropdownsFromDb.populateEmissions();
            grid_emissions.ItemsSource = emissions;
        }


        private void grid_emissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_emissions.SelectedItems.Count > 0)
            {
                Emission selectedEmission = new Emission();
                selectedEmission = emissions[grid_emissions.SelectedIndex];
                txb_emission_id.Text = selectedEmission.Id.ToString();
                txb_emission.Text = selectedEmission.Name;
            }
        }

        private void btns_emission_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_emission_add" && txb_emission.Text != "")
            {
                MySqlCommand cmd_emission_add = new MySqlCommand();
                cmd_emission_add.CommandText = "insert into emission (emission) values (@emission)";
                cmd_emission_add.Parameters.AddWithValue("@emission", txb_emission.Text);
                dbconnection.Insert(cmd_emission_add);

                emissions = populateDropdownsFromDb.populateEmissions();
                grid_emissions.ItemsSource = emissions;
                ClearEmission();
            }

            if (btn.Name == "btn_emission_update" && txb_emission_id.Text != "")
            {
                MySqlCommand cmd_emission_update = new MySqlCommand();
                cmd_emission_update.CommandText = "update emission set emission = @emission where emission_id = @id";
                cmd_emission_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_emission_id.Text));
                cmd_emission_update.Parameters.AddWithValue("@emission", txb_emission.Text);
                dbconnection.Update(cmd_emission_update);

                emissions = populateDropdownsFromDb.populateEmissions();
                grid_emissions.ItemsSource = emissions;
                ClearEmission();
            }

            if (btn.Name == "btn_emission_clear")
            {
                ClearEmission();
            }
            if (btn.Name == "btn_emission_delete" && txb_emission_id.Text != "")
            {
                MySqlCommand cmd_emission_delete = new MySqlCommand();
                cmd_emission_delete.CommandText = "delete from emission where emission_id = @id";
                cmd_emission_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_emission_id.Text));
                dbconnection.Delete(cmd_emission_delete);

                emissions = populateDropdownsFromDb.populateEmissions();
                grid_emissions.ItemsSource = emissions;
                ClearEmission();
            }
        }

        private void ClearEmission()
        {
            txb_emission.Clear();
            txb_emission_id.Clear();
        }
        //emission section ends here

        /*
         * Transient Section
         */ 
        private void InitTransients()
        {
            transients = new ObservableCollection<TransientLegislation>();
            transients = populateDropdownsFromDb.populateTransientLegislations();
            grid_transients.ItemsSource = transients;
        }


        private void grid_transients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_transients.SelectedItems.Count > 0)
            {
                TransientLegislation selectedTransient = new TransientLegislation();
                selectedTransient = transients[grid_transients.SelectedIndex];
                txb_transient_id.Text = selectedTransient.Id.ToString();
                txb_transient.Text = selectedTransient.Legislation;
            }

        }

        private void btns_transient_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_transient_add" && txb_transient.Text != "")
            {
                MySqlCommand cmd_transient_add = new MySqlCommand();
                cmd_transient_add.CommandText = "insert into transientlegislationcycle (cycle_certificate) values (@cycle_certificate)";
                cmd_transient_add.Parameters.AddWithValue("@cycle_certificate", txb_transient.Text);
                dbconnection.Insert(cmd_transient_add);

                transients = populateDropdownsFromDb.populateTransientLegislations();
                grid_transients.ItemsSource = transients;
                ClearTransient();
            }

            if (btn.Name == "btn_transient_update" && txb_transient_id.Text != "")
            {
                MySqlCommand cmd_transient_update = new MySqlCommand();
                cmd_transient_update.CommandText = "update transientlegislationcycle set cycle_certificate = @cycle_certificate where transient_id = @id";
                cmd_transient_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_transient_id.Text));
                cmd_transient_update.Parameters.AddWithValue("@cycle_certificate", txb_transient.Text);
                dbconnection.Update(cmd_transient_update);

                transients = populateDropdownsFromDb.populateTransientLegislations();
                grid_transients.ItemsSource = transients;
                ClearTransient();
            }

            if (btn.Name == "btn_transient_clear")
            {
                ClearTransient();
            }
            if (btn.Name == "btn_transient_delete" && txb_transient_id.Text != "")
            {
                MySqlCommand cmd_transient_delete = new MySqlCommand();
                cmd_transient_delete.CommandText = "delete from transientlegislationcycle where transient_id = @id";
                cmd_transient_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_transient_id.Text));
                dbconnection.Delete(cmd_transient_delete);

                transients = populateDropdownsFromDb.populateTransientLegislations();
                grid_transients.ItemsSource = transients;
                ClearTransient();
            }

        }

        private void ClearTransient()
        {
            txb_transient.Clear();
            txb_transient_id.Clear();
        }
        //transient section ends here

        /*
         * Steady Section
         */ 
        private void InitSteadys()
        {
            steadys = new ObservableCollection<SteadyStateLegislation>();
            steadys = populateDropdownsFromDb.populateSteadyStateLegislations();
            grid_steadys.ItemsSource = steadys;
        }

        private void grid_steadys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_steadys.SelectedItems.Count > 0)
            {
                SteadyStateLegislation selectedSteady = new SteadyStateLegislation();
                selectedSteady = steadys[grid_steadys.SelectedIndex];
                txb_steady_id.Text = selectedSteady.Id.ToString();
                txb_steady.Text = selectedSteady.Legislation;
            }

        }

        private void btns_steady_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_steady_add" && txb_steady.Text != "")
            {
                MySqlCommand cmd_steady_add = new MySqlCommand();
                cmd_steady_add.CommandText = "insert into steadystatelegislationcycle (cycle_certificate) values (@cycle_certificate)";
                cmd_steady_add.Parameters.AddWithValue("@cycle_certificate", txb_steady.Text);
                dbconnection.Insert(cmd_steady_add);

                steadys = populateDropdownsFromDb.populateSteadyStateLegislations();
                grid_steadys.ItemsSource = steadys;
                ClearSteady();
            }

            if (btn.Name == "btn_steady_update" && txb_steady_id.Text != "")
            {
                MySqlCommand cmd_steady_update = new MySqlCommand();
                cmd_steady_update.CommandText = "update steadystatelegislationcycle set cycle_certificate = @cycle_certificate where steady_state_id = @id";
                cmd_steady_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_steady_id.Text));
                cmd_steady_update.Parameters.AddWithValue("@cycle_certificate", txb_steady.Text);
                dbconnection.Update(cmd_steady_update);

                steadys = populateDropdownsFromDb.populateSteadyStateLegislations();
                grid_steadys.ItemsSource = steadys;
                ClearSteady();
            }

            if (btn.Name == "btn_steady_clear")
            {
                ClearSteady();
            }
            if (btn.Name == "btn_steady_delete" && txb_steady_id.Text != "")
            {
                MySqlCommand cmd_steady_delete = new MySqlCommand();
                cmd_steady_delete.CommandText = "delete from steadystatelegislationcycle where steady_state_id = @id";
                cmd_steady_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_steady_id.Text));
                dbconnection.Delete(cmd_steady_delete);

                steadys = populateDropdownsFromDb.populateSteadyStateLegislations();
                grid_steadys.ItemsSource = steadys;
                ClearSteady();
            }

        }

        private void ClearSteady()
        {
            txb_steady_id.Clear();
            txb_steady.Clear();
        }
        //steady section ends here

        /*
         * ModelType Section
         */ 
        private void InitModelTypes()
        {
            modeltypes = new ObservableCollection<ModelType>();
            modeltypes = populateDropdownsFromDb.populateModelTypes();
            grid_modeltypes.ItemsSource = modeltypes;
        }

        private void grid_modeltypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_modeltypes.SelectedItems.Count > 0)
            {
                ModelType selectedModel = new ModelType();
                selectedModel = modeltypes[grid_modeltypes.SelectedIndex];
                txb_modeltype_id.Text = selectedModel.Id.ToString();
                txb_modeltype.Text = selectedModel.Type;
            }
        }

        private void btns_modeltype_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_modeltype_add" && txb_modeltype.Text != "")
            {
                MySqlCommand cmd_modeltype_add = new MySqlCommand();
                cmd_modeltype_add.CommandText = "insert into modeltype (model_type) values (@modeltype)";
                cmd_modeltype_add.Parameters.AddWithValue("@modeltype", txb_modeltype.Text);
                dbconnection.Insert(cmd_modeltype_add);

                modeltypes = populateDropdownsFromDb.populateModelTypes();
                grid_modeltypes.ItemsSource = modeltypes;
                ClearModelType();
            }

            if (btn.Name == "btn_modeltype_update" && txb_modeltype_id.Text != "")
            {
                MySqlCommand cmd_modeltype_update = new MySqlCommand();
                cmd_modeltype_update.CommandText = "update modeltype set model_type = @modeltype where model_type_id = @id";
                cmd_modeltype_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_modeltype_id.Text));
                cmd_modeltype_update.Parameters.AddWithValue("@modeltype", txb_modeltype.Text);
                dbconnection.Update(cmd_modeltype_update);

                modeltypes = populateDropdownsFromDb.populateModelTypes();
                grid_modeltypes.ItemsSource = modeltypes;
                ClearModelType();
            }

            if (btn.Name == "btn_modeltype_clear")
            {
                ClearModelType();
            }
            if (btn.Name == "btn_modeltype_delete" && txb_modeltype_id.Text != "")
            {
                MySqlCommand cmd_modeltype_delete = new MySqlCommand();
                cmd_modeltype_delete.CommandText = "delete from modeltype where model_type_id = @id";
                cmd_modeltype_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_modeltype_id.Text));
                dbconnection.Delete(cmd_modeltype_delete);

                modeltypes = populateDropdownsFromDb.populateModelTypes();
                grid_modeltypes.ItemsSource = modeltypes;
                ClearModelType();
            }
        }

        private void ClearModelType()
        {
            txb_modeltype_id.Clear();
            txb_modeltype.Clear();
        }
        //modeltype section ends here

        /*
         * Simulation Tool Section
         */ 
        private void InitTools()
        {
            tools = new ObservableCollection<SimulationTool>();
            tools = populateDropdownsFromDb.populateSimulationTools();
            grid_tools.ItemsSource = tools;
        }

        private void grid_tools_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid_tools.SelectedItems.Count > 0)
            {
                SimulationTool selectedTool = new SimulationTool();
                selectedTool = tools[grid_tools.SelectedIndex];
                txb_tool_id.Text = selectedTool.Id.ToString();
                txb_tool.Text = selectedTool.Tool;
            }
        }

        private void btns_tool_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "btn_tool_add" && txb_tool.Text != "")
            {
                MySqlCommand cmd_tool_add = new MySqlCommand();
                cmd_tool_add.CommandText = "insert into simulationtool (simulation_tool) values (@simulation_tool)";
                cmd_tool_add.Parameters.AddWithValue("@simulation_tool", txb_tool.Text);
                dbconnection.Insert(cmd_tool_add);

                tools = populateDropdownsFromDb.populateSimulationTools();
                grid_tools.ItemsSource = tools;
                ClearTool();
            }

            if (btn.Name == "btn_tool_update" && txb_tool_id.Text != "")
            {
                MySqlCommand cmd_tool_update = new MySqlCommand();
                cmd_tool_update.CommandText = "update simulationtool set simulation_tool = @simulation_tool where tool_id = @id";
                cmd_tool_update.Parameters.AddWithValue("@id", Convert.ToInt32(txb_tool_id.Text));
                cmd_tool_update.Parameters.AddWithValue("@simulation_tool", txb_tool.Text);
                dbconnection.Update(cmd_tool_update);

                tools = populateDropdownsFromDb.populateSimulationTools();
                grid_tools.ItemsSource = tools;
                ClearTool();
            }

            if (btn.Name == "btn_tool_clear")
            {
                ClearTool();
            }
            if (btn.Name == "btn_tool_delete" && txb_tool_id.Text != "")
            {
                MySqlCommand cmd_tool_delete = new MySqlCommand();
                cmd_tool_delete.CommandText = "delete from simulationtool where tool_id = @id";
                cmd_tool_delete.Parameters.AddWithValue("@id", Convert.ToInt32(txb_tool_id.Text));
                dbconnection.Delete(cmd_tool_delete);

                tools = populateDropdownsFromDb.populateSimulationTools();
                grid_tools.ItemsSource = tools;
                ClearTool();
            }

        }

        private void ClearTool()
        {
            txb_tool_id.Clear();
            txb_tool.Clear();
        }
        //simulation tool section ends here
    }
}
