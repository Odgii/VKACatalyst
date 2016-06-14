using catalyst_project.Database;
using catalyst_project.Model;
using catalyst_project.UIComponents;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace catalyst_project.View
{
    /// <summary>
    /// Interaction logic for MainApplication.xaml
    /// </summary>
    public partial class MainApplication : Window
    {
        Converter converter;
        PopulateDropdownsFromDB dropdowns;
        List<StackPanel> listOfFieldsToUpdateGeneral = new List<StackPanel>();
        List<StackPanel> listOfFieldsForSteadyLegislation = new List<StackPanel>();
        List<StackPanel> listOfFieldsForTransientLegislation = new List<StackPanel>();
        List<StackPanel> listOfFieldsForChem = new List<StackPanel>();

        List<FileToUpload> genFilesToCopy = new List<FileToUpload>();
        List<FileToUpload> lgbFilesToCopy = new List<FileToUpload>();
        List<FileToUpload> simuFilesToCopy = new List<FileToUpload>();
        List<FileToUpload> testFilesToCopy = new List<FileToUpload>();
        List<FileToUpload> chemFilesToCopy = new List<FileToUpload>();

        List<int> firstKind = new List<int> { 1, 7, 5, 2 };
        List<int> secondKind = new List<int> { 7, 2 };
        List<int> thirdKind = new List<int> { 3, 4 };
        List<int> fifthkind = new List<int> { 4, 5 };

        List<int> chemCompositions = new List<int> { 1 };
        List<int> chemSupportMaterials = new List<int> { 1 };

        int startComposition = 2;
        int startSupportmaterial = 2;

        ObservableCollection<Manufacturer> Manufacturers { get; set; }
        public ObservableCollection<WashcoatCatalyticComposition> Washcoats { get; set; }
        ObservableCollection<MonolithMaterial> MonolithMaterials { get; set; }
        ObservableCollection<CatalystType> CatalystTypes { get; set; }
        ObservableCollection<AgingProcedure> AgingProcedures { get; set; }
        ObservableCollection<AgingStatus> AgingStatuses { get; set; }
        ObservableCollection<BoundaryShape> BoundaryShapes { get; set; }
        ObservableCollection<ModelType> ModelTypes { get; set; }
        ObservableCollection<SimulationTool> SimulationTools { get; set; }
        ObservableCollection<SteadyStateLegislation> SteadyStateLegislations { get; set; }
        ObservableCollection<TransientLegislation> TransientLegislations { get; set; }
        ObservableCollection<Emission> Emissions { get; set; }
        ObservableCollection<ApplicationFieldTestbench> ApplicationFieldTestbenchs { get; set; }
        ObservableCollection<PreciousMetalLoading> PreciousMetalLoadings { get; set; }
        ObservableCollection<CristallineWashcoatComponentFunction> CristallineWashcoatComponentFunctions { get; set; }
        ObservableCollection<ApplicationField> ApplicationFields { get; set; }
        ObservableCollection<ApplicationField> ApplicationFieldsTestbench { get; set; }
        ObservableCollection<SourceOfData> SourceOfDatas { get; set; }
        ObservableCollection<SourceOfMeasurement> SourceOfMeasurements { get; set; }
        ObservableCollection<UnitMaxAmmonia> UnitMaxAmmonias { get; set; }
        ObservableCollection<UnitMaxNOX> UnitMaxNOXs { get; set; }
        ObservableCollection<UnitMaxOSC> UnitMaxOSCs { get; set; }
        ObservableCollection<UnitAgingDuration> UnitAgingDurations { get; set; }
        ObservableCollection<UnitPreciousMetalLoading> UnitPreciousLoadings { get; set; }
        ObservableCollection<UnitSupport> UnitSupports { get; set; }

        Dictionary<UIElement, string> updatedFields = new Dictionary<UIElement, string>();
        Dictionary<string, string> updateIDs = new Dictionary<string, string>();
        Dictionary<UIElement, string> currentLoadedCatalyst;
        ObservableCollection<ApplicationField> currentAppplicationFields { get; set; }
        ObservableCollection<Manufacturer> currentManufacturers { get; set; }
        ObservableCollection<WashcoatCatalyticComposition> currentWashcoats { get; set; }
        ObservableCollection<ApplicationField> currentTestbenchApplicationFields { get; set; }
        ObservableCollection<TransientLegislation> currentTransients { get; set; }
        ObservableCollection<SteadyStateLegislation> currentSteadys { get; set; }
        ObservableCollection<PreciousMetalLoading> currentPreciousMetalLoadings { get; set; }
        List<string> normalCycleUnits { get; set; }
        List<string> pnCycleUnits { get; set; }
        Dictionary<string, CustomComboBox> currentCombinedCatalysts = new Dictionary<string, CustomComboBox>();
        Dictionary<string, CustomComboBox> updatedCombinedCatalysts = new Dictionary<string, CustomComboBox>();
        Dictionary<string, string> currentCompositions = new Dictionary<string, string>();
        Dictionary<string, KeyValuePair<string, string>> currentSupportMaterial = new Dictionary<string, KeyValuePair<string, string>>();
        Dictionary<string, KeyValuePair<string, string>> updatedSupportMaterial = new Dictionary<string, KeyValuePair<string, string>>();
        ObservableCollection<ApplicationField> copiedAppplicationFields { get; set; }
        ObservableCollection<Manufacturer> copiedManufacturers { get; set; }
        ObservableCollection<WashcoatCatalyticComposition> copiedWashcoats { get; set; }
        ObservableCollection<ApplicationField> copiedTestbenchApplicationFields { get; set; }
        ObservableCollection<TransientLegislation> copiedTransients { get; set; }
        ObservableCollection<SteadyStateLegislation> copiedSteadys { get; set; }
        ObservableCollection<PreciousMetalLoading> copiedPreciousMetalLoadings { get; set; }
        Dictionary<string, string> copiedGeneralList = null;
        Dictionary<string, string> copiedLgbList = null;
        Dictionary<string, string> copiedSimulationList = null;
        Dictionary<string, string> copiedTestbenchList = null;
        Dictionary<string, string> copiedChemList = null;
        public static string chosenID = "0";

        int last_insertedCatalyst = 0;
        //for generation of general information
        int numberOfFieldInGeneral = 0;
        int startFieldNumber = 21;

        int currSimRow = 7;
        int currSimColumn = 0;


        //for generation of Testbench
        int deletedAtTransient = 0;
        int NewEntryStartRowForSteadyLegislation = 14;
        int NewEntryStartRowForTransientLegislation = 14;
        int testbenchRowCount = 14;

        private int lastRow = 16;

        int previouslySelectedCatalystType = 0;

        List<string> fieldsWithError = new List<string>();
        Dictionary<string, CustomComboBox> textBoxAndUnits = new Dictionary<string, CustomComboBox>();
        Dictionary<string, CustomTextBox[]> comboboxesAndTextboxes = new Dictionary<string, CustomTextBox[]>();


        public MainApplication()
        {
            converter = new Converter();
            InitializeComponent();
            InitializeDropdowns();

            listOfFieldsToUpdateGeneral.Add(st_washcoat_loading);
            listOfFieldsToUpdateGeneral.Add(st_cell_density);
            listOfFieldsToUpdateGeneral.Add(st_wall_thickness);
            listOfFieldsToUpdateGeneral.Add(st_substrate);
            listOfFieldsToUpdateGeneral.Add(st_volume);
            listOfFieldsToUpdateGeneral.Add(st_monolith_length);
            listOfFieldsToUpdateGeneral.Add(st_monolith_diameter);
            listOfFieldsToUpdateGeneral.Add(st_monolith_material);
            listOfFieldsToUpdateGeneral.Add(st_zone_coating);
            listOfFieldsToUpdateGeneral.Add(st_slip_catalyst);
            listOfFieldsToUpdateGeneral.Add(st_maxtemp_axial);
            listOfFieldsToUpdateGeneral.Add(st_maxtemp_radial);
            listOfFieldsToUpdateGeneral.Add(st_maxtemp_peak);
            listOfFieldsToUpdateGeneral.Add(st_maxtemp_longterm);
            listOfFieldsToUpdateGeneral.Add(st_maxhc);
            listOfFieldsToUpdateGeneral.Add(st_segmentxy);
            listOfFieldsToUpdateGeneral.Add(st_loss_coef);
            listOfFieldsToUpdateGeneral.Add(st_soot_mass);
            listOfFieldsToUpdateGeneral.Add(st_inlet_cell);
            listOfFieldsToUpdateGeneral.Add(st_outlet_cell);
            listOfFieldsToUpdateGeneral.Add(st_aged);
            listOfFieldsToUpdateGeneral.Add(st_aging_proc);
            listOfFieldsToUpdateGeneral.Add(st_aging_duration);

            listOfFieldsForChem.Add(st_comp_1);
            listOfFieldsForChem.Add(st_bricksubstrate);
            listOfFieldsForChem.Add(st_washcoat_substrate);
            listOfFieldsForChem.Add(st_prec_metal_loading);
            listOfFieldsForChem.Add(st_support_1);
            listOfFieldsForChem.Add(st_spec_surface);
            listOfFieldsForChem.Add(st_total_porosity);
            listOfFieldsForChem.Add(st_avg_porosity);
            listOfFieldsForChem.Add(st_crist_function);
            listOfFieldsForChem.Add(st_cris_components);
            listOfFieldsForChem.Add(st_crist_zeolith);
            listOfFieldsForChem.Add(st_number_zone);
            listOfFieldsForChem.Add(st_layer);
            listOfFieldsForChem.Add(st_ch_dispersity);
            listOfFieldsForChem.Add(st_ch_heat_capacity);
            initTextBoxAndComboboxes();

            DefineRowsForGeneralnformation(43);
            // GenerateAtEntrance(this.Washcoats);
            Closing += OnWindowClosing;
        }

        static MainApplication()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
        }

        /*
         * Initialize the values of dropdowns from database
         */
        void InitializeDropdowns()
        {
            normalCycleUnits = new List<string>();
            normalCycleUnits.Add("g/kWh");
            normalCycleUnits.Add("g/km");
            normalCycleUnits.Add("g/bhp*hr");
            normalCycleUnits.Add("g/mi");

            pnCycleUnits = new List<string>();
            pnCycleUnits.Add("#/kWh");
            pnCycleUnits.Add("#/km");
            pnCycleUnits.Add("#/bhp*hr");
            pnCycleUnits.Add("#/mi");

            dropdowns = new PopulateDropdownsFromDB();
            SteadyStateLegislations = new ObservableCollection<SteadyStateLegislation>();
            currentSteadys = new ObservableCollection<SteadyStateLegislation>();
            SteadyStateLegislations.CollectionChanged += SteadyStateLegislations_CollectionChanged;
            for (int i = 0; i < dropdowns.populateSteadyStateLegislations().Count(); i++)
            {
                SteadyStateLegislations.Add(dropdowns.populateSteadyStateLegislations()[i]);
                currentSteadys.Add(dropdowns.populateSteadyStateLegislations()[i]);
            }
            cmb_steady_state_legislation.ItemsSource = SteadyStateLegislations;

            SourceOfDatas = new ObservableCollection<SourceOfData>();
            SourceOfDatas = dropdowns.populateSourceOfDatas();
            cmb_src_data.ItemsSource = SourceOfDatas;

            SourceOfMeasurements = new ObservableCollection<SourceOfMeasurement>();
            SourceOfMeasurements = dropdowns.populateSourceOfMeasurements();
            cmb_src_measurement.ItemsSource = SourceOfMeasurements;

            TransientLegislations = new ObservableCollection<TransientLegislation>();
            currentTransients = new ObservableCollection<TransientLegislation>();
            TransientLegislations.CollectionChanged += TransientLegislations_CollectionChanged;
            for (int i = 0; i < dropdowns.populateTransientLegislations().Count(); i++)
            {
                currentTransients.Add(dropdowns.populateTransientLegislations()[i]);
                TransientLegislations.Add(dropdowns.populateTransientLegislations()[i]);
            }
            cmb_transient_legislation_cycle.ItemsSource = TransientLegislations;


            Emissions = dropdowns.populateEmissions();

            ModelTypes = dropdowns.populateModelTypes();
            cmb_model_type.ItemsSource = ModelTypes;

            SimulationTools = dropdowns.populateSimulationTools();
            cmb_simulation_tool.ItemsSource = SimulationTools;

            Manufacturers = dropdowns.populateManufacturers();
            currentManufacturers = dropdowns.populateManufacturers();
            cmb_manufacturer.ItemsSource = this.Manufacturers;

            Washcoats = new ObservableCollection<WashcoatCatalyticComposition>();
            currentWashcoats = new ObservableCollection<WashcoatCatalyticComposition>();
            Washcoats.CollectionChanged += Washcoats_CollectionChanged;
            for (int i = 0; i < dropdowns.populateWashcoats().Count(); i++)
            {
                Washcoats.Add(dropdowns.populateWashcoats()[i]);
                currentWashcoats.Add(dropdowns.populateWashcoats()[i]);
            }
            cmb_wash_cat_comp.ItemsSource = Washcoats;


            MonolithMaterials = dropdowns.populateMonolithMaterials();
            cmb_monoloth_material.ItemsSource = MonolithMaterials;

            CatalystTypes = dropdowns.populateCatalystTypes();
            cmb_catalyst_type.ItemsSource = CatalystTypes;

            AgingProcedures = dropdowns.populateAgingProcedures();
            cmb_aging_procedure.ItemsSource = AgingProcedures;

            AgingStatuses = dropdowns.populateAgingStatuses();
            cmb_aging_status.ItemsSource = AgingStatuses;

            BoundaryShapes = dropdowns.populateBoundaryShapes();
            cmb_substrate_boundary_shape.ItemsSource = BoundaryShapes;

            ApplicationFields = dropdowns.populateApplicationFields();
            currentAppplicationFields = dropdowns.populateApplicationFields();
            cmb_field_of_app.ItemsSource = ApplicationFields;


            ApplicationFieldsTestbench = dropdowns.populateApplicationFields();
            currentTestbenchApplicationFields = dropdowns.populateApplicationFields();
            cmb_app.ItemsSource = ApplicationFieldsTestbench;


            PreciousMetalLoadings = new ObservableCollection<PreciousMetalLoading>();
            currentPreciousMetalLoadings = new ObservableCollection<PreciousMetalLoading>();
            currentPreciousMetalLoadings = dropdowns.populatePreciousMetalLoadings();
            PreciousMetalLoadings.CollectionChanged += PreciousMetalLoadings_CollectionChanged;
            for (int i = 0; i < dropdowns.populatePreciousMetalLoadings().Count(); i++)
            {
                PreciousMetalLoadings.Add(dropdowns.populatePreciousMetalLoadings()[i]);
            }
            cmb_prec_metal_loading.ItemsSource = PreciousMetalLoadings;


            CristallineWashcoatComponentFunctions = dropdowns.populateCristallineWashcoatComponentFunctions();
            cmb_crist_function.ItemsSource = CristallineWashcoatComponentFunctions;

            UnitMaxAmmonias = dropdowns.populateUnitMaxAmmonia();
            UnitMaxNOXs = dropdowns.populateUnitMaxNox();
            UnitMaxOSCs = dropdowns.populateUnitMaxOsc();
            UnitPreciousLoadings = dropdowns.populateUnitPreciousLoading();
            UnitSupports = dropdowns.populateUnitSupport();
            cmb_sup_1.ItemsSource = UnitSupports;

            UnitAgingDurations = dropdowns.populateUnitAgingDuration();
            cmb_aging_duration.ItemsSource = UnitAgingDurations;
        }

        public void initTextBoxAndComboboxes()
        {
            textBoxAndUnits.Add("txb_washcoat_loading", cmb_washcoat_loading_unit);
            textBoxAndUnits.Add("txb_wall_thickness", cmb_wall_thickness_meas);
            textBoxAndUnits.Add("txb_volume", cmb_volume_meas);
            textBoxAndUnits.Add("txb_monolith_length", cmb_monolith_length_meas);
            textBoxAndUnits.Add("txb_monolith_diameter", cmb_monolith_diameter_meas);
            textBoxAndUnits.Add("txb_max_temp_limit_peak", cmb_max_temp_limit_peak);
            textBoxAndUnits.Add("txb_max_temp_limit_longterm", cmb_max_temp_limit_longterm);
            textBoxAndUnits.Add("txb_segment_x", cmb_segmentation_meas);
            textBoxAndUnits.Add("txb_segment_y", cmb_segmentation_meas);

            comboboxesAndTextboxes.Add("cmb_washcoat_loading_unit", new CustomTextBox[] { txb_washcoat_loading });
            comboboxesAndTextboxes.Add("cmb_wall_thickness_meas", new CustomTextBox[] { txb_wall_thickness });
            comboboxesAndTextboxes.Add("cmb_volume_meas", new CustomTextBox[] { txb_volume });
            comboboxesAndTextboxes.Add("cmb_monolith_length_meas", new CustomTextBox[] { txb_monolith_length });
            comboboxesAndTextboxes.Add("cmb_monolith_diameter_meas", new CustomTextBox[] { txb_monolith_diameter });
            comboboxesAndTextboxes.Add("cmb_max_temp_limit_peak", new CustomTextBox[] { txb_max_temp_limit_peak });
            comboboxesAndTextboxes.Add("cmb_max_temp_limit_longterm", new CustomTextBox[] { txb_max_temp_limit_longterm });
            comboboxesAndTextboxes.Add("cmb_segmentation_meas", new CustomTextBox[] { txb_segment_x, txb_segment_y });
        }

        /*
         *  Generate fields when mainapplication xaml is loaded
         */
        public void GenerateAtEntrance(ObservableCollection<WashcoatCatalyticComposition> all)
        {
            foreach (WashcoatCatalyticComposition w in all)
            {
                if (w.IsChecked == true && w.NeedPreciousMetal == true)
                {
                    GenerateGeneralInformation(w);
                }
            }

        }

        /** 
         * Section for methods of GeneralInformation
         * Method is called when item in the washcoat collection is changed
         **/
        void Washcoats_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //get property change item in collection
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= new
                                   PropertyChangedEventHandler(Washcoat_PropertyChanged);
                }

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged +=
                               new PropertyChangedEventHandler(Washcoat_PropertyChanged);
                }
        }

        /*
         * Method is called the on the changed washcoat of the collection to see which value is changed
         */
        void Washcoat_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //get property change event
            WashcoatCatalyticComposition washct = (WashcoatCatalyticComposition)sender;
            if (e.PropertyName == "IsChecked" && washct.NeedPreciousMetal == true)
            {
                GenerateGeneralInformation(washct);
            }
        }

        /*
         * Used for defining rows depending on the given fieldscount
         */
        public void DefineRowsForGeneralnformation(int fieldsCount)
        {

            //determine how many fields to generate
            int rowToGenerate = 0;
            //define rows and length
            if (numberOfFieldInGeneral < fieldsCount)
            {
                rowToGenerate = ReturnRowNumbers(numberOfFieldInGeneral, fieldsCount);
                for (int i = 0; i < rowToGenerate; i++)
                {
                    RowDefinition r = new RowDefinition();
                    r.Height = new GridLength(30);
                    grid_generalInfo.RowDefinitions.Add(r);
                }
            }
            else
            {
                rowToGenerate = ReturnRowNumbers(fieldsCount, numberOfFieldInGeneral);
                for (int i = 0; i < rowToGenerate; i++)
                {
                    grid_generalInfo.RowDefinitions.RemoveAt(grid_generalInfo.RowDefinitions.Count - 1);
                }
            }

            numberOfFieldInGeneral = fieldsCount;
        }

        /*
         * For given previous fieldscount and new fieldscount, returns how many rows should be changed - added or removed
         */
        public int ReturnRowNumbers(int previousFieldsCount, int newFieldsCount)
        {
            int rowToGenerate = 0;
            rowToGenerate = (newFieldsCount - previousFieldsCount) / 2 + 1;
            return rowToGenerate;
        }

        /**
         * Main method for generation of fields and dynamic change of positions.
         * For each selected or unselected washcoatcatalyccomposition, generate or delete corresponding fields from grid
         * */
        void GenerateGeneralInformation(WashcoatCatalyticComposition washcoat)
        {
            //if the washcoat is checked then generate fields accordingly and add those fields to grid and list
            if (washcoat.IsChecked == true)
            {
                //precious metal loading field
                StackPanel stack = new StackPanel();
                stack.Name = "st_" + washcoat.WashcoatValue + "_loading";
                stack.Orientation = Orientation.Horizontal;
                Label l = new Label();
                l.Content = washcoat.WashcoatValue + " Precious Metal Loading";
                l.Width = 200;
                stack.Children.Add(l);
                StackPanel stack_child = new StackPanel();
                stack_child.Orientation = Orientation.Horizontal;
                stack_child.Width = 200;
                CustomTextBox t = new CustomTextBox();
                t.Height = 25;
                t.Width = 135;
                t.Name = "txb_" + washcoat.WashcoatValue;
                t.GroupTitle = "General Information";
                t.LabelTitle = washcoat.WashcoatValue + " Precious Metal Loading";
                t.TableName = "generalwashcoat";
                t.FieldName = "precious_metal_loading";
                t.UpdateId = "general_id";
                t.UpdateHelper = "material_id = " + washcoat.Id;
                t.ContentType = "double";
                t.MouseLeave += validationMouseLeave;
                t.KeyUp += update_KeyUp;
                //register
                grid_generalInfo.RegisterName(t.Name, t);
                stack_child.Children.Add(t);
                CustomComboBox c = new CustomComboBox();
                c.Name = "cmb_" + washcoat.WashcoatValue;
                grid_generalInfo.RegisterName(c.Name, c);
                c.Height = 25;
                c.Width = 60;
                c.Margin = new Thickness(5, 0, 0, 0);
                c.TableName = "GeneralWashcoat";
                c.FieldName = "precious_metal_loading_unit_id";
                c.UpdateId = "general_id";
                c.GroupTitle = "General Information";
                c.LabelTitle = washcoat.WashcoatValue + " Precious Metal Loading Unit";
                c.SelectionChanged += update_SelectionChanged;
                c.ItemsSource = UnitPreciousLoadings;
                c.DisplayMemberPath = "Unit";
                c.SelectedValuePath = "Id";
                c.ValueQuery = "select* from  UnitPreciousMetalLoading where precious_metal_loading_unit_id = ";

                //add to texbox and units to trace the conversion
                if (textBoxAndUnits.ContainsKey("txb_" + washcoat.WashcoatValue))
                {
                    textBoxAndUnits.Remove("txb_" + washcoat.WashcoatValue);
                }
                textBoxAndUnits.Add("txb_" + washcoat.WashcoatValue, c);

                //load precious metal loading source.

                stack_child.Children.Add(c);
                stack.Children.Add(stack_child);


                //precious metal loading field
                StackPanel stack1 = new StackPanel();
                stack1.Name = "st_" + washcoat.WashcoatValue + "_ratio";
                stack1.Orientation = Orientation.Horizontal;
                Label l1 = new Label();
                l1.Content = washcoat.WashcoatValue + " Precious Metal Ratio";
                l1.Width = 200;
                stack1.Children.Add(l1);
                CustomTextBox t1 = new CustomTextBox();
                t1.Margin = new Thickness(0, 3, 0, 0);
                t1.Width = 200;
                t1.Name = "txb_" + washcoat.WashcoatValue + "_ratio";
                t1.TableName = "generalwashcoat";
                t1.FieldName = "precious_metal_ratio";
                t1.UpdateId = "general_id";
                t1.UpdateHelper = "material_id = " + washcoat.Id;
                t1.ContentType = "double";
                t1.GroupTitle = "General Information";
                t1.LabelTitle = washcoat.WashcoatValue + " Precious Metal Ratio";
                t1.MouseLeave += validationMouseLeave;
                t1.KeyUp += update_KeyUp;
                //register
                grid_generalInfo.RegisterName(t1.Name, t1);
                stack1.Children.Add(t1);

                grid_generalInfo.Children.Add(stack);
                grid_generalInfo.Children.Add(stack1);

                DefineRowsForGeneralnformation(numberOfFieldInGeneral + 2);

                listOfFieldsToUpdateGeneral.Insert(0, stack);
                listOfFieldsToUpdateGeneral.Insert(0, stack1);
                ChangeRowsOfGeneralInfo();

            }
            else //if washcoat is unchecked then delete fields from grid and also from the list
            {
                string first = "st_" + washcoat.WashcoatValue + "_loading";
                string second = "st_" + washcoat.WashcoatValue + "_ratio";
                grid_generalInfo.Children.Remove(RemoveStackFromListByName(listOfFieldsToUpdateGeneral, first));
                grid_generalInfo.Children.Remove(RemoveStackFromListByName(listOfFieldsToUpdateGeneral, second));
                grid_generalInfo.UnregisterName("txb_" + washcoat.WashcoatValue);
                grid_generalInfo.UnregisterName("txb_" + washcoat.WashcoatValue + "_ratio");
                grid_generalInfo.UnregisterName("cmb_" + washcoat.WashcoatValue);
                DefineRowsForGeneralnformation(numberOfFieldInGeneral - 2);
                ChangeRowsOfGeneralInfo();
            }
        }

        /*
         * Remove stackpanel from the list of stackpanel - used when washcoat is unchecked 
         */
        StackPanel RemoveStackFromListByName(List<StackPanel> listOfStack, string stackName)
        {
            StackPanel s = new StackPanel();
            for (int i = 0; i < listOfStack.Count; i++)
            {
                if (listOfStack.ElementAt(i).Name == stackName)
                {
                    s = listOfStack.ElementAt(i);
                    listOfStack.Remove(s);
                }
            }

            return s;
        }

        /*
         * Dynamically change the position of the fields according to the added and removed fields
         */
        void ChangeRowsOfGeneralInfo()
        {

            int continueIndex = 0;
            int counter = 0;
            if (startFieldNumber <= (numberOfFieldInGeneral / 2))
            {

                for (int j = startFieldNumber; j <= numberOfFieldInGeneral / 2; j++)
                {
                    Grid.SetRow(listOfFieldsToUpdateGeneral.ElementAt(continueIndex), j);
                    Grid.SetColumn(listOfFieldsToUpdateGeneral.ElementAt(continueIndex), 0);
                    continueIndex++;
                }


                for (int a = continueIndex; a < listOfFieldsToUpdateGeneral.Count; a++)
                {
                    Grid.SetRow(listOfFieldsToUpdateGeneral.ElementAt(a), counter);
                    Grid.SetColumn(listOfFieldsToUpdateGeneral.ElementAt(a), 2);
                    counter++;
                }
            }
            else
            {
                int stRow = startFieldNumber - numberOfFieldInGeneral / 2;
                for (int i = 0; i < listOfFieldsToUpdateGeneral.Count; i++)
                {
                    Grid.SetRow(listOfFieldsToUpdateGeneral.ElementAt(i), stRow - 1);
                    Grid.SetColumn(listOfFieldsToUpdateGeneral.ElementAt(i), 2);
                    stRow++;

                }
            }
        }

        /**
         * Section for methods of Catalyst Characterization
         * Whenever selection of Catalyst Type changed, change the view of Catalyst Characterisation Field, as it heavily depends on catalyst type
         * */
        private void cmb_catalyst_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update_SelectionChanged(sender, e);
            cmb_catalyst_type.ClearValue(CustomComboBox.BorderBrushProperty);
            cmb_catalyst_type.ClearValue(CustomComboBox.BorderThicknessProperty);
            int currentRow = 0;
            int currentColumn = 0;
            //unregister the ui components names from the grid. 
            try
            {
                if (previouslySelectedCatalystType == 1)
                {
                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("char_relatedFiles");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                }

                if (previouslySelectedCatalystType == 2)
                {
                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");

                    grid_catalyst_charac.UnregisterName("txb_oxygen_test");
                    grid_catalyst_charac.UnregisterName("txb_max_OSC");
                    grid_catalyst_charac.UnregisterName("txb_temp_OSC");
                    grid_catalyst_charac.UnregisterName("cmb_max_meas_OSC");
                    grid_catalyst_charac.UnregisterName("txb_eff_nox");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempnox");
                    grid_catalyst_charac.UnregisterName("txb_max_NOx");
                    grid_catalyst_charac.UnregisterName("txb_temp_NOx");
                    grid_catalyst_charac.UnregisterName("cmb_max_meas_NOx");
                    grid_catalyst_charac.UnregisterName("txt_nox_test");
                }

                if (previouslySelectedCatalystType == 3)
                {
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                    grid_catalyst_charac.UnregisterName("txb_max_amn");
                    grid_catalyst_charac.UnregisterName("cbm_max_amn_meas");

                }

                if (previouslySelectedCatalystType == 4)
                {
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                    grid_catalyst_charac.UnregisterName("txb_max_amn");
                    grid_catalyst_charac.UnregisterName("cbm_max_amn_meas");
                    grid_catalyst_charac.UnregisterName("txb_soot_loading");

                }

                if (previouslySelectedCatalystType == 5)
                {

                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                    grid_catalyst_charac.UnregisterName("txb_soot_loading");
                }

                if (previouslySelectedCatalystType == 7)
                {
                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");

                    grid_catalyst_charac.UnregisterName("txb_oxygen_test");
                    grid_catalyst_charac.UnregisterName("txb_max_OSC");
                    grid_catalyst_charac.UnregisterName("txb_temp_OSC");
                    grid_catalyst_charac.UnregisterName("cmb_max_meas_OSC");
                }

                grid_catalyst_charac.UnregisterName("txb_comment_char");
                grid_catalyst_charac.UnregisterName("char_relatedFiles");

            }

            catch { }

            grid_catalyst_charac.Children.Clear();

            if (firstKind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForFirstKind("CO", "CO", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("CO", "CO", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateForFirstKind("C3H6", "C3H6", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("C3H6", "C3H6", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateForFirstKind("C3H8", "C3H8", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("C3H8", "C3H8", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateForFirstKind("NO", "NO to NO2", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("NO", "NO to NO2", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateSpaceVelocityOfLight("Light-Off ", currentRow, currentColumn);
                currentRow = currentRow + 1;
            }

            if (secondKind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForSecondKind("OSC", currentRow, currentRow + 1, currentColumn);
                currentRow = currentRow + 2;
                GenerateOxygenStorage(currentRow, currentColumn);
                currentRow = currentRow + 1;
            }

            if (thirdKind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForThirdKind(currentRow, currentColumn);
                currentRow = currentRow + 1;
                GenerateSpaceVelocityOfLight(" ", currentRow, currentColumn);
                currentRow = currentRow + 1;
            }

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 2)
            {
                GenerateEfficiency("nox", "NOx", currentColumn, currentRow);
                currentRow = currentRow + 2;
                GenerateForSecondKind("NOx", currentRow, currentRow + 1, currentColumn);
                currentRow = currentRow + 2;
                GenerateNoxStorage(currentRow, currentColumn);
                currentRow = currentRow + 1;

            }
            if (fifthkind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForSoot(currentRow, currentColumn);
                currentRow = currentRow + 1;

            }
            GenerateDefaultForCharac(currentRow, currentColumn);
            previouslySelectedCatalystType = Convert.ToInt32(cmb_catalyst_type.SelectedValue);
        }

        /*
         * Generate fields for the light_off_temp_heat_up, light_off_temp_cool_down 
         */
        void GenerateForFirstKind(string name, string secondname, string tooltip, int currentColumn, int currentRow)
        {
            Label la = new Label();
            la.Content = name + " " + tooltip + "Heat-up,Cool-down";
            Grid.SetRow(la, currentRow);
            Grid.SetColumn(la, currentColumn);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk = new StackPanel();
            stk.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk.Height = 25;
            CustomTextBox one = new CustomTextBox();
            one.Name = "txb_heat_" + name;
            one.TableName = "CatalystCharacterisation";
            one.FieldName = name + "_light_off_temp_heat_up";
            one.UpdateId = "lgb_id";
            one.ContentType = "double";
            one.GroupTitle = "Catalyst Characterization";
            one.LabelTitle = name + " " + tooltip + "Heat-up";
            one.MouseLeave += validationMouseLeave;
            one.KeyUp += update_KeyUp;
            one.Width = 65;
            one.ToolTip = tooltip + " Heat-up";
            //register
            grid_catalyst_charac.RegisterName(one.Name, one);

            CustomTextBox two = new CustomTextBox();
            two.Name = "txb_cool_" + name;
            two.TableName = "CatalystCharacterisation";
            two.FieldName = name + "_light_off_temp_cool_down";
            two.UpdateId = "lgb_id";
            two.ContentType = "double";
            two.GroupTitle = "Catalyst Characterization";
            two.LabelTitle = name + " " + tooltip + "Cool-down";
            two.MouseLeave += validationMouseLeave;
            two.KeyUp += update_KeyUp;
            two.Width = 65;
            two.ToolTip = tooltip + " Cool-down";
            two.Margin = new Thickness(5, 0, 0, 0);
            //register
            grid_catalyst_charac.RegisterName(two.Name, two);
            Label l = new Label();
            l.Content = "°C";
            stk.Children.Add(one);
            stk.Children.Add(two);
            stk.Children.Add(l);
            Grid.SetColumn(stk, currentColumn + 1);
            Grid.SetRow(stk, currentRow);
            grid_catalyst_charac.Children.Add(stk);
        }

        /*
         * Generate fields for Maximum Oxygen storage capacity, Maximum NOx storage capacity, Temperature at max Oxygen storage capacity, Temperature at max NOx storage capacity
         */
        void GenerateForSecondKind(string name, int curRow, int nextRow, int curColumn)
        {
            Label la = new Label();
            CustomComboBox cb = new CustomComboBox();
            cb.Width = 60;
            cb.Name = "cmb_max_meas_" + name;
            //register
            grid_catalyst_charac.RegisterName(cb.Name, cb);
            cb.Margin = new Thickness(5, 0, 0, 0);
            cb.DisplayMemberPath = "Unit";
            cb.SelectedValuePath = "Id";
            //to keep in history when the unit has changed
            if (name == "OSC")
            {
                la.Content = "Maximum Oxygen storage capacity";
                cb.ItemsSource = UnitMaxOSCs;
                cb.TableName = "lgb";
                cb.FieldName = "max_osc_unit_id";
                cb.UpdateId = "catalyst_id";
                cb.GroupTitle = "Catalyst Characterization";
                cb.LabelTitle = "Maximum Oxygen storage capacity";
                cb.ValueQuery = "select * from  UnitMaxOSC where max_osc_unit_id = ";
            }
            else if (name == "NOx")
            {
                la.Content = "Maximum NOx storage capacity";
                cb.ItemsSource = UnitMaxNOXs;
                cb.TableName = "lgb";
                cb.FieldName = "max_nox_unit_id";
                cb.UpdateId = "catalyst_id";
                cb.GroupTitle = "Catalyst Characterization";
                cb.LabelTitle = "Maximum NOx storage capacity";
                cb.ValueQuery = "select* from  UnitMaxNOX where max_nox_unit_id = ";
            }
            cb.SelectionChanged += update_SelectionChanged;
            Grid.SetRow(la, curRow);
            Grid.SetColumn(la, curColumn);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk = new StackPanel();
            stk.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk.Height = 25;
            CustomTextBox osc = new CustomTextBox();
            osc.Name = "txb_max_" + name;
            osc.TableName = "lgb";
            osc.FieldName = "max_" + name + "_storage_capacity";
            osc.UpdateId = "catalyst_id";
            osc.ContentType = "double";
            osc.GroupTitle = "Catalyst Characterization";
            osc.LabelTitle = la.Content.ToString();
            //add to texbox and units to trace the conversion
            if (textBoxAndUnits.ContainsKey("txb_max_" + name))
            {
                textBoxAndUnits.Remove("txb_max_" + name);
            }
            textBoxAndUnits.Add("txb_max_" + name, cb);

            //register
            grid_catalyst_charac.RegisterName(osc.Name, osc);
            osc.MouseLeave += validationMouseLeave;
            osc.KeyUp += update_KeyUp;
            osc.Width = 135;

            stk.Children.Add(osc);
            stk.Children.Add(cb);
            Grid.SetColumn(stk, curColumn + 1);
            Grid.SetRow(stk, curRow);
            grid_catalyst_charac.Children.Add(stk);


            Label la1 = new Label();
            if (name == "OSC")
            {
                la1.Content = "Temperature at max Oxygen storage capacity";
            }
            else if (name == "NOx")
            {
                la1.Content = "Temperature at max NOx storage capacity";
            }
            Grid.SetRow(la1, nextRow);
            Grid.SetColumn(la1, curColumn);
            grid_catalyst_charac.Children.Add(la1);
            StackPanel stk2 = new StackPanel();
            stk2.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk2.Height = 25;
            CustomTextBox temp = new CustomTextBox();
            temp.Name = "txb_temp_" + name;
            temp.TableName = "lgb";
            temp.FieldName = "max_" + name + "_storage_capacity_temp";
            temp.UpdateId = "catalyst_id";
            temp.ContentType = "double";
            temp.GroupTitle = "Catalyst Characterization";
            temp.LabelTitle = la1.Content.ToString();
            //register
            grid_catalyst_charac.RegisterName(temp.Name, temp);
            temp.MouseLeave += validationMouseLeave;
            temp.KeyUp += update_KeyUp;
            temp.Width = 135;
            Label ef2 = new Label();
            ef2.Content = "°C";
            stk2.Children.Add(temp);
            stk2.Children.Add(ef2);
            Grid.SetColumn(stk2, curColumn + 1);
            Grid.SetRow(stk2, nextRow);
            grid_catalyst_charac.Children.Add(stk2);

        }

        /*
         * Generate fields for Max Ammonia Storage Capacity at 250 temperature, 
         */
        void GenerateForThirdKind(int currentRow, int currentColumn)
        {
            Label la = new Label();
            la.Content = "Max Ammonia Storage Capacity at 250 temperature";
            Grid.SetRow(la, currentRow);
            Grid.SetColumn(la, currentColumn);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk = new StackPanel();
            stk.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk.Height = 25;
            CustomTextBox max_amn = new CustomTextBox();
            max_amn.Name = "txb_max_amn";
            max_amn.TableName = "lgb";
            max_amn.FieldName = "max_ammonia_storage_capacity";
            max_amn.UpdateId = "catalyst_id";
            max_amn.ContentType = "double";
            max_amn.GroupTitle = "Catalyst Characterization";
            max_amn.LabelTitle = "Max Ammonia Storage Capacity at 250 temperature";
            //register
            grid_catalyst_charac.RegisterName(max_amn.Name, max_amn);
            max_amn.MouseLeave += validationMouseLeave;
            max_amn.KeyUp += update_KeyUp;
            max_amn.Width = 135;
            CustomComboBox cb = new CustomComboBox();
            cb.Width = 60;
            cb.Name = "cbm_max_amn_meas";
            cb.TableName = "Lgb";
            cb.FieldName = "max_ammonia_unit_id";
            cb.UpdateId = "catalyst_id";
            cb.GroupTitle = "Catalyst Characterization";
            cb.LabelTitle = "Max Ammonia Storage Capacity at 250 temperature Unit";
            cb.ValueQuery = "select* from  UnitMaxAmmonia where max_ammonia_unit_id = ";
            cb.ItemsSource = UnitMaxAmmonias;
            cb.DisplayMemberPath = "Unit";
            cb.SelectedValuePath = "Id";
            //add to texbox and units to trace the conversion
            if (textBoxAndUnits.ContainsKey("txb_max_amn"))
            {
                textBoxAndUnits.Remove("txb_max_amn");
            }
            textBoxAndUnits.Add("txb_max_amn", cb);

            //to keep in history when unit has changed
            cb.SelectionChanged += update_SelectionChanged;
            //register
            grid_catalyst_charac.RegisterName(cb.Name, cb);
            stk.Children.Add(max_amn);
            stk.Children.Add(cb);
            Grid.SetColumn(stk, currentColumn + 1);
            Grid.SetRow(stk, currentRow);
            grid_catalyst_charac.Children.Add(stk);

        }

        /*
         * Generate fields for Max Conversion Efficiency and Max Efficiency Temperature
         */
        void GenerateEfficiency(string name, string secondname, int currentColumn, int currentRow)
        {
            Label la1 = new Label();
            la1.Content = secondname + " Max Conversion Efficiency";
            Grid.SetRow(la1, currentRow);
            Grid.SetColumn(la1, currentColumn);
            grid_catalyst_charac.Children.Add(la1);
            StackPanel stk1 = new StackPanel();
            stk1.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk1.Height = 25;
            CustomTextBox eff = new CustomTextBox();
            eff.Name = "txb_eff_" + name;
            eff.TableName = "CatalystCharacterisation";
            if (name != "NO")
            {
                eff.FieldName = name + "_max_conversion_efficiency";
            }
            else
            {
                eff.FieldName = "no_no2_max_conversion_efficiency";
            }

            eff.UpdateId = "lgb_id";
            eff.ContentType = "double";
            eff.GroupTitle = "Catalyst Characterisation";
            eff.LabelTitle = secondname + " Max Conversion Efficiency";
            //register
            grid_catalyst_charac.RegisterName(eff.Name, eff);
            eff.MouseLeave += validationMouseLeave;
            eff.KeyUp += update_KeyUp;
            eff.Width = 135;
            Label ef1 = new Label();
            ef1.Content = "%";
            stk1.Children.Add(eff);
            stk1.Children.Add(ef1);
            Grid.SetColumn(stk1, currentColumn + 1);
            Grid.SetRow(stk1, currentRow);
            grid_catalyst_charac.Children.Add(stk1);

            Label la2 = new Label();
            la2.Content = secondname + " Max Efficiency Temperature";
            Grid.SetRow(la2, currentRow + 1);
            Grid.SetColumn(la2, currentColumn);
            grid_catalyst_charac.Children.Add(la2);
            StackPanel stk2 = new StackPanel();
            stk2.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk2.Height = 25;
            CustomTextBox eff1 = new CustomTextBox();
            eff1.Name = "txb_eff_temp" + name;
            eff1.TableName = "CatalystCharacterisation";
            eff1.GroupTitle = "Catalyst Characterisation";
            eff1.LabelTitle = secondname + " Max Efficiency Temperature";
            if (name != "NO")
            {
                eff1.FieldName = name + "_max_efficiency_temperature";
            }
            else
            {
                eff1.FieldName = "no_no2_max_efficiency_temperature";
            }
            eff1.UpdateId = "lgb_id";
            eff1.ContentType = "double";
            //register 
            grid_catalyst_charac.RegisterName(eff1.Name, eff1);
            eff1.MouseLeave += validationMouseLeave;
            eff1.KeyUp += update_KeyUp;
            eff1.Width = 135;
            Label ef2 = new Label();
            ef2.Content = "°C";
            stk2.Children.Add(eff1);
            stk2.Children.Add(ef2);
            Grid.SetColumn(stk2, currentColumn + 1);
            Grid.SetRow(stk2, currentRow + 1);
            grid_catalyst_charac.Children.Add(stk2);
        }

        /*
         * Generate fields for soot loading
         */
        void GenerateForSoot(int currentRow, int currentColumn)
        {
            Label la = new Label();
            la.Content = "Soot Loading";
            Grid.SetColumn(la, currentColumn);
            Grid.SetRow(la, currentRow);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk2 = new StackPanel();
            stk2.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk2.Height = 25;
            CustomTextBox soot_loading = new CustomTextBox();
            soot_loading.Name = "txb_soot_loading";
            soot_loading.TableName = "lgb";
            soot_loading.FieldName = "soot_loading";
            soot_loading.UpdateId = "catalyst_id";
            soot_loading.ContentType = "double";
            soot_loading.GroupTitle = "Catalyst Characterisation";
            soot_loading.LabelTitle = "Soot Loading";
            soot_loading.MouseLeave += validationMouseLeave;
            soot_loading.KeyUp += update_KeyUp;
            //register
            grid_catalyst_charac.RegisterName(soot_loading.Name, soot_loading);
            soot_loading.Width = 135;
            Label ef2 = new Label();
            ef2.Content = "g/l";
            stk2.Children.Add(soot_loading);
            stk2.Children.Add(ef2);
            Grid.SetColumn(stk2, currentColumn + 1);
            Grid.SetRow(stk2, currentRow);
            grid_catalyst_charac.Children.Add(stk2);
        }

        /*
         * Generate fields for space velocity of light test
         */
        void GenerateSpaceVelocityOfLight(string textname, int currentRow, int currentColumn)
        {
            Label la = new Label();
            la.Content = "Space Velocity of " + textname + "Test";
            Grid.SetColumn(la, currentColumn);
            Grid.SetRow(la, currentRow);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk2 = new StackPanel();
            stk2.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk2.Height = 25;
            CustomTextBox osc1 = new CustomTextBox();
            osc1.Name = "txb_lightoff_test";
            osc1.TableName = "lgb";
            osc1.GroupTitle = "Catalyst Characterisation";
            osc1.LabelTitle = "Space Velocity of " + textname + "Test";
            if (textname == " ")
            {
                osc1.FieldName = "space_velocity_performed_test";
            }
            else
            {
                osc1.FieldName = "space_velocity_lightoff_test";
            }

            osc1.UpdateId = "catalyst_id";
            osc1.ContentType = "double";
            osc1.MouseLeave += validationMouseLeave;
            osc1.KeyUp += update_KeyUp;
            grid_catalyst_charac.RegisterName(osc1.Name, osc1);
            osc1.Width = 135;
            Label ef2 = new Label();
            ef2.Content = "1/h";
            stk2.Children.Add(osc1);
            stk2.Children.Add(ef2);
            Grid.SetColumn(stk2, currentColumn + 1);
            Grid.SetRow(stk2, currentRow);
            grid_catalyst_charac.Children.Add(stk2);
        }

        /*
         *  Generate comment and file uploading parts
         */
        void GenerateDefaultForCharac(int currentRow, int currentColumn)
        {
            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            Label l = new Label();
            l.Content = "Comment";
            l.Width = 100;
            ScrollViewer sw = new ScrollViewer();
            sw.Width = 400;
            sw.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            sw.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            CustomTextBox tx = new CustomTextBox();

            tx.Name = "txb_comment_char";
            tx.TableName = "lgb";
            tx.FieldName = "comment_lgb";
            tx.UpdateId = "catalyst_id";
            tx.GroupTitle = "Catalyst Characterisation";
            tx.LabelTitle = "Comment";
            tx.ContentType = "text";
            tx.KeyUp += update_KeyUp;
            //register
            grid_catalyst_charac.RegisterName(tx.Name, tx);
            tx.Height = 240;
            tx.AcceptsReturn = true;
            tx.TextWrapping = TextWrapping.Wrap;
            tx.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            sw.Content = tx;
            s.Children.Add(l);
            s.Children.Add(sw);
            Grid.SetColumn(s, 3);
            Grid.SetRowSpan(s, 8);
            grid_catalyst_charac.Children.Add(s);

            Label l1 = new Label();
            l1.Content = "Related Files";
            l1.FontWeight = FontWeights.Bold;
            Grid.SetRow(l1, 8);
            Grid.SetColumn(l1, 3);
            grid_catalyst_charac.Children.Add(l1);

            StackPanel ss = new StackPanel();
            Grid.SetRow(ss, 9);
            Grid.SetColumn(ss, 3);
            ss.Orientation = Orientation.Horizontal;
            Button bt = new Button();
            bt.Name = "btn_char_upload";
            bt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            bt.Content = "Upload Files";
            bt.Width = 100;
            bt.Height = 25;
            bt.Click += btn_char_upload_Click;
            Button bt1 = new Button();
            bt1.Name = "btn_char_delete_file";
            bt1.Content = "Delete Chosen File";
            bt1.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            bt1.Width = 120;
            bt1.Height = 25;
            bt1.Margin = new Thickness(280, 0, 0, 0);
            bt1.Background = Brushes.Red;
            bt1.Click += btn_char_delete_file_Click;
            ss.Children.Add(bt);
            ss.Children.Add(bt1);


            grid_catalyst_charac.Children.Add(ss);

            ScrollViewer sv = new ScrollViewer();
            sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            CustomListBox sl = new CustomListBox();
            sl.Name = "char_relatedFiles";
            sl.GroupTitle = "Catalyst Characterisation";
            sl.LabelTitle = "Files";
            grid_catalyst_charac.RegisterName(sl.Name, sl);
            sv.Content = sl;
            Grid.SetRow(sv, 10);
            Grid.SetRowSpan(sv, 9);
            Grid.SetColumn(sv, 3);
            grid_catalyst_charac.Children.Add(sv);
        }

        /*
         *  Genetate fields for Space Velocity of ‎O2 Capacity Test 
         */
        void GenerateOxygenStorage(int currentRow, int currentColumn)
        {
            Label la = new Label();
            la.Content = "Space Velocity of ‎O2 Capacity Test";
            Grid.SetColumn(la, currentColumn);
            Grid.SetRow(la, currentRow);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk2 = new StackPanel();
            stk2.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk2.Height = 25;
            CustomTextBox ox_test = new CustomTextBox();
            ox_test.Name = "txb_oxygen_test";
            ox_test.TableName = "lgb";
            ox_test.FieldName = "space_velocity_o2_capacity_test";
            ox_test.UpdateId = "catalyst_id";
            ox_test.ContentType = "double";
            ox_test.GroupTitle = "Catalyst Characterisation";
            ox_test.LabelTitle = "Space Velocity of ‎O2 Capacity Test";
            ox_test.MouseLeave += validationMouseLeave;
            ox_test.KeyUp += update_KeyUp;
            //register
            grid_catalyst_charac.RegisterName(ox_test.Name, ox_test);

            ox_test.Width = 135;
            Label ef2 = new Label();
            ef2.Content = "1/h";
            stk2.Children.Add(ox_test);
            stk2.Children.Add(ef2);
            Grid.SetColumn(stk2, currentColumn + 1);
            Grid.SetRow(stk2, currentRow);
            grid_catalyst_charac.Children.Add(stk2);
        }

        /*
         * Generate fields Space Velocity of ‎NOX Capacity Test
         */
        void GenerateNoxStorage(int currentRow, int currentColumn)
        {
            Label la = new Label();
            la.Content = "Space Velocity of ‎NOX Capacity Test";
            Grid.SetColumn(la, currentColumn);
            Grid.SetRow(la, currentRow);
            grid_catalyst_charac.Children.Add(la);
            StackPanel stk2 = new StackPanel();
            stk2.Orientation = System.Windows.Controls.Orientation.Horizontal;
            stk2.Height = 25;
            CustomTextBox nox_test = new CustomTextBox();
            nox_test.Name = "txt_nox_test";
            nox_test.TableName = "lgb";
            nox_test.FieldName = "space_velocity_nox_capacity_test";
            nox_test.UpdateId = "catalyst_id";
            nox_test.ContentType = "double";
            nox_test.GroupTitle = "Catalyst Characterisation";
            nox_test.LabelTitle = "Space Velocity of ‎NOX Capacity Test";
            nox_test.MouseLeave += validationMouseLeave;
            nox_test.KeyUp += update_KeyUp;
            //register
            grid_catalyst_charac.RegisterName(nox_test.Name, nox_test);
            nox_test.Width = 135;
            Label ef2 = new Label();
            ef2.Content = "1/h";
            stk2.Children.Add(nox_test);
            stk2.Children.Add(ef2);
            Grid.SetColumn(stk2, currentColumn + 1);
            Grid.SetRow(stk2, currentRow);
            grid_catalyst_charac.Children.Add(stk2);
        }
        //catalyst characterization ends here

        /**
         * Section for methods of Testbench  and Engine
         * */

        /*
         * Called when steady legislation collection is changed
         */
        void SteadyStateLegislations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //get property change item in collection
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= new
                                   PropertyChangedEventHandler(SteadyState_PropertyChanged);
                }

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged +=
                               new PropertyChangedEventHandler(SteadyState_PropertyChanged);
                }
        }

        /*
         * Called on the changed steady legislation to know which value is changed
         */
        void SteadyState_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //get property change event
            SteadyStateLegislation steady = (SteadyStateLegislation)sender;
            if (e.PropertyName == "IsChecked")
            {
                GenerateFieldsForSteadyStateLegislation(steady);
            }
        }

        /*
         * Generate or removie fields for steady state legislation whenever the selection is changed
         */
        void GenerateFieldsForSteadyStateLegislation(SteadyStateLegislation steady)
        {
            MessageBox.Show(steady.Legislation);
            int counter = 0;
            if (steady.IsChecked == false)
            {
                foreach (Emission e in Emissions)
                {
                    grid_testbench.UnregisterName("txb_raw_" + steady.Legislation + e.Name);
                    grid_testbench.UnregisterName("txb_tailpipe_" + steady.Legislation + e.Name);
                    grid_testbench.UnregisterName("cmb_" + steady.Legislation + e.Name);

                    string name = "st_" + steady.Legislation + e.Name;
                    StackPanel s = RemoveStackFromListByName(listOfFieldsForSteadyLegislation, name);
                    deletedAtTransient = Grid.GetRow(s);
                    grid_testbench.Children.Remove(s);
                    ChangeRowsOfTestbench(2);
                    NewEntryStartRowForSteadyLegislation--;
                    DeleteRowsForTestbench();
                }
            }
            else
            {
                foreach (Emission e in Emissions)
                {
                    AddRowsForTestbench(2);
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.Name = "st_" + steady.Legislation + e.Name;
                    Grid.SetRow(st, NewEntryStartRowForSteadyLegislation);
                    Grid.SetColumn(st, 2);

                    Label l = new Label();
                    l.Content = steady.Legislation + " " + e.Name + " Raw, Tailpipe";
                    l.Width = 250;
                    st.Children.Add(l);

                    StackPanel child_st = new StackPanel();
                    child_st.Width = 250;
                    child_st.Orientation = Orientation.Horizontal;
                    child_st.Height = 25;

                    CustomTextBox t1 = new CustomTextBox();
                    t1.Width = 80;
                    t1.Name = "txb_raw_" + steady.Legislation + e.Name;
                    t1.TableName = "testbenchandsteady";
                    t1.FieldName = "raw";
                    t1.UpdateId = "testbench_id";
                    t1.GroupTitle = "Testbench And Engine/Vehicle";
                    t1.LabelTitle = steady.Legislation + " " + e.Name + " Raw";
                    t1.UpdateHelper = "steady_state_id = " + steady.Id + " and emission_id = " + e.Id;
                    t1.ContentType = "double";
                    t1.MouseLeave += validationMouseLeave;
                    t1.KeyUp += update_KeyUp;
                    //register
                    grid_testbench.RegisterName(t1.Name, t1);
                    t1.ToolTip = "Raw";
                    currentLoadedCatalyst.Add(t1, " ");

                    CustomTextBox t2 = new CustomTextBox();
                    t2.Width = 80;
                    t2.Name = "txb_tailpipe_" + steady.Legislation + e.Name;
                    t2.TableName = "testbenchandsteady";
                    t2.FieldName = "tailpipe";
                    t2.UpdateId = "testbench_id";
                    t2.UpdateHelper = "steady_state_id = " + steady.Id + " and emission_id =" + e.Id;
                    t2.ContentType = "double";
                    t2.GroupTitle = "Testbench And Engine/Vehicle";
                    t2.LabelTitle = steady.Legislation + " " + e.Name + " Tailpipe";
                    t2.MouseLeave += validationMouseLeave;
                    t2.KeyUp += update_KeyUp;
                    //register
                    grid_testbench.RegisterName(t2.Name, t2);
                    //add to current
                    currentLoadedCatalyst.Add(t2, " ");
                    t2.ToolTip = "Tailpipe";
                    t2.Margin = new Thickness(5, 0, 0, 0);

                    CustomComboBox c = new CustomComboBox();
                    c.Name = "cmb_" + steady.Legislation + e.Name;
                    c.Width = 75;
                    c.Margin = new Thickness(5, 0, 0, 0);
                    c.TableName = "testbenchandsteady";
                    c.FieldName = "steady_unit";
                    c.UpdateId = "testbench_id";
                    c.UpdateHelper = "steady_state_id = " + steady.Id + " and emission_id =" + e.Id;
                    c.GroupTitle = "Testbench And Engine/Vehicle";
                    c.LabelTitle = "Steady Unit";
                    c.ValueQuery = "itself";
                    if (e.Name == "PN")
                    {
                        c.ItemsSource = pnCycleUnits;
                    }
                    else
                    {
                        c.ItemsSource = normalCycleUnits;
                    }
                    //to keep in history when unit changes
                    c.SelectionChanged += update_SelectionChanged;
                    //register
                    grid_testbench.RegisterName(c.Name, c);
                    //add to current
                    currentLoadedCatalyst.Add(c, " ");
                    child_st.Children.Add(t1);
                    child_st.Children.Add(t2);
                    child_st.Children.Add(c);
                    st.Children.Add(child_st);

                    grid_testbench.Children.Add(st);
                    NewEntryStartRowForSteadyLegislation++;
                    listOfFieldsForSteadyLegislation.Insert(0, st);
                    counter++;
                }
            }

        }


        /*
         * Called when transient legislation collection is changed
         */
        void TransientLegislations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //get property change item in collection
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= new
                                   PropertyChangedEventHandler(Transient_PropertyChanged);
                }

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged +=
                               new PropertyChangedEventHandler(Transient_PropertyChanged);
                }
        }

        /*
         * Called on the changed steady legislation to know which value is changed
         */
        void Transient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //get property change event
            TransientLegislation emission = (TransientLegislation)sender;
            if (e.PropertyName == "IsChecked")
            {
                GenerateFieldsForTransientLegislation(emission);
            }
        }

        /*
         * Generate or removie fields for transient state legislation whenever the selection is changed
         */
        void GenerateFieldsForTransientLegislation(TransientLegislation em)
        {
            int counter = 0;
            if (em.IsChecked == false)
            {
                foreach (Emission e in Emissions)
                {
                    grid_testbench.UnregisterName("txb_raw_" + em.Legislation + e.Name);
                    grid_testbench.UnregisterName("txb_tailpipe_" + em.Legislation + e.Name);
                    grid_testbench.UnregisterName("cmb_" + em.Legislation + e.Name);

                    string name = "st_" + em.Legislation + e.Name;
                    StackPanel s = RemoveStackFromListByName(listOfFieldsForTransientLegislation, name);
                    deletedAtTransient = Grid.GetRow(s);
                    grid_testbench.Children.Remove(s);
                    ChangeRowsOfTestbench(0);
                    NewEntryStartRowForTransientLegislation--;
                    DeleteRowsForTestbench();
                }

            }
            else
            {
                foreach (Emission e in Emissions)
                {
                    AddRowsForTestbench(0);
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.Name = "st_" + em.Legislation + e.Name;
                    Grid.SetRow(st, NewEntryStartRowForTransientLegislation);
                    Grid.SetColumn(st, 0);

                    Label l = new Label();
                    l.Content = em.Legislation + " " + e.Name + " Raw, Tailpipe";
                    l.Width = 250;
                    st.Children.Add(l);

                    StackPanel child_st = new StackPanel();
                    child_st.Width = 250;
                    child_st.Orientation = Orientation.Horizontal;
                    child_st.Height = 25;

                    CustomTextBox t1 = new CustomTextBox();
                    t1.Width = 80;
                    t1.Name = "txb_raw_" + em.Legislation + e.Name;
                    t1.TableName = "testbenchandtransient";
                    t1.FieldName = "raw";
                    t1.UpdateId = "testbench_id";
                    t1.UpdateHelper = " transient_id = " + em.Id + " and emission_id = " + e.Id;
                    t1.ContentType = "double";
                    t1.GroupTitle = "Testbench And Engine/Vehicle";
                    t1.LabelTitle = em.Legislation + " " + e.Name + " Raw";
                    t1.MouseLeave += validationMouseLeave;
                    t1.KeyUp += update_KeyUp;
                    //register
                    grid_testbench.RegisterName(t1.Name, t1);
                    t1.ToolTip = "Raw";
                    //add to current
                    currentLoadedCatalyst.Add(t1, " ");

                    CustomTextBox t2 = new CustomTextBox();
                    t2.Width = 80;
                    t2.Name = "txb_tailpipe_" + em.Legislation + e.Name;
                    t2.TableName = "testbenchandtransient";
                    t2.FieldName = "tailpipe";
                    t2.UpdateId = "testbench_id";
                    t2.UpdateHelper = " transient_id = " + em.Id + " and emission_id = " + e.Id;
                    t2.ContentType = "double";
                    t2.GroupTitle = "Testbench And Engine/Vehicle";
                    t2.LabelTitle = em.Legislation + " " + e.Name + " Tailpipe";
                    t2.MouseLeave += validationMouseLeave;
                    t2.KeyUp += update_KeyUp;
                    //register
                    grid_testbench.RegisterName(t2.Name, t2);
                    t2.ToolTip = "Tailpipe";
                    t2.Margin = new Thickness(5, 0, 0, 0);
                    //add to current
                    currentLoadedCatalyst.Add(t2, " ");

                    CustomComboBox c = new CustomComboBox();
                    c.Name = "cmb_" + em.Legislation + e.Name;
                    c.Width = 75;
                    c.Margin = new Thickness(5, 0, 0, 0);
                    c.TableName = "testbenchandtransient";
                    c.FieldName = "transient_unit";
                    c.UpdateId = "testbench_id";
                    c.GroupTitle = "Testbench And Engine/Vehicle";
                    c.LabelTitle = "Transient Unit";
                    c.UpdateHelper = "transient_id = " + em.Id + " and emission_id = " + e.Id;
                    c.ValueQuery = "itself";
                    if (e.Name == "PN")
                    {
                        c.ItemsSource = pnCycleUnits;
                    }
                    else
                    {
                        c.ItemsSource = normalCycleUnits;
                    }
                    //to keep in history when unit has changed
                    c.SelectionChanged += update_SelectionChanged;
                    //register
                    grid_testbench.RegisterName(c.Name, c);
                    child_st.Children.Add(t1);
                    child_st.Children.Add(t2);
                    child_st.Children.Add(c);
                    st.Children.Add(child_st);
                    //add to current
                    currentLoadedCatalyst.Add(c, " ");

                    grid_testbench.Children.Add(st);
                    NewEntryStartRowForTransientLegislation++;
                    listOfFieldsForTransientLegislation.Insert(0, st);
                    counter++;
                }
            }
        }

        /*
         * Add rows for testbench when either transient or steady legislation is checked 
         */
        void AddRowsForTestbench(int columnNumber)
        {
            if (columnNumber == 0 && NewEntryStartRowForTransientLegislation >= NewEntryStartRowForSteadyLegislation)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(30);
                grid_testbench.RowDefinitions.Add(r);
                testbenchRowCount++;
            }

            else if (columnNumber == 2 && NewEntryStartRowForSteadyLegislation >= NewEntryStartRowForTransientLegislation)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(30);
                grid_testbench.RowDefinitions.Add(r);
                testbenchRowCount++;
            }

        }

        /*
        * Delete rows for testbench when either transient or steady legislation is checked 
        */
        void DeleteRowsForTestbench()
        {
            if (NewEntryStartRowForSteadyLegislation != NewEntryStartRowForTransientLegislation)
            {

                if (NewEntryStartRowForSteadyLegislation > NewEntryStartRowForTransientLegislation)
                {

                    int diff = testbenchRowCount - NewEntryStartRowForSteadyLegislation;
                    for (int i = 0; i < diff; i++)
                    {
                        grid_testbench.RowDefinitions.RemoveAt(grid_testbench.RowDefinitions.Count - 1);
                        testbenchRowCount--;

                    }
                }
                else
                {

                    int diff = testbenchRowCount - NewEntryStartRowForTransientLegislation;
                    for (int i = 0; i < diff; i++)
                    {
                        grid_testbench.RowDefinitions.RemoveAt(grid_testbench.RowDefinitions.Count - 1);
                        testbenchRowCount--;

                    }
                }

            }
        }

        /*
         *  Change rows of the stacks in Testbench according to added or deleted stacks
         */
        void ChangeRowsOfTestbench(int column)
        {
            int continueIndex = 14;
            int diff = deletedAtTransient - continueIndex;
            if (column == 0 && (diff < listOfFieldsForTransientLegislation.Count))
            {
                for (int i = 0; i < listOfFieldsForTransientLegislation.Count; i++)
                {
                    Grid.SetRow(listOfFieldsForTransientLegislation.ElementAt(i), continueIndex);
                    continueIndex++;

                }
            }
            if (column == 2 && (diff < listOfFieldsForSteadyLegislation.Count))
            {
                for (int i = 0; i < listOfFieldsForSteadyLegislation.Count; i++)
                {
                    Grid.SetRow(listOfFieldsForSteadyLegislation.ElementAt(i), continueIndex);
                    continueIndex++;
                }
            }
        }

        //Section for Testbench methods end here

        /**
         * Section for methods of Chem/Phys analysis
         * */

        /*
         * Called when precious metal loading collection is changed
         */
        void PreciousMetalLoadings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //get property change item in collection
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= new
                                   PropertyChangedEventHandler(Preciousmetal_PropertyChanged);
                }

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged +=
                               new PropertyChangedEventHandler(Preciousmetal_PropertyChanged);
                }
        }

        /*
         * Called on the changed precoius metal to find out which property of it was changed
         */
        void Preciousmetal_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //get property change event
            PreciousMetalLoading precious = (PreciousMetalLoading)sender;
            if (e.PropertyName == "IsChecked")
            {
                GenerateLoadingValueForChem(precious);
            }
        }

        /*
         * Add rows to chem when new item is added
         */
        void AddRowToChem()
        {
            RowDefinition r = new RowDefinition();
            r.Height = new GridLength(30);
            grid_chem_analysis.RowDefinitions.Add(r);
        }

        /*
         * Generate fields in Chem for selected precious metal loading values
         */
        void GenerateLoadingValueForChem(PreciousMetalLoading prec)
        {
            int rowToGenerateAt = Grid.GetRow(st_prec_metal_loading) + 1; ;
            if (prec.IsChecked == true)
            {
                AddRowToChem();
                MessageBox.Show(prec.Name);
                StackPanel s = new StackPanel();
                s.Name = "st_" + prec.Name;
                s.Orientation = Orientation.Horizontal;
                Grid.SetRow(s, rowToGenerateAt);
                Grid.SetColumn(s, 0);

                Label l = new Label();
                l.Width = 250;
                l.Content = prec.Name + " Loading Value";
                s.Children.Add(l);

                CustomTextBox t = new CustomTextBox();
                t.Width = 340;
                t.Name = "txb_wash" + prec.Name;
                t.TableName = "Loading";
                t.FieldName = "loading_value";
                t.UpdateId = "analysis_id";
                t.UpdateHelper = "precious_metal_loading_id = " + prec.Id;
                t.ContentType = "double";
                t.GroupTitle = "Chem./Phys. Analysis";
                t.LabelTitle = prec.Name + " Loading Value";
                t.MouseLeave += validationMouseLeave;
                t.KeyUp += update_KeyUp;
                //register
                grid_chem_analysis.RegisterName(t.Name, t);
                t.Height = 25;
                s.Children.Add(t);
                //add to current
                currentLoadedCatalyst.Add(t, " ");

                CustomComboBox c = new CustomComboBox();
                c.Name = "cmb_wash" + prec.Name;
                c.TableName = "Loading";
                c.FieldName = "precious_metal_loading_unit_id";
                c.UpdateId = "analysis_id";
                c.UpdateHelper = "precious_metal_loading_id =" + prec.Id;
                c.GroupTitle = "Chem./Phys. Analysis";
                c.LabelTitle = prec.Name + " Loading Value Unit";
                c.ValueQuery = "select* from UnitPreciousMetalLoading where precious_metal_loading_unit_id = ";

                //to keep in history when unit changes
                c.SelectionChanged += update_SelectionChanged;
                //register
                grid_chem_analysis.RegisterName(c.Name, c);
                c.Width = 50;
                c.Height = 25;
                c.Margin = new Thickness(10, 0, 0, 0);
                c.ItemsSource = UnitPreciousLoadings;
                c.DisplayMemberPath = "Unit";
                c.SelectedValuePath = "Id";
                s.Children.Add(c);
                //add to current
                currentLoadedCatalyst.Add(c, " ");

                grid_chem_analysis.Children.Add(s);
                int index = listOfFieldsForChem.IndexOf(st_prec_metal_loading);
                listOfFieldsForChem.Insert(index + 1, s);
                ChangeRowsOfChem();
            }
            else
            {
                grid_chem_analysis.RowDefinitions.RemoveAt(grid_chem_analysis.RowDefinitions.Count - 1);
                string name = "st_" + prec.Name;
                StackPanel s = RemoveStackFromListByName(listOfFieldsForChem, name);
                grid_chem_analysis.Children.Remove(s);
                ChangeRowsOfChem();
                grid_chem_analysis.UnregisterName("txb_wash" + prec.Name);
                grid_chem_analysis.UnregisterName("cmb_wash" + prec.Name);
            }
        }

        /*
         * Change rows of the stacks in Chem according to added or deleted stacks
         */
        void ChangeRowsOfChem()
        {
            for (int i = 0; i < listOfFieldsForChem.Count; i++)
            {
                Grid.SetRow(listOfFieldsForChem.ElementAt(i), i);
                Grid.SetColumn(listOfFieldsForChem.ElementAt(i), 0);
            }
        }

        /*
         * Called when new support material is added
         */
        void SupportMaterial_Clicked(object sender, RoutedEventArgs e)
        {
            generateSupport();
        }

        void generateSupport()
        {
            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.Name = "st_support_" + startSupportmaterial;

            Label l = new Label();
            l.Content = "Support Material Loading";
            l.Width = 250;
            s.Children.Add(l);

            CustomTextBox t = new CustomTextBox();
            t.Name = "txb_support_mtr_" + startSupportmaterial;
            t.TableName = "supportmaterial";
            t.FieldName = "support_material_loading";
            t.GroupTitle = "Chem./Phys. Analysis";
            t.LabelTitle = "Support Material Loading";
            t.UpdateId = "";
            t.ContentType = "text";
            //register
            grid_chem_analysis.RegisterName(t.Name, t);
            t.Margin = new Thickness(0, 3, 0, 0);
            t.Width = 160;
            t.Height = 25;
            s.Children.Add(t);

            Label l1 = new Label();
            l1.Content = "Value";
            l1.Width = 40;
            s.Children.Add(l1);

            CustomTextBox t1 = new CustomTextBox();
            t1.Name = "txb_support_val_" + startSupportmaterial;
            t1.TableName = "supportmaterial";
            t1.FieldName = "support_material_loading_value";
            t1.GroupTitle = "Chem./Phys. Analysis";
            t1.LabelTitle = "Support Material Loading Value";
            t1.UpdateId = "";
            t1.ContentType = "double";
            //register
            grid_chem_analysis.RegisterName(t1.Name, t1);
            t1.KeyUp += update_KeyUp;
            t1.MouseLeave += validationMouseLeave;
            t1.Width = 70;
            t1.Height = 25;
            t1.Margin = new Thickness(10, 3, 0, 0);
            t1.ToolTip = "Value  of Support Material";
            s.Children.Add(t1);


            CustomComboBox c = new CustomComboBox();
            c.Name = "cmb_sup_" + startSupportmaterial;
            //register
            grid_chem_analysis.RegisterName(c.Name, c);
            c.Width = 50;
            c.Height = 25;
            c.Margin = new Thickness(10, 0, 0, 0);
            c.ItemsSource = UnitSupports;
            c.DisplayMemberPath = "Unit";
            c.SelectedValuePath = "Id";
            c.TableName = "SupportMaterial";
            c.FieldName = "support_unit_id";
            c.GroupTitle = "Chem./Phys. Analysis";
            c.LabelTitle = "Support Material Loading Value Unit";
            c.UpdateId = "";
            c.ValueQuery = "select* from UnitSupport where support_unit_id = ";
            c.SelectionChanged += update_SelectionChanged;
            s.Children.Add(c);

            Button b = new Button();
            b.Content = "Remove";
            b.Name = "btn_s" + startSupportmaterial;
            b.Click += RemoveSupport_Clicked;
            b.Width = 50;
            b.Margin = new Thickness(10, 3, 0, 0);
            s.Children.Add(b);

            AddRowToChem();
            grid_chem_analysis.Children.Add(s);
            int pos = listOfFieldsForChem.IndexOf(st_support_1);
            listOfFieldsForChem.Insert(pos + 1, s);
            ChangeRowsOfChem();
            chemSupportMaterials.Add(startSupportmaterial);
            startSupportmaterial++;
        }

        /*
         * Called when support is removed
         */
        void RemoveSupport_Clicked(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            char[] charToRemove = { 'b', 't', 'n', '_', 's' };
            string name = b.Name.Trim(charToRemove);
            chemCompositions.Remove(Convert.ToInt32(name));

            StackPanel el = VisualTreeHelper.GetParent((DependencyObject)sender) as StackPanel;
            grid_chem_analysis.Children.Remove(el);
            RemoveStackFromListByName(listOfFieldsForChem, el.Name);
            ChangeRowsOfChem();
        }

        //Section for Chem methods end here
        private void menu_data_error_Click(object sender, RoutedEventArgs e)
        {
            catalyst_project.View.ErrorDataWindow newWindow = new View.ErrorDataWindow();
            newWindow.Show();
        }

        private void menu_admin_panel_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow a = new AdminWindow();
            a.Show();
        }
        private void menu_software_bug_Click(object sender, RoutedEventArgs e)
        {
            catalyst_project.View.ErrorSoftwareWindow newWindow = new View.ErrorSoftwareWindow();
            newWindow.Show();
        }

        private void btn_combine_catalyst_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<CombineCatalystWindow>().Count() == 1)
            {
                Application.Current.Windows.OfType<CombineCatalystWindow>().First().Activate();
            }
            else
            {
                CombineCatalystWindow c = new CombineCatalystWindow();
                c.Closed += new EventHandler(chooseCatalystWindowClosed);
                c.Show();
            }

        }

        public void chooseCatalystWindowClosed(object sender, EventArgs e)
        {
            if (!chosenID.Equals("0"))
            {
                if (currSimRow <= 18)
                {
                    generateCombinedCatalysts(chosenID);
                }
            }

        }

        private void generateCombinedCatalysts(string combinedCatalystID)
        {
            Label l = new Label();
            l.Content = "Catalyst ID " + combinedCatalystID + " Model Type";
            l.Name = "lbl_" + combinedCatalystID;
            Grid.SetRow(l, currSimRow);
            Grid.SetColumn(l, currSimColumn);
            grid_simulation.Children.Add(l);

            StackPanel st = new StackPanel();
            st.Name = "st_" + combinedCatalystID;
            //register
            grid_simulation.RegisterName(st.Name, st);

            Grid.SetColumn(st, currSimColumn + 1);
            Grid.SetRow(st, currSimRow);
            st.Orientation = Orientation.Horizontal;

            CustomComboBox c = new CustomComboBox();
            c.Name = "cmb_combined" + combinedCatalystID;
            //register
            grid_simulation.RegisterName(c.Name, c);

            c.ItemsSource = this.ModelTypes;
            c.SelectedValuePath = "Id";
            c.DisplayMemberPath = "Type";
            c.TableName = "CombinedCatalyst";
            c.FieldName = "model_type_id";
            c.UpdateId = "simulation_id";
            c.GroupTitle = "Simulation";
            c.LabelTitle = "Catalyst ID " + combinedCatalystID + " Model Type";
            c.UpdateHelper = "catalyst_id = " + combinedCatalystID;
            c.ValueQuery = "select* from ModelType where model_type_id = ";
            c.Margin = new Thickness(0, 3, 0, 0);
            c.Width = 165;
            c.SelectionChanged += update_SelectionChanged;
            if (!currentLoadedCatalyst.ContainsKey(c))
            {
                currentLoadedCatalyst.Add(c, c.Text);
            }
            updatedCombinedCatalysts.Add(combinedCatalystID, c);
            Button b = new Button();
            Grid.SetRow(b, currSimRow);
            b.Name = "btn_" + combinedCatalystID;
            b.Content = "X";
            b.Width = 25;
            b.Height = 25;
            b.Margin = new Thickness(10, 0, 0, 0);
            b.Background = Brushes.Red;
            b.Click += delete_combined_catalyst_Click;
            st.Children.Add(c);
            st.Children.Add(b);

            grid_simulation.Children.Add(st);
            currSimRow++;
            combinedCatalystID = "0";
        }

        private void delete_combined_catalyst_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string stackname = "st_" + button.Name.Substring(4);
            StackPanel s = (StackPanel)grid_simulation.FindName(stackname);
            int rowIndex = Grid.GetRow(s);
            try
            {
                UIElement a = grid_simulation.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == rowIndex && Grid.GetColumn(el) == 0);
                UIElement b = grid_simulation.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == rowIndex && Grid.GetColumn(el) == 1);
                StackPanel bb = (StackPanel)b;
                grid_simulation.UnregisterName("cmb_combined" + bb.Name.Substring(3));
                grid_simulation.UnregisterName(s.Name);
                grid_simulation.Children.Remove(a);
                grid_simulation.Children.Remove(b);
                updatedCombinedCatalysts.Remove(bb.Name.Substring(3));

                if (rowIndex < currSimRow)
                {
                    for (int i = rowIndex + 1; i < currSimRow; i++)
                    {

                        UIElement a1 = grid_simulation.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == i && Grid.GetColumn(el) == 0);
                        UIElement b1 = grid_simulation.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == i && Grid.GetColumn(el) == 1);
                        Grid.SetRow(a1, i - 1);
                        Grid.SetRow(b1, i - 1);
                    }
                }

                grid_simulation.RowDefinitions.RemoveAt(currSimRow);
                currSimRow = currSimRow - 1;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException();
            }
        }

        private void delete_combined_catalyst_Clickhahah(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int rowIndex = Convert.ToInt32(button.Name);
            try
            {
                UIElement a = grid_generalInfo.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == rowIndex && Grid.GetColumn(el) == 0);
                UIElement b = grid_generalInfo.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == rowIndex && Grid.GetColumn(el) == 1);
                grid_generalInfo.Children.Remove(a);
                grid_generalInfo.Children.Remove(b);
                lastRow--;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        /*
         * Add new catalyst to DB
         * */
        private void btn_add_catalyst_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_catalyst_type.Text.Equals(""))
            {
                cmb_catalyst_type.BorderBrush = new SolidColorBrush(Colors.Red);
                cmb_catalyst_type.BorderThickness = new Thickness(2, 2, 2, 2);
                MessageBox.Show("Please select catalyst type!");
            }
            else
            {
                cmb_catalyst_type.ClearValue(CustomComboBox.BorderBrushProperty);
                cmb_catalyst_type.ClearValue(CustomComboBox.BorderThicknessProperty);
                insertWithSqlite();

            }


        }

        private void insertWithSqlite()
        {
            SqliteDBConnection sqliteDB = new SqliteDBConnection();
            /**
              *  Insert into GeneralInformation
             */


            //insert to catalyst
            SQLiteCommand cmd_catalyst = new SQLiteCommand();
            cmd_catalyst.CommandText = "insert into catalyst (user_id, catalyst_type_id, review_comment, is_approved, is_data_available, date_created, date_modified, link_to_share_folder) values (@user_id, @catalyst_type_id, @review_comment, @is_approved, @is_data_available, @date_created, @date_modified, @link_to_share_folder)";
            cmd_catalyst.Parameters.AddWithValue("@user_id", 1);
            cmd_catalyst.Parameters.AddWithValue("@catalyst_type_id", String.IsNullOrWhiteSpace(cmb_catalyst_type.Text) ? null : cmb_catalyst_type.SelectedValue);
            cmd_catalyst.Parameters.AddWithValue("@review_comment", "haha");
            cmd_catalyst.Parameters.AddWithValue("@is_approved", cxb_is_approved.IsChecked);
            cmd_catalyst.Parameters.AddWithValue("@is_data_available", cxb_confidentiality.IsChecked);
            cmd_catalyst.Parameters.AddWithValue("@date_created", DateTime.Now.ToShortDateString());
            cmd_catalyst.Parameters.AddWithValue("@date_modified", DateTime.Now.ToShortDateString());
            cmd_catalyst.Parameters.AddWithValue("@link_to_share_folder", "haha");
            int last_catalyst_id = sqliteDB.Insert(cmd_catalyst);
            last_insertedCatalyst = last_catalyst_id;
            //insert history
            insertHistory(catalystID: last_catalyst_id.ToString(), group_name: " ", field_name: " ", usercode: MainWindow.userCode, old_value: " ", action: "Added", new_value: " ");



            //insert generalinformation
            SQLiteCommand cmd_generalinfo = new SQLiteCommand();
            cmd_generalinfo.CommandText = "insert into generalinformation (catalyst_id,production_date,catalyst_number,substract_number,project_number,customer,project_manager,eats_case_worker,target_country,target_emission_legislation,target_conf_system,specification_engine,washcoat_loading,cell_density,wall_thickness,shape_id,volume,length,diameter,monolith_material_id,zone_coating,slip_catalyst_applied,max_temp_gradient_axial,max_temp_gradient_radial,max_temp_limitation_peak,max_temp_limitation_longterm,max_hc_limit,segmentation_size_x,segmentation_size_y,pressure_loss_coefficient,soot_mass_limit,dpf_inlet_cell,dpf_outlet_cell,aging_status_id,aging_procedure_id,aging_duration,aging_duration_unit_id,link_to_share_folder,comment_general) values (@catalyst_id, @production_date, @catalyst_number, @substract_number, @project_number, @customer, @project_manager, @eats_case_worker, @target_country, @target_emission_legislation, @target_conf_system, @specification_engine, @washcoat_loading, @cell_density, @wall_thickness, @shape_id, @volume, @length, @diameter, @monolith_material_id, @zone_coating, @slip_catalyst_applied, @max_temp_gradient_axial, @max_temp_gradient_radial, @max_temp_limitation_peak, @max_temp_limitation_longterm, @max_hc_limit, @segmentation_size_x, @segmentation_size_y, @pressure_loss_coefficient, @soot_mass_limit, @dpf_inlet_cell, @dpf_outlet_cell, @aging_status_id, @aging_procedure_id, @aging_duration, @aging_duration_unit_id, @link_to_share_folder, @comment_general) ";
            cmd_generalinfo.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
            cmd_generalinfo.Parameters.AddWithValue("@production_date", dtp_production_date.Text);
            cmd_generalinfo.Parameters.AddWithValue("@catalyst_number", txb_catalyst_nr.Text);
            cmd_generalinfo.Parameters.AddWithValue("@substract_number", txb_substrat_nr.Text);
            cmd_generalinfo.Parameters.AddWithValue("@project_number", txb_project_number.Text);
            cmd_generalinfo.Parameters.AddWithValue("@customer", txb_customer.Text);
            cmd_generalinfo.Parameters.AddWithValue("@project_manager", txb_project_manager.Text);
            cmd_generalinfo.Parameters.AddWithValue("@eats_case_worker", txb_eats_case_worker.Text);
            cmd_generalinfo.Parameters.AddWithValue("@target_country", txb_target_country.Text);
            cmd_generalinfo.Parameters.AddWithValue("@target_emission_legislation", txb_target_emission_legislation.Text);
            cmd_generalinfo.Parameters.AddWithValue("@target_conf_system", txb_conf_of_target_system.Text);
            cmd_generalinfo.Parameters.AddWithValue("@specification_engine", txb_spec_of_engine.Text);
            cmd_generalinfo.Parameters.AddWithValue("@washcoat_loading", converter.convertToGL(cmb_washcoat_loading_unit.Text, txb_washcoat_loading.Text));
            cmd_generalinfo.Parameters.AddWithValue("@cell_density", String.IsNullOrWhiteSpace(txb_cell_density.Text) ? 0 : Convert.ToInt32(txb_cell_density.Text));
            cmd_generalinfo.Parameters.AddWithValue("@wall_thickness", converter.convertToMil(cmb_wall_thickness_meas.Text, txb_wall_thickness.Text));
            cmd_generalinfo.Parameters.AddWithValue("@shape_id", String.IsNullOrWhiteSpace(cmb_substrate_boundary_shape.Text) ? null : cmb_substrate_boundary_shape.SelectedValue);
            cmd_generalinfo.Parameters.AddWithValue("@volume", converter.convertToLiter(cmb_volume_meas.Text, txb_volume.Text));
            cmd_generalinfo.Parameters.AddWithValue("@length", converter.convertToMMDouble(cmb_monolith_length_meas.Text, txb_monolith_length.Text));
            cmd_generalinfo.Parameters.AddWithValue("@diameter", converter.convertToMMDouble(cmb_monolith_diameter_meas.Text, txb_monolith_diameter.Text));
            cmd_generalinfo.Parameters.AddWithValue("@monolith_material_id", String.IsNullOrWhiteSpace(cmb_monoloth_material.Text) ? 0 : cmb_monoloth_material.SelectedValue);
            cmd_generalinfo.Parameters.AddWithValue("@zone_coating", cbx_zone_coating.IsChecked);
            cmd_generalinfo.Parameters.AddWithValue("@slip_catalyst_applied", cbx_slip_catalyst_applied.IsChecked);
            cmd_generalinfo.Parameters.AddWithValue("@max_temp_gradient_axial", String.IsNullOrWhiteSpace(txb_max_temp_grad_axial.Text) ? 0 : Convert.ToDouble(txb_max_temp_grad_axial.Text));
            cmd_generalinfo.Parameters.AddWithValue("@max_temp_gradient_radial", String.IsNullOrWhiteSpace(txb_max_temp_grad_radial.Text) ? 0 : Convert.ToDouble(txb_max_temp_grad_radial.Text));
            cmd_generalinfo.Parameters.AddWithValue("@max_temp_limitation_peak", converter.convertToCelsius(cmb_max_temp_limit_peak.Text, txb_max_temp_limit_peak.Text));
            cmd_generalinfo.Parameters.AddWithValue("@max_temp_limitation_longterm", converter.convertToCelsius(cmb_max_temp_limit_longterm.Text, txb_max_temp_limit_longterm.Text));
            cmd_generalinfo.Parameters.AddWithValue("@max_hc_limit", txb_max_hc_limit.Text);
            cmd_generalinfo.Parameters.AddWithValue("@segmentation_size_x", converter.convertToInch(cmb_segmentation_meas.Text, txb_segment_x.Text));
            cmd_generalinfo.Parameters.AddWithValue("@segmentation_size_y", converter.convertToInch(cmb_segmentation_meas.Text, txb_segment_y.Text));
            cmd_generalinfo.Parameters.AddWithValue("@pressure_loss_coefficient", String.IsNullOrWhiteSpace(txb_prec_loss_coef.Text) ? 0 : Convert.ToDouble(txb_prec_loss_coef.Text));
            cmd_generalinfo.Parameters.AddWithValue("@soot_mass_limit", String.IsNullOrWhiteSpace(txb_soot_mass_limit.Text) ? 0 : Convert.ToDouble(txb_soot_mass_limit.Text));
            cmd_generalinfo.Parameters.AddWithValue("@dpf_inlet_cell", String.IsNullOrWhiteSpace(txb_dpf_inlet_cell.Text) ? 0 : Convert.ToDouble(txb_dpf_inlet_cell.Text));
            cmd_generalinfo.Parameters.AddWithValue("@dpf_outlet_cell", String.IsNullOrWhiteSpace(txb_dpf_outlet_cell.Text) ? 0 : Convert.ToDouble(txb_dpf_outlet_cell.Text));
            cmd_generalinfo.Parameters.AddWithValue("@aging_status_id", String.IsNullOrWhiteSpace(cmb_aging_status.Text) ? 0 : Convert.ToInt32(cmb_aging_status.SelectedValue));
            cmd_generalinfo.Parameters.AddWithValue("@aging_procedure_id", String.IsNullOrWhiteSpace(cmb_aging_procedure.Text) ? 0 : cmb_aging_procedure.SelectedValue);
            cmd_generalinfo.Parameters.AddWithValue("@aging_duration", String.IsNullOrWhiteSpace(txb_aging_duration.Text) ? 0 : Convert.ToDouble(txb_aging_duration.Text));
            cmd_generalinfo.Parameters.AddWithValue("@aging_duration_unit_id", String.IsNullOrWhiteSpace(cmb_aging_duration.Text) ? 0 : Convert.ToInt32(cmb_aging_duration.SelectedValue));
            cmd_generalinfo.Parameters.AddWithValue("@link_to_share_folder", "haha");
            cmd_generalinfo.Parameters.AddWithValue("@comment_general", txb_comment_general_info.Text);
            int general_id = sqliteDB.Insert(cmd_generalinfo);

            //insert manufacturers
            for (int i = 0; i < Manufacturers.Count(); i++)
            {
                if (Manufacturers[i].IsChecked == true)
                {
                    SQLiteCommand cmd_manufacturers = new SQLiteCommand();
                    cmd_manufacturers.CommandText = "insert into GeneralInformationManufacturer (manufacturer_id, general_id) values (@manufacturer_id, @general_id)";
                    cmd_manufacturers.Parameters.AddWithValue("@manufacturer_id", Manufacturers[i].Id);
                    cmd_manufacturers.Parameters.AddWithValue("@general_id", general_id);
                    int n = sqliteDB.Insert(cmd_manufacturers);
                }
            }
            //insert application fields    
            for (int i = 0; i < ApplicationFields.Count(); i++)
            {
                if (ApplicationFields[i].IsChecked == true)
                {
                    SQLiteCommand cmd_appfields = new SQLiteCommand();
                    cmd_appfields.CommandText = "insert into GeneralInformationApplicationField (app_field_id, general_id) values (@app_field_id, @general_id)";
                    cmd_appfields.Parameters.AddWithValue("@app_field_id", ApplicationFields[i].Id);
                    cmd_appfields.Parameters.AddWithValue("@general_id", general_id);
                    int n = sqliteDB.Insert(cmd_appfields);
                }
            }

            //insert washcoat composition
            for (int i = 0; i < Washcoats.Count(); i++)
            {
                if (Washcoats[i].IsChecked == true)
                {
                    SQLiteCommand cmd_washcoat = new SQLiteCommand();
                    cmd_washcoat.CommandText = "insert into GeneralWashcoat (general_id, material_id, precious_metal_loading_unit_id, precious_metal_ratio, precious_metal_loading) values ( @general_id, @material_id,@precious_metal_loading_unit_id, @precious_metal_ratio, @precious_metal_loading)";
                    cmd_washcoat.Parameters.AddWithValue("@general_id", general_id);
                    cmd_washcoat.Parameters.AddWithValue("@material_id", Washcoats[i].Id);

                    if (Washcoats[i].NeedPreciousMetal == true)
                    {
                        string metal_loading = "txb_" + Washcoats[i].WashcoatValue;
                        CustomTextBox txb_m_loading = (CustomTextBox)grid_generalInfo.FindName(metal_loading);
                        string metal_ratio = "txb_" + Washcoats[i].WashcoatValue + "_ratio";
                        CustomTextBox txb_m_ratio = (CustomTextBox)grid_generalInfo.FindName(metal_ratio);
                        string unit_name = "cmb_" + Washcoats[i].WashcoatValue;
                        CustomComboBox cmb_unit = (CustomComboBox)grid_generalInfo.FindName(unit_name);
                        cmd_washcoat.Parameters.AddWithValue("@precious_metal_ratio", txb_m_ratio.Text);
                        cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading", txb_m_loading.Text);
                        cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading_unit_id", String.IsNullOrWhiteSpace(cmb_unit.Text) ? 0 : Convert.ToInt32(cmb_unit.SelectedValue));
                    }
                    else
                    {
                        cmd_washcoat.Parameters.AddWithValue("@precious_metal_ratio", "");
                        cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading", "");
                        cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading_unit_id", 0);
                    }
                    int n = sqliteDB.Insert(cmd_washcoat);
                }
            }
            //general info end here

            /**
             *  Insert for catalyst charac tab
             */

            //when the catalysttype is chosen as DOC, insertion is done following
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 1)
            {
                //insert into lgb
                CustomTextBox lightoff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,null,null,null,null,null,null,null,null,null,null,@space_velocity_lightoff_test,null,null,@link_to_share_folder,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_lightoff_test", lightoff.Text);
                cmd_lgb.Parameters.AddWithValue("@link_to_share_folder", "hahaha");
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                int id_last_lgb = sqliteDB.Insert(cmd_lgb);
                //get last id of lgb

                //isnert into catalyst charac
                CustomTextBox heat_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                CustomTextBox cool_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                CustomTextBox eff_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                CustomTextBox eff_temp_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                CustomTextBox heat_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                CustomTextBox cool_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                CustomTextBox eff_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                CustomTextBox eff_temp_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                CustomTextBox heat_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                CustomTextBox cool_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                CustomTextBox eff_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                CustomTextBox eff_temp_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                CustomTextBox heat_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                CustomTextBox cool_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                CustomTextBox eff_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                CustomTextBox eff_temp_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                SQLiteCommand cmd_charac = new SQLiteCommand();
                cmd_charac.CommandText = "insert into catalystcharacterisation (lgb_id,co_light_off_temp_heat_up,co_light_off_temp_cool_down,co_max_efficiency_temperature,co_max_conversion_efficiency,c3h6_light_off_temp_cool_down,c3h6_light_off_temp_heat_up,c3h6_max_efficiency_temperature,c3h6_max_conversion_efficiency,c3h8_light_off_temp_cool_down,c3h8_light_off_temp_heat_up,c3h8_max_efficiency_temperature,c3h8_max_conversion_efficiency,No_light_off_temp_heat_up,No_light_off_temp_cool_down,No_No2_max_efficiency_temperature,No_No2_max_conversion_efficiency,Nox_max_efficiency_temperature,Nox_max_conversion_efficiency) values (@lgb_id, @co_light_off_temp_heat_up, @co_light_off_temp_cool_down, @co_max_efficiency_temperature, @co_max_conversion_efficiency, @c3h6_light_off_temp_cool_down, @c3h6_light_off_temp_heat_up, @c3h6_max_efficiency_temperature, @c3h6_max_conversion_efficiency, @c3h8_light_off_temp_cool_down, @c3h8_light_off_temp_heat_up, @c3h8_max_efficiency_temperature, @c3h8_max_conversion_efficiency, @No_light_off_temp_heat_up, @No_light_off_temp_cool_down, @No_No2_max_efficiency_temperature, @No_No2_max_conversion_efficiency, null, null)";
                cmd_charac.Parameters.AddWithValue("@lgb_id", id_last_lgb);
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_co.Text) ? 0 : Convert.ToDouble(heat_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_co.Text) ? 0 : Convert.ToDouble(cool_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_co.Text) ? 0 : Convert.ToDouble(eff_temp_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_co.Text) ? 0 : Convert.ToDouble(eff_co.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h6.Text) ? 0 : Convert.ToDouble(cool_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h6.Text) ? 0 : Convert.ToDouble(heat_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h6.Text) ? 0 : Convert.ToDouble(eff_temp_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h6.Text) ? 0 : Convert.ToDouble(eff_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h8.Text) ? 0 : Convert.ToDouble(cool_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h8.Text) ? 0 : Convert.ToDouble(heat_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h8.Text) ? 0 : Convert.ToDouble(eff_temp_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h8.Text) ? 0 : Convert.ToDouble(eff_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_no.Text) ? 0 : Convert.ToDouble(heat_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_no.Text) ? 0 : Convert.ToDouble(cool_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_no.Text) ? 0 : Convert.ToDouble(eff_temp_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_no.Text) ? 0 : Convert.ToDouble(eff_no.Text));
                int id_last_char = sqliteDB.Insert(cmd_charac);
            }

            // when the catalysttype is chosen as NSC/LNT, insertion is done as following
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 2)
            {
                CustomTextBox lightoff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                CustomTextBox oxygen_test = (CustomTextBox)grid_catalyst_charac.FindName("txb_oxygen_test");
                CustomTextBox max_osc = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_OSC");
                CustomTextBox max_osc_temp = (CustomTextBox)grid_catalyst_charac.FindName("txb_temp_OSC");
                CustomComboBox meas_osc = (CustomComboBox)grid_catalyst_charac.FindName("cmb_max_meas_OSC");
                CustomTextBox eff_nox = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_nox");
                CustomTextBox eff_tempnox = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempnox");
                CustomTextBox max_nox = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_NOx");
                CustomTextBox max_nox_temp = (CustomTextBox)grid_catalyst_charac.FindName("txb_temp_NOx");
                CustomComboBox meas_nox = (CustomComboBox)grid_catalyst_charac.FindName("cmb_max_meas_NOx");
                CustomTextBox nox_test = (CustomTextBox)grid_catalyst_charac.FindName("txt_nox_test");
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");


                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,null, @max_nox_storage_capacity, @max_nox_storage_capacity_temp, @max_nox_unit_id, null, null, @max_osc_storage_capacity, @max_osc_storage_capacity_temp, @max_osc_unit_id, null, @space_velocity_lightoff_test, @space_velocity_nox_capacity_test, @space_velocity_o2_capacity_test, @link_to_share_folder, @comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_lightoff_test", lightoff.Text);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_o2_capacity_test", oxygen_test.Text);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_nox_capacity_test", nox_test.Text);
                cmd_lgb.Parameters.AddWithValue("@max_osc_storage_capacity", String.IsNullOrWhiteSpace(max_osc.Text) ? 0 : Convert.ToDouble(max_osc.Text));
                cmd_lgb.Parameters.AddWithValue("@max_osc_unit_id", String.IsNullOrWhiteSpace(meas_osc.Text) ? 0 : Convert.ToInt32(meas_osc.SelectedValue));
                cmd_lgb.Parameters.AddWithValue("@max_osc_storage_capacity_temp", String.IsNullOrWhiteSpace(max_osc_temp.Text) ? 0 : Convert.ToDouble(max_osc_temp.Text));
                cmd_lgb.Parameters.AddWithValue("@max_nox_storage_capacity", String.IsNullOrWhiteSpace(max_nox.Text) ? 0 : Convert.ToDouble(max_nox.Text));
                cmd_lgb.Parameters.AddWithValue("@max_nox_storage_capacity_temp", String.IsNullOrWhiteSpace(max_nox_temp.Text) ? 0 : Convert.ToInt32(max_nox_temp.Text));
                cmd_lgb.Parameters.AddWithValue("@max_nox_unit_id", String.IsNullOrWhiteSpace(meas_osc.Text) ? 0 : Convert.ToInt32(meas_osc.SelectedValue));
                cmd_lgb.Parameters.AddWithValue("@link_to_share_folder", "hahaha");
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                int id_last_lgb = sqliteDB.Insert(cmd_lgb);

                //insert into catalyst charac
                CustomTextBox heat_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                CustomTextBox cool_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                CustomTextBox eff_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                CustomTextBox eff_temp_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                CustomTextBox heat_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                CustomTextBox cool_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                CustomTextBox eff_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                CustomTextBox eff_temp_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                CustomTextBox heat_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                CustomTextBox cool_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                CustomTextBox eff_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                CustomTextBox eff_temp_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                CustomTextBox heat_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                CustomTextBox cool_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                CustomTextBox eff_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                CustomTextBox eff_temp_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                CustomTextBox nox_max_eff = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_nox");
                CustomTextBox nox_max_eff_temp = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempnox");
                SQLiteCommand cmd_charac = new SQLiteCommand();
                cmd_charac.CommandText = "insert into catalystcharacterisation (lgb_id,co_light_off_temp_heat_up,co_light_off_temp_cool_down,co_max_efficiency_temperature,co_max_conversion_efficiency,c3h6_light_off_temp_cool_down,c3h6_light_off_temp_heat_up,c3h6_max_efficiency_temperature,c3h6_max_conversion_efficiency,c3h8_light_off_temp_cool_down,c3h8_light_off_temp_heat_up,c3h8_max_efficiency_temperature,c3h8_max_conversion_efficiency,No_light_off_temp_heat_up,No_light_off_temp_cool_down,No_No2_max_efficiency_temperature,No_No2_max_conversion_efficiency,Nox_max_efficiency_temperature,Nox_max_conversion_efficiency) values (@lgb_id, @co_light_off_temp_heat_up, @co_light_off_temp_cool_down, @co_max_efficiency_temperature, @co_max_conversion_efficiency, @c3h6_light_off_temp_cool_down, @c3h6_light_off_temp_heat_up, @c3h6_max_efficiency_temperature, @c3h6_max_conversion_efficiency, @c3h8_light_off_temp_cool_down, @c3h8_light_off_temp_heat_up, @c3h8_max_efficiency_temperature, @c3h8_max_conversion_efficiency, @No_light_off_temp_heat_up, @No_light_off_temp_cool_down, @No_No2_max_efficiency_temperature, @No_No2_max_conversion_efficiency, @Nox_max_efficiency_temperature, @Nox_max_conversion_efficiency)";
                cmd_charac.Parameters.AddWithValue("@lgb_id", id_last_lgb);
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_co.Text) ? 0 : Convert.ToDouble(heat_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_co.Text) ? 0 : Convert.ToDouble(cool_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_co.Text) ? 0 : Convert.ToDouble(eff_temp_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_co.Text) ? 0 : Convert.ToDouble(eff_co.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h6.Text) ? 0 : Convert.ToDouble(cool_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h6.Text) ? 0 : Convert.ToDouble(heat_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h6.Text) ? 0 : Convert.ToDouble(eff_temp_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h6.Text) ? 0 : Convert.ToDouble(eff_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h8.Text) ? 0 : Convert.ToDouble(cool_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h8.Text) ? 0 : Convert.ToDouble(heat_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h8.Text) ? 0 : Convert.ToDouble(eff_temp_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h8.Text) ? 0 : Convert.ToDouble(eff_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_no.Text) ? 0 : Convert.ToDouble(heat_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_no.Text) ? 0 : Convert.ToDouble(cool_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_no.Text) ? 0 : Convert.ToDouble(eff_temp_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_no.Text) ? 0 : Convert.ToDouble(eff_no.Text));
                cmd_charac.Parameters.AddWithValue("@Nox_max_efficiency_temperature", String.IsNullOrWhiteSpace(nox_max_eff_temp.Text) ? 0 : Convert.ToDouble(nox_max_eff_temp.Text));
                cmd_charac.Parameters.AddWithValue("@Nox_max_conversion_efficiency", String.IsNullOrWhiteSpace(nox_max_eff.Text) ? 0 : Convert.ToDouble(nox_max_eff.Text));
                int id_last_char = sqliteDB.Insert(cmd_charac);

            }

            // when the catalysttype is chosen as SCR, insertion is done as following
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 3)
            {
                CustomTextBox max_amn = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_amn");
                CustomComboBox max_amn_meas = (CustomComboBox)grid_catalyst_charac.FindName("cbm_max_amn_meas");
                CustomTextBox lightoff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");

                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,null,null,null,null,@max_ammonia_storage_capacity,@max_ammonia_unit_id,null,null,null,@space_velocity_performed_test,null,null,null,@link_to_share_folder,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_performed_test", lightoff.Text);
                cmd_lgb.Parameters.AddWithValue("@max_ammonia_storage_capacity", String.IsNullOrWhiteSpace(max_amn.Text) ? 0 : Convert.ToDouble(max_amn.Text));
                cmd_lgb.Parameters.AddWithValue("@max_ammonia_unit_id", String.IsNullOrWhiteSpace(max_amn_meas.Text) ? 0 : Convert.ToInt32(max_amn_meas.SelectedValue));
                cmd_lgb.Parameters.AddWithValue("@link_to_share_folder", "hahaha");
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                sqliteDB.Insert(cmd_lgb);


            }

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 4)
            {
                CustomTextBox max_amn = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_amn");
                ComboBox max_amn_meas = (ComboBox)grid_catalyst_charac.FindName("cbm_max_amn_meas");
                CustomTextBox lightoff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                CustomTextBox soot_loading = (CustomTextBox)grid_catalyst_charac.FindName("txb_soot_loading");
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");

                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,@soot_loading,null,null,null,@max_ammonia_storage_capacity,@max_ammonia_unit_id,null,null,null,@space_velocity_performed_test,null,null,null,@link_to_share_folder,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_performed_test", lightoff.Text);
                cmd_lgb.Parameters.AddWithValue("@max_ammonia_storage_capacity", String.IsNullOrWhiteSpace(max_amn.Text) ? 0 : Convert.ToDouble(max_amn.Text));
                cmd_lgb.Parameters.AddWithValue("@max_ammonia_unit_id", String.IsNullOrWhiteSpace(max_amn_meas.Text) ? 0 : Convert.ToInt32(max_amn_meas.SelectedValue));
                cmd_lgb.Parameters.AddWithValue("@soot_loading", String.IsNullOrWhiteSpace(soot_loading.Text) ? 0 : Convert.ToInt32(soot_loading.Text));
                cmd_lgb.Parameters.AddWithValue("@link_to_share_folder", "hahaha");
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                sqliteDB.Insert(cmd_lgb);
            }

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 5)
            {
                //insert into lgb
                CustomTextBox lightoff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                CustomTextBox soot_loading = (CustomTextBox)grid_catalyst_charac.FindName("txb_soot_loading");
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");

                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,@soot_loading,null,null,null,null, null,null,null,null,null,@space_velocity_performed_test,null,null,@link_to_share_folder,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@soot_loading", soot_loading.Text);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_performed_test", lightoff.Text);
                cmd_lgb.Parameters.AddWithValue("@link_to_share_folder", "hahaha");
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                int id_last_lgb = sqliteDB.Insert(cmd_lgb);

                //isnert into catalyst charac
                CustomTextBox heat_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                CustomTextBox cool_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                CustomTextBox eff_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                CustomTextBox eff_temp_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                CustomTextBox heat_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                CustomTextBox cool_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                CustomTextBox eff_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                CustomTextBox eff_temp_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                CustomTextBox heat_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                CustomTextBox cool_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                CustomTextBox eff_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                CustomTextBox eff_temp_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                CustomTextBox heat_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                CustomTextBox cool_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                CustomTextBox eff_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                CustomTextBox eff_temp_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                SQLiteCommand cmd_charac = new SQLiteCommand();
                cmd_charac.CommandText = "insert into catalystcharacterisation (lgb_id,co_light_off_temp_heat_up,co_light_off_temp_cool_down,co_max_efficiency_temperature,co_max_conversion_efficiency,c3h6_light_off_temp_cool_down,c3h6_light_off_temp_heat_up,c3h6_max_efficiency_temperature,c3h6_max_conversion_efficiency,c3h8_light_off_temp_cool_down,c3h8_light_off_temp_heat_up,c3h8_max_efficiency_temperature,c3h8_max_conversion_efficiency,No_light_off_temp_heat_up,No_light_off_temp_cool_down,No_No2_max_efficiency_temperature,No_No2_max_conversion_efficiency,Nox_max_efficiency_temperature,Nox_max_conversion_efficiency) values (@lgb_id, @co_light_off_temp_heat_up, @co_light_off_temp_cool_down, @co_max_efficiency_temperature, @co_max_conversion_efficiency, @c3h6_light_off_temp_cool_down, @c3h6_light_off_temp_heat_up, @c3h6_max_efficiency_temperature, @c3h6_max_conversion_efficiency, @c3h8_light_off_temp_cool_down, @c3h8_light_off_temp_heat_up, @c3h8_max_efficiency_temperature, @c3h8_max_conversion_efficiency, @No_light_off_temp_heat_up, @No_light_off_temp_cool_down, @No_No2_max_efficiency_temperature, @No_No2_max_conversion_efficiency, null, null)";
                cmd_charac.Parameters.AddWithValue("@lgb_id", id_last_lgb);
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_co.Text) ? 0 : Convert.ToDouble(heat_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_co.Text) ? 0 : Convert.ToDouble(cool_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_co.Text) ? 0 : Convert.ToDouble(eff_temp_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_co.Text) ? 0 : Convert.ToDouble(eff_co.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h6.Text) ? 0 : Convert.ToDouble(cool_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h6.Text) ? 0 : Convert.ToDouble(heat_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h6.Text) ? 0 : Convert.ToDouble(eff_temp_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h6.Text) ? 0 : Convert.ToDouble(eff_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h8.Text) ? 0 : Convert.ToDouble(cool_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h8.Text) ? 0 : Convert.ToDouble(heat_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h8.Text) ? 0 : Convert.ToDouble(eff_temp_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h8.Text) ? 0 : Convert.ToDouble(eff_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_no.Text) ? 0 : Convert.ToDouble(heat_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_no.Text) ? 0 : Convert.ToDouble(cool_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_no.Text) ? 0 : Convert.ToDouble(eff_temp_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_no.Text) ? 0 : Convert.ToDouble(eff_no.Text));
                int id_last_char = sqliteDB.Insert(cmd_charac);
            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 6)
            {
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");

                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,null,null,null,null,null,null,null,null,null,null,null,null,null,null,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                sqliteDB.Insert(cmd_lgb);
            }

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 7)
            {
                CustomTextBox lightoff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                CustomTextBox oxygen_test = (CustomTextBox)grid_catalyst_charac.FindName("txb_oxygen_test");
                CustomTextBox max_osc = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_OSC");
                CustomTextBox max_osc_temp = (CustomTextBox)grid_catalyst_charac.FindName("txb_temp_OSC");
                ComboBox meas_osc = (ComboBox)grid_catalyst_charac.FindName("cmb_max_meas_OSC");
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");


                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,null,null,null,null,null,null,@max_osc_storage_capacity,@max_osc_storage_capacity_temp,@max_osc_unit_id,null,@space_velocity_performed_test,null,@space_velocity_o2_capacity_test,@link_to_share_folder,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@space_velocity_performed_test", String.IsNullOrWhiteSpace(lightoff.Text) ? 0 : Convert.ToDouble(lightoff.Text));
                cmd_lgb.Parameters.AddWithValue("@space_velocity_o2_capacity_test", String.IsNullOrWhiteSpace(oxygen_test.Text) ? 0 : Convert.ToDouble(oxygen_test.Text));
                cmd_lgb.Parameters.AddWithValue("@max_osc_storage_capacity", String.IsNullOrWhiteSpace(max_osc.Text) ? 0 : Convert.ToDouble(max_osc.Text));
                cmd_lgb.Parameters.AddWithValue("@max_osc_unit_id", String.IsNullOrWhiteSpace(meas_osc.Text) ? 0 : Convert.ToInt32(meas_osc.SelectedValue));
                cmd_lgb.Parameters.AddWithValue("@max_osc_storage_capacity_temp", String.IsNullOrWhiteSpace(max_osc_temp.Text) ? 0 : Convert.ToDouble(max_osc_temp.Text));
                cmd_lgb.Parameters.AddWithValue("@link_to_share_folder", "hahahah");
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                int id_last_lgb = sqliteDB.Insert(cmd_lgb);

                //isnert into catalyst charac
                CustomTextBox heat_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                CustomTextBox cool_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                CustomTextBox eff_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                CustomTextBox eff_temp_co = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                CustomTextBox heat_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                CustomTextBox cool_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                CustomTextBox eff_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                CustomTextBox eff_temp_c3h6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                CustomTextBox heat_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                CustomTextBox cool_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                CustomTextBox eff_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                CustomTextBox eff_temp_c3h8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                CustomTextBox heat_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                CustomTextBox cool_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                CustomTextBox eff_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                CustomTextBox eff_temp_no = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                SQLiteCommand cmd_charac = new SQLiteCommand();
                cmd_charac.CommandText = "insert into catalystcharacterisation (lgb_id,co_light_off_temp_heat_up,co_light_off_temp_cool_down,co_max_efficiency_temperature,co_max_conversion_efficiency,c3h6_light_off_temp_cool_down,c3h6_light_off_temp_heat_up,c3h6_max_efficiency_temperature,c3h6_max_conversion_efficiency,c3h8_light_off_temp_cool_down,c3h8_light_off_temp_heat_up,c3h8_max_efficiency_temperature,c3h8_max_conversion_efficiency,No_light_off_temp_heat_up,No_light_off_temp_cool_down,No_No2_max_efficiency_temperature,No_No2_max_conversion_efficiency,Nox_max_efficiency_temperature,Nox_max_conversion_efficiency) values (@lgb_id, @co_light_off_temp_heat_up, @co_light_off_temp_cool_down, @co_max_efficiency_temperature, @co_max_conversion_efficiency, @c3h6_light_off_temp_cool_down, @c3h6_light_off_temp_heat_up, @c3h6_max_efficiency_temperature, @c3h6_max_conversion_efficiency, @c3h8_light_off_temp_cool_down, @c3h8_light_off_temp_heat_up, @c3h8_max_efficiency_temperature, @c3h8_max_conversion_efficiency, @No_light_off_temp_heat_up, @No_light_off_temp_cool_down, @No_No2_max_efficiency_temperature, @No_No2_max_conversion_efficiency, null, null)";
                cmd_charac.Parameters.AddWithValue("@lgb_id", id_last_lgb);
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_co.Text) ? 0 : Convert.ToDouble(heat_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_co.Text) ? 0 : Convert.ToDouble(cool_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_co.Text) ? 0 : Convert.ToDouble(eff_temp_co.Text));
                cmd_charac.Parameters.AddWithValue("@co_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_co.Text) ? 0 : Convert.ToDouble(eff_co.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h6.Text) ? 0 : Convert.ToDouble(cool_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h6.Text) ? 0 : Convert.ToDouble(heat_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h6.Text) ? 0 : Convert.ToDouble(eff_temp_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h6_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h6.Text) ? 0 : Convert.ToDouble(eff_c3h6.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_c3h8.Text) ? 0 : Convert.ToDouble(cool_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_c3h8.Text) ? 0 : Convert.ToDouble(heat_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_c3h8.Text) ? 0 : Convert.ToDouble(eff_temp_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@c3h8_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_c3h8.Text) ? 0 : Convert.ToDouble(eff_c3h8.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_heat_up", String.IsNullOrWhiteSpace(heat_no.Text) ? 0 : Convert.ToDouble(heat_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_light_off_temp_cool_down", String.IsNullOrWhiteSpace(cool_no.Text) ? 0 : Convert.ToDouble(cool_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_efficiency_temperature", String.IsNullOrWhiteSpace(eff_temp_no.Text) ? 0 : Convert.ToDouble(eff_temp_no.Text));
                cmd_charac.Parameters.AddWithValue("@No_No2_max_conversion_efficiency", String.IsNullOrWhiteSpace(eff_no.Text) ? 0 : Convert.ToDouble(eff_no.Text));
                int id_last_char = sqliteDB.Insert(cmd_charac);

            }

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 8)
            {
                CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");

                SQLiteCommand cmd_lgb = new SQLiteCommand();
                cmd_lgb.CommandText = "insert into lgb (catalyst_id,soot_loading,max_nox_storage_capacity,max_nox_storage_capacity_temp,max_nox_unit_id,max_ammonia_storage_capacity,max_ammonia_unit_id,max_osc_storage_capacity,max_osc_storage_capacity_temp,max_osc_unit_id,space_velocity_performed_test,space_velocity_lightoff_test,space_velocity_nox_capacity_test,space_velocity_o2_capacity_test,link_to_share_folder,comment_lgb) values (@catalyst_id,null,null,null,null,null,null,null,null,null,null,null,null,null,null,@comment_lgb)";
                cmd_lgb.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
                cmd_lgb.Parameters.AddWithValue("@comment_lgb", comment.Text);
                sqliteDB.Insert(cmd_lgb);
            }


            /**
             * End for LGB
             */


            /**
             * Beginning of inserting simulation
             */

            SQLiteCommand cmd_simulation = new SQLiteCommand();
            cmd_simulation.CommandText = "insert into simulation(src_measurement_id, src_data_id, tool_id, catalyst_id, model_type_id, model_version, link_to_share_folder, comment_simulation) values (@src_measurement_id, @src_data_id, @tool_id, @catalyst_id, @model_type_id, @model_version, @link_to_share_folder, @comment_simulation)";
            cmd_simulation.Parameters.AddWithValue("@src_measurement_id", String.IsNullOrWhiteSpace(cmb_src_measurement.Text) ? null : cmb_src_measurement.SelectedValue);
            cmd_simulation.Parameters.AddWithValue("@src_data_id", String.IsNullOrWhiteSpace(cmb_src_data.Text) ? null : cmb_src_data.SelectedValue);
            cmd_simulation.Parameters.AddWithValue("@tool_id", String.IsNullOrWhiteSpace(cmb_simulation_tool.Text) ? null : cmb_simulation_tool.SelectedValue);
            cmd_simulation.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
            cmd_simulation.Parameters.AddWithValue("@model_type_id", String.IsNullOrWhiteSpace(cmb_model_type.Text) ? null : cmb_model_type.SelectedValue);
            cmd_simulation.Parameters.AddWithValue("@model_version", txb_model_version.Text);
            cmd_simulation.Parameters.AddWithValue("@link_to_share_folder", txb_model_version.Text);
            cmd_simulation.Parameters.AddWithValue("@comment_simulation", txb_comment_simulation.Text);
            int last_simulation_id = sqliteDB.Insert(cmd_simulation);

            for (int i = 7; i < currSimRow; i++)
            {
                Label a = (Label)grid_simulation.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == i && Grid.GetColumn(el) == 0);
                StackPanel st = (StackPanel)grid_simulation.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == i && Grid.GetColumn(el) == 1);
                CustomComboBox c = null;
                foreach (UIElement e in st.Children)
                {
                    c = (CustomComboBox)e;
                    break;
                }

                int catalyst_id = Convert.ToInt32(c.Name.Substring(4));
                SQLiteCommand s = new SQLiteCommand();
                s.CommandText = "insert into CombinedCatalyst (catalyst_id, model_type_id, simulation_id) values  (@catalyst_id, @model_type_id, @simulation_id)";
                s.Parameters.AddWithValue("@catalyst_id", catalyst_id);
                s.Parameters.AddWithValue("@model_type_id", String.IsNullOrWhiteSpace(c.Text) ? 0 : c.SelectedValue);
                s.Parameters.AddWithValue("@simulation_id", last_simulation_id);
                sqliteDB.Insert(s);
            }



            //insert combined catalysts


            //insert the combined 

            /*
             * End of inserting to simulation
             */

            /*
             * Insert Testbench and vehicle
             */


            SQLiteCommand cmd_testbench = new SQLiteCommand();
            cmd_testbench.CommandText = "insert into testbenchandvehicle (catalyst_id,engine_manufacturer,application_field,emission_legislation,engine_displacement,engine_power,number_of_cylinders,eats_setup,comment_on_ecu_labels,link_to_share_folder,comment_on_testbench) values (@catalyst_id, @engine_manufacturer, @application_field, @emission_legislation, @engine_displacement, @engine_power, @number_of_cylinders, @eats_setup, @comment_on_ecu_labels, @link_to_share_folder, @comment_on_testbench)";
            cmd_testbench.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
            cmd_testbench.Parameters.AddWithValue("@application_field", txb_app_field.Text);
            cmd_testbench.Parameters.AddWithValue("@engine_manufacturer", txb_engine_manufac.Text);
            cmd_testbench.Parameters.AddWithValue("@emission_legislation", txb_emission.Text);
            cmd_testbench.Parameters.AddWithValue("@engine_displacement", String.IsNullOrWhiteSpace(txb_engine_displ.Text) ? 0 : Convert.ToDouble(txb_engine_displ.Text));
            cmd_testbench.Parameters.AddWithValue("@engine_power", String.IsNullOrWhiteSpace(txb_engine_power.Text) ? 0 : Convert.ToInt16(txb_engine_power.Text));
            cmd_testbench.Parameters.AddWithValue("@number_of_cylinders", String.IsNullOrWhiteSpace(txb_number_cylinder.Text) ? 0 : Convert.ToInt16(txb_number_cylinder.Text));
            cmd_testbench.Parameters.AddWithValue("@eats_setup", txb_eats_setup.Text);
            cmd_testbench.Parameters.AddWithValue("@comment_on_ecu_labels", txb_comment_testbench_ecu.Text);
            cmd_testbench.Parameters.AddWithValue("@link_to_share_folder", "hahah");
            cmd_testbench.Parameters.AddWithValue("@comment_on_testbench", txb_comment_testbench.Text);
            int id_last_testbench = sqliteDB.Insert(cmd_testbench);


            //insert for application fields of testbench
            for (int i = 0; i < ApplicationFieldsTestbench.Count(); i++)
            {
                if (ApplicationFieldsTestbench[i].IsChecked == true)
                {
                    SQLiteCommand cmd_appfields = new SQLiteCommand();
                    cmd_appfields.CommandText = "insert into TestBenchApp (app_field_id,testbench_id) values (@app_field_id, @testbench_id)";
                    cmd_appfields.Parameters.AddWithValue("@app_field_id", ApplicationFieldsTestbench[i].Id);
                    cmd_appfields.Parameters.AddWithValue("@testbench_id", id_last_testbench);
                    sqliteDB.Insert(cmd_appfields);
                }
            }


            for (int i = 0; i < SteadyStateLegislations.Count(); i++)
            {
                if (SteadyStateLegislations[i].IsChecked == true)
                {
                    for (int j = 0; j < Emissions.Count(); j++)
                    {
                        string raw_name = "txb_raw_" + SteadyStateLegislations[i].Legislation + Emissions[j].Name;
                        string tailpipe_name = "txb_tailpipe_" + SteadyStateLegislations[i].Legislation + Emissions[j].Name;
                        string unit_name = "cmb_" + SteadyStateLegislations[i].Legislation + Emissions[j].Name;
                        CustomTextBox raw = (CustomTextBox)grid_testbench.FindName(raw_name);
                        CustomTextBox tailpipe = (CustomTextBox)grid_testbench.FindName(tailpipe_name);
                        ComboBox cmb_unit = (ComboBox)grid_testbench.FindName(unit_name);
                        SQLiteCommand cmd_emission = new SQLiteCommand();
                        cmd_emission.CommandText = "insert into TestbenchAndSteady (emission_id, steady_state_id, testbench_id, raw, tailpipe, steady_unit) values (@emission_id, @steady_state_id, @testbench_id, @raw, @tailpipe, @steady_unit)";
                        cmd_emission.Parameters.AddWithValue("@emission_id", Emissions[j].Id);
                        cmd_emission.Parameters.AddWithValue("@steady_state_id", SteadyStateLegislations[i].Id);
                        cmd_emission.Parameters.AddWithValue("@testbench_id", id_last_testbench);
                        cmd_emission.Parameters.AddWithValue("@raw", raw.Text);
                        cmd_emission.Parameters.AddWithValue("@tailpipe", tailpipe.Text);
                        cmd_emission.Parameters.AddWithValue("@steady_unit", cmb_unit.Text);
                        sqliteDB.Insert(cmd_emission);
                    }
                }
            }

            for (int i = 0; i < TransientLegislations.Count(); i++)
            {
                if (TransientLegislations[i].IsChecked == true)
                {
                    for (int j = 0; j < Emissions.Count(); j++)
                    {
                        string raw_name = "txb_raw_" + TransientLegislations[i].Legislation + Emissions[j].Name;
                        string tailpipe_name = "txb_tailpipe_" + TransientLegislations[i].Legislation + Emissions[j].Name;
                        string unit_name = "cmb_" + TransientLegislations[i].Legislation + Emissions[j].Name;
                        CustomTextBox raw = (CustomTextBox)grid_testbench.FindName(raw_name);
                        CustomTextBox tailpipe = (CustomTextBox)grid_testbench.FindName(tailpipe_name);
                        ComboBox cmb_unit = (ComboBox)grid_testbench.FindName(unit_name);
                        SQLiteCommand cmd_emission = new SQLiteCommand();
                        cmd_emission.CommandText = "insert into testbenchandtransient (emission_id, transient_id, testbench_id, raw, tailpipe, transient_unit) values (@emission_id, @transient_id, @testbench_id, @raw, @tailpipe, @transient_unit)";
                        cmd_emission.Parameters.AddWithValue("@emission_id", Emissions[j].Id);
                        cmd_emission.Parameters.AddWithValue("@transient_id", TransientLegislations[i].Id);
                        cmd_emission.Parameters.AddWithValue("@testbench_id", id_last_testbench);
                        cmd_emission.Parameters.AddWithValue("@raw", raw.Text);
                        cmd_emission.Parameters.AddWithValue("@tailpipe", tailpipe.Text);
                        cmd_emission.Parameters.AddWithValue("@transient_unit", cmb_unit.Text);
                        sqliteDB.Insert(cmd_emission);
                    }
                }
            }

            /*
             * End for testbench
             */

            /*
             *  Beginning of chem/analysis
             */
            SQLiteCommand cmd_chemanalysis = new SQLiteCommand();
            cmd_chemanalysis.CommandText = "insert into ChemPhysAnalysis (cristalline_function_id,catalyst_id,brick_substrate,washcoat_substrate,ssa_specific_surface_area_,makroporosity_total_porosity,makroporosity_average_pore_radius,cristalline_washcoat_components_,cristalline_washcoat_zeolith,zone_coating,layer,dispersity,heat_capacity,comment_chem,link_to_share_folder) values ( @cristalline_function_id, @catalyst_id, @brick_substrate, @washcoat_substrate, @ssa_specific_surface_area_, @makroporosity_total_porosity, @makroporosity_average_pore_radius, @cristalline_washcoat_components_, @cristalline_washcoat_zeolith, @zone_coating, @layer, @dispersity, @heat_capacity, @comment_chem, 'nanananna')";
            cmd_chemanalysis.Parameters.AddWithValue("@cristalline_function_id", String.IsNullOrWhiteSpace(cmb_crist_function.Text) ? null : cmb_crist_function.SelectedValue);
            cmd_chemanalysis.Parameters.AddWithValue("@catalyst_id", last_catalyst_id);
            cmd_chemanalysis.Parameters.AddWithValue("@brick_substrate", txb_brick_substrate.Text);
            cmd_chemanalysis.Parameters.AddWithValue("@washcoat_substrate", txb_washcoat_substrate.Text);
            cmd_chemanalysis.Parameters.AddWithValue("@ssa_specific_surface_area_", String.IsNullOrWhiteSpace(txb_spec_surface.Text) ? 0 : Convert.ToDouble(txb_spec_surface.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@makroporosity_total_porosity", String.IsNullOrWhiteSpace(txb_total_porosity.Text) ? 0 : Convert.ToInt32(txb_total_porosity.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@makroporosity_average_pore_radius", String.IsNullOrWhiteSpace(txb_avg_porosity.Text) ? 0 : Convert.ToDouble(txb_avg_porosity.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@cristalline_washcoat_components_", txb_comp_1.Text);
            cmd_chemanalysis.Parameters.AddWithValue("@cristalline_washcoat_zeolith", chk_crist_zeolith.IsChecked);
            cmd_chemanalysis.Parameters.AddWithValue("@zone_coating", String.IsNullOrWhiteSpace(cmb_number_zone.Text) ? 0 : Convert.ToInt32(cmb_number_zone.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@layer", String.IsNullOrWhiteSpace(cmb_layer.Text) ? 0 : Convert.ToInt32(cmb_layer.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@dispersity", String.IsNullOrWhiteSpace(txb_ch_dispersity.Text) ? 0 : Convert.ToDouble(txb_ch_dispersity.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@heat_capacity", String.IsNullOrWhiteSpace(txb_ch_heat_capacity.Text) ? 0 : Convert.ToDouble(txb_ch_heat_capacity.Text));
            cmd_chemanalysis.Parameters.AddWithValue("@comment_chem", txb_comment_chem.Text);
            int id_last_chem = sqliteDB.Insert(cmd_chemanalysis);

            for (int i = 0; i < PreciousMetalLoadings.Count(); i++)
            {
                if (PreciousMetalLoadings[i].IsChecked == true)
                {
                    string txb = "txb_wash" + PreciousMetalLoadings[i].Name;
                    string cmb = "cmb_wash" + PreciousMetalLoadings[i].Name;
                    CustomTextBox prec_val = (CustomTextBox)grid_chem_analysis.FindName(txb);
                    CustomComboBox prec_unit = (CustomComboBox)grid_chem_analysis.FindName(cmb);
                    SQLiteCommand cmd_precious = new SQLiteCommand();
                    cmd_precious.CommandText = "insert into loading (analysis_id, precious_metal_loading_id, loading_value, precious_metal_loading_unit_id) values (@analysis_id, @precious_metal_loading_id, @loading_value, @precious_metal_loading_unit_id)";
                    cmd_precious.Parameters.AddWithValue("@analysis_id", id_last_chem);
                    cmd_precious.Parameters.AddWithValue("@precious_metal_loading_id", PreciousMetalLoadings[i].Id);
                    cmd_precious.Parameters.AddWithValue("@loading_value", String.IsNullOrWhiteSpace(prec_val.Text) ? 0 : Convert.ToDouble(prec_val.Text));
                    cmd_precious.Parameters.AddWithValue("@precious_metal_loading_unit_id", String.IsNullOrWhiteSpace(prec_unit.Text) ? 0 : Convert.ToInt32(prec_unit.SelectedValue));
                    sqliteDB.Insert(cmd_precious);
                }
            }

            for (int i = 0; i < chemCompositions.Count(); i++)
            {
                string comp = "txb_comp_" + chemCompositions[i];
                string comp_val = "txb_compval_" + chemCompositions[i];
                CustomTextBox txb_c = (CustomTextBox)grid_chem_analysis.FindName(comp);
                CustomTextBox txb_v = (CustomTextBox)grid_chem_analysis.FindName(comp_val);
                if (!String.IsNullOrWhiteSpace(txb_c.Text) && !String.IsNullOrWhiteSpace(txb_v.Text))
                {
                    SQLiteCommand cmd_comp = new SQLiteCommand();
                    cmd_comp.CommandText = "insert into WashcoatElemental (analysis_id, washcoat_composition, washcoat_composition_value) values (@analysis_id, @washcoat_composition, @washcoat_composition_value)";
                    cmd_comp.Parameters.AddWithValue("@analysis_id", id_last_chem);
                    cmd_comp.Parameters.AddWithValue("@washcoat_composition", txb_c.Text);
                    cmd_comp.Parameters.AddWithValue("@washcoat_composition_value", String.IsNullOrWhiteSpace(txb_v.Text) ? 0 : Convert.ToDouble(txb_v.Text));
                    sqliteDB.Insert(cmd_comp);
                }
            }

            for (int i = 0; i < chemSupportMaterials.Count(); i++)
            {
                string s_mtr = "txb_support_mtr_" + chemSupportMaterials[i];
                string s_val = "txb_support_val_" + chemSupportMaterials[i];
                string s_sup = "cmb_sup_" + chemSupportMaterials[i];
                CustomTextBox t_sp = (CustomTextBox)grid_chem_analysis.FindName(s_mtr);
                CustomTextBox t_vl = (CustomTextBox)grid_chem_analysis.FindName(s_val);
                CustomComboBox c_sup = (CustomComboBox)grid_chem_analysis.FindName(s_sup);
                if (!String.IsNullOrWhiteSpace(t_sp.Text) && !String.IsNullOrWhiteSpace(t_vl.Text))
                {
                    SQLiteCommand cmd_support = new SQLiteCommand();
                    cmd_support.CommandText = "insert into supportmaterial (analysis_id, support_material_loading, support_material_loading_value, support_unit_id) values (@analysis_id, @support_material_loading, @support_material_loading_value, @support_unit_id)";
                    cmd_support.Parameters.AddWithValue("@analysis_id", id_last_chem);
                    cmd_support.Parameters.AddWithValue("@support_material_loading", t_sp.Text);
                    cmd_support.Parameters.AddWithValue("@support_material_loading_value", String.IsNullOrWhiteSpace(t_vl.Text) ? 0 : Convert.ToDouble(t_vl.Text));
                    cmd_support.Parameters.AddWithValue("@support_unit_id", String.IsNullOrWhiteSpace(c_sup.Text) ? 0 : Convert.ToInt32(c_sup.SelectedValue));
                    sqliteDB.Insert(cmd_support);
                }


            }
            MessageBoxResult result = MessageBox.Show(" New Catalyst Data with ID = " + last_catalyst_id + " added. Start uploading the files now?", "Successfull Catalyst Insert", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    System.IO.Directory.CreateDirectory(last_catalyst_id.ToString());
                    System.IO.Directory.CreateDirectory(last_catalyst_id + "/GeneralInformation");
                    for (int i = 0; i < genFilesToCopy.Count(); i++)
                    {
                        System.IO.File.Copy(genFilesToCopy[i].Location, last_catalyst_id + "/GeneralInformation/" + genFilesToCopy[i].Name, true);
                    }
                    System.IO.Directory.CreateDirectory(last_catalyst_id + "/LGB");
                    for (int i = 0; i < lgbFilesToCopy.Count(); i++)
                    {
                        System.IO.File.Copy(lgbFilesToCopy[i].Location, last_catalyst_id + "/LGB/" + lgbFilesToCopy[i].Name, true);
                    }
                    System.IO.Directory.CreateDirectory(last_catalyst_id + "/Simulation");
                    for (int i = 0; i < simuFilesToCopy.Count(); i++)
                    {
                        System.IO.File.Copy(simuFilesToCopy[i].Location, last_catalyst_id + "/Simulation/" + simuFilesToCopy[i].Name, true);
                    }
                    System.IO.Directory.CreateDirectory(last_catalyst_id + "/Testbench");
                    for (int i = 0; i < testFilesToCopy.Count(); i++)
                    {
                        System.IO.File.Copy(testFilesToCopy[i].Location, last_catalyst_id + "/Testbench/" + testFilesToCopy[i].Name, true);
                    }
                    System.IO.Directory.CreateDirectory(last_catalyst_id + "/ChemPhys");
                    for (int i = 0; i < chemFilesToCopy.Count(); i++)
                    {
                        System.IO.File.Copy(chemFilesToCopy[i].Location, last_catalyst_id + "/ChemPhys/" + chemFilesToCopy[i].Name, true);
                    }
                    MessageBox.Show("Completed Successfully!");
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("No Files were added!");
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }

        }



        private void btn_saveon_localdisk_Click(object sender, RoutedEventArgs e)
        {

        }


        private void menu_search_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow s = new SearchWindow();
            s.Show();
        }

        public void OpenCatalyst(int catalystID)
        {
            SqliteDBConnection db = new SqliteDBConnection();
            currentLoadedCatalyst = new Dictionary<UIElement, string>();
            //for genealinformation
            List<String>[] resultList = db.Select("select * from generalinformation where generalinformation.catalyst_id =  " + catalystID);
            //add to ids
            if (updateIDs.ContainsKey("catalyst_id"))
            {
                updateIDs.Remove("catalyst_id");
            }
            updateIDs.Add("catalyst_id", catalystID.ToString());
            catalyst_id.Text = resultList[1][0].ToString();
            currentLoadedCatalyst.Add(catalyst_id, resultList[1][0].ToString());
            string general_id = resultList[0][0].ToString();
            //add to ids
            if (updateIDs.ContainsKey("general_id"))
            {
                updateIDs.Remove("general_id");
            }
            updateIDs.Add("general_id", general_id);
            if (!String.IsNullOrWhiteSpace(resultList[2][0]))
            {
                DateTime date = DateTime.ParseExact(resultList[2][0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                dtp_production_date.SelectedDate = date;
            }
            currentLoadedCatalyst.Add(dtp_production_date, resultList[2][0]);
            txb_catalyst_nr.Text = resultList[3][0];
            currentLoadedCatalyst.Add(txb_catalyst_nr, resultList[3][0]);
            txb_substrat_nr.Text = resultList[4][0];
            currentLoadedCatalyst.Add(txb_substrat_nr, resultList[4][0]);
            txb_project_number.Text = resultList[5][0];
            currentLoadedCatalyst.Add(txb_project_number, resultList[5][0]);
            txb_customer.Text = resultList[6][0];
            currentLoadedCatalyst.Add(txb_customer, resultList[6][0]);
            txb_project_manager.Text = resultList[7][0];
            currentLoadedCatalyst.Add(txb_project_manager, resultList[7][0]);
            txb_eats_case_worker.Text = resultList[8][0];
            currentLoadedCatalyst.Add(txb_eats_case_worker, resultList[8][0]);
            txb_target_country.Text = resultList[9][0];
            currentLoadedCatalyst.Add(txb_target_country, resultList[9][0]);
            txb_target_emission_legislation.Text = resultList[10][0];
            currentLoadedCatalyst.Add(txb_target_emission_legislation, resultList[10][0]);
            txb_conf_of_target_system.Text = resultList[11][0];
            currentLoadedCatalyst.Add(txb_conf_of_target_system, resultList[11][0]);
            txb_spec_of_engine.Text = resultList[12][0];
            currentLoadedCatalyst.Add(txb_spec_of_engine, resultList[12][0]);
            txb_washcoat_loading.Text = resultList[13][0] == "0" ? "" : resultList[13][0];
            currentLoadedCatalyst.Add(txb_washcoat_loading, resultList[13][0] == "0" ? "" : resultList[13][0]);
            txb_cell_density.Text = resultList[14][0] == "0" ? "" : resultList[14][0];
            currentLoadedCatalyst.Add(txb_cell_density, resultList[14][0] == "0" ? "" : resultList[14][0]);
            txb_wall_thickness.Text = resultList[15][0] == "0" ? "" : resultList[15][0];
            currentLoadedCatalyst.Add(txb_wall_thickness, resultList[15][0] == "0" ? "" : resultList[15][0]);
            if (!String.IsNullOrWhiteSpace(resultList[16][0]))
            {
                cmb_substrate_boundary_shape.SelectedValue = Convert.ToInt32(resultList[16][0]);
            }
            currentLoadedCatalyst.Add(cmb_substrate_boundary_shape, resultList[16][0]);
            cbx_zone_coating.IsChecked = Convert.ToBoolean(resultList[17][0]);
            currentLoadedCatalyst.Add(cbx_zone_coating, resultList[17][0]);
            cbx_slip_catalyst_applied.IsChecked = Convert.ToBoolean(resultList[18][0]);
            currentLoadedCatalyst.Add(cbx_slip_catalyst_applied, resultList[18][0]);
            txb_volume.Text = resultList[19][0] == "0" ? "" : resultList[19][0];
            currentLoadedCatalyst.Add(txb_volume, resultList[19][0] == "0" ? "" : resultList[19][0]);
            txb_monolith_length.Text = resultList[20][0] == "0" ? "" : resultList[20][0];
            currentLoadedCatalyst.Add(txb_monolith_length, resultList[20][0] == "0" ? "" : resultList[20][0]);
            txb_monolith_diameter.Text = resultList[21][0] == "0" ? "" : resultList[21][0];
            currentLoadedCatalyst.Add(txb_monolith_diameter, resultList[21][0] == "0" ? "" : resultList[21][0]);
            if (!String.IsNullOrWhiteSpace(resultList[22][0]))
            {
                cmb_monoloth_material.SelectedValue = Convert.ToInt32(resultList[22][0]);
            }
            currentLoadedCatalyst.Add(cmb_monoloth_material, resultList[22][0]);
            txb_max_temp_grad_axial.Text = resultList[23][0] == "0" ? "" : resultList[23][0];
            currentLoadedCatalyst.Add(txb_max_temp_grad_axial, resultList[23][0] == "0" ? "" : resultList[23][0]);
            txb_max_temp_grad_radial.Text = resultList[24][0] == "0" ? "" : resultList[24][0];
            currentLoadedCatalyst.Add(txb_max_temp_grad_radial, resultList[24][0] == "0" ? "" : resultList[24][0]);
            txb_max_temp_limit_peak.Text = resultList[25][0] == "0" ? "" : resultList[25][0];
            currentLoadedCatalyst.Add(txb_max_temp_limit_peak, resultList[25][0] == "0" ? "" : resultList[25][0]);
            txb_max_temp_limit_longterm.Text = resultList[26][0] == "0" ? "" : resultList[26][0];
            currentLoadedCatalyst.Add(txb_max_temp_limit_longterm, resultList[26][0] == "0" ? "" : resultList[26][0]);
            txb_max_hc_limit.Text = resultList[27][0];
            currentLoadedCatalyst.Add(txb_max_hc_limit, resultList[27][0]);
            txb_segment_x.Text = resultList[28][0] == "0" ? "" : resultList[28][0];
            currentLoadedCatalyst.Add(txb_segment_x, resultList[28][0] == "0" ? "" : resultList[28][0]);
            txb_segment_y.Text = resultList[29][0] == "0" ? "" : resultList[29][0];
            currentLoadedCatalyst.Add(txb_segment_y, resultList[29][0] == "0" ? "" : resultList[29][0]);
            txb_prec_loss_coef.Text = resultList[30][0] == "0" ? "" : resultList[30][0];
            currentLoadedCatalyst.Add(txb_prec_loss_coef, resultList[30][0] == "0" ? "" : resultList[30][0]);
            txb_soot_mass_limit.Text = resultList[31][0] == "0" ? "" : resultList[31][0];
            currentLoadedCatalyst.Add(txb_soot_mass_limit, resultList[31][0] == "0" ? "" : resultList[31][0]);
            txb_dpf_inlet_cell.Text = resultList[32][0] == "0" ? "" : resultList[32][0];
            currentLoadedCatalyst.Add(txb_dpf_inlet_cell, resultList[32][0] == "0" ? "" : resultList[32][0]);
            txb_dpf_outlet_cell.Text = resultList[33][0] == "0" ? "" : resultList[33][0];
            currentLoadedCatalyst.Add(txb_dpf_outlet_cell, resultList[33][0] == "0" ? "" : resultList[33][0]);
            if (!String.IsNullOrWhiteSpace(resultList[34][0]))
            {
                cmb_aging_status.SelectedValue = Convert.ToInt32(resultList[34][0]);

            }
            currentLoadedCatalyst.Add(cmb_aging_status, resultList[34][0]);
            if (!String.IsNullOrWhiteSpace(resultList[35][0]))
            {
                cmb_aging_procedure.SelectedValue = Convert.ToInt32(resultList[35][0]);
            }
            currentLoadedCatalyst.Add(cmb_aging_procedure, resultList[35][0]);
            txb_aging_duration.Text = resultList[36][0] == "0" ? "" : resultList[36][0];
            currentLoadedCatalyst.Add(txb_aging_duration, resultList[36][0] == "0" ? "" : resultList[36][0]);
            if (!String.IsNullOrWhiteSpace(resultList[37][0]))
            {
                cmb_aging_duration.SelectedValue = Convert.ToInt32(resultList[37][0]);
            }
            currentLoadedCatalyst.Add(cmb_aging_duration, resultList[37][0]);
            txb_comment_general_info.Text = resultList[40][0];
            currentLoadedCatalyst.Add(txb_comment_general_info, resultList[40][0]);

            //For GeneralWashcoat
            List<String>[] washcoatResult = db.Select("select * from GeneralWashcoat where GeneralWashcoat.general_id = " + general_id);
            if (washcoatResult != null && washcoatResult[0].Count() > 0)
            {
                for (int j = 0; j < Washcoats.Count(); j++)
                {
                    for (int i = 0; i < washcoatResult[0].Count(); i++)
                    {
                        if (Washcoats[j].Id == Convert.ToInt32(washcoatResult[2][i]))
                        {
                            Washcoats[j].IsChecked = true;
                            currentWashcoats[j].IsChecked = true;

                            CustomTextBox t = (CustomTextBox)grid_generalInfo.FindName("txb_" + Washcoats[j].WashcoatValue);
                            CustomComboBox c = (CustomComboBox)grid_generalInfo.FindName("cmb_" + Washcoats[j].WashcoatValue);
                            CustomTextBox t1 = (CustomTextBox)grid_generalInfo.FindName("txb_" + Washcoats[j].WashcoatValue + "_ratio");
                            t.Text = washcoatResult[5][i];
                            currentLoadedCatalyst.Add(t, washcoatResult[5][i]);
                            if (Convert.ToInt32(washcoatResult[3][i]) != 0 && washcoatResult[3][i] != null)
                            {
                                c.SelectedValue = washcoatResult[3][i];
                                currentLoadedCatalyst.Add(c, washcoatResult[3][i]);
                            }
                            t1.Text = washcoatResult[4][i];
                            currentLoadedCatalyst.Add(t1, washcoatResult[4][i]);
                        }
                    }
                }
            }

            //For GeneralApplicationField
            List<String>[] generalAppResult = db.Select("select * from GeneralInformationApplicationField where GeneralInformationApplicationField.general_id=  " + general_id);
            if (generalAppResult != null && generalAppResult[0].Count() > 0)
            {
                for (int i = 0; i < ApplicationFields.Count(); i++)
                {
                    for (int j = 0; j < generalAppResult[0].Count(); j++)
                    {
                        if (ApplicationFields[i].Id == Convert.ToInt32(generalAppResult[2][j]))
                        {
                            ApplicationFields[i].IsChecked = true;
                            currentAppplicationFields[i].IsChecked = true;
                        }
                    }
                }
            }

            //For GeneralManufacturer
            List<String>[] generalManuResult = db.Select("select * from GeneralInformationManufacturer where GeneralInformationManufacturer.general_id = " + general_id);
            if (generalManuResult != null && generalManuResult[0].Count() > 0)
            {
                for (int i = 0; i < Manufacturers.Count(); i++)
                {
                    for (int j = 0; j < generalManuResult[0].Count(); j++)
                    {
                        if (Manufacturers[i].Id == Convert.ToInt32(generalManuResult[2][j]))
                        {
                            Manufacturers[i].IsChecked = true;
                            currentManufacturers[i].IsChecked = true;
                        }
                    }
                }
            }
            //Catalyst Result
            List<String>[] catalystResult = db.Select("select * from catalyst where catalyst.catalyst_id =  " + catalystID);
            if (!String.IsNullOrWhiteSpace(catalystResult[2][0]))
            {
                cmb_catalyst_type.SelectedValue = Convert.ToInt32(catalystResult[2][0]);
                currentLoadedCatalyst.Add(cmb_catalyst_type, catalystResult[2][0]);
            }
            cxb_is_approved.IsChecked = Convert.ToBoolean(catalystResult[4][0]);
            currentLoadedCatalyst.Add(cxb_is_approved, catalystResult[4][0]);
            cxb_confidentiality.IsChecked = Convert.ToBoolean(catalystResult[5][0]);
            currentLoadedCatalyst.Add(cxb_confidentiality, catalystResult[5][0]);

            if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalystID + "/GeneralInformation"))
            {
                string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/GeneralInformation");
                foreach (string name in genFiles)
                {
                    general_relatedFiles.Items.Add(Path.GetFileName(name));
                }
            }
            cmb_catalyst_type.ClearValue(CustomComboBox.BorderBrushProperty);
            cmb_catalyst_type.ClearValue(CustomComboBox.BorderThicknessProperty);
            int currentRow = 0;
            int currentColumn = 0;
            //Catalyst Characterisation
            //unregister the ui components names from the grid. 
            try
            {
                if (previouslySelectedCatalystType == 1)
                {
                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                }

                if (previouslySelectedCatalystType == 2)
                {
                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");

                    grid_catalyst_charac.UnregisterName("txb_oxygen_test");
                    grid_catalyst_charac.UnregisterName("txb_max_OSC");
                    grid_catalyst_charac.UnregisterName("txb_temp_OSC");
                    grid_catalyst_charac.UnregisterName("cmb_max_meas_OSC");
                    grid_catalyst_charac.UnregisterName("txb_eff_nox");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempnox");
                    grid_catalyst_charac.UnregisterName("txb_max_NOx");
                    grid_catalyst_charac.UnregisterName("txb_temp_NOx");
                    grid_catalyst_charac.UnregisterName("cmb_max_meas_NOx");
                    grid_catalyst_charac.UnregisterName("txt_nox_test");
                }

                if (previouslySelectedCatalystType == 3)
                {
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                    grid_catalyst_charac.UnregisterName("txb_max_amn");
                    grid_catalyst_charac.UnregisterName("cbm_max_amn_meas");
                }

                if (previouslySelectedCatalystType == 4)
                {
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                    grid_catalyst_charac.UnregisterName("txb_max_amn");
                    grid_catalyst_charac.UnregisterName("cbm_max_amn_meas");
                    grid_catalyst_charac.UnregisterName("txb_soot_loading");

                }

                if (previouslySelectedCatalystType == 5)
                {

                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");
                    grid_catalyst_charac.UnregisterName("txb_soot_loading");
                }

                if (previouslySelectedCatalystType == 7)
                {
                    grid_catalyst_charac.UnregisterName("txb_heat_CO");
                    grid_catalyst_charac.UnregisterName("txb_cool_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_CO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempCO");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H6");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H6");
                    grid_catalyst_charac.UnregisterName("txb_heat_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_cool_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_C3H8");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempC3H8");
                    grid_catalyst_charac.UnregisterName("txb_heat_NO");
                    grid_catalyst_charac.UnregisterName("txb_cool_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_NO");
                    grid_catalyst_charac.UnregisterName("txb_eff_tempNO");
                    grid_catalyst_charac.UnregisterName("txb_lightoff_test");

                    grid_catalyst_charac.UnregisterName("txb_oxygen_test");
                    grid_catalyst_charac.UnregisterName("txb_max_OSC");
                    grid_catalyst_charac.UnregisterName("txb_temp_OSC");
                    grid_catalyst_charac.UnregisterName("cmb_max_meas_OSC");
                }

                grid_catalyst_charac.UnregisterName("txb_comment_char");
                grid_catalyst_charac.UnregisterName("char_relatedFiles");
            }

            catch { }

            grid_catalyst_charac.Children.Clear();

            if (firstKind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForFirstKind("CO", "CO", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("CO", "CO", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateForFirstKind("C3H6", "C3H6", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("C3H6", "C3H6", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateForFirstKind("C3H8", "C3H8", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("C3H8", "C3H8", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateForFirstKind("NO", "NO to NO2", "Light-off Temperature", currentColumn, currentRow);
                GenerateEfficiency("NO", "NO to NO2", currentColumn, currentRow + 1);
                currentRow = currentRow + 3;
                GenerateSpaceVelocityOfLight("Light-Off ", currentRow, currentColumn);
                currentRow = currentRow + 1;
            }

            if (secondKind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForSecondKind("OSC", currentRow, currentRow + 1, currentColumn);
                currentRow = currentRow + 2;
                GenerateOxygenStorage(currentRow, currentColumn);
                currentRow = currentRow + 1;
            }

            if (thirdKind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForThirdKind(currentRow, currentColumn);
                currentRow = currentRow + 1;
                GenerateSpaceVelocityOfLight(" ", currentRow, currentColumn);
                currentRow = currentRow + 1;
            }

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 2)
            {
                GenerateEfficiency("nox", "NOx", currentColumn, currentRow);
                currentRow = currentRow + 2;
                GenerateForSecondKind("NOx", currentRow, currentRow + 1, currentColumn);
                currentRow = currentRow + 2;
                GenerateNoxStorage(currentRow, currentColumn);
                currentRow = currentRow + 1;

            }
            if (fifthkind.Contains(Convert.ToInt32(cmb_catalyst_type.SelectedValue)))
            {
                GenerateForSoot(currentRow, currentColumn);
                currentRow = currentRow + 1;

            }
            previouslySelectedCatalystType = Convert.ToInt32(cmb_catalyst_type.SelectedValue);
            GenerateDefaultForCharac(currentRow, currentColumn);

            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 1)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);
                if (lgbResult != null && lgbResult[0].Count() > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox lightOff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                    lightOff.Text = lgbResult[12][0] == "0" ? "" : lgbResult[12][0];
                    currentLoadedCatalyst.Add(lightOff, lgbResult[12][0] == "0" ? "" : lgbResult[12][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                    List<string>[] charResult = db.Select("select * from CatalystCharacterisation where CatalystCharacterisation.lgb_id =" + lgbResult[0][0]);
                    if (charResult != null && charResult[0].Count() > 0)
                    {
                        //add to ids
                        if (updateIDs.ContainsKey("characterisation_id"))
                        {
                            updateIDs.Remove("characterisation_id");
                        }
                        updateIDs.Add("characterisation_id", charResult[0][0]);
                        CustomTextBox txb_heat_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                        txb_heat_CO.Text = charResult[2][0] == "0" ? "" : charResult[2][0];
                        currentLoadedCatalyst.Add(txb_heat_CO, charResult[2][0] == "0" ? "" : charResult[2][0]);
                        CustomTextBox txb_cool_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                        txb_cool_CO.Text = charResult[3][0] == "0" ? "" : charResult[3][0];
                        currentLoadedCatalyst.Add(txb_cool_CO, charResult[3][0] == "0" ? "" : charResult[3][0]);
                        CustomTextBox txb_eff_tempCO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                        txb_eff_tempCO.Text = charResult[4][0] == "0" ? "" : charResult[4][0];
                        currentLoadedCatalyst.Add(txb_eff_tempCO, charResult[4][0] == "0" ? "" : charResult[4][0]);
                        CustomTextBox txb_eff_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                        txb_eff_CO.Text = charResult[5][0] == "0" ? "" : charResult[5][0];
                        currentLoadedCatalyst.Add(txb_eff_CO, charResult[5][0] == "0" ? "" : charResult[5][0]);
                        CustomTextBox txb_cool_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                        txb_cool_C3H6.Text = charResult[6][0] == "0" ? "" : charResult[6][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H6, charResult[6][0] == "0" ? "" : charResult[6][0]);
                        CustomTextBox txb_heat_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                        txb_heat_C3H6.Text = charResult[7][0] == "0" ? "" : charResult[7][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H6, charResult[7][0] == "0" ? "" : charResult[7][0]);
                        CustomTextBox txb_eff_tempC3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                        txb_eff_tempC3H6.Text = charResult[8][0] == "0" ? "" : charResult[8][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H6, charResult[8][0] == "0" ? "" : charResult[8][0]);
                        CustomTextBox txb_eff_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                        txb_eff_C3H6.Text = charResult[9][0] == "0" ? "" : charResult[9][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H6, charResult[9][0] == "0" ? "" : charResult[9][0]);
                        CustomTextBox txb_cool_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                        txb_cool_C3H8.Text = charResult[10][0] == "0" ? "" : charResult[10][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H8, charResult[10][0] == "0" ? "" : charResult[10][0]);
                        CustomTextBox txb_heat_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                        txb_heat_C3H8.Text = charResult[11][0] == "0" ? "" : charResult[11][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H8, charResult[11][0] == "0" ? "" : charResult[11][0]);
                        CustomTextBox txb_eff_tempC3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                        txb_eff_tempC3H8.Text = charResult[12][0] == "0" ? "" : charResult[12][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H8, charResult[12][0] == "0" ? "" : charResult[12][0]);
                        CustomTextBox txb_eff_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                        txb_eff_C3H8.Text = charResult[13][0] == "0" ? "" : charResult[13][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H8, charResult[13][0] == "0" ? "" : charResult[13][0]);
                        CustomTextBox txb_heat_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                        txb_heat_NO.Text = charResult[14][0] == "0" ? "" : charResult[14][0];
                        currentLoadedCatalyst.Add(txb_heat_NO, charResult[14][0] == "0" ? "" : charResult[14][0]);
                        CustomTextBox txb_cool_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                        txb_cool_NO.Text = charResult[15][0] == "0" ? "" : charResult[15][0];
                        currentLoadedCatalyst.Add(txb_cool_NO, charResult[15][0] == "0" ? "" : charResult[15][0]);
                        CustomTextBox txb_eff_tempNO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                        txb_eff_tempNO.Text = charResult[16][0] == "0" ? "" : charResult[16][0];
                        currentLoadedCatalyst.Add(txb_eff_tempNO, charResult[16][0] == "0" ? "" : charResult[16][0]);
                        CustomTextBox txb_eff_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                        txb_eff_NO.Text = charResult[17][0] == "0" ? "" : charResult[17][0];
                        currentLoadedCatalyst.Add(txb_eff_NO, charResult[17][0] == "0" ? "" : charResult[17][0]);

                    }

                    if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB"))
                    {
                        string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                        ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                        l.Items.Clear();
                        foreach (string name in genFiles)
                        {
                            l.Items.Add(Path.GetFileName(name));
                        }
                    }
                }
            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 2)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);

                if (lgbResult != null && lgbResult[0].Count() > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox txb_max_NOx = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_NOx");
                    txb_max_NOx.Text = lgbResult[3][0] == "0" ? "" : lgbResult[3][0];
                    currentLoadedCatalyst.Add(txb_max_NOx, lgbResult[3][0] == "0" ? "" : lgbResult[3][0]);
                    CustomTextBox txb_temp_NOx = (CustomTextBox)grid_catalyst_charac.FindName("txb_temp_NOx");
                    txb_temp_NOx.Text = lgbResult[4][0] == "0" ? "" : lgbResult[4][0];
                    currentLoadedCatalyst.Add(txb_temp_NOx, lgbResult[4][0] == "0" ? "" : lgbResult[4][0]);
                    CustomComboBox cmb_max_meas_NOx = (CustomComboBox)grid_catalyst_charac.FindName("cmb_max_meas_NOx");
                    if (!String.IsNullOrWhiteSpace(lgbResult[5][0]))
                    {
                        cmb_max_meas_NOx.SelectedValue = Convert.ToInt32(lgbResult[5][0]);
                    }
                    currentLoadedCatalyst.Add(cmb_max_meas_NOx, lgbResult[5][0]);
                    CustomTextBox txb_max_OSC = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_OSC");
                    txb_max_OSC.Text = lgbResult[8][0] == "0" ? "" : lgbResult[8][0];
                    currentLoadedCatalyst.Add(txb_max_OSC, lgbResult[8][0] == "0" ? "" : lgbResult[8][0]);
                    CustomTextBox txb_temp_OSC = (CustomTextBox)grid_catalyst_charac.FindName("txb_temp_OSC");
                    txb_temp_OSC.Text = lgbResult[9][0] == "0" ? "" : lgbResult[9][0];
                    currentLoadedCatalyst.Add(txb_temp_OSC, lgbResult[9][0] == "0" ? "" : lgbResult[9][0]);
                    CustomComboBox cmb_max_meas_OSC = (CustomComboBox)grid_catalyst_charac.FindName("cmb_max_meas_OSC");
                    if (!String.IsNullOrWhiteSpace(lgbResult[10][0]))
                    {
                        cmb_max_meas_OSC.SelectedValue = Convert.ToInt32(lgbResult[10][0]);
                    }
                    currentLoadedCatalyst.Add(cmb_max_meas_OSC, lgbResult[10][0]);
                    CustomTextBox lightOff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                    lightOff.Text = lgbResult[12][0] == "0" ? "" : lgbResult[12][0];
                    currentLoadedCatalyst.Add(lightOff, lgbResult[12][0] == "0" ? "" : lgbResult[12][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                    CustomTextBox txt_nox_test = (CustomTextBox)grid_catalyst_charac.FindName("txt_nox_test");
                    txt_nox_test.Text = lgbResult[13][0] == "0" ? "" : lgbResult[13][0];
                    currentLoadedCatalyst.Add(txt_nox_test, lgbResult[13][0] == "0" ? "" : lgbResult[13][0]);
                    CustomTextBox txb_oxygen_test = (CustomTextBox)grid_catalyst_charac.FindName("txb_oxygen_test");
                    txb_oxygen_test.Text = lgbResult[14][0] == "0" ? "" : lgbResult[14][0];
                    currentLoadedCatalyst.Add(txb_oxygen_test, lgbResult[14][0] == "0" ? "" : lgbResult[14][0]);

                    List<string>[] charResult = db.Select("select * from CatalystCharacterisation where CatalystCharacterisation.lgb_id =" + lgbResult[0][0]);
                    if (charResult != null && charResult[0].Count() > 0)
                    {
                        //add to ids
                        if (updateIDs.ContainsKey("characterisation_id"))
                        {
                            updateIDs.Remove("characterisation_id");
                        }
                        updateIDs.Add("characterisation_id", charResult[0][0]);
                        CustomTextBox txb_heat_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                        txb_heat_CO.Text = charResult[2][0] == "0" ? "" : charResult[2][0];
                        currentLoadedCatalyst.Add(txb_heat_CO, charResult[2][0] == "0" ? "" : charResult[2][0]);
                        CustomTextBox txb_cool_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                        txb_cool_CO.Text = charResult[3][0] == "0" ? "" : charResult[3][0];
                        currentLoadedCatalyst.Add(txb_cool_CO, charResult[3][0] == "0" ? "" : charResult[3][0]);
                        CustomTextBox txb_eff_tempCO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                        txb_eff_tempCO.Text = charResult[4][0] == "0" ? "" : charResult[4][0];
                        currentLoadedCatalyst.Add(txb_eff_tempCO, charResult[4][0] == "0" ? "" : charResult[4][0]);
                        CustomTextBox txb_eff_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                        txb_eff_CO.Text = charResult[5][0] == "0" ? "" : charResult[5][0];
                        currentLoadedCatalyst.Add(txb_eff_CO, charResult[5][0] == "0" ? "" : charResult[5][0]);
                        CustomTextBox txb_cool_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                        txb_cool_C3H6.Text = charResult[6][0] == "0" ? "" : charResult[6][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H6, charResult[6][0] == "0" ? "" : charResult[6][0]);
                        CustomTextBox txb_heat_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                        txb_heat_C3H6.Text = charResult[7][0] == "0" ? "" : charResult[7][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H6, charResult[7][0] == "0" ? "" : charResult[7][0]);
                        CustomTextBox txb_eff_tempC3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                        txb_eff_tempC3H6.Text = charResult[8][0] == "0" ? "" : charResult[8][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H6, charResult[8][0] == "0" ? "" : charResult[8][0]);
                        CustomTextBox txb_eff_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                        txb_eff_C3H6.Text = charResult[9][0] == "0" ? "" : charResult[9][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H6, charResult[9][0] == "0" ? "" : charResult[9][0]);
                        CustomTextBox txb_cool_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                        txb_cool_C3H8.Text = charResult[10][0] == "0" ? "" : charResult[10][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H8, charResult[10][0] == "0" ? "" : charResult[10][0]);
                        CustomTextBox txb_heat_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                        txb_heat_C3H8.Text = charResult[11][0] == "0" ? "" : charResult[11][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H8, charResult[11][0] == "0" ? "" : charResult[11][0]);
                        CustomTextBox txb_eff_tempC3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                        txb_eff_tempC3H8.Text = charResult[12][0] == "0" ? "" : charResult[12][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H8, charResult[12][0] == "0" ? "" : charResult[12][0]);
                        CustomTextBox txb_eff_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                        txb_eff_C3H8.Text = charResult[13][0] == "0" ? "" : charResult[13][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H8, charResult[13][0] == "0" ? "" : charResult[13][0]);

                        CustomTextBox txb_heat_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                        txb_heat_NO.Text = charResult[14][0] == "0" ? "" : charResult[14][0];
                        currentLoadedCatalyst.Add(txb_heat_NO, charResult[14][0] == "0" ? "" : charResult[14][0]);
                        CustomTextBox txb_cool_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                        txb_cool_NO.Text = charResult[15][0] == "0" ? "" : charResult[15][0];
                        currentLoadedCatalyst.Add(txb_cool_NO, charResult[15][0] == "0" ? "" : charResult[15][0]);
                        CustomTextBox txb_eff_tempNO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                        txb_eff_tempNO.Text = charResult[16][0] == "0" ? "" : charResult[16][0];
                        currentLoadedCatalyst.Add(txb_eff_tempNO, charResult[16][0] == "0" ? "" : charResult[16][0]);
                        CustomTextBox txb_eff_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                        txb_eff_NO.Text = charResult[17][0] == "0" ? "" : charResult[17][0];
                        currentLoadedCatalyst.Add(txb_eff_NO, charResult[17][0] == "0" ? "" : charResult[17][0]);
                        CustomTextBox txb_eff_nox = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_nox");
                        txb_eff_nox.Text = charResult[18][0] == "0" ? "" : charResult[18][0];
                        currentLoadedCatalyst.Add(txb_eff_nox, charResult[18][0] == "0" ? "" : charResult[18][0]);
                        CustomTextBox txb_eff_tempnox = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempnox");
                        txb_eff_tempnox.Text = charResult[19][0] == "0" ? "" : charResult[19][0];
                        currentLoadedCatalyst.Add(txb_eff_tempnox, charResult[19][0] == "0" ? "" : charResult[19][0]);
                    }
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB/"))
                    {
                        string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                        ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                        l.Items.Clear();
                        foreach (string name in genFiles)
                        {
                            l.Items.Add(Path.GetFileName(name));
                        }
                    }
                }
            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 3)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);
                if (lgbResult != null && lgbResult[0].Count > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox lightOff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                    lightOff.Text = lgbResult[11][0] == "0" ? "" : lgbResult[11][0];
                    currentLoadedCatalyst.Add(lightOff, lgbResult[11][0] == "0" ? "" : lgbResult[11][0]);
                    CustomTextBox txb_max_amn = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_amn");
                    txb_max_amn.Text = lgbResult[6][0] == "0" ? "" : lgbResult[6][0];
                    currentLoadedCatalyst.Add(txb_max_amn, lgbResult[6][0] == "0" ? "" : lgbResult[6][0]);
                    CustomComboBox cbm_max_amn_meas = (CustomComboBox)grid_catalyst_charac.FindName("cbm_max_amn_meas");
                    if (!String.IsNullOrWhiteSpace(lgbResult[7][0]))
                    {
                        cbm_max_amn_meas.SelectedValue = Convert.ToInt32(lgbResult[7][0]);
                    }
                    currentLoadedCatalyst.Add(cbm_max_amn_meas, lgbResult[7][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB/"))
                {
                    string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                    ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                    l.Items.Clear();
                    foreach (string name in genFiles)
                    {
                        l.Items.Add(Path.GetFileName(name));
                    }
                }
            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 4)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);
                if (lgbResult != null && lgbResult[0].Count > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox lightOff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                    lightOff.Text = lgbResult[11][0] == "0" ? "" : lgbResult[11][0];
                    currentLoadedCatalyst.Add(lightOff, lgbResult[11][0] == "0" ? "" : lgbResult[11][0]);
                    CustomTextBox txb_soot_loading = (CustomTextBox)grid_catalyst_charac.FindName("txb_soot_loading");
                    txb_soot_loading.Text = lgbResult[2][0] == "0" ? "" : lgbResult[2][0];
                    currentLoadedCatalyst.Add(txb_soot_loading, lgbResult[2][0] == "0" ? "" : lgbResult[2][0]);
                    CustomTextBox txb_max_amn = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_amn");
                    txb_max_amn.Text = lgbResult[6][0] == "0" ? "" : lgbResult[6][0];
                    currentLoadedCatalyst.Add(txb_max_amn, lgbResult[6][0] == "0" ? "" : lgbResult[6][0]);
                    CustomComboBox cbm_max_amn_meas = (CustomComboBox)grid_catalyst_charac.FindName("cbm_max_amn_meas");
                    if (!String.IsNullOrWhiteSpace(lgbResult[7][0]))
                    {
                        cbm_max_amn_meas.SelectedValue = Convert.ToInt32(lgbResult[7][0]);
                    }
                    currentLoadedCatalyst.Add(cbm_max_amn_meas, lgbResult[7][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB/"))
                {
                    string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                    ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                    l.Items.Clear();
                    foreach (string name in genFiles)
                    {
                        l.Items.Add(Path.GetFileName(name));
                    }
                }
            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 5)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);
                if (lgbResult != null && lgbResult[0].Count() > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox lightOff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                    lightOff.Text = lgbResult[12][0] == "0" ? "" : lgbResult[12][0];
                    currentLoadedCatalyst.Add(lightOff, lgbResult[12][0] == "0" ? "" : lgbResult[12][0]);
                    CustomTextBox txb_soot_loading = (CustomTextBox)grid_catalyst_charac.FindName("txb_soot_loading");
                    txb_soot_loading.Text = lgbResult[2][0] == "0" ? "" : lgbResult[2][0];
                    currentLoadedCatalyst.Add(txb_soot_loading, lgbResult[2][0] == "0" ? "" : lgbResult[2][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                    List<string>[] charResult = db.Select("select * from CatalystCharacterisation where CatalystCharacterisation.lgb_id =" + lgbResult[0][0]);
                    if (charResult != null && charResult[0].Count() > 0)
                    {
                        //add to ids
                        if (updateIDs.ContainsKey("characterisation_id"))
                        {
                            updateIDs.Remove("characterisation_id");
                        }
                        updateIDs.Add("characterisation_id", charResult[0][0]);
                        CustomTextBox txb_heat_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                        txb_heat_CO.Text = charResult[2][0] == "0" ? "" : charResult[2][0];
                        currentLoadedCatalyst.Add(txb_heat_CO, charResult[2][0] == "0" ? "" : charResult[2][0]);
                        CustomTextBox txb_cool_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                        txb_cool_CO.Text = charResult[3][0] == "0" ? "" : charResult[3][0];
                        currentLoadedCatalyst.Add(txb_cool_CO, charResult[3][0] == "0" ? "" : charResult[3][0]);
                        CustomTextBox txb_eff_tempCO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                        txb_eff_tempCO.Text = charResult[4][0] == "0" ? "" : charResult[4][0];
                        currentLoadedCatalyst.Add(txb_eff_tempCO, charResult[4][0] == "0" ? "" : charResult[4][0]);
                        CustomTextBox txb_eff_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                        txb_eff_CO.Text = charResult[5][0] == "0" ? "" : charResult[5][0];
                        currentLoadedCatalyst.Add(txb_eff_CO, charResult[5][0] == "0" ? "" : charResult[5][0]);
                        CustomTextBox txb_cool_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                        txb_cool_C3H6.Text = charResult[6][0] == "0" ? "" : charResult[6][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H6, charResult[6][0] == "0" ? "" : charResult[6][0]);
                        CustomTextBox txb_heat_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                        txb_heat_C3H6.Text = charResult[7][0] == "0" ? "" : charResult[7][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H6, charResult[7][0] == "0" ? "" : charResult[7][0]);
                        CustomTextBox txb_eff_tempC3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                        txb_eff_tempC3H6.Text = charResult[8][0] == "0" ? "" : charResult[8][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H6, charResult[8][0] == "0" ? "" : charResult[8][0]);
                        CustomTextBox txb_eff_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                        txb_eff_C3H6.Text = charResult[9][0] == "0" ? "" : charResult[9][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H6, charResult[9][0] == "0" ? "" : charResult[9][0]);
                        CustomTextBox txb_cool_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                        txb_cool_C3H8.Text = charResult[10][0] == "0" ? "" : charResult[10][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H8, charResult[10][0] == "0" ? "" : charResult[10][0]);
                        CustomTextBox txb_heat_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                        txb_heat_C3H8.Text = charResult[11][0] == "0" ? "" : charResult[11][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H8, charResult[11][0] == "0" ? "" : charResult[11][0]);
                        CustomTextBox txb_eff_tempC3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                        txb_eff_tempC3H8.Text = charResult[12][0] == "0" ? "" : charResult[12][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H8, charResult[12][0] == "0" ? "" : charResult[12][0]);
                        CustomTextBox txb_eff_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                        txb_eff_C3H8.Text = charResult[13][0] == "0" ? "" : charResult[13][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H8, charResult[13][0] == "0" ? "" : charResult[13][0]);
                        CustomTextBox txb_heat_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                        txb_heat_NO.Text = charResult[14][0] == "0" ? "" : charResult[14][0];
                        currentLoadedCatalyst.Add(txb_heat_NO, charResult[14][0] == "0" ? "" : charResult[14][0]);
                        CustomTextBox txb_cool_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                        txb_cool_NO.Text = charResult[15][0] == "0" ? "" : charResult[15][0];
                        currentLoadedCatalyst.Add(txb_cool_NO, charResult[15][0] == "0" ? "" : charResult[15][0]);
                        CustomTextBox txb_eff_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                        txb_eff_NO.Text = charResult[16][0] == "0" ? "" : charResult[16][0];
                        currentLoadedCatalyst.Add(txb_eff_NO, charResult[16][0] == "0" ? "" : charResult[16][0]);
                        CustomTextBox txb_eff_tempNO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                        txb_eff_tempNO.Text = charResult[17][0] == "0" ? "" : charResult[17][0];
                        currentLoadedCatalyst.Add(txb_eff_tempNO, charResult[17][0] == "0" ? "" : charResult[17][0]);
                    }

                    if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB"))
                    {
                        string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                        ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                        l.Items.Clear();
                        foreach (string name in genFiles)
                        {
                            l.Items.Add(Path.GetFileName(name));
                        }
                    }
                }
            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 6 || Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 8)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);
                if (lgbResult != null && lgbResult[0].Count() > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB"))
                {
                    string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                    ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                    l.Items.Clear();
                    foreach (string name in genFiles)
                    {
                        l.Items.Add(Path.GetFileName(name));
                    }
                }

            }
            if (Convert.ToInt32(cmb_catalyst_type.SelectedValue) == 7)
            {
                List<String>[] lgbResult = db.Select("select * from Lgb where Lgb.catalyst_id  = " + catalystID);
                if (lgbResult != null && lgbResult[0].Count() > 0)
                {
                    //add to ids
                    if (updateIDs.ContainsKey("lgb_id"))
                    {
                        updateIDs.Remove("lgb_id");
                    }
                    updateIDs.Add("lgb_id", lgbResult[0][0]);
                    CustomTextBox lightOff = (CustomTextBox)grid_catalyst_charac.FindName("txb_lightoff_test");
                    lightOff.Text = lgbResult[12][0] == "0" ? "" : lgbResult[12][0];
                    currentLoadedCatalyst.Add(lightOff, lgbResult[12][0] == "0" ? "" : lgbResult[12][0]);
                    CustomTextBox comment = (CustomTextBox)grid_catalyst_charac.FindName("txb_comment_char");
                    comment.Text = lgbResult[16][0];
                    currentLoadedCatalyst.Add(comment, lgbResult[16][0]);
                    CustomTextBox txb_max_OSC = (CustomTextBox)grid_catalyst_charac.FindName("txb_max_OSC");
                    txb_max_OSC.Text = lgbResult[8][0] == "0" ? "" : lgbResult[8][0];
                    currentLoadedCatalyst.Add(txb_max_OSC, lgbResult[8][0] == "0" ? "" : lgbResult[8][0]);
                    CustomTextBox txb_temp_OSC = (CustomTextBox)grid_catalyst_charac.FindName("txb_temp_OSC");
                    txb_temp_OSC.Text = lgbResult[9][0] == "0" ? "" : lgbResult[9][0];
                    currentLoadedCatalyst.Add(txb_temp_OSC, lgbResult[9][0] == "0" ? "" : lgbResult[9][0]);
                    CustomTextBox txb_oxygen_test = (CustomTextBox)grid_catalyst_charac.FindName("txb_oxygen_test");
                    txb_oxygen_test.Text = lgbResult[14][0] == "0" ? "" : lgbResult[14][0];
                    currentLoadedCatalyst.Add(txb_oxygen_test, lgbResult[14][0] == "0" ? "" : lgbResult[14][0]);
                    CustomComboBox cmb_max_meas_OSC = (CustomComboBox)grid_catalyst_charac.FindName("cmb_max_meas_OSC");
                    if (lgbResult[10][0] != "" && lgbResult[10][0] != null)
                    {
                        cmb_max_meas_OSC.SelectedValue = Convert.ToInt32(lgbResult[10][0]);
                    }
                    currentLoadedCatalyst.Add(cmb_max_meas_OSC, lgbResult[10][0]);
                    List<string>[] charResult = db.Select("select * from CatalystCharacterisation where CatalystCharacterisation.lgb_id =" + lgbResult[0][0]);
                    if (charResult != null && charResult[0].Count() > 0)
                    {
                        //add to ids
                        if (updateIDs.ContainsKey("characterisation_id"))
                        {
                            updateIDs.Remove("characterisation_id");
                        }
                        updateIDs.Add("characterisation_id", charResult[0][0]);
                        CustomTextBox txb_heat_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_CO");
                        txb_heat_CO.Text = charResult[2][0] == "0" ? "" : charResult[2][0];
                        currentLoadedCatalyst.Add(txb_heat_CO, charResult[2][0] == "0" ? "" : charResult[2][0]);
                        CustomTextBox txb_cool_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_CO");
                        txb_cool_CO.Text = charResult[3][0] == "0" ? "" : charResult[3][0];
                        currentLoadedCatalyst.Add(txb_cool_CO, charResult[3][0] == "0" ? "" : charResult[3][0]);
                        CustomTextBox txb_eff_tempCO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempCO");
                        txb_eff_tempCO.Text = charResult[4][0] == "0" ? "" : charResult[4][0];
                        currentLoadedCatalyst.Add(txb_eff_tempCO, charResult[4][0] == "0" ? "" : charResult[4][0]);
                        CustomTextBox txb_eff_CO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_CO");
                        txb_eff_CO.Text = charResult[5][0] == "0" ? "" : charResult[5][0];
                        currentLoadedCatalyst.Add(txb_eff_CO, charResult[5][0] == "0" ? "" : charResult[5][0]);
                        CustomTextBox txb_cool_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H6");
                        txb_cool_C3H6.Text = charResult[6][0] == "0" ? "" : charResult[6][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H6, charResult[6][0] == "0" ? "" : charResult[6][0]);
                        CustomTextBox txb_heat_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H6");
                        txb_heat_C3H6.Text = charResult[7][0] == "0" ? "" : charResult[7][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H6, charResult[7][0] == "0" ? "" : charResult[7][0]);
                        CustomTextBox txb_eff_tempC3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H6");
                        txb_eff_tempC3H6.Text = charResult[8][0] == "0" ? "" : charResult[8][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H6, charResult[8][0] == "0" ? "" : charResult[8][0]);
                        CustomTextBox txb_eff_C3H6 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H6");
                        txb_eff_C3H6.Text = charResult[9][0] == "0" ? "" : charResult[9][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H6, charResult[9][0] == "0" ? "" : charResult[9][0]);
                        CustomTextBox txb_cool_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_C3H8");
                        txb_cool_C3H8.Text = charResult[10][0] == "0" ? "" : charResult[10][0];
                        currentLoadedCatalyst.Add(txb_cool_C3H8, charResult[10][0] == "0" ? "" : charResult[10][0]);
                        CustomTextBox txb_heat_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_C3H8");
                        txb_heat_C3H8.Text = charResult[11][0] == "0" ? "" : charResult[11][0];
                        currentLoadedCatalyst.Add(txb_heat_C3H8, charResult[11][0] == "0" ? "" : charResult[11][0]);
                        CustomTextBox txb_eff_tempC3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempC3H8");
                        txb_eff_tempC3H8.Text = charResult[12][0] == "0" ? "" : charResult[12][0];
                        currentLoadedCatalyst.Add(txb_eff_tempC3H8, charResult[12][0] == "0" ? "" : charResult[12][0]);
                        CustomTextBox txb_eff_C3H8 = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_C3H8");
                        txb_eff_C3H8.Text = charResult[13][0] == "0" ? "" : charResult[13][0];
                        currentLoadedCatalyst.Add(txb_eff_C3H8, charResult[13][0] == "0" ? "" : charResult[13][0]);
                        CustomTextBox txb_heat_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_heat_NO");
                        txb_heat_NO.Text = charResult[14][0] == "0" ? "" : charResult[14][0];
                        currentLoadedCatalyst.Add(txb_heat_NO, charResult[14][0] == "0" ? "" : charResult[14][0]);
                        CustomTextBox txb_cool_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_cool_NO");
                        txb_cool_NO.Text = charResult[15][0] == "0" ? "" : charResult[15][0];
                        currentLoadedCatalyst.Add(txb_cool_NO, charResult[15][0] == "0" ? "" : charResult[15][0]);
                        CustomTextBox txb_eff_NO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_NO");
                        txb_eff_NO.Text = charResult[16][0] == "0" ? "" : charResult[16][0];
                        currentLoadedCatalyst.Add(txb_eff_NO, charResult[16][0] == "0" ? "" : charResult[16][0]);
                        CustomTextBox txb_eff_tempNO = (CustomTextBox)grid_catalyst_charac.FindName("txb_eff_tempNO");
                        txb_eff_tempNO.Text = charResult[17][0] == "0" ? "" : charResult[17][0];
                        currentLoadedCatalyst.Add(txb_eff_tempNO, charResult[17][0] == "0" ? "" : charResult[17][0]);
                    }

                    if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB"))
                    {
                        string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/LGB");
                        ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                        l.Items.Clear();
                        foreach (string name in genFiles)
                        {
                            l.Items.Add(Path.GetFileName(name));
                        }
                    }
                }

            }
            // Simulation
            List<String>[] simulationResult = db.Select("select * from Simulation where Simulation.catalyst_id = " + catalystID);
            if (simulationResult != null && simulationResult[0].Count() > 0)
            {
                if (updateIDs.ContainsKey("simulation_id"))
                {
                    updateIDs.Remove("simulation_id");
                }
                updateIDs.Add("simulation_id", simulationResult[0][0]);
                if (simulationResult[1][0] != null && simulationResult[1][0] != "")
                {
                    cmb_src_measurement.SelectedValue = Convert.ToInt32(simulationResult[1][0]);
                }
                currentLoadedCatalyst.Add(cmb_src_measurement, simulationResult[1][0]);
                if (simulationResult[2][0] != null && simulationResult[2][0] != "")
                {
                    cmb_src_data.SelectedValue = Convert.ToInt32(simulationResult[2][0]);
                }
                currentLoadedCatalyst.Add(cmb_src_data, simulationResult[2][0]);
                if (simulationResult[3][0] != null && simulationResult[3][0] != "")
                {
                    cmb_simulation_tool.SelectedValue = Convert.ToInt32(simulationResult[3][0]);
                }
                currentLoadedCatalyst.Add(cmb_simulation_tool, simulationResult[3][0]);
                if (simulationResult[5][0] != null && simulationResult[5][0] != "")
                {
                    cmb_model_type.SelectedValue = Convert.ToInt32(simulationResult[5][0]);
                }
                currentLoadedCatalyst.Add(cmb_model_type, simulationResult[5][0]);
                txb_model_version.Text = simulationResult[6][0];
                currentLoadedCatalyst.Add(txb_model_version, simulationResult[6][0]);
                txb_comment_simulation.Text = simulationResult[8][0];
                currentLoadedCatalyst.Add(txb_comment_simulation, simulationResult[8][0]);


                //Combined DB
                List<String>[] combinedCatalystResult = db.Select("select * from CombinedCatalyst where CombinedCatalyst.simulation_id = " + simulationResult[0][0]);
                if (combinedCatalystResult != null && combinedCatalystResult[0].Count() > 0)
                {
                    for (int i = 0; i < combinedCatalystResult[0].Count(); i++)
                    {
                        generateCombinedCatalysts(combinedCatalystResult[3][i]);
                        CustomComboBox c = (CustomComboBox)grid_simulation.FindName("cmb_combined" + combinedCatalystResult[3][i]);
                        currentCombinedCatalysts.Add(combinedCatalystResult[3][i], c);
                        if (!String.IsNullOrWhiteSpace(combinedCatalystResult[1][i]))
                        {
                            c.SelectedValue = Convert.ToInt32(combinedCatalystResult[1][i]);
                        }
                        if (currentLoadedCatalyst.ContainsKey(c))
                        {
                            currentLoadedCatalyst.Remove(c);
                        }
                        currentLoadedCatalyst.Add(c, combinedCatalystResult[1][i]);
                    }
                }
            }
            if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Simulation"))
            {
                string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/Simulation");
                simulation_relatedFiles.Items.Clear();
                foreach (string name in genFiles)
                {
                    simulation_relatedFiles.Items.Add(Path.GetFileName(name));
                }
            }

            //TestbenchVehicle
            List<String>[] testbenchResult = db.Select("select * from TestbenchAndVehicle where TestbenchAndVehicle.catalyst_id = " + catalystID);
            if (testbenchResult != null && testbenchResult[0].Count > 0)
            {
                //add to ids
                if (updateIDs.ContainsKey("testbench_id"))
                {
                    updateIDs.Remove("testbench_id");
                }
                updateIDs.Add("testbench_id", testbenchResult[0][0]);
                txb_engine_manufac.Text = testbenchResult[2][0];
                currentLoadedCatalyst.Add(txb_engine_manufac, testbenchResult[2][0]);

                List<String>[] appTestbench = db.Select("select * from TestBenchApp where TestBenchApp.testbench_id =" + testbenchResult[0][0]);
                if (appTestbench != null && appTestbench[0].Count() > 0)
                {
                    for (int i = 0; i < ApplicationFieldsTestbench.Count(); i++)
                    {
                        for (int j = 0; j < appTestbench[0].Count(); j++)
                        {

                            if (ApplicationFieldsTestbench[i].Id == Convert.ToInt32(appTestbench[2][j]))
                            {
                                ApplicationFieldsTestbench[i].IsChecked = true;
                                currentTestbenchApplicationFields[i].IsChecked = true;
                            }
                        }
                    }
                }

                txb_app_field.Text = testbenchResult[3][0];
                currentLoadedCatalyst.Add(txb_app_field, testbenchResult[3][0]);
                txb_emission.Text = testbenchResult[4][0];
                currentLoadedCatalyst.Add(txb_emission, testbenchResult[4][0]);
                txb_engine_displ.Text = testbenchResult[5][0] == "0" ? "" : testbenchResult[5][0];
                currentLoadedCatalyst.Add(txb_engine_displ, testbenchResult[5][0] == "0" ? "" : testbenchResult[5][0]);
                txb_engine_power.Text = testbenchResult[6][0] == "0" ? "" : testbenchResult[6][0];
                currentLoadedCatalyst.Add(txb_engine_power, testbenchResult[6][0] == "0" ? "" : testbenchResult[6][0]);
                txb_number_cylinder.Text = testbenchResult[7][0] == "0" ? "" : testbenchResult[7][0];
                currentLoadedCatalyst.Add(txb_number_cylinder, testbenchResult[7][0] == "0" ? "" : testbenchResult[7][0]);
                txb_eats_setup.Text = testbenchResult[8][0];
                currentLoadedCatalyst.Add(txb_eats_setup, testbenchResult[8][0]);
                txb_comment_testbench_ecu.Text = testbenchResult[9][0];
                currentLoadedCatalyst.Add(txb_comment_testbench_ecu, testbenchResult[9][0]);
                txb_comment_testbench.Text = testbenchResult[11][0];
                currentLoadedCatalyst.Add(txb_comment_testbench, testbenchResult[11][0]);

                List<String>[] steadyResult = db.Select("select * from TestbenchAndSteady where TestbenchAndSteady.testbench_id = " + testbenchResult[0][0]);
                if (steadyResult != null && steadyResult[0].Count > 0)
                {
                    for (int j = 0; j < steadyResult[0].Count / 5; j++)
                    {
                        for (int i = 0; i < SteadyStateLegislations.Count; i++)
                        {
                            if (SteadyStateLegislations[i].Id == Convert.ToInt32(steadyResult[2][j * 5]))
                            {
                                SteadyStateLegislations[i].IsChecked = true;
                                currentSteadys[i].IsChecked = true;
                            }
                        }
                    }
                    for (int i = 0; i < SteadyStateLegislations.Count; i++)
                    {
                        if (SteadyStateLegislations[i].IsChecked == true)
                        {
                            List<String>[] steadyEachResult = db.Select("select * from TestbenchAndSteady where TestbenchAndSteady.testbench_id = " + testbenchResult[0][0] + " and TestbenchAndSteady.steady_state_id = " + SteadyStateLegislations[i].Id);
                            if (steadyEachResult != null && steadyEachResult[0].Count > 0)
                            {
                                for (int j = 0; j < Emissions.Count; j++)
                                {
                                    string raw_name = "txb_raw_" + SteadyStateLegislations[i].Legislation + Emissions[j].Name;
                                    string tailpipe_name = "txb_tailpipe_" + SteadyStateLegislations[i].Legislation + Emissions[j].Name;
                                    string unit_name = "cmb_" + SteadyStateLegislations[i].Legislation + Emissions[j].Name;
                                    CustomTextBox raw = (CustomTextBox)grid_testbench.FindName(raw_name);
                                    CustomTextBox tailpipe = (CustomTextBox)grid_testbench.FindName(tailpipe_name);
                                    ComboBox cmb_unit = (ComboBox)grid_testbench.FindName(unit_name);
                                    raw.Text = steadyEachResult[4][j] == "0" ? "" : steadyEachResult[4][j];
                                    if (currentLoadedCatalyst.ContainsKey(raw))
                                    {
                                        currentLoadedCatalyst.Remove(raw);
                                    }
                                    currentLoadedCatalyst.Add(raw, steadyEachResult[4][j] == "0" ? "" : steadyEachResult[4][j]);
                                    tailpipe.Text = steadyEachResult[5][j] == "0" ? "" : steadyEachResult[5][j];
                                    if (currentLoadedCatalyst.ContainsKey(tailpipe))
                                    {
                                        currentLoadedCatalyst.Remove(tailpipe);
                                    }
                                    currentLoadedCatalyst.Add(tailpipe, steadyEachResult[5][j] == "0" ? "" : steadyEachResult[5][j]);
                                    cmb_unit.SelectedValue = steadyEachResult[6][j];
                                    if (currentLoadedCatalyst.ContainsKey(cmb_unit))
                                    {
                                        currentLoadedCatalyst.Remove(cmb_unit);
                                    }
                                    currentLoadedCatalyst.Add(cmb_unit, steadyEachResult[6][j]);
                                }
                            }
                        }
                    }
                }



                List<String>[] transientResult = db.Select("select * from TestbenchAndTransient where TestbenchAndTransient.testbench_id =  " + testbenchResult[0][0]);
                if (transientResult != null && transientResult[0].Count > 0)
                {
                    for (int j = 0; j < transientResult[0].Count / 5; j++)
                    {
                        for (int i = 0; i < TransientLegislations.Count; i++)
                        {

                            if (TransientLegislations[i].Id == Convert.ToInt32(transientResult[2][j * 5]))
                            {
                                TransientLegislations[i].IsChecked = true;
                                currentTransients[i].IsChecked = true;
                            }
                        }
                    }
                    for (int i = 0; i < TransientLegislations.Count; i++)
                    {
                        if (TransientLegislations[i].IsChecked == true)
                        {
                            List<String>[] transientEachResult = db.Select("select * from TestbenchAndTransient where TestbenchAndTransient.testbench_id =  " + testbenchResult[0][0] + " and TestbenchAndTransient.transient_id =" + TransientLegislations[i].Id);
                            if (transientEachResult != null && transientEachResult[0].Count > 0)
                            {
                                for (int j = 0; j < Emissions.Count; j++)
                                {
                                    string raw_name = "txb_raw_" + TransientLegislations[i].Legislation + Emissions[j].Name;
                                    string tailpipe_name = "txb_tailpipe_" + TransientLegislations[i].Legislation + Emissions[j].Name;
                                    string unit_name = "cmb_" + TransientLegislations[i].Legislation + Emissions[j].Name;
                                    CustomTextBox raw = (CustomTextBox)grid_testbench.FindName(raw_name);
                                    CustomTextBox tailpipe = (CustomTextBox)grid_testbench.FindName(tailpipe_name);
                                    ComboBox cmb_unit = (ComboBox)grid_testbench.FindName(unit_name);
                                    raw.Text = transientEachResult[4][j] == "0" ? "" : transientEachResult[4][j];
                                    if (currentLoadedCatalyst.ContainsKey(raw))
                                    {
                                        currentLoadedCatalyst.Remove(raw);
                                    }
                                    currentLoadedCatalyst.Add(raw, transientEachResult[4][j] == "0" ? "" : transientEachResult[4][j]);
                                    tailpipe.Text = transientEachResult[5][j] == "0" ? "" : transientEachResult[5][j];
                                    if (currentLoadedCatalyst.ContainsKey(tailpipe))
                                    {
                                        currentLoadedCatalyst.Remove(tailpipe);
                                    }
                                    currentLoadedCatalyst.Add(tailpipe, transientEachResult[5][j] == "0" ? "" : transientEachResult[5][j]);
                                    cmb_unit.SelectedValue = transientEachResult[6][j];
                                    if (currentLoadedCatalyst.ContainsKey(cmb_unit))
                                    {
                                        currentLoadedCatalyst.Remove(cmb_unit);
                                    }
                                    currentLoadedCatalyst.Add(cmb_unit, transientEachResult[6][j]);
                                }
                            }
                        }
                    }
                }
            }

            if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Testbench"))
            {
                string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/Testbench");
                testbench_relatedFiles.Items.Clear();
                foreach (string name in genFiles)
                {
                    testbench_relatedFiles.Items.Add(Path.GetFileName(name));
                }
            }

            List<String>[] chemResult = db.Select("select *  from ChemPhysAnalysis where ChemPhysAnalysis.catalyst_id = " + catalystID);
            if (chemResult != null && chemResult[0].Count > 0)
            {
                //add ids
                if (updateIDs.ContainsKey("analysis_id"))
                {
                    updateIDs.Remove("analysis_id");
                }
                updateIDs.Add("analysis_id", chemResult[0][0]);
                List<String>[] washcoatElementalResult = db.Select("select *  from WashcoatElemental where WashcoatElemental.analysis_id  = " + chemResult[0][0]);
                if (washcoatElementalResult != null && washcoatElementalResult[0].Count > 0)
                {
                    for (int i = 0; i < washcoatElementalResult[0].Count - 1; i++)
                    {
                        generateComposition();
                    }
                    int brickRow = 0;
                    for (int i = 0; i < listOfFieldsForChem.Count; i++)
                    {
                        if (listOfFieldsForChem[i].Name.Equals("st_bricksubstrate"))
                        {
                            brickRow = i;
                            break;
                        }
                    }
                    for (int i = 0; i < brickRow; i++)
                    {
                        StackPanel s = (StackPanel)grid_chem_analysis.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == i && Grid.GetColumn(el) == 0);
                        CustomTextBox t = (CustomTextBox)grid_chem_analysis.FindName("txb_comp_" + s.Name.Substring(8));
                        CustomTextBox t1 = (CustomTextBox)grid_chem_analysis.FindName("txb_compval_" + s.Name.Substring(8));
                        t.Text = washcoatElementalResult[2][i];
                        currentLoadedCatalyst.Add(t, washcoatElementalResult[2][i]);
                        t1.Text = washcoatElementalResult[3][i] == "0" ? "" : washcoatElementalResult[3][i];
                        currentLoadedCatalyst.Add(t1, washcoatElementalResult[3][i] == "0" ? "" : washcoatElementalResult[3][i]);
                        if (!currentCompositions.ContainsKey(washcoatElementalResult[2][i]))
                        {
                            currentCompositions.Add(washcoatElementalResult[2][i], washcoatElementalResult[3][i]);
                        }
                    }
                }
                txb_brick_substrate.Text = chemResult[2][0];
                currentLoadedCatalyst.Add(txb_brick_substrate, chemResult[2][0]);
                txb_washcoat_substrate.Text = chemResult[3][0];
                currentLoadedCatalyst.Add(txb_washcoat_substrate, chemResult[3][0]);
                txb_spec_surface.Text = chemResult[4][0] == "0" ? "" : chemResult[4][0];
                currentLoadedCatalyst.Add(txb_spec_surface, chemResult[4][0] == "0" ? "" : chemResult[4][0]);
                txb_total_porosity.Text = chemResult[5][0] == "0" ? "" : chemResult[5][0];
                currentLoadedCatalyst.Add(txb_total_porosity, chemResult[5][0] == "0" ? "" : chemResult[5][0]);
                txb_avg_porosity.Text = chemResult[6][0] == "0" ? "" : chemResult[6][0];
                currentLoadedCatalyst.Add(txb_avg_porosity, chemResult[6][0] == "0" ? "" : chemResult[6][0]);
                txb_cris_components.Text = chemResult[8][0] == "0" ? "" : chemResult[8][0];
                currentLoadedCatalyst.Add(txb_cris_components, chemResult[8][0] == "0" ? "" : chemResult[8][0]);
                txb_ch_dispersity.Text = chemResult[11][0] == "0" ? "" : chemResult[11][0];
                currentLoadedCatalyst.Add(txb_ch_dispersity, chemResult[11][0] == "0" ? "" : chemResult[11][0]);
                txb_ch_heat_capacity.Text = chemResult[12][0] == "0" ? "" : chemResult[12][0];
                currentLoadedCatalyst.Add(txb_ch_heat_capacity, chemResult[12][0] == "0" ? "" : chemResult[12][0]);
                List<String>[] loadingResult = db.Select("select *  from Loading where Loading.analysis_id =  " + chemResult[0][0]);
                if (loadingResult != null && loadingResult[0].Count > 0)
                {
                    for (int i = 0; i < PreciousMetalLoadings.Count; i++)
                    {
                        for (int j = 0; j < loadingResult[0].Count; j++)
                        {
                            if (PreciousMetalLoadings[i].Id == Convert.ToInt32(loadingResult[3][j]))
                            {
                                PreciousMetalLoadings[i].IsChecked = true;
                                currentPreciousMetalLoadings[i].IsChecked = true;

                                CustomTextBox t = (CustomTextBox)grid_chem_analysis.FindName("txb_wash" + PreciousMetalLoadings[i].Name);
                                CustomComboBox c = (CustomComboBox)grid_chem_analysis.FindName("cmb_wash" + PreciousMetalLoadings[i].Name);
                                t.Text = loadingResult[2][j] == "0" ? "" : loadingResult[2][j];
                                if (currentLoadedCatalyst.ContainsKey(t))
                                {
                                    currentLoadedCatalyst.Remove(t);
                                }
                                currentLoadedCatalyst.Add(t, loadingResult[2][j] == "0" ? "" : loadingResult[2][j]);
                                c.SelectedValue = loadingResult[4][j];
                                if (currentLoadedCatalyst.ContainsKey(c))
                                {
                                    currentLoadedCatalyst.Remove(c);
                                }
                                currentLoadedCatalyst.Add(c, loadingResult[4][j]);
                            }
                        }
                    }
                }
                List<String>[] supportResult = db.Select("select *  from SupportMaterial where SupportMaterial.analysis_id = " + chemResult[0][0]);
                if (supportResult != null && supportResult[0].Count > 0)
                {
                    int specRow = 0;
                    int startSupport = 0;
                    for (int i = 0; i < supportResult[0].Count - 1; i++)
                    {
                        generateSupport();
                    }
                    for (int i = 0; i < listOfFieldsForChem.Count; i++)
                    {
                        if (listOfFieldsForChem[i].Name.Equals("st_spec_surface"))
                        {
                            specRow = i;
                        }
                        if (listOfFieldsForChem[i].Name.Equals("st_support_1"))
                        {
                            startSupport = i;
                        }
                    }
                    for (int i = startSupport; i < specRow; i++)
                    {
                        StackPanel s = (StackPanel)grid_chem_analysis.Children.Cast<UIElement>().First(el => Grid.GetRow(el) == i && Grid.GetColumn(el) == 0);
                        CustomTextBox t = (CustomTextBox)grid_chem_analysis.FindName("txb_support_mtr_" + s.Name.Substring(11));
                        CustomTextBox t1 = (CustomTextBox)grid_chem_analysis.FindName("txb_support_val_" + s.Name.Substring(11));
                        CustomComboBox c = (CustomComboBox)grid_chem_analysis.FindName("cmb_sup_" + s.Name.Substring(11));

                        int n = i - startSupport;
                        t.Text = supportResult[3][n];
                        currentLoadedCatalyst.Add(t, supportResult[3][n]);
                        t1.Text = supportResult[4][n] == "0" ? "" : supportResult[4][n];
                        currentLoadedCatalyst.Add(t1, supportResult[4][n] == "0" ? "" : supportResult[4][n]);
                        if (supportResult[1][n] != null && Convert.ToInt32(supportResult[1][n]) != 0)
                        {
                            c.SelectedValue = Convert.ToInt32(supportResult[1][n]);
                            currentLoadedCatalyst.Add(c, supportResult[1][n]);
                        }
                        if (!currentSupportMaterial.ContainsKey(t1.Text))
                        {
                            currentSupportMaterial.Add(t.Text, new KeyValuePair<string, string>(t1.Text, String.IsNullOrWhiteSpace(c.Text) ? "0" : c.SelectedValue.ToString()));
                        }
                    }
                }
            }

            if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/ChemPhys"))
            {
                string[] genFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalystID + "/ChemPhys");
                chem_relatedFiles.Items.Clear();
                foreach (string name in genFiles)
                {
                    chem_relatedFiles.Items.Add(Path.GetFileName(name));
                }
            }
        }

        private void btn_delete_catalyst_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(catalyst_id.Text))
            {
                if (Convert.ToInt32(catalyst_id.Text) > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this catalyst?", "Delete Catalyst ", MessageBoxButton.YesNoCancel);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            SqliteDBConnection db = new SqliteDBConnection();
                            string catalyst_id_delete = catalyst_id.Text;
                            insertHistory(catalystID: catalyst_id.Text, group_name: " ", field_name: " ", usercode: MainWindow.userCode, old_value: " ", action: "Deleted", new_value: " ");

                            List<string>[] listGen = db.Select("select GeneralInformation.general_id from GeneralInformation where GeneralInformation.catalyst_id =" + catalyst_id_delete);
                            string general_id_delete = listGen[0][0];

                            SQLiteCommand genDel = new SQLiteCommand("delete from GeneralInformationManufacturer where GeneralInformationManufacturer.general_id = " + general_id_delete);
                            db.Delete(genDel);

                            SQLiteCommand genDel1 = new SQLiteCommand("delete from GeneralInformationApplicationField where GeneralInformationApplicationField.general_id = " + general_id_delete);
                            db.Delete(genDel1);

                            SQLiteCommand genDel2 = new SQLiteCommand("delete from GeneralWashcoat where GeneralWashcoat.general_id = " + general_id_delete);
                            db.Delete(genDel2);

                            SQLiteCommand genDel3 = new SQLiteCommand("delete from GeneralInformation where GeneralInformation.catalyst_id =  " + catalyst_id_delete);
                            db.Delete(genDel3);

                            List<string>[] listLGB = db.Select("select Lgb.lgb_id from Lgb where Lgb.catalyst_id =" + catalyst_id_delete);
                            string lgb_id_delete = listLGB[0][0];

                            SQLiteCommand lgbDel = new SQLiteCommand("delete from  CatalystCharacterisation where CatalystCharacterisation.lgb_id =  " + lgb_id_delete);
                            db.Delete(lgbDel);

                            SQLiteCommand lgbDel1 = new SQLiteCommand("delete from lgb where lgb.catalyst_id =  " + catalyst_id_delete);
                            db.Delete(lgbDel1);

                            List<string>[] listSimulation = db.Select("select Simulation.simulation_id from Simulation where Simulation.catalyst_id = " + catalyst_id_delete);
                            string simulation_id_delete = listSimulation[0][0];

                            SQLiteCommand simDel = new SQLiteCommand("delete from CombinedCatalyst where CombinedCatalyst.simulation_id =  " + simulation_id_delete);
                            db.Delete(simDel);

                            SQLiteCommand simDel1 = new SQLiteCommand("delete from Simulation where Simulation.catalyst_id =   " + catalyst_id_delete);
                            db.Delete(simDel1);

                            List<string>[] listTestbench = db.Select("select TestbenchAndVehicle.testbench_id from TestbenchAndVehicle where TestbenchAndVehicle.catalyst_id = " + catalyst_id_delete);
                            string testbench_id_delete = listTestbench[0][0];

                            SQLiteCommand testDel = new SQLiteCommand("delete from TestBenchApp where TestBenchApp.testbench_id = " + testbench_id_delete);
                            db.Delete(testDel);

                            SQLiteCommand testDel1 = new SQLiteCommand("delete from TestbenchAndSteady where TestbenchAndSteady.testbench_id =  " + testbench_id_delete);
                            db.Delete(testDel1);

                            SQLiteCommand testDel2 = new SQLiteCommand("delete from TestbenchAndTransient where TestbenchAndTransient.testbench_id =  " + testbench_id_delete);
                            db.Delete(testDel2);

                            SQLiteCommand testDel3 = new SQLiteCommand("delete from TestbenchAndVehicle where TestbenchAndVehicle.catalyst_id  =  " + catalyst_id_delete);
                            db.Delete(testDel3);

                            List<string>[] listChem = db.Select("select ChemPhysAnalysis.analysis_id from ChemPhysAnalysis where ChemPhysAnalysis.catalyst_id =  " + catalyst_id_delete);
                            string analysis_id_delete = listChem[0][0];

                            SQLiteCommand chemDel = new SQLiteCommand("delete from WashcoatElemental where WashcoatElemental.analysis_id = " + analysis_id_delete);
                            db.Delete(chemDel);

                            SQLiteCommand chemDel1 = new SQLiteCommand("delete from Loading where Loading.analysis_id =  " + analysis_id_delete);
                            db.Delete(chemDel1);

                            SQLiteCommand chemDel2 = new SQLiteCommand("delete from TestbenchAndTransient where TestbenchAndTransient.testbench_id =  " + analysis_id_delete);
                            db.Delete(chemDel2);

                            SQLiteCommand chemDel3 = new SQLiteCommand("delete from SupportMaterial where SupportMaterial.analysis_id = " + catalyst_id_delete);
                            db.Delete(chemDel3);

                            MessageBox.Show("Successfully deleted!");
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }

            }
        }

        private void btn_simulation_upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    simulation_relatedFiles.Items.Add(Path.GetFileName(filename));
                    FileToUpload f = new FileToUpload(Path.GetFileName(filename), filename);
                    simuFilesToCopy.Add(f);
                }

            }
        }

        private void btn_general_upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    general_relatedFiles.Items.Add(Path.GetFileName(filename));
                    FileToUpload f = new FileToUpload(Path.GetFileName(filename), filename);
                    genFilesToCopy.Add(f);
                }
            }
        }

        private void btn_testbench_upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    testbench_relatedFiles.Items.Add(Path.GetFileName(filename));
                    FileToUpload f = new FileToUpload(Path.GetFileName(filename), filename);
                    testFilesToCopy.Add(f);
                }

            }

        }

        private void btn_chem_upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    chem_relatedFiles.Items.Add(Path.GetFileName(filename));
                    FileToUpload f = new FileToUpload(Path.GetFileName(filename), filename);
                    chemFilesToCopy.Add(f);
                }

            }
        }


        private void btn_char_upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                ListBox l = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
                foreach (string filename in openFileDialog.FileNames)
                {
                    l.Items.Add(Path.GetFileName(filename));
                    FileToUpload f = new FileToUpload(Path.GetFileName(filename), filename);
                    lgbFilesToCopy.Add(f);
                }

            }

        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {

        }

        /*
 * Called when new composition is added 
 */
        void Composition_Clicked(object sender, RoutedEventArgs e)
        {
            generateComposition();
        }

        void generateComposition()
        {
            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            s.Name = "st_comp_" + startComposition;

            Label l = new Label();
            l.Content = "Washcoat Elemental Composition";
            l.Width = 250;
            s.Children.Add(l);

            CustomTextBox t = new CustomTextBox();
            t.Name = "txb_comp_" + startComposition;
            t.TableName = "washcoatelemental";
            t.FieldName = "washcoat_composition";
            t.GroupTitle = "Chem./Phys. Analysis";
            t.LabelTitle = "Washcoat Elemental Composition";
            t.UpdateId = "";
            t.ContentType = "text";

            //register
            grid_chem_analysis.RegisterName(t.Name, t);
            t.Width = 200;
            t.Height = 25;
            s.Children.Add(t);

            Label l1 = new Label();
            l1.Content = "Value";
            l1.Width = 50;
            s.Children.Add(l1);

            CustomTextBox t1 = new CustomTextBox();
            t1.Name = "txb_compval_" + startComposition;
            t1.TableName = "washcoatelemental";
            t1.FieldName = "washcoat_composition_value";
            t1.GroupTitle = "Chem./Phys. Analysis";
            t1.LabelTitle = "Washcoat Elemental Composition Value";
            t1.UpdateId = "";
            t1.ContentType = "double";

            //register
            grid_chem_analysis.RegisterName(t1.Name, t1);
            t1.KeyUp += update_KeyUp;
            t1.MouseLeave += validationMouseLeave;
            t1.Width = 70;
            t1.Height = 25;
            t1.ToolTip = "Value  of Composition";
            s.Children.Add(t1);

            Label l2 = new Label();
            l2.Content = "%";
            l2.Width = 20;
            s.Children.Add(l2);

            Button b = new Button();
            b.Content = "Remove";
            b.Name = "btn_c" + startComposition;
            b.Click += RemoveComposition_Clicked;
            b.Width = 50;
            b.Margin = new Thickness(10, 3, 0, 0);
            s.Children.Add(b);

            AddRowToChem();
            grid_chem_analysis.Children.Add(s);
            int pos = listOfFieldsForChem.IndexOf(st_comp_1);
            listOfFieldsForChem.Insert(pos + 1, s);
            ChangeRowsOfChem();
            chemCompositions.Add(startComposition);
            startComposition++;

        }

        /*
         * Called when composition is removed
         */
        void RemoveComposition_Clicked(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            char[] charToRemove = { 'b', 't', 'n', '_', 'c' };
            string name = b.Name.Trim(charToRemove);
            CustomTextBox comp = (CustomTextBox)grid_chem_analysis.FindName("txb_compval_" + name);
            CustomTextBox compVal = (CustomTextBox)grid_chem_analysis.FindName("txb_compval_" + name);
            if (comp != null && compVal != null)
            {
                if (currentCompositions.ContainsKey(comp.Text))
                {
                    currentCompositions.Remove(comp.Text);
                }
            }
            chemCompositions.Remove(Convert.ToInt32(name));

            StackPanel el = VisualTreeHelper.GetParent((DependencyObject)sender) as StackPanel;
            grid_chem_analysis.Children.Remove(el);
            RemoveStackFromListByName(listOfFieldsForChem, el.Name);
            ChangeRowsOfChem();

        }


        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (updatedFields.ContainsKey((UIElement)sender))
            {
                updatedFields.Remove((UIElement)sender);
            }
            CustomCheckBox checkBox = (CustomCheckBox)sender;
            int isChecked = checkBox.IsChecked == true ? 1 : 0;
            updatedFields.Add((UIElement)sender, isChecked.ToString());
        }

        private void update_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updatedFields.ContainsKey((UIElement)sender))
            {
                updatedFields.Remove((UIElement)sender);
            }
            CustomComboBox combo = (CustomComboBox)sender;
            if (combo.SelectedValue != null)
            {
                if (combo.ValueQuery.Equals("itself"))
                {
                    updatedFields.Add((UIElement)sender, String.IsNullOrWhiteSpace(combo.Text) ? null : combo.Text.ToString());
                }
                else if (combo.Name.Contains("combined") && updatedFields.ContainsKey(combo))
                {
                    updatedFields.Remove(combo);
                    updatedFields.Add((UIElement)sender, String.IsNullOrWhiteSpace(combo.Text) ? null : combo.SelectedValue.ToString());
                }
                else
                {
                    updatedFields.Add((UIElement)sender, String.IsNullOrWhiteSpace(combo.Text) ? null : combo.SelectedValue.ToString());
                }
            }
        }


        private void update_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updatedFields.ContainsKey((UIElement)sender))
            {
                updatedFields.Remove((UIElement)sender);
            }
            CustomDatePicker datePicker = (CustomDatePicker)sender;
            updatedFields.Add((UIElement)sender, String.IsNullOrWhiteSpace(datePicker.Text) ? null : datePicker.SelectedDate.Value.ToShortDateString());
        }

        private void btn_update_catalyst_Click(object sender, RoutedEventArgs e)
        {

            SqliteDBConnection sqliteDb = new SqliteDBConnection();
            if (String.IsNullOrWhiteSpace(catalyst_id.Text))
            {
                MessageBox.Show("No Catalyst Is Chosen!");
            }
            else
            {
                //for combined catalyst
                List<string> cDel = combinedToDelete(currentCombinedCatalysts, updatedCombinedCatalysts);
                foreach (string s in cDel)
                {
                    string deleteCombined = "delete from CombinedCatalyst where CombinedCatalyst.simulation_id = " + updateIDs["simulation_id"] + " and CombinedCatalyst.catalyst_id = " + s;
                    SQLiteCommand cmdCombined = new SQLiteCommand(deleteCombined);
                    sqliteDb.Delete(cmdCombined);
                    //insert into history which catalysts are removed
                    insertHistory(catalystID: catalyst_id.Text, group_name: "Simulation", field_name: "Combined Catalyst", old_value: s, new_value: " ", usercode: MainWindow.userCode, action: "Updated");
                }

                //insert
                Dictionary<string, CustomComboBox> combinedInsert = combinedToInsert(currentCombinedCatalysts, updatedCombinedCatalysts);
                foreach (KeyValuePair<string, CustomComboBox> entry in combinedInsert)
                {
                    if (!String.IsNullOrWhiteSpace(entry.Value.Text) && !sqliteDb.EntryExist("select * from CombinedCatalyst where CombinedCatalyst.catalyst_id = " + entry.Key + " and CombinedCatalyst.simulation_id = " + updateIDs["simulation_id"] + " and CombinedCatalyst.model_type_id = " + entry.Value.SelectedValue))
                    {
                        SQLiteCommand s = new SQLiteCommand();
                        s.CommandText = "insert into CombinedCatalyst (catalyst_id, model_type_id, simulation_id) values  (@catalyst_id, @model_type_id, @simulation_id)";
                        s.Parameters.AddWithValue("@catalyst_id", entry.Key);
                        s.Parameters.AddWithValue("@model_type_id", String.IsNullOrWhiteSpace(entry.Value.Text) ? null : entry.Value.SelectedValue);
                        s.Parameters.AddWithValue("@simulation_id", updateIDs["simulation_id"]);
                        sqliteDb.Insert(s);
                        //insert into history which catalyst is combined
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Simulation", field_name: "Combined Catalyst", old_value: "", new_value: entry.Key, usercode: MainWindow.userCode, action: "Updated");
                        //since updated change the current values
                        /*          if (currentLoadedCatalyst.ContainsKey(entry.Value)) {
                                      currentLoadedCatalyst.Remove(entry.Value);
                                  }
                                  currentLoadedCatalyst.Add(entry.Value, entry.Value.SelectedValue.ToString());
                         * */
                    }
                }
                if (currentManufacturers != null)
                {
                    List<string> manuToDelete = manufacturersToDelete(currentManufacturers, Manufacturers);
                    foreach (string s in manuToDelete)
                    {
                        //delete application fields
                        string deleteManu = "delete from GeneralInformationManufacturer where GeneralInformationManufacturer.general_id =" + updateIDs["general_id"] + " and GeneralInformationManufacturer.manufacturer_id = " + s;
                        SQLiteCommand cmdManu = new SQLiteCommand(deleteManu);
                        sqliteDb.Delete(cmdManu);
                        //insert history
                        List<String>[] oldVal = sqliteDb.Select("select * from CatalystManufacturer where manufacturer_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "General Information", field_name: "Manufacturer", old_value: oldVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");
                    }

                    List<string> manuToInsert = manufacturersToInsert(currentManufacturers, Manufacturers);
                    foreach (string s in manuToInsert)
                    {
                        if (!sqliteDb.EntryExist("select * from GeneralInformationManufacturer where manufacturer_id = " + s + " and general_id = " + updateIDs["general_id"]))
                        { //Check if the entry already existed
                            SQLiteCommand cmd_manufacturers = new SQLiteCommand();
                            cmd_manufacturers.CommandText = "insert into GeneralInformationManufacturer (manufacturer_id, general_id) values (@manufacturer_id, @general_id)";
                            cmd_manufacturers.Parameters.AddWithValue("@manufacturer_id", Convert.ToInt32(s));
                            cmd_manufacturers.Parameters.AddWithValue("@general_id", updateIDs["general_id"]);
                            int n = sqliteDb.Insert(cmd_manufacturers);
                            //insert history
                            List<String>[] newVal = sqliteDb.Select("select * from CatalystManufacturer where manufacturer_id = " + s);
                            insertHistory(catalystID: catalyst_id.Text, group_name: "General Information", field_name: "Manufacturer", old_value: " ", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");

                        }
                    }
                }

                if (currentAppplicationFields != null)
                {
                    List<string> appToDelete = fieldsToDelete(currentAppplicationFields, ApplicationFields);
                    foreach (string s in appToDelete)
                    {
                        //delete manufacturers
                        string deleteApplication = "delete from GeneralInformationApplicationField where GeneralInformationApplicationField.general_id = " + updateIDs["general_id"];
                        SQLiteCommand cmdApplication = new SQLiteCommand(deleteApplication);
                        sqliteDb.Delete(cmdApplication);
                        //insert history
                        List<String>[] oldVal = sqliteDb.Select("select * from ApplicationField where app_field_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "General Information", field_name: "Field Of Application", old_value: oldVal[1][0], new_value: " ", usercode: MainWindow.userCode, action: "Updated");
                        currentAppplicationFields.Single(x => x.Id == Convert.ToInt32(s));
                    }

                    List<string> appToInsert = fieldsToInsert(currentAppplicationFields, ApplicationFields);
                    foreach (string s in appToInsert)
                    {
                        if (!sqliteDb.EntryExist("select * from GeneralInformationApplicationField where app_field_id = " + s + " and general_id = " + updateIDs["general_id"]))
                        {
                            SQLiteCommand cmd_appfields = new SQLiteCommand();
                            cmd_appfields.CommandText = "insert into GeneralInformationApplicationField (app_field_id, general_id) values (@app_field_id, @general_id)";
                            cmd_appfields.Parameters.AddWithValue("@app_field_id", Convert.ToInt32(s));
                            cmd_appfields.Parameters.AddWithValue("@general_id", updateIDs["general_id"]);
                            sqliteDb.Insert(cmd_appfields);
                            //insert history
                            List<String>[] newVal = sqliteDb.Select("select * from ApplicationField where app_field_id = " + s);
                            insertHistory(catalystID: catalyst_id.Text, group_name: "General Information", field_name: "Field Of Application", old_value: " ", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");
                        }

                    }
                }
                if (currentWashcoats != null)
                {
                    List<string> washToDelete = washcoatsToDelete(currentWashcoats, Washcoats);
                    foreach (string s in washToDelete)
                    {
                        //delete washcoat composition
                        string deleteWashcoat = "delete from GeneralWashcoat where GeneralWashcoat.general_id  =   " + updateIDs["general_id"] + " and GeneralWashcoat.material_id = " + s;
                        MessageBox.Show(deleteWashcoat);
                        SQLiteCommand cmdWashcoat = new SQLiteCommand(deleteWashcoat);
                        sqliteDb.Delete(cmdWashcoat);
                        //insert history
                        List<String>[] oldVal = sqliteDb.Select("select * from WashcoatMaterial where material_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "General Information", field_name: "Washcoat Catalytic Composition", old_value: oldVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");
                    }

                    List<WashcoatCatalyticComposition> washToInsert = washcoatsToInsert(currentWashcoats, Washcoats);
                    foreach (WashcoatCatalyticComposition s in washToInsert)
                    {
                        if (!sqliteDb.EntryExist("select * from GeneralWashcoat where GeneralWashcoat.general_id  =  " + updateIDs["general_id"] + " and GeneralWashcoat.material_id = " + s.Id))
                        {
                            SQLiteCommand cmd_washcoat = new SQLiteCommand();
                            cmd_washcoat.CommandText = "insert into GeneralWashcoat (general_id, material_id, precious_metal_loading_unit_id, precious_metal_ratio, precious_metal_loading) values ( @general_id, @material_id,@precious_metal_loading_unit_id, @precious_metal_ratio, @precious_metal_loading)";
                            cmd_washcoat.Parameters.AddWithValue("@general_id", updateIDs["general_id"]);
                            cmd_washcoat.Parameters.AddWithValue("@material_id", s.Id);

                            if (s.NeedPreciousMetal == true)
                            {
                                string metal_loading = "txb_" + s.WashcoatValue;
                                CustomTextBox txb_m_loading = (CustomTextBox)grid_generalInfo.FindName(metal_loading);
                                string metal_ratio = "txb_" + s.WashcoatValue + "_ratio";
                                CustomTextBox txb_m_ratio = (CustomTextBox)grid_generalInfo.FindName(metal_ratio);
                                string unit_name = "cmb_" + s.WashcoatValue;
                                CustomComboBox cmb_unit = (CustomComboBox)grid_generalInfo.FindName(unit_name);
                                cmd_washcoat.Parameters.AddWithValue("@precious_metal_ratio", txb_m_ratio.Text);
                                cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading", txb_m_loading.Text);
                                cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading_unit_id", cmb_unit.Text == "" ? 0 : Convert.ToInt32(cmb_unit.SelectedValue));
                            }
                            else
                            {
                                cmd_washcoat.Parameters.AddWithValue("@precious_metal_ratio", "");
                                cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading", "");
                                cmd_washcoat.Parameters.AddWithValue("@precious_metal_loading_unit_id", 0);
                            }
                            sqliteDb.Insert(cmd_washcoat);
                            //insert history
                            List<String>[] newVal = sqliteDb.Select("select * from WashcoatMaterial where material_id = " + s.Id);
                            insertHistory(catalystID: catalyst_id.Text, group_name: "General Information", field_name: "Washcoat Catalytic Composition", old_value: " ", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");
                        }
                    }
                }

                if (currentTestbenchApplicationFields != null)
                {
                    List<string> appToDelete = fieldsToDelete(currentTestbenchApplicationFields, ApplicationFieldsTestbench);
                    foreach (string s in appToDelete)
                    {
                        //delete testbenchapp
                        string deleteAppTestbench = "delete from TestBenchApp where TestBenchApp.testbench_id =  " + updateIDs["testbench_id"] + " and TestBenchApp.app_field_id = " + s;
                        SQLiteCommand cmdAppTestbench = new SQLiteCommand(deleteAppTestbench);
                        sqliteDb.Delete(cmdAppTestbench);
                        List<String>[] oldVal = sqliteDb.Select("select * from ApplicationField where app_field_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Testbench And Engine/Vehicle", field_name: "Application", old_value: oldVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");
                    }

                    List<string> appToInsert = fieldsToInsert(currentTestbenchApplicationFields, ApplicationFieldsTestbench);
                    foreach (string s in appToInsert)
                    {
                        //insert 
                        if (!sqliteDb.EntryExist("select * from TestBenchApp where TestBenchApp.testbench_id =  " + updateIDs["testbench_id"] + " and TestBenchApp.app_field_id = " + s))
                        {
                            SQLiteCommand cmd_appfields = new SQLiteCommand();
                            cmd_appfields.CommandText = "insert into TestBenchApp (app_field_id,testbench_id) values (@app_field_id, @testbench_id)";
                            cmd_appfields.Parameters.AddWithValue("@app_field_id", Convert.ToInt32(s));
                            cmd_appfields.Parameters.AddWithValue("@testbench_id", updateIDs["testbench_id"]);
                            sqliteDb.Insert(cmd_appfields);
                            List<String>[] newVal = sqliteDb.Select("select * from ApplicationField where app_field_id = " + s);
                            insertHistory(catalystID: catalyst_id.Text, group_name: "Testbench And Engine/Vehicle", field_name: "Application", old_value: "", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");

                        }
                    }
                }

                if (currentSteadys != null)
                {
                    List<string> steadyDelete = steadysToDelete(currentSteadys, SteadyStateLegislations);
                    foreach (string s in steadyDelete)
                    {
                        string deleteSteady = "delete from TestbenchAndSteady where TestbenchAndSteady.testbench_id =   " + updateIDs["testbench_id"] + " and steady_state_id = " + s;
                        SQLiteCommand cmdSteady = new SQLiteCommand(deleteSteady);
                        sqliteDb.Delete(cmdSteady);

                        List<String>[] oldVal = sqliteDb.Select("select * from SteadyStateLegislationCycle where steady_state_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Testbench And Engine/Vehicle", field_name: "Steady State Legislation Cycle", old_value: oldVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");

                    }

                    List<SteadyStateLegislation> steadyInsert = steadysToInsert(currentSteadys, SteadyStateLegislations);
                    foreach (SteadyStateLegislation st in steadyInsert)
                    {
                        for (int j = 0; j < Emissions.Count(); j++)
                        {
                            //check if the entry exist
                            if (!sqliteDb.EntryExist("select * from TestbenchAndSteady where TestbenchAndSteady.emission_id = " + Emissions[j].Id + " and TestbenchAndSteady.steady_state_id = " + st.Id + " and TestbenchAndSteady.testbench_id = " + updateIDs["testbench_id"]))
                            {
                                string raw_name = "txb_raw_" + st.Legislation + Emissions[j].Name;
                                string tailpipe_name = "txb_tailpipe_" + st.Legislation + Emissions[j].Name;
                                string unit_name = "cmb_" + st.Legislation + Emissions[j].Name;
                                CustomTextBox raw = (CustomTextBox)grid_testbench.FindName(raw_name);
                                CustomTextBox tailpipe = (CustomTextBox)grid_testbench.FindName(tailpipe_name);
                                ComboBox cmb_unit = (ComboBox)grid_testbench.FindName(unit_name);
                                SQLiteCommand cmd_emission = new SQLiteCommand();
                                cmd_emission.CommandText = "insert into TestbenchAndSteady (emission_id, steady_state_id, testbench_id, raw, tailpipe, steady_unit) values (@emission_id, @steady_state_id, @testbench_id, @raw, @tailpipe, @steady_unit)";
                                cmd_emission.Parameters.AddWithValue("@emission_id", Emissions[j].Id);
                                cmd_emission.Parameters.AddWithValue("@steady_state_id", st.Id);
                                cmd_emission.Parameters.AddWithValue("@testbench_id", updateIDs["testbench_id"]);
                                cmd_emission.Parameters.AddWithValue("@raw", raw.Text);
                                cmd_emission.Parameters.AddWithValue("@tailpipe", tailpipe.Text);
                                cmd_emission.Parameters.AddWithValue("@steady_unit", cmb_unit.Text);
                                sqliteDb.Insert(cmd_emission);
                            }
                        }
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Testbench And Engine/Vehicle", field_name: "Steady State Legislation Cycle", old_value: "", new_value: st.Legislation, usercode: MainWindow.userCode, action: "Updated");

                    }
                }

                if (currentTransients != null)
                {
                    List<string> transientDelete = transientToDelete(currentTransients, TransientLegislations);
                    foreach (string s in transientDelete)
                    {
                        string deleteTransient = "delete from TestbenchAndTransient where TestbenchAndTransient.testbench_id =   " + updateIDs["testbench_id"] + " and transient_id = " + s;
                        SQLiteCommand cmdTransient = new SQLiteCommand(deleteTransient);
                        sqliteDb.Delete(cmdTransient);

                        List<String>[] oldVal = sqliteDb.Select("select * from TransientLegislationCycle where transient_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Testbench And Engine/Vehicle", field_name: "Transient State Legislation Cycle", old_value: oldVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");

                    }

                    List<TransientLegislation> transientInsert = transientToInsert(currentTransients, TransientLegislations);
                    foreach (TransientLegislation st in transientInsert)
                    {
                        for (int j = 0; j < Emissions.Count(); j++)
                        {
                            if (!sqliteDb.EntryExist("select * from TestbenchAndTransient where TestbenchAndTransient.emission_id = " + Emissions[j].Id + " and  TestbenchAndTransient.testbench_id = " + updateIDs["testbench_id"] + " and TestbenchAndTransient.transient_id = " + st.Id))
                            {
                                string raw_name = "txb_raw_" + st.Legislation + Emissions[j].Name;
                                string tailpipe_name = "txb_tailpipe_" + st.Legislation + Emissions[j].Name;
                                string unit_name = "cmb_" + st.Legislation + Emissions[j].Name;
                                CustomTextBox raw = (CustomTextBox)grid_testbench.FindName(raw_name);
                                CustomTextBox tailpipe = (CustomTextBox)grid_testbench.FindName(tailpipe_name);
                                ComboBox cmb_unit = (ComboBox)grid_testbench.FindName(unit_name);
                                SQLiteCommand cmd_emission = new SQLiteCommand();
                                cmd_emission.CommandText = "insert into testbenchandtransient (emission_id, transient_id, testbench_id, raw, tailpipe, transient_unit) values (@emission_id, @transient_id, @testbench_id, @raw, @tailpipe, @transient_unit)";
                                cmd_emission.Parameters.AddWithValue("@emission_id", Emissions[j].Id);
                                cmd_emission.Parameters.AddWithValue("@transient_id", st.Id);
                                cmd_emission.Parameters.AddWithValue("@testbench_id", updateIDs["testbench_id"]);
                                cmd_emission.Parameters.AddWithValue("@raw", raw.Text);
                                cmd_emission.Parameters.AddWithValue("@tailpipe", tailpipe.Text);
                                cmd_emission.Parameters.AddWithValue("@transient_unit", cmb_unit.Text);
                                sqliteDb.Insert(cmd_emission);
                            }
                        }
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Testbench And Engine/Vehicle", field_name: "Transient State Legislation Cycle", old_value: "", new_value: st.Legislation, usercode: MainWindow.userCode, action: "Updated");

                    }
                }

                //delete from composition
                Dictionary<string, string> updatedComposition = new Dictionary<string, string>();
                for (int i = 0; i < chemCompositions.Count(); i++)
                {
                    string comp = "txb_comp_" + chemCompositions[i];
                    string comp_val = "txb_compval_" + chemCompositions[i];
                    CustomTextBox txb_c = (CustomTextBox)grid_chem_analysis.FindName(comp);
                    CustomTextBox txb_v = (CustomTextBox)grid_chem_analysis.FindName(comp_val);
                    if (!updatedComposition.ContainsKey(txb_c.Text))
                    {
                        if (!String.IsNullOrWhiteSpace(txb_c.Text) && !String.IsNullOrWhiteSpace(txb_v.Text))
                        {
                            updatedComposition.Add(txb_c.Text, txb_v.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Washcoat Composition Already Exist! This Entry Will Not Be Added! ");
                    }
                }
                Dictionary<string, string> compToDelete = comToDelete(currentCompositions, updatedComposition);
                foreach (KeyValuePair<string, string> s in compToDelete)
                {
                    if (sqliteDb.EntryExist("select * from WashcoatElemental where analysis_id = " + updateIDs["analysis_id"] + " and washcoat_composition = '" + s.Key + "'"))
                    {
                        string deleteComp = "delete from WashcoatElemental where analysis_id = " + updateIDs["analysis_id"] + " and washcoat_composition = '" + s.Key + "'";
                        SQLiteCommand delComp = new SQLiteCommand(deleteComp);
                        sqliteDb.Delete(delComp);

                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Washcoat Elemental Composition", old_value: s.Key, new_value: "", usercode: MainWindow.userCode, action: "Updated");
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Washcoat Elemental Composition Value", old_value: s.Value, new_value: "", usercode: MainWindow.userCode, action: "Updated");
                    }
                }
                Dictionary<string, string> compToInsert = comToInsert(currentCompositions, updatedComposition);

                foreach (KeyValuePair<string, string> entry in compToInsert)
                {
                    if (!sqliteDb.EntryExist("select * from WashcoatElemental where analysis_id= " + updateIDs["analysis_id"] + " and washcoat_composition = '" + entry.Key + "'"))
                    {
                        SQLiteCommand cmd_comp = new SQLiteCommand();
                        cmd_comp.CommandText = "insert into WashcoatElemental (analysis_id, washcoat_composition, washcoat_composition_value) values (@analysis_id, @washcoat_composition, @washcoat_composition_value)";
                        cmd_comp.Parameters.AddWithValue("@analysis_id", updateIDs["analysis_id"]);
                        cmd_comp.Parameters.AddWithValue("@washcoat_composition", entry.Key);
                        cmd_comp.Parameters.AddWithValue("@washcoat_composition_value", entry.Value);
                        sqliteDb.Insert(cmd_comp);

                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Washcoat Elemental Composition", old_value: "", new_value: entry.Key, usercode: MainWindow.userCode, action: "Updated");
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Washcoat Elemental Composition Value", old_value: "", new_value: entry.Value, usercode: MainWindow.userCode, action: "Updated");
                    }
                }


                if (currentPreciousMetalLoadings != null)
                {
                    //for precious to delete
                    List<string> preciousDel = preciousDelete(currentPreciousMetalLoadings, PreciousMetalLoadings);
                    foreach (string s in preciousDel)
                    {
                        string deleteCombined = "delete from Loading where Loading.analysis_id =  " + updateIDs["analysis_id"] + " and Loading.precious_metal_loading_id =" + s;
                        SQLiteCommand cmdCombined = new SQLiteCommand(deleteCombined);
                        sqliteDb.Delete(cmdCombined);

                        List<String>[] oldVal = sqliteDb.Select("select * from PreciousMetalLoading where precious_metal_loading_id = " + s);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: oldVal[1][0] + " Loading Value", old_value: oldVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");
                    }

                    //insert precious
                    List<PreciousMetalLoading> preciousInsert = preciousToInsert(currentPreciousMetalLoadings, PreciousMetalLoadings);
                    foreach (PreciousMetalLoading p in preciousInsert)
                    {
                        string txb = "txb_wash" + p.Name;
                        string cmb = "cmb_wash" + p.Name;
                        CustomTextBox prec_val = (CustomTextBox)grid_chem_analysis.FindName(txb);
                        CustomComboBox prec_unit = (CustomComboBox)grid_chem_analysis.FindName(cmb);
                        SQLiteCommand cmd_precious = new SQLiteCommand();
                        cmd_precious.CommandText = "insert into loading (analysis_id, precious_metal_loading_id, loading_value, precious_metal_loading_unit_id) values (@analysis_id, @precious_metal_loading_id, @loading_value, @precious_metal_loading_unit_id)";
                        cmd_precious.Parameters.AddWithValue("@analysis_id", updateIDs["analysis_id"]);
                        cmd_precious.Parameters.AddWithValue("@precious_metal_loading_id", p.Id);
                        cmd_precious.Parameters.AddWithValue("@loading_value", prec_val.Text == "" ? 0 : Convert.ToDouble(prec_val.Text));
                        cmd_precious.Parameters.AddWithValue("@precious_metal_loading_unit_id", prec_unit.Text.Equals("") ? 0 : Convert.ToInt32(prec_unit.SelectedValue));
                        sqliteDb.Insert(cmd_precious);

                        List<String>[] newVal = sqliteDb.Select("select * from PreciousMetalLoading where precious_metal_loading_id = " + p.Id);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: newVal[1][0] + " Loading Value", old_value: " ", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");

                    }
                }
                updatedSupportMaterial = new Dictionary<string, KeyValuePair<string, string>>();
                for (int i = 0; i < chemSupportMaterials.Count(); i++)
                {
                    string s_mtr = "txb_support_mtr_" + chemSupportMaterials[i];
                    string s_val = "txb_support_val_" + chemSupportMaterials[i];
                    string s_sup = "cmb_sup_" + chemSupportMaterials[i];
                    CustomTextBox t_sp = (CustomTextBox)grid_chem_analysis.FindName(s_mtr);
                    CustomTextBox t_vl = (CustomTextBox)grid_chem_analysis.FindName(s_val);
                    CustomComboBox c_sup = (CustomComboBox)grid_chem_analysis.FindName(s_sup);

                    if (updatedSupportMaterial.ContainsKey(t_sp.Text))
                    {
                        MessageBox.Show("Support Material Already Exist! Entry Will Not Be Added!");
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(t_sp.Text) && !String.IsNullOrWhiteSpace(t_vl.Text) && !String.IsNullOrEmpty(c_sup.Text))
                        {
                            updatedSupportMaterial.Add(t_sp.Text, new KeyValuePair<string, string>(t_vl.Text, c_sup.Text == "" ? "0" : c_sup.SelectedValue.ToString()));
                        }
                    }
                }


                Dictionary<string, KeyValuePair<string, string>> supToDel = supportToDelete(currentSupportMaterial, updatedSupportMaterial);
                foreach (KeyValuePair<string, KeyValuePair<string, string>> s in supToDel)
                {
                    if (sqliteDb.EntryExist("select * from SupportMaterial where analysis_id = " + updateIDs["analysis_id"] + "  and support_material_loading = '" + s.Key + "'"))
                    {
                        MessageBox.Show("To Delete - " + s.Key + " : " + s.Value.Key + " , " + s.Value.Value);
                        string deleteSupport = "delete from SupportMaterial where analysis_id =  " + updateIDs["analysis_id"] + "  and support_material_loading = '" + s.Key + "'";
                        SQLiteCommand cmdSupport = new SQLiteCommand(deleteSupport);
                        sqliteDb.Delete(cmdSupport);

                        List<String>[] newVal = sqliteDb.Select("select * from UnitSupport where support_unit_id = " + s.Value.Value);


                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Support Material Loading", old_value: s.Key, new_value: "", usercode: MainWindow.userCode, action: "Updated");
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Support Material Loading Value", old_value: s.Value.Key, new_value: "", usercode: MainWindow.userCode, action: "Updated");
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Support Material Loading Unit", old_value: newVal[1][0], new_value: "", usercode: MainWindow.userCode, action: "Updated");

                    }

                }

                Dictionary<string, KeyValuePair<string, string>> supToInsert = supportToInsert(currentSupportMaterial, updatedSupportMaterial);
                foreach (KeyValuePair<string, KeyValuePair<string, string>> entry in supToInsert)
                {
                    if (!sqliteDb.EntryExist("select * from SupportMaterial where support_material_loading = '" + entry.Key + "'"))
                    {
                        MessageBox.Show("To Insert - " + entry.Key + " : " + entry.Value.Key + " , " + entry.Value.Value);
                        SQLiteCommand cmd_support = new SQLiteCommand();
                        cmd_support.CommandText = "insert into supportmaterial (analysis_id, support_material_loading, support_material_loading_value, support_unit_id) values (@analysis_id, @support_material_loading, @support_material_loading_value, @support_unit_id)";
                        cmd_support.Parameters.AddWithValue("@analysis_id", updateIDs["analysis_id"]);
                        cmd_support.Parameters.AddWithValue("@support_material_loading", entry.Key);
                        cmd_support.Parameters.AddWithValue("@support_material_loading_value", entry.Value.Key);
                        cmd_support.Parameters.AddWithValue("@support_unit_id", entry.Value.Value);
                        sqliteDb.Insert(cmd_support);


                        List<String>[] newVal = sqliteDb.Select("select * from UnitSupport where support_unit_id = " + entry.Value.Value);
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Support Material Loading", old_value: "", new_value: entry.Key, usercode: MainWindow.userCode, action: "Updated");
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Support Material Loading Value", old_value: "", new_value: entry.Value.Key, usercode: MainWindow.userCode, action: "Updated");
                        insertHistory(catalystID: catalyst_id.Text, group_name: "Chem./Phys. Analysis", field_name: "Support Material Loading Unit", old_value: "", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");

                    }

                }

                for (int i = 0; i < updatedFields.Count; i++)
                {
                    string updateDateModified = "update Catalyst set date_modified =  '" + DateTime.Now.ToShortDateString() + "' where catalyst_id = " + catalyst_id.Text;
                    SQLiteCommand updateDate = new SQLiteCommand(updateDateModified);
                    sqliteDb.Update(updateDate);
                    //When the uielement is checkbox
                    if (updatedFields.ElementAt(i).Key is CustomCheckBox)
                    {
                        CustomCheckBox checkBox = (CustomCheckBox)updatedFields.ElementAt(i).Key;
                        if (currentLoadedCatalyst[checkBox] != null && Convert.ToBoolean(currentLoadedCatalyst[checkBox]) != checkBox.IsChecked)
                        {
                            if (!String.IsNullOrWhiteSpace(checkBox.UpdateId) && updateIDs.ContainsKey(checkBox.UpdateId))
                            {
                                //update only if the field already exist in the database
                                string query = "select * from " + checkBox.TableName + " where " + checkBox.UpdateId + " = " + updateIDs[checkBox.UpdateId];
                                List<String>[] itExist = sqliteDb.Select(query);
                                if (itExist != null && itExist[0].Count > 0)
                                {
                                    string checkQuery = "select * from " + checkBox.TableName + " where  " + checkBox.FieldName + " = " + updatedFields[updatedFields.ElementAt(i).Key] + " and " + checkBox.UpdateId + " = " + updateIDs[checkBox.UpdateId];
                                    List<String>[] checkExist = sqliteDb.Select(checkQuery);
                                    //update only when the such entry doesn't exist
                                    if (checkExist == null || checkExist[0].Count == 0)
                                    {
                                        string updateQuery = "update " + checkBox.TableName + " set " + checkBox.FieldName + " = " + updatedFields[updatedFields.ElementAt(i).Key] + " where " + checkBox.UpdateId + " = " + updateIDs[checkBox.UpdateId];
                                        SQLiteCommand updateCmd = new SQLiteCommand(updateQuery);
                                        sqliteDb.Update(updateCmd);
                                        //insert history
                                        insertHistory(catalystID: catalyst_id.Text, group_name: checkBox.GroupTitle, field_name: checkBox.LabelTitle, old_value: currentLoadedCatalyst[checkBox], new_value: checkBox.IsChecked.ToString(), usercode: MainWindow.userCode, action: "Updated");
                                        //update the current loaded caralyst with updated value
                                        currentLoadedCatalyst[checkBox] = checkBox.IsEnabled.ToString();
                                    }
                                }
                            }
                        }

                    }
                    if (updatedFields.ElementAt(i).Key is CustomTextBox)
                    {
                        CustomTextBox textBox = (CustomTextBox)updatedFields.ElementAt(i).Key;
                        if (currentLoadedCatalyst.ContainsKey(textBox))
                        {
                            if (!currentLoadedCatalyst[textBox].Equals(textBox.Text) && textBox.UpdateId != null && textBox.UpdateId != "" && updateIDs.ContainsKey(textBox.UpdateId))
                            {
                                string query = "select * from " + textBox.TableName + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                MessageBox.Show(query);
                                List<String>[] itExist = sqliteDb.Select(query);
                                if (itExist != null && itExist[0].Count > 0 && textBox.ContentType == "text")
                                {
                                    string updateQuery = "";
                                    if (!String.IsNullOrWhiteSpace(textBox.UpdateHelper))
                                    {
                                        updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  '" + updatedFields[updatedFields.ElementAt(i).Key] + "' where " + textBox.UpdateHelper + " and " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                    }
                                    else
                                    {
                                        updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  '" + updatedFields[updatedFields.ElementAt(i).Key] + "' where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                    }

                                    MessageBox.Show(updateQuery);
                                    SQLiteCommand updateCmd = new SQLiteCommand(updateQuery);
                                    sqliteDb.Update(updateCmd);
                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: textBox.Text, usercode: MainWindow.userCode, action: "Updated");
                                    //update the current loaded caralyst with updated value
                                    currentLoadedCatalyst[textBox] = textBox.Text;
                                }
                                else if ((itExist != null && itExist[0].Count > 0 && textBox.ContentType != "text"))
                                {
                                    string updateQuery = "";
                                    if (!String.IsNullOrWhiteSpace(textBox.DefaultUnit) && !String.IsNullOrEmpty(textBox.DefaultUnit))
                                    {
                                        if (textBox.DefaultUnit.Equals("convertToGL"))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToGL(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                            //insert history for each converted values
                                            insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToGL(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                            //set current value to the updated one
                                            currentLoadedCatalyst[textBox] = converter.convertToGL(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString();
                                        }
                                        else if (textBox.DefaultUnit.Equals("convertToMil"))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToMil(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                            //insert history for each converted values
                                            insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToMil(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                            //set current value to the updated one
                                            currentLoadedCatalyst[textBox] = converter.convertToMil(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString();

                                        }
                                        else if (textBox.DefaultUnit.Equals("convertToLiter"))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                            //insert history for each converted values
                                            insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                            //set current value to the updated one
                                            currentLoadedCatalyst[textBox] = converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString();

                                        }
                                        else if (textBox.DefaultUnit.Equals("convertToMMDouble"))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                            //insert history for each converted values
                                            insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                            //set current value to the updated one
                                            currentLoadedCatalyst[textBox] = converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString();

                                        }
                                        else if (textBox.DefaultUnit.Equals("convertToCelsius"))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                            //insert history for each converted values
                                            insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                            //set current value to the updated one
                                            currentLoadedCatalyst[textBox] = converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString();

                                        }
                                        else if (textBox.DefaultUnit.Equals("convertToInch"))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToInch(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                            //insert history for each converted values
                                            insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToInch(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                            //set current value to the updated one
                                            currentLoadedCatalyst[textBox] = converter.convertToInch(textBoxAndUnits[textBox.Name].Text, updatedFields[updatedFields.ElementAt(i).Key]).ToString();
                                        }
                                    }

                                    else
                                    {
                                        if (!String.IsNullOrWhiteSpace(textBox.UpdateHelper))
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + updatedFields[updatedFields.ElementAt(i).Key] + " where " + textBox.UpdateHelper + " and " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                        }
                                        else
                                        {
                                            updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + updatedFields[updatedFields.ElementAt(i).Key] + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                        }
                                        //insert history for each non convertable values
                                        insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: textBox.Text, usercode: MainWindow.userCode, action: "Updated");
                                        //set current value to the updated one
                                        currentLoadedCatalyst[textBox] = textBox.Text;
                                    }

                                    SQLiteCommand updateCmd = new SQLiteCommand(updateQuery);
                                    sqliteDb.Update(updateCmd);
                                    //remove from updateFields
                                    //             editedUpdates.Remove(textBox);
                                }
                            }
                        }
                    }
                    if (updatedFields.ElementAt(i).Key is CustomComboBox)
                    {
                        CustomComboBox comboBox = (CustomComboBox)updatedFields.ElementAt(i).Key;
                        if (currentLoadedCatalyst.ContainsKey(comboBox))
                        {
                            MessageBox.Show(comboBox.Name);
                            if (!currentLoadedCatalyst[comboBox].Equals(comboBox.SelectedValue.ToString()) && comboBox.UpdateId != null && comboBox.UpdateId != "" && updateIDs.ContainsKey(comboBox.UpdateId))
                            {
                                string query = "select * from " + comboBox.TableName + " where " + comboBox.UpdateId + " = " + updateIDs[comboBox.UpdateId];
                                List<String>[] itExist = sqliteDb.Select(query);
                                if (itExist != null && itExist[0].Count > 0 && comboBox.SelectedValue != null)
                                {
                                    string updateQuery = "";

                                    if (!String.IsNullOrWhiteSpace(comboBox.UpdateHelper))
                                    {
                                        updateQuery = "update " + comboBox.TableName + " set " + comboBox.FieldName + " = '" + comboBox.SelectedValue + "' where " + comboBox.UpdateHelper + " and " + comboBox.UpdateId + " = " + updateIDs[comboBox.UpdateId];
                                    }
                                    else
                                    {
                                        updateQuery = "update " + comboBox.TableName + " set " + comboBox.FieldName + " = " + comboBox.SelectedValue + " where " + comboBox.UpdateId + " = " + updateIDs[comboBox.UpdateId];

                                    }
                                    MessageBox.Show("update query :" + updateQuery);
                                    SQLiteCommand updateCmd = new SQLiteCommand(updateQuery);
                                    sqliteDb.Update(updateCmd);
                                    if (!String.IsNullOrEmpty(comboBox.ValueQuery) && !String.IsNullOrWhiteSpace(comboBox.ValueQuery))
                                    {
                                        if (comboBox.ValueQuery.Equals("itself"))
                                        {
                                            insertHistory(catalystID: catalyst_id.Text, group_name: comboBox.GroupTitle, field_name: comboBox.LabelTitle, old_value: currentLoadedCatalyst[comboBox], new_value: comboBox.Text, usercode: MainWindow.userCode, action: "Updated");
                                            currentLoadedCatalyst[comboBox] = comboBox.Text;
                                        }
                                        else
                                        {
                                            List<string>[] newVal = sqliteDb.Select(comboBox.ValueQuery + comboBox.SelectedValue);
                                            if (String.IsNullOrEmpty(currentLoadedCatalyst[comboBox]) || String.IsNullOrWhiteSpace(currentLoadedCatalyst[comboBox]) || currentLoadedCatalyst[comboBox].Equals("0"))
                                            {
                                                insertHistory(catalystID: catalyst_id.Text, group_name: comboBox.GroupTitle, field_name: comboBox.LabelTitle, old_value: " ", new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");
                                            }
                                            else
                                            {
                                                MessageBox.Show(comboBox.ValueQuery + currentLoadedCatalyst[comboBox]);
                                                List<string>[] oldVal = sqliteDb.Select(comboBox.ValueQuery + currentLoadedCatalyst[comboBox]);
                                                //       MessageBox.Show(oldVal[1][0] + " , " + newVal[1][0]);
                                                insertHistory(catalystID: catalyst_id.Text, group_name: comboBox.GroupTitle, field_name: comboBox.LabelTitle, old_value: oldVal[1][0], new_value: newVal[1][0], usercode: MainWindow.userCode, action: "Updated");
                                            }
                                            currentLoadedCatalyst[comboBox] = comboBox.SelectedValue.ToString();
                                        }
                                    }
                                }
                            }
                        }
                        else if (!String.IsNullOrEmpty(comboBox.Convertible))
                        {
                            if (comboBox.Convertible.Equals("yes"))
                            {
                                //for convertible units as they are not stored in db thus not loaded into currentloaded
                                string updateQuery = "";
                                //when convertible unit has changed convert the corresponding textbox value and update then insert into history
                                CustomTextBox[] textBoxes = comboboxesAndTextboxes[comboBox.Name];
                                foreach (CustomTextBox textBox in textBoxes)
                                {
                                    if (textBox != null)
                                    {
                                        if (!String.IsNullOrEmpty(textBox.Text) && !String.IsNullOrWhiteSpace(textBox.Text))
                                        {
                                            if (textBox.DefaultUnit.Equals("convertToGL"))
                                            {
                                                if (!currentLoadedCatalyst[textBox].Equals(converter.convertToGL(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString()))
                                                {
                                                    updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToGL(textBoxAndUnits[textBox.Name].Text, textBox.Text) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];

                                                    //insert history for each converted values
                                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToGL(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                                    //set current value to the updated one
                                                    currentLoadedCatalyst[textBox] = converter.convertToGL(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString();
                                                }
                                            }
                                            if (textBox.DefaultUnit.Equals("convertToMil"))
                                            {
                                                if (!currentLoadedCatalyst[textBox].Equals(converter.convertToMil(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString()))
                                                {
                                                    updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToMil(textBoxAndUnits[textBox.Name].Text, textBox.Text) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];

                                                    //insert history for each converted values
                                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToMil(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                                    //set current value to the updated one
                                                    currentLoadedCatalyst[textBox] = converter.convertToMil(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString();
                                                }
                                            }
                                            if (textBox.DefaultUnit.Equals("convertToLiter"))
                                            {
                                                updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, textBox.Text) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                                if (!currentLoadedCatalyst[textBox].Equals(converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString()))
                                                {
                                                    //insert history for each converted values
                                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                                    //set current value to the updated one
                                                    currentLoadedCatalyst[textBox] = converter.convertToLiter(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString();
                                                }
                                            }
                                            if (textBox.DefaultUnit.Equals("convertToMMDouble"))
                                            {
                                                if (!currentLoadedCatalyst[textBox].Equals(converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString()))
                                                {
                                                    updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, textBox.Text) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                                    //insert history for each converted values
                                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                                    //set current value to the updated one
                                                    currentLoadedCatalyst[textBox] = converter.convertToMMDouble(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString();

                                                }

                                            }
                                            if (textBox.DefaultUnit.Equals("convertToCelsius"))
                                            {
                                                if (!currentLoadedCatalyst[textBox].Equals(converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString()))
                                                {
                                                    updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, textBox.Text) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                                    //insert history for each converted values
                                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                                    //set current value to the updated one
                                                    currentLoadedCatalyst[textBox] = converter.convertToCelsius(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString();

                                                }

                                            }
                                            if (textBox.DefaultUnit.Equals("convertToInch"))
                                            {
                                                if (!currentLoadedCatalyst[textBox].Equals(converter.convertToInch(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString()))
                                                {
                                                    updateQuery = "update " + textBox.TableName + " set " + textBox.FieldName + " =  " + converter.convertToInch(textBoxAndUnits[textBox.Name].Text, textBox.Text) + " where " + textBox.UpdateId + " = " + updateIDs[textBox.UpdateId];
                                                    //insert history for each converted values
                                                    insertHistory(catalystID: catalyst_id.Text, group_name: textBox.GroupTitle, field_name: textBox.LabelTitle, old_value: currentLoadedCatalyst[textBox], new_value: converter.convertToInch(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString(), usercode: MainWindow.userCode, action: "Updated");
                                                    //set current value to the updated one
                                                    currentLoadedCatalyst[textBox] = converter.convertToInch(textBoxAndUnits[textBox.Name].Text, textBox.Text).ToString();

                                                }
                                            }
                                            if (!String.IsNullOrWhiteSpace(updateQuery) && !String.IsNullOrEmpty(updateQuery))
                                            {
                                                SQLiteCommand updateTextCmd = new SQLiteCommand(updateQuery);
                                                sqliteDb.Update(updateTextCmd);
                                                //remove from updateFields   
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    if (updatedFields.ElementAt(i).Key is CustomDatePicker)
                    {
                        CustomDatePicker datePicker = (CustomDatePicker)updatedFields.ElementAt(i).Key;
                        if (datePicker.UpdateId != null && datePicker.UpdateId != "" && updateIDs.ContainsKey(datePicker.UpdateId))
                        {
                            string genQuery = "select * from " + datePicker.TableName + " where " + datePicker.UpdateId + " = " + updateIDs[datePicker.UpdateId];
                            List<String>[] genExist = sqliteDb.Select(genQuery);
                            if (genExist != null && genExist[0].Count > 0)
                            {
                                string dateQuery = "select * from " + datePicker.TableName + " where " + datePicker.UpdateId + " = " + updateIDs[datePicker.UpdateId] + " and " + datePicker.FieldName + " = '" + datePicker.SelectedDate.Value.ToShortDateString() + "'";
                                List<String>[] dateExist = sqliteDb.Select(dateQuery);
                                if (dateExist == null || dateExist[0].Count == 0)
                                {
                                    MessageBox.Show(updatedFields[updatedFields.ElementAt(i).Key]);
                                    string updateQuery = "update " + datePicker.TableName + " set " + datePicker.FieldName + " = '" + updatedFields[updatedFields.ElementAt(i).Key] + "' where " + datePicker.UpdateId + " = " + updateIDs[datePicker.UpdateId];
                                    MessageBox.Show(updateQuery);
                                    SQLiteCommand updateCmd = new SQLiteCommand(updateQuery);
                                    sqliteDb.Update(updateCmd);
                                    //insert history
                                    insertHistory(catalystID: catalyst_id.Text, group_name: datePicker.GroupTitle, field_name: datePicker.LabelTitle, old_value: currentLoadedCatalyst[datePicker], new_value: datePicker.SelectedDate.Value.ToShortDateString(), usercode: MainWindow.userCode, action: "Updated");

                                }
                            }
                        }
                    }
                }
                updatedFields = new Dictionary<UIElement, string>();
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/GeneralInformation"))
                {

                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/GeneralInformation");
                    string[] currentFiles = new string[files.Count()];
                    for (int i = 0; i < files.Count(); i++)
                    {
                        currentFiles[i] = Path.GetFileName(files[i]);
                    }
                    string[] updatedFiles = general_relatedFiles.Items.OfType<string>().ToArray();
                    List<string> fToDel = filesToDelete(currentFiles, updatedFiles);
                    foreach (string s in fToDel)
                    {
                        string fPath = Path.Combine(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/GeneralInformation/", s);
                        File.Delete(fPath);
                    }
                    for (int i = 0; i < genFilesToCopy.Count; i++)
                    {
                        System.IO.File.Copy(genFilesToCopy[i].Location, catalyst_id.Text + "/GeneralInformation/" + genFilesToCopy[i].Name, true);
                    }
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB"))
                {

                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB");
                    string[] currentFiles = new string[files.Count()];
                    for (int i = 0; i < files.Count(); i++)
                    {
                        currentFiles[i] = Path.GetFileName(files[i]);
                    }
                    string[] updatedFiles = general_relatedFiles.Items.OfType<string>().ToArray();
                    List<string> fToDel = filesToDelete(currentFiles, updatedFiles);
                    foreach (string s in fToDel)
                    {
                        string fPath = Path.Combine(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/LGB/", s);
                        File.Delete(fPath);
                    }
                    for (int i = 0; i < genFilesToCopy.Count; i++)
                    {
                        System.IO.File.Copy(genFilesToCopy[i].Location, catalyst_id.Text + "/LGB/" + genFilesToCopy[i].Name, true);
                    }
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Simulation"))
                {

                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Simulation");
                    string[] currentFiles = new string[files.Count()];
                    for (int i = 0; i < files.Count(); i++)
                    {
                        currentFiles[i] = Path.GetFileName(files[i]);
                    }
                    string[] updatedFiles = general_relatedFiles.Items.OfType<string>().ToArray();
                    List<string> fToDel = filesToDelete(currentFiles, updatedFiles);
                    foreach (string s in fToDel)
                    {
                        string fPath = Path.Combine(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Simulation/", s);
                        File.Delete(fPath);
                    }
                    for (int i = 0; i < genFilesToCopy.Count; i++)
                    {
                        System.IO.File.Copy(genFilesToCopy[i].Location, catalyst_id.Text + "/Simulation/" + genFilesToCopy[i].Name, true);
                    }
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Testbench"))
                {

                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Testbench");
                    string[] currentFiles = new string[files.Count()];
                    for (int i = 0; i < files.Count(); i++)
                    {
                        currentFiles[i] = Path.GetFileName(files[i]);
                    }
                    string[] updatedFiles = general_relatedFiles.Items.OfType<string>().ToArray();
                    List<string> fToDel = filesToDelete(currentFiles, updatedFiles);
                    foreach (string s in fToDel)
                    {
                        string fPath = Path.Combine(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/Testbench/", s);
                        File.Delete(fPath);
                    }
                    for (int i = 0; i < genFilesToCopy.Count; i++)
                    {
                        System.IO.File.Copy(genFilesToCopy[i].Location, catalyst_id.Text + "/Testbench/" + genFilesToCopy[i].Name, true);
                    }
                }
                if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/ChemPhys"))
                {

                    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/ChemPhys");
                    string[] currentFiles = new string[files.Count()];
                    for (int i = 0; i < files.Count(); i++)
                    {
                        currentFiles[i] = Path.GetFileName(files[i]);
                    }
                    string[] updatedFiles = general_relatedFiles.Items.OfType<string>().ToArray();
                    List<string> fToDel = filesToDelete(currentFiles, updatedFiles);
                    foreach (string s in fToDel)
                    {
                        string fPath = Path.Combine(Directory.GetCurrentDirectory() + "/" + catalyst_id.Text + "/ChemPhys/", s);
                        File.Delete(fPath);
                    }
                    for (int i = 0; i < genFilesToCopy.Count; i++)
                    {
                        System.IO.File.Copy(genFilesToCopy[i].Location, catalyst_id.Text + "/ChemPhys/" + genFilesToCopy[i].Name, true);
                    }
                }
                MessageBox.Show("Catalyst ID = " + updateIDs["catalyst_id"] + " Successfully Updated!");


            }
        }

        private List<string> filesToDelete(string[] currentFiles, string[] updatedFile)
        {
            List<string> files = new List<string>();
            for (int i = 0; i < currentFiles.Count(); i++)
            {
                if (!updatedFile.Contains(currentFiles[i]))
                {
                    files.Add(currentFiles[i]);
                }
            }
            return files;
        }

        private Dictionary<string, KeyValuePair<string, string>> supportToDelete(Dictionary<string, KeyValuePair<string, string>> current, Dictionary<string, KeyValuePair<string, string>> updated)
        {
            Dictionary<string, KeyValuePair<string, string>> result = new Dictionary<string, KeyValuePair<string, string>>();
            Dictionary<string, KeyValuePair<string, string>> temp = new Dictionary<string, KeyValuePair<string, string>>(current);

            foreach (KeyValuePair<string, KeyValuePair<string, string>> entry in current)
            {
                if (!updated.ContainsKey(entry.Key) || (updated.ContainsKey(entry.Key) && updated[entry.Key].Key.Equals(entry.Value.Key) && !updated[entry.Key].Value.Equals(entry.Value.Value)) || (updated.ContainsKey(entry.Key) && !updated[entry.Key].Key.Equals(entry.Value.Key) && !updated[entry.Key].Value.Equals(entry.Value.Value)))
                {
                    result.Add(entry.Key, entry.Value);
                    //delete from current
                    temp.Remove(entry.Key);
                }
            }
            current = new Dictionary<string, KeyValuePair<string, string>>(temp);
            return result;
        }

        private Dictionary<string, KeyValuePair<string, string>> supportToInsert(Dictionary<string, KeyValuePair<string, string>> current, Dictionary<string, KeyValuePair<string, string>> updated)
        {
            Dictionary<string, KeyValuePair<string, string>> result = new Dictionary<string, KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, KeyValuePair<string, string>> entry in updated)
            {
                if (!current.ContainsKey(entry.Key) || (current.ContainsKey(entry.Key) && current[entry.Key].Key.Equals(entry.Value.Key) && !current[entry.Key].Value.Equals(entry.Value.Value)) || (current.ContainsKey(entry.Key) && !current[entry.Key].Key.Equals(entry.Value.Key) && !current[entry.Key].Value.Equals(entry.Value.Value)))
                {
                    result.Add(entry.Key, entry.Value);
                    //add
                    if (current.ContainsKey(entry.Key))
                    {
                        current.Remove(entry.Key);
                    }
                    current.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }


        private List<string> preciousDelete(ObservableCollection<PreciousMetalLoading> current, ObservableCollection<PreciousMetalLoading> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < updated.Count; i++)
            {
                if (current[i].IsChecked && !updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //uncheck
                    current[i].IsChecked = false;
                }
            }
            return result;
        }

        private List<PreciousMetalLoading> preciousToInsert(ObservableCollection<PreciousMetalLoading> current, ObservableCollection<PreciousMetalLoading> updated)
        {
            List<PreciousMetalLoading> result = new List<PreciousMetalLoading>();
            for (int i = 0; i < updated.Count; i++)
            {
                if (!current[i].IsChecked && updated[i].IsChecked)
                {
                    result.Add(updated[i]);
                    //check
                    current[i].IsChecked = true;
                }
            }
            return result;
        }

        private Dictionary<string, string> comToDelete(Dictionary<string, string> current, Dictionary<string, string> updated)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> temp = new Dictionary<string, string>(current);

            foreach (KeyValuePair<string, string> entry in current)
            {
                //             MessageBox.Show("Key : " + entry.Key + "  -  " + updated.ContainsKey(entry.Key).ToString());
                //            MessageBox.Show("Value : " + entry.Value + "  -  " + updated[entry.Key].Equals(entry.Value).ToString());
                if (!updated.ContainsKey(entry.Key) || (updated.ContainsKey(entry.Key) && !updated[entry.Key].Equals(entry.Value)))
                {
                    result.Add(entry.Key, entry.Value);
                    //remove
                    temp.Remove(entry.Key);
                }
            }

            current = new Dictionary<string, string>(temp);
            return result;
        }

        private Dictionary<string, string> comToInsert(Dictionary<string, string> current, Dictionary<string, string> updated)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (updated.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in updated)
                {
                    if (!current.ContainsKey(entry.Key) || (current.ContainsKey(entry.Key) && !current.ContainsValue(entry.Value)))
                    {
                        result.Add(entry.Key, entry.Value);
                        //add
                        if (current.ContainsKey(entry.Key))
                        {
                            current.Remove(entry.Key);
                        }
                        current.Add(entry.Key, entry.Value);
                    }
                }
            }

            return result;
        }

        private List<string> combinedToDelete(Dictionary<string, CustomComboBox> current, Dictionary<string, CustomComboBox> updated)
        {
            List<string> result = new List<string>();
            Dictionary<string, CustomComboBox> copiedDic = new Dictionary<string, CustomComboBox>(current);
            foreach (KeyValuePair<string, CustomComboBox> entry in current)
            {
                if (!updated.ContainsKey(entry.Key) && !result.Contains(entry.Key))
                {
                    result.Add(entry.Key);
                    //remove
                    copiedDic.Remove(entry.Key);
                }
            }
            current = new Dictionary<string, CustomComboBox>(copiedDic);

            return result;
        }

        private Dictionary<string, CustomComboBox> combinedToInsert(Dictionary<string, CustomComboBox> current, Dictionary<string, CustomComboBox> updated)
        {
            Dictionary<string, CustomComboBox> result = new Dictionary<string, CustomComboBox>();
            if (updated.Count > 0)
            {
                foreach (KeyValuePair<string, CustomComboBox> entry in updated)
                {
                    if (!current.ContainsKey(entry.Key) && !result.ContainsKey(entry.Key))
                    {
                        result.Add(entry.Key, entry.Value);
                        //add
                        current.Add(entry.Key, entry.Value);
                    }
                }
            }

            return result;

        }

        private List<string> transientToDelete(ObservableCollection<TransientLegislation> current, ObservableCollection<TransientLegislation> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < updated.Count; i++)
            {
                if (current[i].IsChecked && !updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //uncheck
                    current[i].IsChecked = false;
                }
            }
            return result;
        }

        private List<TransientLegislation> transientToInsert(ObservableCollection<TransientLegislation> current, ObservableCollection<TransientLegislation> updated)
        {
            List<TransientLegislation> result = new List<TransientLegislation>();
            for (int i = 0; i < current.Count; i++)
            {
                if (!current[i].IsChecked && updated[i].IsChecked)
                {
                    result.Add(updated[i]);
                    //check
                    current[i].IsChecked = true;
                }
            }
            return result;
        }

        private List<string> steadysToDelete(ObservableCollection<SteadyStateLegislation> current, ObservableCollection<SteadyStateLegislation> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < updated.Count; i++)
            {
                if (current[i].IsChecked && !updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //uncheck
                    current[i].IsChecked = false;
                }
            }
            return result;
        }

        private List<SteadyStateLegislation> steadysToInsert(ObservableCollection<SteadyStateLegislation> current, ObservableCollection<SteadyStateLegislation> updated)
        {
            List<SteadyStateLegislation> result = new List<SteadyStateLegislation>();
            for (int i = 0; i < current.Count; i++)
            {
                if (!current[i].IsChecked && updated[i].IsChecked)
                {
                    result.Add(updated[i]);
                    //check
                    current[i].IsChecked = true;
                }
            }
            return result;
        }

        private List<string> manufacturersToDelete(ObservableCollection<Manufacturer> current, ObservableCollection<Manufacturer> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < updated.Count; i++)
            {
                if (current[i].IsChecked && !updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //uncheck
                    current[i].IsChecked = false;
                }
            }
            return result;
        }

        private List<string> manufacturersToInsert(ObservableCollection<Manufacturer> current, ObservableCollection<Manufacturer> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < updated.Count; i++)
            {
                if (!current[i].IsChecked && updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //check current
                    current[i].IsChecked = true;
                }
            }
            return result;
        }

        private List<string> fieldsToDelete(ObservableCollection<ApplicationField> current, ObservableCollection<ApplicationField> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < current.Count; i++)
            {
                if (current[i].IsChecked && !updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //uncheck
                    current[i].IsChecked = false;
                }
            }
            return result;
        }

        private List<string> fieldsToInsert(ObservableCollection<ApplicationField> current, ObservableCollection<ApplicationField> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < current.Count; i++)
            {
                if (!current[i].IsChecked && updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //check
                    current[i].IsChecked = true;
                }
            }
            return result;
        }

        private List<string> washcoatsToDelete(ObservableCollection<WashcoatCatalyticComposition> current, ObservableCollection<WashcoatCatalyticComposition> updated)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < current.Count; i++)
            {
                if (current[i].IsChecked && !updated[i].IsChecked)
                {
                    result.Add(updated[i].Id.ToString());
                    //uncheck
                    current[i].IsChecked = false;
                }
            }
            return result;
        }

        private List<WashcoatCatalyticComposition> washcoatsToInsert(ObservableCollection<WashcoatCatalyticComposition> current, ObservableCollection<WashcoatCatalyticComposition> updated)
        {
            List<WashcoatCatalyticComposition> result = new List<WashcoatCatalyticComposition>();
            for (int i = 0; i < current.Count; i++)
            {
                if (!current[i].IsChecked && updated[i].IsChecked)
                {
                    result.Add(updated[i]);
                    //check
                    current[i].IsChecked = true;
                }
            }
            return result;
        }

        public bool isValid(CustomTextBox textBox)
        {
            if (textBox.ContentType.Equals("text"))
            {
                return true;
            }
            else if (textBox.ContentType.Equals("double") && isDouble(textBox))
            {
                if (fieldsWithError.Count == 0)
                {
                    btn_add_catalyst.IsEnabled = true;
                    btn_update_catalyst.IsEnabled = true;
                }
                return true;
            }
            else if (textBox.ContentType.Equals("int") && isInteger(textBox))
            {
                if (fieldsWithError.Count == 0)
                {
                    btn_add_catalyst.IsEnabled = true;
                    btn_update_catalyst.IsEnabled = true;
                }
                return true;
            }
            else if(fieldsWithError.Count > 0 )
            {    
                btn_add_catalyst.IsEnabled = false;
                btn_update_catalyst.IsEnabled = false;
                return false;
            }
            return false;
        }


        private void update_KeyUp(object sender, KeyEventArgs e)
        {
            CustomTextBox textBox = (CustomTextBox)sender;
            if (e.Key == Key.Enter)
            {
            }
            else if (isValid(textBox))
            {
                if (!String.IsNullOrWhiteSpace(textBox.UpdateId))
                {
                    if (updatedFields.ContainsKey((UIElement)sender))
                    {
                        updatedFields.Remove((UIElement)sender);
                    }
                    if (textBox.ContentType.Equals("text"))
                    {
                        updatedFields.Add((UIElement)sender, textBox.Text);
                    }
                    if (textBox.ContentType.Equals("double") && isDouble(textBox))
                    {
                        int textBoxValue = String.IsNullOrWhiteSpace(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
                        updatedFields.Add((UIElement)sender, textBox.Text);

                    }
                    if (textBox.ContentType.Equals("int") && isInteger(textBox))
                    {
                        int textBoxValue = String.IsNullOrWhiteSpace(textBox.Text) ? 0 : Convert.ToInt32(textBox.Text);
                        updatedFields.Add((UIElement)sender, textBox.Text);
                    }
                }
            }
        }

        private void validationMouseLeave(object sender, MouseEventArgs e)
        {
        }


        private bool isDouble(CustomTextBox txb)
        {
            double result;
            if (!String.IsNullOrWhiteSpace(txb.Text) && !String.IsNullOrEmpty(txb.Text))
            {
                if (!Double.TryParse(txb.Text, out result) && txb.Text != "")
                {
                    txb.BorderBrush = new SolidColorBrush(Colors.Red);
                    txb.BorderThickness = new Thickness(2, 2, 2, 2);
                    txb.ToolTip = "Only number is accepted!";
                    if (!fieldsWithError.Contains(txb.Name))
                    {
                        fieldsWithError.Add(txb.Name);
                    }
                    return false;
                }
                else
                {
                    txb.ClearValue(CustomTextBox.BorderBrushProperty);
                    txb.ClearValue(CustomTextBox.BorderThicknessProperty);
                    txb.ClearValue(CustomTextBox.ToolTipProperty);
                    if (fieldsWithError.Contains(txb.Name))
                    {
                        fieldsWithError.Remove(txb.Name);
                    }
                    return true;
                }
            }
            else
            {
                txb.ClearValue(CustomTextBox.BorderBrushProperty);
                txb.ClearValue(CustomTextBox.BorderThicknessProperty);
                txb.ClearValue(CustomTextBox.ToolTipProperty);
                if (fieldsWithError.Contains(txb.Name))
                {
                    fieldsWithError.Remove(txb.Name);
                }
                return true;
            }
        }

        private bool isInteger(CustomTextBox txb)
        {
            int result;
            if (!String.IsNullOrWhiteSpace(txb.Text) && !String.IsNullOrEmpty(txb.Text))
            {
                if (!Int32.TryParse(txb.Text, out result))
                {
                    txb.BorderBrush = new SolidColorBrush(Colors.Red);
                    txb.BorderThickness = new Thickness(2, 2, 2, 2);
                    txb.ToolTip = "Only integer is accepted!";
                    return false;
                }
                else
                {
                    txb.ClearValue(CustomTextBox.BorderBrushProperty);
                    txb.ClearValue(CustomTextBox.BorderThicknessProperty);
                    txb.ClearValue(CustomTextBox.ToolTipProperty);
                    return true;
                }
            }
            else
            {
                txb.ClearValue(CustomTextBox.BorderBrushProperty);
                txb.ClearValue(CustomTextBox.BorderThicknessProperty);
                txb.ClearValue(CustomTextBox.ToolTipProperty);
                return true;
            }

        }

        private void btn_general_file_delete_Click(object sender, RoutedEventArgs e)
        {
            if (general_relatedFiles.SelectedIndex >= 0)
            {

                FileToUpload f = new FileToUpload(Path.GetFileName(general_relatedFiles.SelectedValue.ToString()), general_relatedFiles.SelectedValue.ToString());
                if (genFilesToCopy.Contains(f))
                {
                    genFilesToCopy.Remove(f);
                }
                general_relatedFiles.Items.RemoveAt(general_relatedFiles.SelectedIndex);
            }
        }

        private void btn_simulation_delete_file_Click(object sender, RoutedEventArgs e)
        {
            if (simulation_relatedFiles.SelectedIndex >= 0)
            {
                simulation_relatedFiles.Items.RemoveAt(simulation_relatedFiles.SelectedIndex);
                simuFilesToCopy.RemoveAt(simulation_relatedFiles.SelectedIndex);
            }
        }

        private void btn_testbench_delete_file_Click(object sender, RoutedEventArgs e)
        {
            if (testbench_relatedFiles.SelectedIndex >= 0)
            {
                testbench_relatedFiles.Items.RemoveAt(testbench_relatedFiles.SelectedIndex);
                testFilesToCopy.RemoveAt(testbench_relatedFiles.SelectedIndex);
            }
        }

        private void btn_chem_delete_file_Click(object sender, RoutedEventArgs e)
        {
            if (chem_relatedFiles.SelectedIndex >= 0)
            {
                chem_relatedFiles.Items.RemoveAt(chem_relatedFiles.SelectedIndex);
                chemFilesToCopy.RemoveAt(chem_relatedFiles.SelectedIndex);
            }
        }

        private void btn_char_delete_file_Click(object sender, RoutedEventArgs e)
        {
            ListBox list = (ListBox)grid_catalyst_charac.FindName("char_relatedFiles");
            if (list.SelectedIndex >= 0)
            {
                list.Items.RemoveAt(list.SelectedIndex);
                lgbFilesToCopy.RemoveAt(list.SelectedIndex);
            }
        }

        private void btn_clear_field_Click(object sender, RoutedEventArgs e)
        {
            foreach (Manufacturer man in Manufacturers)
            {
                man.IsChecked = false;
            }

            foreach (ApplicationField app in ApplicationFields)
            {
                app.IsChecked = false;
            }
            foreach (ApplicationField app in ApplicationFieldsTestbench)
            {
                app.IsChecked = false;
            }
            foreach (WashcoatCatalyticComposition washcoat in Washcoats)
            {
                if (washcoat.IsChecked == true)
                {
                    washcoat.IsChecked = false;
                }
            }
            foreach (PreciousMetalLoading precious in PreciousMetalLoadings)
            {
                if (precious.IsChecked == true)
                {
                    precious.IsChecked = false;
                }
            }

            List<Grid> allGrids = new List<Grid>() { grid_catalyst_charac, grid_chem_analysis, grid_generalInfo, grid_simulation, grid_testbench };
            foreach (Grid grid in allGrids)
            {
                clearFields(grid);
            }

            cmb_monoloth_material.SelectedIndex = 0;
            cmb_aging_status.SelectedIndex = 0;


        }

        private void clearFields(Grid grid)
        {
            foreach (UIElement element in grid.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel stack = (StackPanel)element;
                    clearStack(stack);
                }
            }
        }

        private void clearStack(StackPanel stack)
        {
            foreach (UIElement el in stack.Children)
            {
                if (el is StackPanel)
                {
                    StackPanel s = (StackPanel)el;
                    clearStack(s);
                }
                if (el is CustomCheckBox)
                {
                    CustomCheckBox c = (CustomCheckBox)el;
                    c.ClearValue(CustomCheckBox.IsCheckedProperty);
                }
                if (el is CustomComboBox)
                {
                    CustomComboBox c1 = (CustomComboBox)el;
                    c1.ClearValue(CustomComboBox.SelectedIndexProperty);
                    c1.ClearValue(CustomComboBox.SelectedItemProperty);
                    c1.ClearValue(CustomComboBox.SelectedValueProperty);

                }
                if (el is CustomTextBox)
                {
                    CustomTextBox t = (CustomTextBox)el;
                    t.ClearValue(CustomTextBox.TextProperty);
                }
                if (el is CustomDatePicker)
                {
                    CustomDatePicker d = (CustomDatePicker)el;
                    d.ClearValue(CustomDatePicker.SelectedDateProperty);
                }

                if (el is ScrollViewer)
                {
                    ScrollViewer s = (ScrollViewer)el;
                    if (s.Content is ListBox)
                    {
                        ListBox l = (ListBox)s.Content;
                        l.Items.Clear();
                    }
                    if (s.Content is CustomTextBox)
                    {
                        CustomTextBox t = (CustomTextBox)s.Content;
                        t.ClearValue(CustomTextBox.TextProperty);
                    }

                }
            }
        }

        private void btn_paste_data_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<Grid, Dictionary<string, string>> allGrids = new Dictionary<Grid, Dictionary<string, string>>();
            allGrids.Add(grid_generalInfo, copiedGeneralList);
            allGrids.Add(grid_catalyst_charac, copiedLgbList);
            allGrids.Add(grid_simulation, copiedSimulationList);
            allGrids.Add(grid_testbench, copiedTestbenchList);
            allGrids.Add(grid_chem_analysis, copiedChemList);
            if (copiedGeneralList != null)
            {
                foreach (KeyValuePair<Grid, Dictionary<string, string>> grid in allGrids)
                {
                    if (grid.Value != null)
                    {
                        pasteGrid(grid.Key, grid.Value);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Catalyst Copied!");
            }


        }

        private void pasteGrid(Grid grid, Dictionary<string, string> listToPasteFrom)
        {
            foreach (UIElement element in grid.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel stack = (StackPanel)element;
                    pasteStack(stack, listToPasteFrom);
                }
            }
        }

        private void pasteStack(StackPanel stack, Dictionary<string, string> listToPasteFrom)
        {
            foreach (UIElement el in stack.Children)
            {
                if (el is StackPanel)
                {
                    StackPanel s = (StackPanel)el;
                    pasteStack(s, listToPasteFrom);
                }
                if (el is CustomCheckBox)
                {
                    CustomCheckBox c = (CustomCheckBox)el;
                    if (listToPasteFrom.ContainsKey(c.Name))
                    {
                        c.IsChecked = Convert.ToBoolean(listToPasteFrom[c.Name]);
                    }
                }
                if (el is CustomComboBox)
                {
                    CustomComboBox c1 = (CustomComboBox)el;
                    if (listToPasteFrom.ContainsKey(c1.Name))
                    {
                        c1.SelectedValue = listToPasteFrom[c1.Name];
                    }
                }
                if (el is CustomTextBox)
                {
                    CustomTextBox t = (CustomTextBox)el;
                    if (listToPasteFrom.ContainsKey(t.Name))
                    {
                        t.Text = listToPasteFrom[t.Name];
                    }
                }
                if (el is CustomDatePicker)
                {
                    CustomDatePicker d = (CustomDatePicker)el;
                    if (listToPasteFrom.ContainsKey(d.Name))
                    {
                        //todo
                    }
                }

                if (el is ScrollViewer)
                {
                    ScrollViewer s = (ScrollViewer)el;
                    if (s.Content is CustomTextBox)
                    {
                        CustomTextBox t = (CustomTextBox)s.Content;
                        if (listToPasteFrom.ContainsKey(t.Name))
                        {
                            t.Text = listToPasteFrom[t.Name];
                        }
                    }

                }
            }
        }



        private void btn_copy_data_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<Grid, Dictionary<string, string>> allGrids = new Dictionary<Grid, Dictionary<string, string>>();
            copiedGeneralList = new Dictionary<string, string>();
            copiedLgbList = new Dictionary<string, string>();
            copiedSimulationList = new Dictionary<string, string>();
            copiedTestbenchList = new Dictionary<string, string>();
            copiedChemList = new Dictionary<string, string>();
            allGrids.Add(grid_generalInfo, copiedGeneralList);
            allGrids.Add(grid_catalyst_charac, copiedLgbList);
            allGrids.Add(grid_simulation, copiedSimulationList);
            allGrids.Add(grid_testbench, copiedTestbenchList);
            allGrids.Add(grid_chem_analysis, copiedChemList);
            foreach (KeyValuePair<Grid, Dictionary<string, string>> grid in allGrids)
            {
                copyGrid(grid.Key, grid.Value);
            }
            if (!String.IsNullOrWhiteSpace(catalyst_id.Text))
            {
                MessageBox.Show("Successfully Copied Catalyst ID = " + catalyst_id.Text + "!");
            }
            else
            {
                MessageBox.Show("Successfully Copied Current Data!");
            }


        }

        private void copyGrid(Grid grid, Dictionary<string, string> listToCopy)
        {
            foreach (UIElement element in grid.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel stack = (StackPanel)element;
                    copyStack(stack, listToCopy);
                }
            }
        }

        private void copyStack(StackPanel stack, Dictionary<string, string> listToCopy)
        {
            foreach (UIElement el in stack.Children)
            {
                if (el is StackPanel)
                {
                    StackPanel s = (StackPanel)el;
                    copyStack(s, listToCopy);
                }
                if (el is CustomCheckBox)
                {
                    CustomCheckBox c = (CustomCheckBox)el;
                    listToCopy.Add(c.Name, c.IsChecked.ToString());
                }
                if (el is CustomComboBox)
                {
                    CustomComboBox c1 = (CustomComboBox)el;
                    if (c1.SelectedItem != null && c1.SelectedValue != null)
                    {
                        listToCopy.Add(c1.Name, c1.SelectedValue.ToString());
                    }
                }
                if (el is CustomTextBox)
                {
                    CustomTextBox t = (CustomTextBox)el;
                    if (!String.IsNullOrWhiteSpace(t.Text))
                    {
                        listToCopy.Add(t.Name, t.Text);
                    }
                }
                if (el is CustomDatePicker)
                {
                    DateTime? mytime = null;
                    CustomDatePicker d = (CustomDatePicker)el;
                    if (d.SelectedDate != mytime)
                    {
                        listToCopy.Add(d.Name, d.SelectedDate.Value.ToShortDateString());
                    }
                }
                if (el is ScrollViewer)
                {
                    ScrollViewer s = (ScrollViewer)el;
                    if (s.Content is CustomTextBox)
                    {
                        CustomTextBox t = (CustomTextBox)s.Content;
                        if (t.Text != null)
                        {
                            listToCopy.Add(t.Name, t.Text);
                        }
                    }
                }
            }
        }

        private bool insertHistory(string catalystID, string group_name, string field_name, string usercode, string old_value, string action, string new_value)
        {
            SqliteDBConnection sqlite_db = new SqliteDBConnection();
            SQLiteCommand cmd_history = new SQLiteCommand();
            cmd_history.CommandText = "insert into History (catalyst_id, table_name, field_name, old_value, new_value, user_name, user_action, modified_date ) values (@catalyst_id, @table_name, @field_name, @old_value, @new_value, @user_name, @user_action, @modified_date ) ";
            cmd_history.Parameters.AddWithValue("@catalyst_id", catalystID);
            cmd_history.Parameters.AddWithValue("@table_name", group_name);
            cmd_history.Parameters.AddWithValue("@field_name", field_name);
            cmd_history.Parameters.AddWithValue("@old_value", old_value);
            cmd_history.Parameters.AddWithValue("@new_value", new_value);
            cmd_history.Parameters.AddWithValue("@user_name", usercode);
            cmd_history.Parameters.AddWithValue("@user_action", action);
            cmd_history.Parameters.AddWithValue("@modified_date", DateTime.Now.ToShortDateString());
            sqlite_db.Insert(cmd_history);
            return true;
        }

    }
}
