using catalyst_project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using catalyst_project.Command;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Controls;
using catalyst_project.View;
namespace catalyst_project.ViewModel
{
    class MainAppViewModel
    {
        private ObservableCollection<WashcoatCatalyticComposition> _Washcoats;
        private ObservableCollection<Manufacturer> _Manufacturers;
        private ObservableCollection<MonolithMaterial> _MonolithMaterials;
        private ObservableCollection<CatalystType> _CatalystTypes;
        private ObservableCollection<AgingProcedure> _AgingProcedures;
        private ObservableCollection<BoundaryShape> _BoundaryShapes;
        
        public MainAppViewModel() 
        {

            GenerateGeneralCommand = new GenerateFieldInGeneral(this);

            _Washcoats = new ObservableCollection<WashcoatCatalyticComposition>();
            Washcoats.CollectionChanged += items_CollectionChanged;

            Washcoats.Add(new WashcoatCatalyticComposition(false, "Pt", true));
            Washcoats.Add(new WashcoatCatalyticComposition(true, "Pd", true));
            Washcoats.Add(new WashcoatCatalyticComposition(false, "Rh", false));
            Washcoats.Add(new WashcoatCatalyticComposition(false, "Ce", false));
          

            _Manufacturers = new ObservableCollection<Manufacturer>();
            Manufacturers.Add(new Manufacturer(false,1,"NGK"));
            Manufacturers.Add(new Manufacturer(false,2,"Ibiden"));
            Manufacturers.Add(new Manufacturer(true,3,"Unifrax"));
            Manufacturers.Add(new Manufacturer(false,4,"Twintec AG"));
            Manufacturers.Add(new Manufacturer(false,5, "Sumitomo Chemical"));

            _MonolithMaterials = new ObservableCollection<MonolithMaterial>();
            MonolithMaterials.Add(new MonolithMaterial(1,"Corderite"));
            MonolithMaterials.Add(new MonolithMaterial(2,"Silicon carbide (SiC)"));
            MonolithMaterials.Add(new MonolithMaterial(3,"Aluminum titanate (AT)"));
            MonolithMaterials.Add(new MonolithMaterial(4,"Metal"));
            MonolithMaterials.Add(new MonolithMaterial(5, "Vanadium oxide"));

            _CatalystTypes = new ObservableCollection<CatalystType>();
            CatalystTypes.Add(new CatalystType(1,"DOC"));
            CatalystTypes.Add(new CatalystType(2,"NSC/LNT"));
            CatalystTypes.Add(new CatalystType(3,"SCR"));
            CatalystTypes.Add(new CatalystType(4,"SDPF/SCRF"));
            CatalystTypes.Add(new CatalystType(5,"CDPF"));

            _AgingProcedures = new ObservableCollection<AgingProcedure>();
            _AgingProcedures.Add(new AgingProcedure(1, "hydrothermal"));
            _AgingProcedures.Add(new AgingProcedure(2, "TB"));

            _BoundaryShapes = new ObservableCollection<BoundaryShape>();
            _BoundaryShapes.Add(new BoundaryShape(1, "Cylinder"));
            _BoundaryShapes.Add(new BoundaryShape(2, "Half-Cylinder"));
            _BoundaryShapes.Add(new BoundaryShape(3, "Oval"));

        }


        void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {     
            if(e.OldItems != null)
                 foreach (INotifyPropertyChanged item in e.OldItems)
                 {
                     Console.WriteLine("old");
                        item.PropertyChanged -= new
                                       PropertyChangedEventHandler(item_PropertyChanged);
                 }
           
            if(e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    Console.WriteLine("new");
                    item.PropertyChanged +=
                               new PropertyChangedEventHandler(item_PropertyChanged);
                }
        }

        static void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            WashcoatCatalyticComposition c = (WashcoatCatalyticComposition)sender;
            Console.WriteLine(c._WashcoatValue);
            if (e.PropertyName == "IsChecked")
            {
            }
               
        }

        public ICommand GenerateGeneralCommand {
            get;
            private set;
        }




        public ObservableCollection<WashcoatCatalyticComposition> Washcoats
        {

            get
            {
                return _Washcoats;
            }

        }

        public ObservableCollection<Manufacturer> Manufacturers
        {
            get
            {
                return _Manufacturers;
            }
        }

        public ObservableCollection<MonolithMaterial> MonolithMaterials
        {
            get
            {
                return _MonolithMaterials;
            }
        }

        public ObservableCollection<CatalystType> CatalystTypes
        {
            get
            {
                return _CatalystTypes;
            }
        }

        public ObservableCollection<BoundaryShape> BoundaryShapes
        {
            get 
            {
                return _BoundaryShapes;
            }
        }

        public ObservableCollection<AgingProcedure> AgingProcedures
        {
            get
            {
                return _AgingProcedures;
            }
        }
    }
}
