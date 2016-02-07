using catalyst_project.Database;
using catalyst_project.Model;
using catalyst_project.UIComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace catalyst_project.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        PopulateDropdownsFromDB dropdowns;
        ObservableCollection<CustomComboBoxItem> forGeneralInformation { get; set; }
        ObservableCollection<CustomComboBoxItem> forCharacterization { get; set; }
        ObservableCollection<CustomComboBoxItem> forSimulation { get; set; }
        ObservableCollection<CustomComboBoxItem> forTestbenchandVehicle { get; set; }
        ObservableCollection<CustomComboBoxItem> forChemPhys { get; set; }
        Array arrManufacturers ;
        Array arrWashcoats ;
        Array arrMonolithMaterials ;
        Array arrCatalystTypes ;
        Array arrAgingProcedures ;
        Array arrAgingStatuses ;
        Array arrBoundaryShapes ;
        Array arrModelTypes;
        Array arrSimulationTools;
        Array arrSteadyStateLegislations;
        Array arrTransientLegislations ;
        Array arrApplicationFieldTestbenchs ;
        Array arrPreciousMetalLoadings;
        Array arrCristallineWashcoatComponentFunctions;
        Array arrApplicationFields;
        Array arrApplicationFieldsTestbench;
        Array arrSourceOfDatas;
        Array arrSourceOfMeasurements;
        Array arrCheckBox;

        Hashtable collections = new Hashtable();
        public SearchWindow()
        {
            InitializeComponent();
            InitializeDropdowns();
            InitializeCollections();
        }

        public void InitializeDropdowns()
        {
            dropdowns = new PopulateDropdownsFromDB();

            ObservableCollection<Manufacturer> Manufacturers = dropdowns.populateManufacturers();
            arrManufacturers = Manufacturers.Cast<DBModel>().ToArray();

            ObservableCollection<SteadyStateLegislation> SteadyStateLegislations = dropdowns.populateSteadyStateLegislations();
            arrSteadyStateLegislations = SteadyStateLegislations.Cast<DBModel>().ToArray();

            ObservableCollection<SourceOfData> SourceOfDatas = dropdowns.populateSourceOfDatas();
            arrSourceOfDatas = SourceOfDatas.Cast<DBModel>().ToArray();

            ObservableCollection<SourceOfMeasurement> SourceOfMeasurements = dropdowns.populateSourceOfMeasurements();
            arrSourceOfMeasurements = SourceOfMeasurements.Cast<DBModel>().ToArray();

            ObservableCollection<TransientLegislation> TransientLegislations = dropdowns.populateTransientLegislations();
            arrTransientLegislations = TransientLegislations.Cast<DBModel>().ToArray();

            ObservableCollection<ModelType>  ModelTypes = dropdowns.populateModelTypes();
            arrModelTypes = ModelTypes.Cast<DBModel>().ToArray();

            ObservableCollection<SimulationTool>  SimulationTools = dropdowns.populateSimulationTools();
            arrSimulationTools = SimulationTools.Cast<DBModel>().ToArray();

            ObservableCollection<WashcoatCatalyticComposition> Washcoats = dropdowns.populateWashcoats();
            arrWashcoats = Washcoats.Cast<DBModel>().ToArray();

            ObservableCollection<MonolithMaterial>  MonolithMaterials = dropdowns.populateMonolithMaterials();
            arrMonolithMaterials = MonolithMaterials.Cast<DBModel>().ToArray();

            ObservableCollection<CatalystType> CatalystTypes = dropdowns.populateCatalystTypes();
            arrCatalystTypes = CatalystTypes.Cast<DBModel>().ToArray();

            ObservableCollection<AgingProcedure> AgingProcedures = dropdowns.populateAgingProcedures();
            arrAgingProcedures =  AgingProcedures.Cast<DBModel>().ToArray();


            ObservableCollection<AgingStatus> AgingStatuses = dropdowns.populateAgingStatuses();
            arrAgingStatuses = AgingStatuses.Cast<DBModel>().ToArray();

            ObservableCollection<BoundaryShape> BoundaryShapes = dropdowns.populateBoundaryShapes();
            arrBoundaryShapes = BoundaryShapes.Cast<DBModel>().ToArray();

            ObservableCollection<ApplicationField> ApplicationFields = dropdowns.populateApplicationFields();
            arrApplicationFields = ApplicationFields.Cast<DBModel>().ToArray();

            ObservableCollection<ApplicationField> ApplicationFieldsTestbenchs = dropdowns.populateApplicationFields();
            arrApplicationFieldsTestbench = ApplicationFieldsTestbenchs.Cast<DBModel>().ToArray();

            ObservableCollection<PreciousMetalLoading>  PreciousMetalLoadings = dropdowns.populatePreciousMetalLoadings();
            arrPreciousMetalLoadings = PreciousMetalLoadings.Cast<DBModel>().ToArray();


            ObservableCollection<CristallineWashcoatComponentFunction> CristallineWashcoatComponentFunctions = dropdowns.populateCristallineWashcoatComponentFunctions();
            arrCristallineWashcoatComponentFunctions = CristallineWashcoatComponentFunctions.Cast<DBModel>().ToArray();

            ObservableCollection<CheckBoxValues> CheckBoxVals = new ObservableCollection<CheckBoxValues>();
            CheckBoxVals.Add(new CheckBoxValues("Yes"));
            CheckBoxVals.Add(new CheckBoxValues("No"));
            arrCheckBox = CheckBoxVals.Cast<DBModel>().ToArray();
        }

        public void InitializeCollections()
        {

        

            forGeneralInformation = new ObservableCollection<CustomComboBoxItem>();
            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst_ID", "Catalyst", "catalyst_id",true, true, null, false,false,false, null,null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst Data is Approved", "Catalyst", "is_approved", false, false, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forGeneralInformation.Add(new CustomComboBoxItem("Confidentiality", "Catalyst", "is_data_available", false, false, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));

            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst Type", "Catalyst", "catalyst_type_id", false, false, null, false, true, false, "Id","Type", arrCatalystTypes));
            forGeneralInformation.Add(new CustomComboBoxItem("Manufacturer", "GeneralInformation", "manufacturer_id", false, false, null, false, true, false, "Id", "Name", arrManufacturers));
            forGeneralInformation.Add(new CustomComboBoxItem("Production Date", "GeneralInformation", "production_date", false, false, null, true, false, false, null, null, null));
            
            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst Nr#", "GeneralInformation", "catalyst_number", true, true, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Substract Nr#", "GeneralInformation", "substract_number", true, true, null, false, false, false, null, null, null));

            forGeneralInformation.Add(new CustomComboBoxItem("Project Nr#", "GeneralInformation", "project_number", true, true, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Customer", "GeneralInformation", "customer", true, false, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Project Manager", "GeneralInformation", "project_manager", true, false, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("EATS Case Worker", "GeneralInformation", "eats_case_worker", true, false, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Target Country", "GeneralInformation", "target_country", true, false, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Target Emission Legislation", "GeneralInformation", "target_emission_legislation", true, false, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Field Of Application", "GeneralInformation", "app_field_id", false, false, null, false, true, false, "Id", "Name", arrApplicationFields));
            forGeneralInformation.Add(new CustomComboBoxItem("Target Configuration Of EATS", "GeneralInformation", "conf_target_system", true, false, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Specification Of Engine", "GeneralInformation", "specification_engine", true, false, null, false, false, false, null, null, null));

            forGeneralInformation.Add(new CustomComboBoxItem("Washcoat, Catalyc Composition", "GeneralInformation", "material_id", false, false, null, false, true, false, "Id", "WashcoatValue", arrWashcoats));
            forGeneralInformation.Add(new CustomComboBoxItem("Precious Metal Ratio", "general_washcoat", "precious_metal_ratio", true, true, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Precious Metal Loading", "general_washcoat", "precious_metal_loading", true, true, "g/l", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Washcoat Loading", "GeneralInformation", "washcoat_loading", true, true, "g/l", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Cell Density", "GeneralInformation", "cell_density", true, true, "cpsi",false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Wall Thickness", "GeneralInformation", "wall_thickness", true, true, "mil", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Substrate Boundary Shape", "GeneralInformation", "shape_id", false, false, null, false, true, false, "Id", "Shape", arrBoundaryShapes));
            forGeneralInformation.Add(new CustomComboBoxItem("Volume", "GeneralInformation", "volume", true, true, "liter", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Monolith Length", "GeneralInformation", "length", true, true, "mm",false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Monolith Diameter", "GeneralInformation", "diameter", true, true, "mm",false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Monolith Material", "GeneralInformation", "monolith_material_id", false, false, null, false, true, false, "Id", "Material", arrMonolithMaterials));
            forGeneralInformation.Add(new CustomComboBoxItem("Zone Coating", "GeneralInformation", "zone_coating", false, false, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forGeneralInformation.Add(new CustomComboBoxItem("Slip Catalyst Applied", "GeneralInformation", "slip_catalyst_applied", false, false, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Gradient Axial", "GeneralInformation", "max_temp_gradient_axial", true, true, "K", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Gradient Radial", "GeneralInformation", "max_temp_gradiend_radial", true, true, "K", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Limit Peak", "GeneralInformation", "max_temp_limitation_peak", true, true, "°C", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Limit Long Term", "GeneralInformation", "max_temp_limitation_longterm", true, true, "°C", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max HC Limit", "GeneralInformation", "max_hc_limit", true, true, "g/l", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Segmentation size X", "GeneralInformation", "segmentation_size_x", true, true, "inch", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Segmentation size Y", "GeneralInformation", "segmentation_size_y", true, true, "inch", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Pressure Loss Coefficient", "GeneralInformation", "pressue_loss_coefficient", true, true, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Soot Mass Limit", "GeneralInformation", "soot_mass_limit", true, true, "g/l", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("DPF Inlet Cell Area", "GeneralInformation", "dpf_inlet_cell", true, true, "mm²", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("DPF Outlet Cell Area", "GeneralInformation", "dpf_outlet_cell", true, true, "mm²", false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Aging Status", "GeneralInformation", "aging_status_id", false, false, null, false, true, false, "Id", "Status", arrAgingStatuses));
            forGeneralInformation.Add(new CustomComboBoxItem("Aging Procedure", "GeneralInformation", "aging_procedure_id", false, false, null, false, true, false, "Id", "Procedure", arrAgingProcedures));
            forGeneralInformation.Add(new CustomComboBoxItem("Aging Duration", "GeneralInformation", "aging_duration", true, true, "h", false, true, false, null, null, null));
        }

        private void cmb_tab_fields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cmb_current_row = Grid.GetRow((ComboBox)sender);
            int cmb_current_column = 4;
            ifExistThenDelete(cmb_current_row);
            ComboBox currentFields = (ComboBox) sender;
            CustomComboBoxItem item = (CustomComboBoxItem)currentFields.SelectedItem;
            if (item.IsTextBox)
            {
                if (item.IsNumber)
                {
                    ComboBox cmb = new ComboBox();
                    cmb.Width = 200;
                    cmb.Margin = new Thickness(2);
                    cmb.Name = "cmb_" + currentFields.Name;
                    Grid.SetRow(cmb, cmb_current_row);
                    Grid.SetColumn(cmb, cmb_current_column);

                    ComboBoxItem c = new ComboBoxItem();
                    c.Content = "Less Than";
                    ComboBoxItem c1 = new ComboBoxItem();
                    c1.Content = "More Than";
                    ComboBoxItem c2 = new ComboBoxItem();
                    c2.Content = "Between";
                    cmb.Items.Add(c);
                    cmb.Items.Add(c1);
                    cmb.Items.Add(c2);
                    grid_search.Children.Add(cmb);
                    cmb_current_column ++;

                    TextBox cTextBox = new CustomTextBox();
                    cTextBox.Width = 150;
                    cTextBox.Margin = new Thickness(2);
                    Grid.SetRow(cTextBox, cmb_current_row);
                    Grid.SetColumn(cTextBox, cmb_current_column);
                    grid_search.Children.Add(cTextBox);
                    cmb_current_column++;
                }

                TextBox cTextBox1 = new CustomTextBox();
                cTextBox1.Width = 200;
                cTextBox1.Margin = new Thickness(2);
                Grid.SetRow(cTextBox1, cmb_current_row);
                Grid.SetColumn(cTextBox1, cmb_current_column);
                grid_search.Children.Add(cTextBox1);
                cmb_current_column++;

                if (item.Unit != null)
                {
                    Label l = new Label();
                    l.Content = item.Unit;
                    Grid.SetRow(l, cmb_current_row);
                    Grid.SetColumn(l, cmb_current_column);
                    grid_search.Children.Add(l);                  
                }               
            }
            if (item.IsComboBox)
            {
                ComboBox cmb = new ComboBox();
                cmb.Width = 200;
                cmb.Margin = new Thickness(2);
                cmb.Name = "cmb_" + currentFields.Name;
                Grid.SetRow(cmb, cmb_current_row);
                Grid.SetColumn(cmb, cmb_current_column);
                cmb.ItemsSource = item.DBModelCollection;
                if (item.SelectedValuePath != null)
                {
                    cmb.SelectedValuePath = item.SelectedValuePath;
                }
                if (item.DisplayMemberPath != null)
                {
                    cmb.DisplayMemberPath = item.DisplayMemberPath;
                }
                grid_search.Children.Add(cmb);
            }

            /*                            <ComboBox Name="cmb_wash_cat_comp" Margin="0,3" Width="200">
                                <ComboBox.ItemTemplate>
                                    <DataTemplate>
                                        <StackPanel Orientation="Horizontal">
                                            <CheckBox IsChecked="{Binding IsChecked, Mode=TwoWay}" Width="20" />
                                            <TextBlock Text="{Binding WashcoatValue}" Width="180" />
                                        </StackPanel>
                                    </DataTemplate>
                                </ComboBox.ItemTemplate>
                            </ComboBox>
             * */
            if (item.IsDateTimePicker)
            { 
            }

        }

        private void ifExistThenDelete(int currentGenRow)
        {
            for (int i = 4; i <= 7; i++)
            {
                foreach (UIElement control in grid_search.Children)
                {
                    if (Grid.GetRow(control) == currentGenRow && Grid.GetColumn(control) == i)
                    {
                        grid_search.Children.Remove(control);
                        break;
                    }
                }
            }
        }

        private void measure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmb_tab_names_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBoxSrc = (ComboBox)sender;
            if (comboBoxSrc.Name.Contains("1"))
            {
                if (cmb_tab_names1.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields1.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names1.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields1.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names1.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields1.ItemsSource = forSimulation;
                }
                if (cmb_tab_names1.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields1.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names1.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields1.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.Contains("2"))
            {
                if (cmb_tab_names2.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields2.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names2.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields2.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names2.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields2.ItemsSource = forSimulation;
                }
                if (cmb_tab_names2.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields2.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names2.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields2.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.Contains("3"))
            {
                if (cmb_tab_names3.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields3.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names3.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields3.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names3.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields3.ItemsSource = forSimulation;
                }
                if (cmb_tab_names3.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields3.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names3.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields3.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.Contains("4"))
            {
                if (cmb_tab_names4.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields4.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names4.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields4.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names4.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields4.ItemsSource = forSimulation;
                }
                if (cmb_tab_names4.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields4.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names4.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields4.ItemsSource = forChemPhys;
                }
            }
            else 
            {
                if (cmb_tab_names.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields.ItemsSource = forSimulation;
                }
                if (cmb_tab_names.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields.ItemsSource = forChemPhys;
                }
            }

        }

        private void changeSources(object sender, String number)
        {
            String tabName = "cmb_tab_names" + number;
            String fieldName = "cmb_tab_field" + number;
            ComboBox tabs = (ComboBox)sender;
            ComboBox fields = (ComboBox)grid_search.FindName("cmb_tab_field");

        }
    }
}
