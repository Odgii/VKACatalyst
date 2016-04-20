using catalyst_project.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalyst_project.Model
{
    class SearchResult
    {
        private string _catalyst_id;
        private string _catalyst_type;
        private string _target_conf_system;
        private string _volume;
        private string _monolith_material;
        private string _aging_status;
        private string _engine_displacement;
        private string _engine_power;

        public SearchResult() { 
        }

        public SearchResult( string catalyst_id, string catalyst_type_name, string target_conf_system, string volume, string monolith_material, string aging_status, string engine_displacement,string engine_power) {
            CatalystID = catalyst_id;
            CatalystTypeName = catalyst_type_name;
            TargetConfSystem = target_conf_system;
            Volume = volume;
            MonolithMaterial = monolith_material;
            AgingStatus = aging_status;
            EngineDisplacement = engine_displacement;
            EnginePower = engine_power;
        }

        public string CatalystID {
            get {
                return _catalyst_id;
            }
            set
            {
                _catalyst_id = value;
            }
        }
        
        public string CatalystTypeName {
            get
            {
                return _catalyst_type;
            }
            set
            {
                _catalyst_type = value;
            }
        
        }
        
        public string TargetConfSystem {
            get
            {
                return _target_conf_system;
            }
            set
            {
                _target_conf_system = value;
            }
        
        }
        
        public string Volume {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
            }
        }
        
        public string MonolithMaterial {
            get
            {
                return _monolith_material;
            }
            set
            {
                _monolith_material = value;
            }
        }
        
        public string AgingStatus {
            get
            {
                return _aging_status;
            }
            set
            {
                _aging_status = value;
            }
        }
        
        public string EngineDisplacement {
            get
            {
                return _engine_displacement;
            }
            set
            {
                _engine_displacement = value;
            }
        }

        public string EnginePower {
            get
            {
                return _engine_power;
            }
            set
            {
                _engine_power = value;
            }
        }
    }
}
