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
        ObservableCollection<CustomComboBoxItem> searchParamters { get; set; }
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
        Array arrPreciousMetalLoadings;
        Array arrCristallineWashcoatComponentFunctions;
        Array arrApplicationFields;
        Array arrApplicationFieldsTestbench;
        Array arrSourceOfDatas;
        Array arrSourceOfMeasurements;
        Array arrCheckBox;
        Array arrCycle;
        Array arrOtherCycle;
        Array arrZone;
        Array arrLayer;
        Array arrMaxO2;
        Array arrMaxNSC;
        Array arrMaxAmmonia;
        Array arrUnitLoading;
        Array arrHalfWashcoats;
        CustomComboBoxItem previousChosen = null; 

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

            ObservableCollection<WashcoatCatalyticComposition> HalfWashcoats = dropdowns.populateHalfWashcoats();
            arrHalfWashcoats = HalfWashcoats.Cast<DBModel>().ToArray();


            ObservableCollection<CristallineWashcoatComponentFunction> CristallineWashcoatComponentFunctions = dropdowns.populateCristallineWashcoatComponentFunctions();
            arrCristallineWashcoatComponentFunctions = CristallineWashcoatComponentFunctions.Cast<DBModel>().ToArray();

            ObservableCollection<CheckBoxValues> CheckBoxVals = new ObservableCollection<CheckBoxValues>();
            CheckBoxVals.Add(new CheckBoxValues("1"));
            CheckBoxVals.Add(new CheckBoxValues("0"));
            arrCheckBox = CheckBoxVals.Cast<DBModel>().ToArray();

            ObservableCollection<ZoneLayer> Zones = new ObservableCollection<ZoneLayer>();
            Zones.Add(new ZoneLayer(1));
            Zones.Add(new ZoneLayer(2));
            Zones.Add(new ZoneLayer(3));
            Zones.Add(new ZoneLayer(4));
            Zones.Add(new ZoneLayer(5));
            arrZone = Zones.Cast<DBModel>().ToArray();

            ObservableCollection<ZoneLayer> Layers = new ObservableCollection<ZoneLayer>();
            Zones.Add(new ZoneLayer(1));
            Zones.Add(new ZoneLayer(2));
            Zones.Add(new ZoneLayer(3));
            Zones.Add(new ZoneLayer(4));
            Zones.Add(new ZoneLayer(5));
            arrLayer = Layers.Cast<DBModel>().ToArray();

            ObservableCollection<UnitPNCycle> normalCycles = new ObservableCollection<UnitPNCycle>();
           
            normalCycles.Add(new UnitPNCycle("g/kWh"));
            normalCycles.Add(new UnitPNCycle("g/km"));
            normalCycles.Add(new UnitPNCycle("g/bhp*hr"));
            normalCycles.Add(new UnitPNCycle("g/mi"));
            arrCycle = normalCycles.Cast<DBModel>().ToArray();

            ObservableCollection<UnitNormalCycle> otherCycles = new ObservableCollection<UnitNormalCycle>();
            otherCycles.Add(new UnitNormalCycle("#/kWh"));
            otherCycles.Add(new UnitNormalCycle("#/km"));
            otherCycles.Add(new UnitNormalCycle("#/bhp*hr"));
            otherCycles.Add(new UnitNormalCycle("#/mi"));
            arrOtherCycle = otherCycles.Cast<DBModel>().ToArray();

            ObservableCollection<UnitMaxOSC> maxO2Units = new ObservableCollection<UnitMaxOSC>();
            maxO2Units.Add(new UnitMaxOSC(1, "µmol/g"));
            maxO2Units.Add(new UnitMaxOSC(2, "mmol/g"));
            maxO2Units.Add(new UnitMaxOSC(3, "mmol/l"));
            maxO2Units.Add(new UnitMaxOSC(4, "g/l"));
            maxO2Units.Add(new UnitMaxOSC(5, "mol/l"));
            maxO2Units.Add(new UnitMaxOSC(6, "g/ft³"));
            maxO2Units.Add(new UnitMaxOSC(7, "mol/ft³"));
            arrMaxO2 = maxO2Units.Cast<DBModel>().ToArray();

            ObservableCollection<UnitMaxNOX> maxNSCUnits = new ObservableCollection<UnitMaxNOX>();
            maxNSCUnits.Add(new UnitMaxNOX(1, "µmol/g"));
            maxNSCUnits.Add(new UnitMaxNOX(2, "mmol/g"));
            maxNSCUnits.Add(new UnitMaxNOX(3, "mmol/l"));
            maxNSCUnits.Add(new UnitMaxNOX(4, "g/l"));
            maxNSCUnits.Add(new UnitMaxNOX(5, "mol/l"));
            maxNSCUnits.Add(new UnitMaxNOX(6, "g/ft³"));
            maxNSCUnits.Add(new UnitMaxNOX(7, "mol/ft³"));
            arrMaxNSC = maxNSCUnits.Cast<DBModel>().ToArray();

            ObservableCollection<UnitMaxAmmonia> maxAmmoniaUnits = new ObservableCollection<UnitMaxAmmonia>();
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(1, "µmol/g"));
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(2, "mmol/g"));
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(3, "mmol/l"));
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(4, "g/l"));
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(5, "mol/l"));
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(6, "g/ft³"));
            maxAmmoniaUnits.Add(new UnitMaxAmmonia(7, "mol/ft³"));
            arrMaxAmmonia = maxAmmoniaUnits.Cast<DBModel>().ToArray();

            ObservableCollection<UnitPreciousMetalLoading> UnitLoadings = new ObservableCollection<UnitPreciousMetalLoading>();
            UnitLoadings.Add(new UnitPreciousMetalLoading(1, "g/l"));
            UnitLoadings.Add(new UnitPreciousMetalLoading(2, "g/ft³"));
            UnitLoadings.Add(new UnitPreciousMetalLoading(3, "g/Kat"));
            arrUnitLoading = UnitLoadings.Cast<DBModel>().ToArray();
            


        }

        public void InitializeCollections()
        {

            forGeneralInformation = new ObservableCollection<CustomComboBoxItem>();
            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst_ID", "Catalyst", "catalyst_id", null, "Catalyst.catalyst_id", true, true, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst Data is Approved",  "Catalyst", "is_approved", null, "Catalyst.is_approved", false, false, true, null, null, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forGeneralInformation.Add(new CustomComboBoxItem("Confidentiality", "Catalyst", "is_data_available", null, "Catalyst.is_data_available", false, false, true, null, null, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));

            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst Type",  "Catalyst", "catalyst_type_id", null, "Catalyst.catalyst_type_id", false, false, true, null, null, null, false, true, false, "Id", "Type", arrCatalystTypes));
            forGeneralInformation.Add(new CustomComboBoxItem("Manufacturer",  "GeneralInformation", "manufacturer_id", "join GeneralInformationManufacturer on GeneralInformationManufacturer.general_id = GeneralInformation.general_id", "GeneralInformationManufacturer.manufacturer_id", false, false, true, null, null, null, false, true, false, "Id", "Name", arrManufacturers));
            forGeneralInformation.Add(new CustomComboBoxItem("Production Date", "GeneralInformation", "production_date", null, "GeneralInformation.production_date", false, false, true, null, null, null, true, false, false, null, null, null));

            forGeneralInformation.Add(new CustomComboBoxItem("Catalyst Nr#", "GeneralInformation", "catalyst_number", null, "GeneralInformation.catalyst_number", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Substract Nr#", "GeneralInformation", "substract_number", null, "GeneralInformation.substract_number", true, false, true, null, null, null, false, false, false, null, null, null));

            forGeneralInformation.Add(new CustomComboBoxItem("Project Nr#", "GeneralInformation", "project_number", null, "GeneralInformation.project_number", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Customer", "GeneralInformation", "customer", null, "GeneralInformation.customer", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Project Manager", "GeneralInformation", "project_manager", null, "GeneralInformation.project_manager", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("EATS Case Worker", "GeneralInformation", "eats_case_worker", null, "GeneralInformation.eats_case_worker", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Target Country", "GeneralInformation", "target_country", null, "GeneralInformation.target_country", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Target Emission Legislation", "GeneralInformation", "target_emission_legislation", null, "GeneralInformation.target_emission_legislation", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Field Of Application", "generalinformationapplicationfield", "app_field_id", "join generalinformationapplicationfield on generalinformationapplicationfield.general_id = generalinformation.general_id ", " generalinformationapplicationfield.app_field_id ", false, false, true, null, null, null, false, true, false, "Id", "Name", arrApplicationFields));
            forGeneralInformation.Add(new CustomComboBoxItem("Target Configuration Of EATS", "GeneralInformation", "conf_target_system", null, "GeneralInformation.conf_target_system", true, false, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Specification Of Engine", "GeneralInformation", "specification_engine", null, "GeneralInformation.specification_engine", true, false, true, null, null, null, false, false, false, null, null, null));

            forGeneralInformation.Add(new CustomComboBoxItem("Washcoat, Catalyc Composition", "GeneralInformation", "material_id", null, null, false, false, true, null, null, null, false, true, false, "Id", "WashcoatValue", arrHalfWashcoats));
            forGeneralInformation.Add(new CustomComboBoxItem("Pt Precious Metal Ratio", "general_washcoat", "precious_metal_ratio", "join generalwashcoat on generalwashcoat.general_id = generalinformation.general_id", " generalwashcoat.material_id = 1 and generalwashcoat.precious_metal_ratio ", true, true, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Pt Precious Metal Loading", "general_washcoat", "precious_metal_loading", "join generalwashcoat on generalwashcoat.general_id = generalinformation.general_id", " generalwashcoat.material_id = 1 and generalwashcoat.precious_metal_loading ", true, true, true, "g/l", null, null, false, false, false, null, null, arrUnitLoading));
            forGeneralInformation.Add(new CustomComboBoxItem("Pd Precious Metal Loading", "general_washcoat", "precious_metal_loading", "join generalwashcoat on generalwashcoat.general_id = generalinformation.general_id", " generalwashcoat.material_id = 2 and generalwashcoat.precious_metal_ratio ", true, true, true, "g/l", null, null, false, false, false, null, null, arrUnitLoading));
            forGeneralInformation.Add(new CustomComboBoxItem("Rh Precious Metal Ratio", "general_washcoat", "precious_metal_ratio", "join generalwashcoat on generalwashcoat.general_id = generalinformation.general_id", " generalwashcoat.material_id = 2 and generalwashcoat.precious_metal_loading ", true, true, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Rh Precious Metal Loading", "general_washcoat", "precious_metal_loading", "join generalwashcoat on generalwashcoat.general_id = generalinformation.general_id", " generalwashcoat.material_id = 3 and generalwashcoat.precious_metal_ratio ", true, true, true, "g/l", null, null, false, false, false, null, null, arrUnitLoading));
            forGeneralInformation.Add(new CustomComboBoxItem("Washcoat Loading", "GeneralInformation", "washcoat_loading", "join generalwashcoat on generalwashcoat.general_id = generalinformation.general_id", " generalwashcoat.material_id = 3 and generalwashcoat.precious_metal_loading ", true, true, true, "g/l", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Cell Density", "GeneralInformation", "cell_density", null, "GeneralInformation.cell_density", true, true, true, "cpsi", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Wall Thickness", "GeneralInformation", "wall_thickness", null, "GeneralInformation.wall_thickness", true, true, true, "mil", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Substrate Boundary Shape", "GeneralInformation", "shape_id", null, "GeneralInformation.shape_id", false, false, true, null, null, null, false, true, false, "Id", "Shape", arrBoundaryShapes));
            forGeneralInformation.Add(new CustomComboBoxItem("Volume", "GeneralInformation", "volume", null, "GeneralInformation.volume", true, true, true, "liter", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Monolith Length", "GeneralInformation", "length", null, "GeneralInformation.length", true, true, true, "mm", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Monolith Diameter", "GeneralInformation", "diameter", null, "GeneralInformation.diameter", true, true, true, "mm", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Monolith Material", "GeneralInformation", "monolith_material_id", null, "GeneralInformation.monolith_material_id",  false, false, true, null, null, null, false, true, false, "Id", "Material", arrMonolithMaterials));
            forGeneralInformation.Add(new CustomComboBoxItem("Zone Coating", "GeneralInformation", "zone_coating", null, "GeneralInformation.zone_coating", false, false, true, null, null, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forGeneralInformation.Add(new CustomComboBoxItem("Slip Catalyst Applied", "GeneralInformation", "slip_catalyst_applied", null, "GeneralInformation.slip_catalyst_applied",  false, false, true, null, null, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Gradient Axial", "GeneralInformation", "max_temp_gradient_axial", null, "GeneralInformation.max_temp_gradient_axial", true, true, true, "K", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Gradient Radial", "GeneralInformation", "max_temp_gradiend_radial", null, "GeneralInformation.max_temp_gradiend_radial", true, true, true, "K", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Limit Peak", "GeneralInformation", "max_temp_limitation_peak", null, "GeneralInformation.max_temp_limitation_peak", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max Temperature Limit Long Term", "GeneralInformation", "max_temp_limitation_longterm", null, "GeneralInformation.max_temp_limitation_longterm", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Max HC Limit", "GeneralInformation", "max_hc_limit", null, "GeneralInformation.max_hc_limit",true, true, true, "g/l", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Segmentation size X", "GeneralInformation", "segmentation_size_x", null, "GeneralInformation.segmentation_size_x", true, true, true, "inch", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Segmentation size Y", "GeneralInformation", "segmentation_size_y", null, "GeneralInformation.segmentation_size_y", true, true, true, "inch", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Pressure Loss Coefficient", "GeneralInformation", "pressue_loss_coefficient", null, "GeneralInformation.pressue_loss_coefficient",  true, true, true, null, null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Soot Mass Limit", "GeneralInformation", "soot_mass_limit", null, "GeneralInformation.soot_mass_limit", true, true, true, "g/l", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("DPF Inlet Cell Area", "GeneralInformation", "dpf_inlet_cell", null, "GeneralInformation.dpf_inlet_cell",  true, true, true, "mm²", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("DPF Outlet Cell Area", "GeneralInformation", "dpf_outlet_cell", null, "GeneralInformation.dpf_outlet_cell", true, true, true, "mm²", null, null, false, false, false, null, null, null));
            forGeneralInformation.Add(new CustomComboBoxItem("Aging Status", "GeneralInformation", "aging_status_id", null, "GeneralInformation.aging_status_id", false, false, true, null, null, null, false, true, false, "Id", "Status", arrAgingStatuses));
            forGeneralInformation.Add(new CustomComboBoxItem("Aging Procedure", "GeneralInformation", "aging_procedure_id", null, "GeneralInformation.aging_procedure_id",  false, false, true, null, null, null, false, true, false, "Id", "Procedure", arrAgingProcedures));
            forGeneralInformation.Add(new CustomComboBoxItem("Aging Duration", "GeneralInformation", "aging_duration", null, "GeneralInformation.aging_duration", true, true, true, "h", null, null, false, false, false, null, null, null));

            forCharacterization = new ObservableCollection<CustomComboBoxItem>();
            forCharacterization.Add(new CustomComboBoxItem("CO Light-off Temperature/Heat-up", "CatalystCharacterisation", "co_light_off_temp_heat_up", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.co_light_off_temp_heat_up", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("CO Light-off Temperature/Cool-down", "CatalystCharacterisation", "co_light_off_temp_cool_down", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.co_light_off_temp_cool_down", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max CO conversion efficiency", "CatalystCharacterisation", "co_max_conversion_efficiency", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.co_max_conversion_efficiency", true, true, true, "%", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max CO efficiency temperature", "CatalystCharacterisation", "co_max_efficiency_temperature", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.co_max_efficiency_temperature", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("C3H6 Light-off Temperature/Heat-up", "CatalystCharacterisation", "c3h6_light_off_temp_heat_up", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h6_light_off_temp_heat_up", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("C3H6 Light-off Temperature/Cool-down", "CatalystCharacterisation", "c3h6_light_off_temp_cool_down", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h6_light_off_temp_cool_down", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max C3H6 conversion efficiency", "CatalystCharacterisation", "c3h6_max_conversion_efficiency", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h6_max_conversion_efficiency", true, true, true, "%", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max C3H6 efficiency temperature", "CatalystCharacterisation", "c3h6_max_efficiency_temperature", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h6_max_efficiency_temperature", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("C3H8 Light-off Temperature/Heat-up", "CatalystCharacterisation", "c3h8_light_off_temp_heat_up", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h8_light_off_temp_heat_up", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("C3H8 Light-off Temperature/Cool-down", "CatalystCharacterisation", "c3h8_light_off_temp_cool_down", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h8_light_off_temp_cool_down", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max C3H8 conversion efficiency", "CatalystCharacterisation", "c3h8_max_conversion_efficiency", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h8_max_conversion_efficiency", true, true, true, "%", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max C3H8 efficiency temperature", "CatalystCharacterisation", "c3h8_max_efficiency_temperature", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.c3h8_max_efficiency_temperature", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max NOX conversion efficiency", "CatalystCharacterisation", "Nox_max_conversion_efficiency", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.Nox_max_conversion_efficiency", true, true, true, "%", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max NOX efficiency temperature", "CatalystCharacterisation", "Nox_max_efficiency_temperature", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.Nox_max_efficiency_temperature", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("NO Light-off Temperature/Heat-up", "CatalystCharacterisation", "no_light_off_temp_heat_up", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.no_light_off_temp_heat_up", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("NO Light-off Temperature/Cool-down", "CatalystCharacterisation", "no_light_off_temp_cool_down", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id",  "CatalystCharacterisation.no_light_off_temp_cool_down", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max NO to NO2 conversion efficiency", "CatalystCharacterisation", "no_no2_max_conversion_efficiency", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.no_no2_max_conversion_efficiency", true, true, true, "%", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max NO to NO2 efficiency temperature", "CatalystCharacterisation", "no_no2_max_efficiency_temperature", "join catalystcharacterisation on catalystcharacterisation.lgb_id = lgb.lgb_id", "CatalystCharacterisation.no_no2_max_efficiency_temperature", true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Maximum Oxygen storage capacity", "LGB", "max_o2_storage_capacity", null, "LGB.max_o2_storage_capacity",  true, true, false, null, "max_o2_unit_id", arrMaxO2, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Temperature at maximum Oxygen storage capacity", "LGB", "max_o2_storage_capacity_temp", null, "LGB.max_o2_storage_capacity_temp",  true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Maximum NOx storage capacity", "LGB", "max_nsc_storage_capacity", null, "LGB.max_nsc_storage_capacity", true, true, false, null, "max_nsc_unit_id", arrMaxNSC, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Temperature at maximum NOx storage capacity",  "LGB", "max_nsc_storage_capacity_temp", null, "LGB.max_nsc_storage_capacity_temp",  true, true, true, "°C", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Max ammonia storage capacity", "LGB", "max_ammonia_storage_capacity", null, "LGB.max_ammonia_storage_capacity", true, true, false, null, "max_ammonia_unit_id", arrMaxAmmonia, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Space velocity of performed test ",  "LGB", "space_velocity_performed_test", null, "LGB.space_velocity_performed_test", true, true, true, "1/h", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Space velocity of light-off test",  "LGB", "space_velocity_lightoff_test", null, "LGB.space_velocity_lightoff_test",  true, true, true, "1/h", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Space velocity of NOx capacity test",  "LGB", "space_velocity_nox_capacity_test", null, "LGB.space_velocity_nox_capacity_test", true, true, true, "1/h", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Space velocity of o2 capacity test", "LGB", "space_velocity_o2_capacity_test", null, "LGB.space_velocity_o2_capacity_test", true, true, true, "1/h", null, null, false, false, false, null, null, null));
            forCharacterization.Add(new CustomComboBoxItem("Soot Loading",  "LGB", "soot_loading", null, "LGB.soot_loading", true, true, true, "g/l", null, null, false, false, false, null, null, null));

            forSimulation = new ObservableCollection<CustomComboBoxItem>();
            forSimulation.Add(new CustomComboBoxItem("Model Type",  "Simulation", "model_type_id", null, "Simulation.model_type_id", false, false, true, null, null, null, false, true, false, "Id", "Type", arrModelTypes));
            forSimulation.Add(new CustomComboBoxItem("Model Version", "Simulation", "model_version", null, "Simulation.model_version", true, false, true, null, null, null, false, false, false, null, null, null));
            forSimulation.Add(new CustomComboBoxItem("Simulation Tool", "Simulation", "tool_id", null, "Simulation.tool_id",  false, false, true, null, null, null, false, true, false, "Id", "Tool", arrSimulationTools));
            forSimulation.Add(new CustomComboBoxItem("Source Of Measurement", "Simulation", "src_measurement_id", null, "Simulation.src_measurement_id", false, false, true, null, null, null, false, true, false, "Id", "Source", arrSourceOfMeasurements));
            forSimulation.Add(new CustomComboBoxItem("Source Of Data", "Simulation", "src_data_id", null, "Simulation.src_data_id", false, false, true, null, null, null, false, true, false, "Id", "Source", arrSourceOfDatas));
            forSimulation.Add(new CustomComboBoxItem("Combined Catalyst ID", "CombinedCatalyst", "catalyst_id", "join CombinedCatalyst on CombinedCatalyst.simulation_id = Simulation.simulation_id ", "CombinedCatalyst.catalyst_id", true, true, true, null, null, null, false, false, false, null, null, null));

            forTestbenchandVehicle = new ObservableCollection<CustomComboBoxItem>();
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Engine/Vehicle Manufacturer", "TestbenchAndVehicle", "engine_manufacturer", null, "TestbenchAndVehicle.engine_manufacturer", true, false, true, null, null, null, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Application", "TestbenchAndVehicle", "application_field",null, "TestbenchAndVehicle.application_field", false, false, true, null, null, null, false, true, false, "Id", "Name", arrApplicationFields));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Application Field", "TestbenchAndVehicle", "app_field_id", "join TestBenchApp on TestBenchApp.testbench_id = TestbenchAndVehicle.testbench_id", "TestBenchApp.app_field_id",  true, false, true, null, null, null, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Emission Legislation", "TestbenchAndVehicle", "emission_legislation", null, "TestbenchAndVehicle.emission_legislation", true, false, true, null, null, null, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Engine Displacement", "TestbenchAndVehicle", "engine_displacement", null, "TestbenchAndVehicle.engine_displacement", true, true, true, "Liter", null, null, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Engine Power", "TestbenchAndVehicle", "engine_power", null, "TestbenchAndVehicle.engine_power", true, true, true, "kW", null, null, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Number of Cylinders", "TestbenchAndVehicle", "number_of_cylinders", null, "TestbenchAndVehicle.number_of_cylinders", true, true, true, null, null, null, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("EATS Setup", "TestbenchAndVehicle", "eats_setup", null, "TestbenchAndVehicle.eats_setup", true, false, false, null, null, null, false, false, true, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("Comments on ECU labels", "TestbenchAndVehicle", "comment_on_ecu_labels", null, "TestbenchAndVehicle.comment_on_ecu_labels", true, false, true, null, null, null, false, false, false, null, null, null));
           
            
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 3 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 3 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHTC PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 1 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 3 and TestbenchAndTransient.transient_id =2 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 3 and TestbenchAndTransient.transient_id =2 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NRTC PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 2 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 3 and TestbenchAndTransient.transient_id =3 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WLTP PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 3 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 3 and TestbenchAndTransient.transient_id =4 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("10_15 PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 4 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ETC PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 5 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 6and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("FTP_75 PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 6 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JC08 PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 7 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("JE05 PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 8 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("SC03 PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 9 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("USSC PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 10 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US06 PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 11 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 1 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 2 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id =3 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 4 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.raw ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("NEDC PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 12 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway PM Tailpipe", " TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Highway PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 13 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient HC Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient HC Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient CO Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient CO Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient NOx Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient NOx Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient PM Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient PM Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient PN Raw", "TestbenchAndTransient", "raw", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("US_Transient PN Tailpipe", "TestbenchAndTransient", "tailpipe", "join TestbenchAndTransient on TestbenchAndTransient.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndTransient.emission_id = 5 and TestbenchAndTransient.transient_id = 14 and TestbenchAndTransient.tailpipe ", true, true, false, null, "transient_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC HC Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 1 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.raw ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC HC Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 1 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.tailpipe ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC CO Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 2 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.raw ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC CO Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 2 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.tailpipe ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC NOx Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id =3 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.raw ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC NOx Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id =3 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.tailpipe ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC PM Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 4 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.raw ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC PM Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 4 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.tailpipe ", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC PN Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 5 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.raw ", true, true, false, null, "steady_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("WHSC PN Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 5 and TestbenchAndSteady.steady_state_id = 1 and TestbenchAndSteady.tailpipe ", true, true, false, null, "steady_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC HC Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 1 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC HC Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 1 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC CO Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 2 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC CO Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 2 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC NOx Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id =3 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC NOx Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id =3 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC PM Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 4 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC PM Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 4 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC PN Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 5 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ESC PN Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 5 and TestbenchAndSteady.steady_state_id = 2 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrOtherCycle, false, false, false, null, null, null));

            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 HC Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 1 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 HC Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 1 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 CO Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 2 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 CO Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 2 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 NOx Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id =3 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 NOx Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id =3 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 PM Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 4 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 PM Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 4 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 PN Raw", "TestbenchAndSteady", "raw", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 5 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.raw", true, true, false, null, "steady_unit", arrOtherCycle, false, false, false, null, null, null));
            forTestbenchandVehicle.Add(new CustomComboBoxItem("ISO08178 PN Tailpipe", "TestbenchAndSteady", "tailpipe", "join TestbenchAndSteady on TestbenchAndSteady.testbench_id = TestbenchAndVehicle.testbench_id", "TestbenchAndSteady.emission_id = 5 and TestbenchAndSteady.steady_state_id = 3 and TestbenchAndSteady.tailpipe", true, true, false, null, "steady_unit", arrOtherCycle, false, false, false, null, null, null));

            forChemPhys = new ObservableCollection<CustomComboBoxItem>();
            forChemPhys.Add(new CustomComboBoxItem("Washcoat Elemental Composition", "WashcoatElemental", "washcoat_composition", "join WashcoatElemental on WashcoatElemental.analysis_id = ChemPhysAnalysis.analysis_id", "WashcoatElemental.washcoat_composition ", true, false, false, null, null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Waschoat Elemental Composition Value", "WashcoatElemental", "waschoat_composition_value", "join WashcoatElemental on WashcoatElemental.analysis_id = ChemPhysAnalysis.analysis_id", " WashcoatElemental.washcoat_composition_value", true, true, false, "Liter", null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Brick Substrate" , "ChemPhysAnalysis", "brick_substrate", null, "ChemPhysAnalysis.brick_substrate", true, false, false, null, null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Washcoat Substrate Composition", "ChemPhysAnalysis", "washcoat_substrate", null, "ChemPhysAnalysis.washcoat_substrate", true, false, false, null, null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Pt Loading Value", "Loading", "loading_value_pt", "join Loading on Loading.analysis_id = ChemPhysAnalysis.analysis_id", "Loading.precious_metal_loading_id = 1 and  Loading.loading_value", true, true, false, null, "precious_metal_loading_unit_id", arrUnitLoading, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Pd Loading Value", "Loading", "loading_value_pd", "join Loading on Loading.analysis_id = ChemPhysAnalysis.analysis_id", "Loading.precious_metal_loading_id = 2 and  Loading.loading_value", true, true, false, null, "precious_metal_loading_unit_id", arrUnitLoading, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Rh Loading Value", "WashcoatElemental", "loading_value_rh", "join Loading on Loading.analysis_id = ChemPhysAnalysis.analysis_id", "Loading.precious_metal_loading_id = 3 and  Loading.loading_value", true, true, false, null, "precious_metal_loading_unit_id", arrUnitLoading, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Au Loading Value", "WashcoatElemental", "loading_value_au", "join Loading on Loading.analysis_id = ChemPhysAnalysis.analysis_id", "Loading.precious_metal_loading_id = 4 and  Loading.loading_value", true, true, false, null, "precious_metal_loading_unit_id", arrUnitLoading, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Ag Loading Value", "WashcoatElemental", "loading_value_ag", "join Loading on Loading.analysis_id = ChemPhysAnalysis.analysis_id", "Loading.precious_metal_loading_id = 5 and  Loading.loading_value", true, true, false, null, "precious_metal_loading_unit_id", arrUnitLoading, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Support Material Loading", "SupportMaterial", "support_material_loading ", "join SupportMaterial on SupportMaterial.analysis_id = ChemPhysAnalysis.analysis_id", "SupportMaterial.support_material_loading", true, false, false, null, null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Support Material Loading Value", "SupportMaterial", "support_material_loading_value ", "join SupportMaterial on SupportMaterial.analysis_id = ChemPhysAnalysis.analysis_id", "SupportMaterial.support_material_loading_value", true, false, false, null, null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("SSA Specific Surface Area", "ChemPhysAnalysis", "ssa_specific_surface_area", null, "ChemPhysAnalysis.ssa_specific_surface_area", true, true, false, "m²/g", null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Makroporosity (DPF-Porosity) total porosity", "ChemPhysAnalysis", "makroporosity_total_porosity", null, "ChemPhysAnalysis.makroporosity_total_porosity", true, true, false, "%", null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Makroporosity Average pore radius", "ChemPhysAnalysis", "makroporosity_average_pore_radius", null, "ChemPhysAnalysis.makroporosity_average_pore_radius", true, true, false, "µm", null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Cristalline Washcoat Component Function", "ChemPhysAnalysis", "cristalline_function_id", null, "ChemPhysAnalysis.cristalline_function_id", false, false, false, null, null, null, false, true, false, "Id", "Function", arrCristallineWashcoatComponentFunctions));
            forChemPhys.Add(new CustomComboBoxItem("Cristalline Washcoat Components", "ChemPhysAnalysis", "cristalline_washcoat_components", null, "ChemPhysAnalysis.cristalline_washcoat_components", true, false, false, null, null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Cristalline Washcoat Zeolith", "ChemPhysAnalysis", "cristalline_washcoat_zeolith", null, "ChemPhysAnalysis.cristalline_washcoat_zeolith",  false, false, false, null, null, null, false, true, false, "BoxValue", "BoxValue", arrCheckBox));
            forChemPhys.Add(new CustomComboBoxItem("Zone Coating (Number ofZones)", "ChemPhysAnalysis", "zone_coating", null, "ChemPhysAnalysis.zone_coating",  false, false, false, null, null, null, false, true, false, "Id", "Id", arrZone));
            forChemPhys.Add(new CustomComboBoxItem("Cristalline Washcoat Zeolith",  "ChemPhysAnalysis", "layer", null, "ChemPhysAnalysis.layer", false, false, false, null, null, null, false, true, false, "Id", "Id", arrLayer));
            forChemPhys.Add(new CustomComboBoxItem("Dispersity", "ChemPhysAnalysis", "dispersity", null, "ChemPhysAnalysis.dispersity", true, true, false, "nm", null, null, false, false, false, null, null, null));
            forChemPhys.Add(new CustomComboBoxItem("Heat Capacity (at 500 C)", "ChemPhysAnalysis", "heat_capacity", null, "ChemPhysAnalysis.heat_capacity",true, true, false, "J/gK", null, null, false, false, false, null, null, null));
        }
        public void unregisterOldSelectedItem(CustomComboBoxItem previousItem){
            if (previousItem != null)
            {
                if (previousItem.IsTextBox)
                {
                    if (previousItem.IsNumber)
                    {
                        string comboName = "cmb_" + previousItem.FieldName;
                        //register
                        string textName = "txb__" + previousItem.FieldName;
                        //register
                        grid_search.UnregisterName(comboName);
                        grid_search.UnregisterName(textName);
                    }

                    string text1Name = "txb_" + previousItem.FieldName;
                    grid_search.UnregisterName(text1Name);

                    if (previousItem.UnitIsConvertible == false && previousItem.UnitCollection != null)
                    {
                        string comboName = "unit_" + previousItem.FieldName;
                        grid_search.UnregisterName(comboName);
                    }
                }
                if (previousItem.IsComboBox)
                {
                    string comboName = "cmb_" + previousItem.FieldName;
                    grid_search.UnregisterName(comboName);
                }
                if (previousItem.IsDateTimePicker)
                {
                    string dt = "dt_" + previousChosen.FieldName;
                    grid_search.UnregisterName(dt);
                }
            }
        }

        private void cmb_tab_fields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unregisterOldSelectedItem(previousChosen);
            int cmb_current_row = Grid.GetRow((CustomComboBox)sender);
            int cmb_current_column = 4;
            ifExistThenDelete(cmb_current_row);
            CustomComboBox currentFields = (CustomComboBox)sender;
            CustomComboBoxItem item = (CustomComboBoxItem)currentFields.SelectedItem;
            if(item != null)
            { 
            if (item.IsTextBox)
            {
                if (item.IsNumber)
                {
                    CustomComboBox cmb = new CustomComboBox();
                    cmb.Width = 200;
                    cmb.Margin = new Thickness(2);
                    cmb.Name = "cmb_" + item.FieldName;
                    //register
                    grid_search.RegisterName(cmb.Name, cmb);
                    Grid.SetRow(cmb, cmb_current_row);
                    Grid.SetColumn(cmb, cmb_current_column);
                    cmb.SelectionChanged += setSecondDisabled;

                    ComboBoxItem c = new ComboBoxItem();
                    c.Content = " >= ";
                    ComboBoxItem c1 = new ComboBoxItem();
                    c1.Content = " <=";
                    ComboBoxItem c2 = new ComboBoxItem();
                    c2.Content = "Between";
                    cmb.Items.Add(c);
                    cmb.Items.Add(c1);
                    cmb.Items.Add(c2);
                    grid_search.Children.Add(cmb);
                    cmb_current_column ++;

                    CustomTextBox cTextBox = new CustomTextBox();
                    cTextBox.Name = "txb__" + item.FieldName;
                    //register
                    grid_search.RegisterName(cTextBox.Name, cTextBox);
                    cTextBox.Width = 150;
                    cTextBox.Margin = new Thickness(2);
                    Grid.SetRow(cTextBox, cmb_current_row);
                    Grid.SetColumn(cTextBox, cmb_current_column);
                    grid_search.Children.Add(cTextBox);
                    cmb_current_column++;
                }

                CustomTextBox cTextBox1 = new CustomTextBox();
                cTextBox1.Name = "txb_" + item.FieldName;
                //register
                grid_search.RegisterName(cTextBox1.Name, cTextBox1);
                cTextBox1.Width = 200;
                cTextBox1.Margin = new Thickness(2);
                Grid.SetRow(cTextBox1, cmb_current_row);
                Grid.SetColumn(cTextBox1, cmb_current_column);
                grid_search.Children.Add(cTextBox1);
                cmb_current_column++;

                if (item.UnitIsConvertible == true &&item.Unit != null)
                {
                    Label l = new Label();
                    l.Content = item.Unit;
                    Grid.SetRow(l, cmb_current_row);
                    Grid.SetColumn(l, cmb_current_column);
                    grid_search.Children.Add(l);
                    cmb_current_column++;
                }
                if (item.UnitIsConvertible == false && item.UnitCollection != null) {
                    CustomComboBox cmb = new CustomComboBox();
                    cmb.Name = "unit_" + item.FieldName;
                    //register
                    grid_search.RegisterName(cmb.Name, cmb);
                    cmb.ItemsSource = item.UnitCollection;
                    cmb.SelectedValuePath = "Id";
                    cmb.DisplayMemberPath = "Unit";
                    Grid.SetColumn(cmb, cmb_current_column);
                    Grid.SetRow(cmb, cmb_current_row);
                    grid_search.Children.Add(cmb);
                    cmb_current_column++;
                }
            }
            if (item.IsComboBox)
            {
                CustomComboBox cmb = new CustomComboBox();
                cmb.Width = 200;
                cmb.Margin = new Thickness(2);
                cmb.Name = "cmb_" + item.FieldName;
                grid_search.RegisterName(cmb.Name, cmb);
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
                cmb_current_column++;
            }
            if (item.IsDateTimePicker)
            {
                DatePicker d = new DatePicker();
                d.Name = "dt_" + item.FieldName;
                grid_search.RegisterName(d.Name, d);
                d.Width = 200;
                d.Margin = new Thickness(2);
                Grid.SetRow(d, cmb_current_row);
                Grid.SetColumn(d, cmb_current_column);
                grid_search.Children.Add(d);               
            }
            }
            previousChosen = item;
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

        private void setSecondDisabled(object sender, SelectionChangedEventArgs e) {
            CustomComboBox c = (CustomComboBox)sender;
            if (c.SelectedItem.ToString().Contains("Between"))
            {
                string fieldname = "txb_" + c.Name.Substring(4);
                CustomTextBox t = (CustomTextBox)grid_search.FindName(fieldname);
                t.IsEnabled = true;
            }
            else
            {
                string fieldname = "txb_" + c.Name.Substring(4);
                CustomTextBox t = (CustomTextBox)grid_search.FindName(fieldname);
                t.IsEnabled = false;
            }

        }

        private void measure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmb_tab_names_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {             
            ComboBox comboBoxSrc = (ComboBox)sender;
            int cmb_current_row = Grid.GetRow((ComboBox)sender); 
            if (comboBoxSrc.Name.EndsWith("1"))
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
            if (comboBoxSrc.Name.EndsWith("2"))
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
            if (comboBoxSrc.Name.EndsWith("3"))
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
            if (comboBoxSrc.Name.EndsWith("4"))
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
            if (comboBoxSrc.Name.EndsWith("5"))
            {
                if (cmb_tab_names5.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields5.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names5.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields5.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names5.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields5.ItemsSource = forSimulation;
                }
                if (cmb_tab_names5.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields5.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names5.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields5.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.EndsWith("6"))
            {
                if (cmb_tab_names6.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields6.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names6.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields6.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names6.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields6.ItemsSource = forSimulation;
                }
                if (cmb_tab_names6.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields6.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names6.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields6.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.EndsWith("7"))
            {
                if (cmb_tab_names7.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields7.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names7.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields7.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names7.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields7.ItemsSource = forSimulation;
                }
                if (cmb_tab_names7.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields7.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names7.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields7.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.EndsWith("8"))
            {
                if (cmb_tab_names8.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields8.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names8.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields8.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names8.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields8.ItemsSource = forSimulation;
                }
                if (cmb_tab_names8.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields8.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names8.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields8.ItemsSource = forChemPhys;
                }
            }
            if (comboBoxSrc.Name.EndsWith("9"))
            {
                if (cmb_tab_names9.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields9.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names9.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields9.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names9.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields9.ItemsSource = forSimulation;
                }
                if (cmb_tab_names9.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields9.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names9.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields9.ItemsSource = forChemPhys;
                }
            }
            if(comboBoxSrc.Name.EndsWith("10")) 
            {
                if (cmb_tab_names10.SelectedValue.Equals("General Information"))
                {
                    cmb_tab_fields10.ItemsSource = forGeneralInformation;
                }
                if (cmb_tab_names10.SelectedValue.Equals("Catalyst Characterization"))
                {
                    cmb_tab_fields10.ItemsSource = forCharacterization;
                }
                if (cmb_tab_names10.SelectedValue.Equals("Simulation"))
                {
                    cmb_tab_fields10.ItemsSource = forSimulation;
                }
                if (cmb_tab_names10.SelectedValue.Equals("Testbench and Vehicle/Engine"))
                {
                    cmb_tab_fields10.ItemsSource = forTestbenchandVehicle;
                }
                if (cmb_tab_names10.SelectedValue.Equals("Chem./Phys Analysis"))
                {
                    cmb_tab_fields10.ItemsSource = forChemPhys;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            searchParamters = new ObservableCollection<CustomComboBoxItem>();
            if (cmb_tab_fields1.SelectedIndex > 0) {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields1.SelectedItem);
            }
            if (cmb_tab_fields2.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields2.SelectedItem);
            }
            if (cmb_tab_fields3.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields3.SelectedItem);
            }
            if (cmb_tab_fields4.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields4.SelectedItem);
            }
            if (cmb_tab_fields5.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields5.SelectedItem);
            }
            if (cmb_tab_fields6.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields6.SelectedItem);
            }
            if (cmb_tab_fields7.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields7.SelectedItem);
            }
            if (cmb_tab_fields8.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields8.SelectedItem);
            }
            if (cmb_tab_fields9.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields9.SelectedItem);
            }
            if (cmb_tab_fields10.SelectedIndex > 0)
            {
                searchParamters.Add((CustomComboBoxItem)cmb_tab_fields10.SelectedItem);
            }
            Search s = new Search();
            string query = "select catalyst.catalyst_id,"
                                            + " catalysttype.catalyst_type_name, "
                                            + " generalinformation.target_conf_system, "
                                            + " generalinformation.volume, "
                                            + " monolithmaterial.monolith_material,"
                                            + " agingstatus.aging_status, "
                                            + " testbenchandvehicle.engine_displacement,"
                                            + " testbenchandvehicle.engine_power"
                                            + " from catalyst"
                                            + " join catalysttype on catalysttype.catalyst_type_id = catalyst.catalyst_type_id"
                                            + " join generalinformation on generalinformation.catalyst_id = catalyst.catalyst_id"
                                            + " join agingstatus on agingstatus.aging_status_id = generalinformation.aging_status_id"
                                            + " join monolithmaterial on monolithmaterial.monolith_material_id = generalinformation.monolith_material_id"
                                            + " join lgb on lgb.catalyst_id = catalyst.catalyst_id"
                                            + " join testbenchandvehicle on testbenchandvehicle.catalyst_id = catalyst.catalyst_id"
                                            + " join simulation on simulation.catalyst_id = catalyst.catalyst_id"
                                            + " join chemphysanalysis on chemphysanalysis.catalyst_id = catalyst.catalyst_id ";
            for (int i = 0; i < searchParamters.Count(); i++) {
                if (searchParamters[i].JoinQuery != null) {
                    query = query + searchParamters[i].JoinQuery + " ";
                }
            }
            for (int i = 0; i < searchParamters.Count(); i++) {
                
                string searchQuery = "" ;
                if (searchParamters[i].IsTextBox && searchParamters[i].IsNumber) {
                    MessageBox.Show("get result : cmb_" + searchParamters[i].FieldName);
                    CustomComboBox comboBox = (CustomComboBox)grid_search.FindName("cmb_" + searchParamters[i].FieldName);
                    
                    if (comboBox!=null && !comboBox.Text.Equals("")) {
                        string search_t = comboBox.Text;
                        if (search_t.Equals("Between"))
                        {
                            CustomTextBox first = (CustomTextBox)grid_search.FindName("txb__" + searchParamters[i].FieldName);
                            CustomTextBox second = (CustomTextBox)grid_search.FindName("txb_" + searchParamters[i].FieldName);
                            if (first != null && second != null) {
                                if (!first.Equals("") && !second.Equals(""))
                                {
                                    MessageBox.Show(searchParamters[i].WhereClause);
                                    searchQuery = searchParamters[i].WhereClause;
                                    searchQuery = searchQuery + " between " + first.Text + " and " + second.Text + " ";
                                }
                            }
                        }
                        else
                        {
                            CustomTextBox second = (CustomTextBox)grid_search.FindName("txb_" + searchParamters[i].FieldName);
                            if (second != null) {
                                if (!second.Text.Equals(""))
                                {
                                    MessageBox.Show(searchParamters[i].WhereClause);
                                    searchQuery = searchParamters[i].WhereClause;
                                    searchQuery = " " + search_t + " " + second.Text;
                                }
                            }
                        }
                    }
                }
                if (searchParamters[i].IsTextBox && searchParamters[i].IsNumber == false) {
                    CustomTextBox textBox = (CustomTextBox)grid_search.FindName("txb_" + searchParamters[i].FieldName);
                    if (textBox != null && !textBox.Text.Equals("")) {
                        MessageBox.Show(searchParamters[i].WhereClause);
                        searchQuery = searchParamters[i].WhereClause;
                        searchQuery = searchQuery + " like " + "'%" +textBox.Text + "%'";
                    }
                }
                if (searchParamters[i].IsComboBox) {
                    CustomComboBox comboBox = (CustomComboBox)grid_search.FindName("cmb_" + searchParamters[i].FieldName);                   
                    if (comboBox!=null && !comboBox.Text.Equals("")) {
                        MessageBox.Show(searchParamters[i].WhereClause);
                        searchQuery = searchParamters[i].WhereClause;
                        searchQuery = searchQuery + "="+comboBox.SelectedValue;
                    }
                }
                if (searchParamters[i].IsDateTimePicker) {
                    DatePicker d = (DatePicker)grid_search.FindName("dt_" + searchParamters[i].FieldName);
                    if (d!=null && !d.Text.Equals("")) {
                        searchQuery = searchParamters[i].WhereClause;
                        searchQuery = searchQuery + "=" +  "'" + d.Text+"'";
                    }
                }
                if (i != searchParamters.Count() - 1 && !searchQuery.Equals(""))
                {
                    searchQuery = searchQuery + " or ";
                }
                if (i == 0 && !searchQuery.Equals(""))
                {
                    query = query + " where ";
                    query = query + searchQuery;
                }
                else if(i!=0){
                    query = query + searchQuery;
                }
            }
            MessageBox.Show(query);
            datagrid_result.ItemsSource = s.getSearchResult(8, query);
        }

        private void datagrid_result_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (grid.SelectedItem != null) {
                SearchResult s = (SearchResult)grid.SelectedItem;
                MainApplication m = new MainApplication();
                m.Show();
                m.OpenCatalyst(Convert.ToInt32(s.CatalystID));
            }       
        }

        public ObservableCollection<CustomComboBoxItem> putJoinBeforeWhere(ObservableCollection<CustomComboBoxItem> list) {
            ObservableCollection<CustomComboBoxItem> newList = new ObservableCollection<CustomComboBoxItem>();
            for (int i = 0; i < list.Count(); i++) {
                if (list[i].JoinQuery != null) {
                    newList.Add(list[i]);
                    list.RemoveAt(i);
                }
            }
            newList.Concat(list);
            return newList;
        }
    }
}
