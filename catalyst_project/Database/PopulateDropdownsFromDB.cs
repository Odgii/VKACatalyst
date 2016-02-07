using catalyst_project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Database
{
    class PopulateDropdownsFromDB
    {

        DBConnection database;

        public PopulateDropdownsFromDB() {
            database = new DBConnection();
         //   SqliteDBConnection s = new SqliteDBConnection();
        }
        public ObservableCollection<Manufacturer> populateManufacturers() 
        {
            ObservableCollection<Manufacturer> manufacturers = new ObservableCollection<Manufacturer>();
            List<string>[] list = database.Select("catalystmanufacturer", "select * from catalystmanufacturer ");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                Manufacturer m = new Manufacturer(false, id, name);
                manufacturers.Add(m);
            }
            return manufacturers;        
        }

        public ObservableCollection<WashcoatCatalyticComposition> populateWashcoats()
        {
            ObservableCollection<WashcoatCatalyticComposition> washcoats = new ObservableCollection<WashcoatCatalyticComposition>();
            List<string>[] list = database.Select("washcoat_material", "select * from washcoat_material ");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                bool needs = Convert.ToBoolean(list[2][i]);
                WashcoatCatalyticComposition w = new WashcoatCatalyticComposition(false, id, name , needs);
                washcoats.Add(w);
            }
            return washcoats;
        }
        public ObservableCollection<MonolithMaterial> populateMonolithMaterials() 
        {
            ObservableCollection<MonolithMaterial> materials = new ObservableCollection<MonolithMaterial>();
            List<string>[] list = database.Select("monolithmaterial", "select * from monolithmaterial");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                MonolithMaterial m = new MonolithMaterial(id, name, false);
                materials.Add(m);
            }

            return materials;
        }
        public ObservableCollection<CatalystType> populateCatalystTypes()
        {
            ObservableCollection<CatalystType> catalystTypes = new ObservableCollection<CatalystType>();
            List<string>[] list = database.Select("catalysttype", "select * from catalysttype");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                CatalystType type = new CatalystType(id, name, false);
                catalystTypes.Add(type);
            }
            return catalystTypes;
        }
        public ObservableCollection<AgingProcedure> populateAgingProcedures()
        {
            ObservableCollection<AgingProcedure> agingProcedures = new ObservableCollection<AgingProcedure>();
            List<string>[] list = database.Select("agingprocedure", "select * from agingprocedure");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                AgingProcedure procedure = new AgingProcedure(id, name, false);
                agingProcedures.Add(procedure);
            }
            return agingProcedures;
        }

        public ObservableCollection<AgingStatus> populateAgingStatuses()
        {
            ObservableCollection<AgingStatus> agingStatuses = new ObservableCollection<AgingStatus>();
            List<string>[] list = database.Select("AgingStatus", "select * from agingstatus");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                AgingStatus status = new AgingStatus(id, name,false);
                agingStatuses.Add(status);
            }
            return agingStatuses;
        }

        public ObservableCollection<BoundaryShape> populateBoundaryShapes() 
        {
            ObservableCollection<BoundaryShape> boundaryShapes = new ObservableCollection<BoundaryShape>();
            List<string>[] list = database.Select("substrateboundaryshape", "select * from substrateboundaryshape");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                BoundaryShape shape = new BoundaryShape(id, name,false);
                boundaryShapes.Add(shape);
            }
            return boundaryShapes;
        }
        public ObservableCollection<ModelType> populateModelTypes() 
        {
            ObservableCollection<ModelType> modelTypes = new ObservableCollection<ModelType>();
            List<string>[] list = database.Select("modeltype", "select * from modeltype");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                ModelType model = new ModelType(id, name,false);
                modelTypes.Add(model);
            }
            return modelTypes;
        }
        public ObservableCollection<SimulationTool> populateSimulationTools() 
        {
            ObservableCollection<SimulationTool> simulationTools = new ObservableCollection<SimulationTool>();
            List<string>[] list = database.Select("simulationtool", "select * from simulationtool");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                SimulationTool s = new SimulationTool(id, name,false);
                simulationTools.Add(s);
            }
            return simulationTools;
        }
        public ObservableCollection<SteadyStateLegislation> populateSteadyStateLegislations()
        {
            ObservableCollection<SteadyStateLegislation> steadyLegislations = new ObservableCollection<SteadyStateLegislation>();
            List<string>[] list = database.Select("steadystatelegislationcycle", "select * from steadystatelegislationcycle");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                SteadyStateLegislation steady = new SteadyStateLegislation(false, id, name);
                steadyLegislations.Add(steady);
            }
            return steadyLegislations;   

        }
        public ObservableCollection<TransientLegislation> populateTransientLegislations() 
        {
            ObservableCollection<TransientLegislation> transientLegislations = new ObservableCollection<TransientLegislation>();
            List<string>[] list = database.Select("transientlegislationcycle", "select * from transientlegislationcycle");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                TransientLegislation steady = new TransientLegislation(false, id, name);
                transientLegislations.Add(steady);
            }
            return transientLegislations;
        }
        public ObservableCollection<Emission> populateEmissions()
        {
            ObservableCollection<Emission> emissions = new ObservableCollection<Emission>();
            List<string>[] list = database.Select("emission", "select * from emission");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                Emission em = new Emission(id, name);
                emissions.Add(em);
            }
            return emissions;
        }

        public ObservableCollection<PreciousMetalLoading> populatePreciousMetalLoadings()
        {
            ObservableCollection<PreciousMetalLoading> preciousMetalLoadings = new ObservableCollection<PreciousMetalLoading>();
            List<string>[] list = database.Select("preciousmetalloading", "select * from preciousmetalloading");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                PreciousMetalLoading em = new PreciousMetalLoading(false, id, name);
                preciousMetalLoadings.Add(em);
            }
            return preciousMetalLoadings;
        }
        public ObservableCollection<CristallineWashcoatComponentFunction> populateCristallineWashcoatComponentFunctions()
        {
            ObservableCollection<CristallineWashcoatComponentFunction> functions = new ObservableCollection<CristallineWashcoatComponentFunction>();
            List<string>[] list = database.Select("cristallinewashcoatcomponentfunction", "select * from cristallinewashcoatcomponentfunction");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                CristallineWashcoatComponentFunction cr = new CristallineWashcoatComponentFunction(id, name, false);
                functions.Add(cr);
            }
            return functions;
        }
        public ObservableCollection<ApplicationField> populateApplicationFields()
        {
            ObservableCollection<ApplicationField> applicationFields = new ObservableCollection<ApplicationField>();
            List<string>[] list = database.Select("applicationfield", "select * from applicationfield");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                ApplicationField appField = new ApplicationField(false, id, name);
                applicationFields.Add(appField);
            }
            return applicationFields;
        }
        public ObservableCollection<SourceOfData> populateSourceOfDatas() 
        {
            ObservableCollection<SourceOfData> sourceDatas = new ObservableCollection<SourceOfData>();
            List<string>[] list = database.Select("SourceOfData", "select * from SourceOfData");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                SourceOfData src = new SourceOfData(id, name, false);
                sourceDatas.Add(src);
            }
            return sourceDatas;
        }
        public ObservableCollection<SourceOfMeasurement> populateSourceOfMeasurements() 
        {
            ObservableCollection<SourceOfMeasurement> sourceOfMeasurements = new ObservableCollection<SourceOfMeasurement>();
            List<string>[] list = database.Select("sourceofmeasurement", "select * from sourceOfMeasurement");
            for (int i = 0; i < list[0].Count(); i++)
            {
                string idstring = list[0][i];
                int id = Convert.ToInt32(idstring);
                string name = list[1][i];
                SourceOfMeasurement src = new SourceOfMeasurement(id, name, false);
                sourceOfMeasurements.Add(src);
            }
            return sourceOfMeasurements;
        }

        public ObservableCollection<UserRole> populateUserRoles()
        {
            ObservableCollection<UserRole> roles = new ObservableCollection<UserRole>();
            List<string>[] list = database.Select("user_role", "select * from user_role");
            for (int i = 0; i < list[0].Count(); i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string role = list[1][i];
                UserRole r = new UserRole(id, role);
                roles.Add(r);
            }
            return roles;
        }
    }
}
