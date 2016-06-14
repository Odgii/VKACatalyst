using catalyst_project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace catalyst_project.Database
{
    class Search
    {
        SqliteDBConnection db;

        public Search()
        {
            db = new SqliteDBConnection();
        }

        public ObservableCollection<SearchResult> getSearchResult(int num_param, string query)
        {
            ObservableCollection<SearchResult> result = new ObservableCollection<SearchResult>();
            List<string>[] list = db.SelectWithItemsNumber(num_param, query);
            for (int i = 0; i < list[0].Count(); i++)
            {
                string CatalystID = list[0][i].ToString();
                string CatalystTypeName = list[1][i].ToString();
                string TargetConfSystem = list[2][i].ToString();
                string Volume = list[3][i].ToString();
                string MonolithMaterial = list[4][i].ToString();
                string AgingStatus = list[5][i].ToString();
                string EngineDisplacement = list[6][i].ToString();
                string EnginePower = list[7][i].ToString();
                SearchResult s = new SearchResult(CatalystID, CatalystTypeName, TargetConfSystem, Volume, MonolithMaterial, AgingStatus, EngineDisplacement, EnginePower);
                result.Add(s);
            }
            return result;
        }
    }
}