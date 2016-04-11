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

        SqliteDBConnection database;

        public PopulateDropdownsFromDB() {
            database = new SqliteDBConnection();
         //   SqliteDBConnection s = new SqliteDBConnection();
        }
        public ObservableCollection<Manufacturer> populateManufacturers() 
        {
            ObservableCollection<Manufacturer> manufacturers = new ObservableCollection<Manufacturer>();
            List<string>[] list = database.Select("select * from catalystmanufacturer ");
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
            List<string>[] list = database.Select("select * from washcoatmaterial ");
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
            List<string>[] list = database.Select("select * from monolithmaterial");
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
            List<string>[] list = database.Select( "select * from catalysttype");
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
            List<string>[] list = database.Select("select * from agingprocedure");
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
            List<string>[] list = database.Select("select * from agingstatus");
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
            List<string>[] list = database.Select("select * from substrateboundaryshape");
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
            List<string>[] list = database.Select("select * from modeltype");
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
            List<string>[] list = database.Select("select * from simulationtool");
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
            List<string>[] list = database.Select("select * from steadystatelegislationcycle");
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
            List<string>[] list = database.Select("select * from transientlegislationcycle");
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
            List<string>[] list = database.Select("select * from emissionlegislation");
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
            List<string>[] list = database.Select("select * from preciousmetalloading");
         
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
            List<string>[] list = database.Select("select * from cristallinewashcoatcomponentfunction");
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
            List<string>[] list = database.Select( "select * from applicationfield");
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
            List<string>[] list = database.Select("select * from SourceData");
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
            List<string>[] list = database.Select("select * from sourceMeasurement");
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
            List<string>[] list = database.Select("select * from userrole");
            for (int i = 0; i < list[0].Count(); i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string role = list[1][i];
                UserRole r = new UserRole(id, role);
                roles.Add(r);
            }
            return roles;
        }

        public ObservableCollection<UnitPreciousMetalLoading> populateUnitPreciousLoading()
        {
            ObservableCollection<UnitPreciousMetalLoading> units = new ObservableCollection<UnitPreciousMetalLoading>();
            List<string>[] list = database.Select("select * from unitpreciousmetalloading");
            for (int i = 0; i < list[0].Count; i++) {
                int id = Convert.ToInt32(list[0][i]);
                string unit = list[1][i];
                UnitPreciousMetalLoading u = new UnitPreciousMetalLoading(id, unit);
                units.Add(u);     
            }
            return units;
        }

        public ObservableCollection<UnitMaxAmmonia> populateUnitMaxAmmonia()
        {
            ObservableCollection<UnitMaxAmmonia> units = new ObservableCollection<UnitMaxAmmonia>();
            List<string>[] list = database.Select("select * from unitmaxammonia");
            for (int i = 0; i < list[0].Count; i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string unit = list[1][i];
                UnitMaxAmmonia u = new UnitMaxAmmonia(id, unit);
                units.Add(u);
            }
            return units;
        }

        public ObservableCollection<UnitSupport> populateUnitSupport()
        {
            ObservableCollection<UnitSupport> units = new ObservableCollection<UnitSupport>();
            List<string>[] list = database.Select( "select * from unitsupport");
            for (int i = 0; i < list[0].Count; i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string unit = list[1][i];
                UnitSupport u = new UnitSupport(id, unit);
                units.Add(u);
            }
            return units;
        }

        public ObservableCollection<UnitMaxNOX> populateUnitMaxNox()
        {
            ObservableCollection<UnitMaxNOX> units = new ObservableCollection<UnitMaxNOX>();
            List<string>[] list = database.Select( "select * from unitmaxnox");
            for (int i = 0; i < list[0].Count; i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string unit = list[1][i];
                UnitMaxNOX u = new UnitMaxNOX(id, unit);
                units.Add(u);
            }
            return units;
        }

        public ObservableCollection<UnitMaxOSC> populateUnitMaxOsc()
        {
            ObservableCollection<UnitMaxOSC> units = new ObservableCollection<UnitMaxOSC>();
            List<string>[] list = database.Select( "select * from UnitMaxOSC");
            for (int i = 0; i < list[0].Count; i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string unit = list[1][i];
                UnitMaxOSC u = new UnitMaxOSC(id, unit);
                units.Add(u);
            }
            return units;
        }
                
        public ObservableCollection<UnitAgingDuration> populateUnitAgingDuration()
        {
            ObservableCollection<UnitAgingDuration> units = new ObservableCollection<UnitAgingDuration>();
            List<string>[] list = database.Select( "select * from unitagingduration");
            for (int i = 0; i < list[0].Count; i++)
            {
                int id = Convert.ToInt32(list[0][i]);
                string unit = list[1][i];
                UnitAgingDuration u = new UnitAgingDuration(id, unit);
                units.Add(u);
            }
            return units;
        }
       
    }
}
