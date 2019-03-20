using MusicPlayer.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MusicPlayer.Source.Radio
{
    static class RadioInit
    {
        public static void LoadData()
        {
            Stopwatch sw = new Stopwatch();
            string Data = System.IO.File.ReadAllText(Constants.StationListPath);
            StationCollection station = Newtonsoft.Json.JsonConvert.DeserializeObject<StationCollection>(Data);

            //To prevent null's in Description

            foreach (Station s in station.Stations)
            {
                if (!s.Description.Any())
                {
                    s.Description.Add("");
                }
            }

            References.stationCollection = station;
           CreateStationList();
        }
        //Requires REWRITING TO IMPROVE PERFOMANCE 
        private static void CreateStationList()
        {
            UI.Elements.RadioTab radioTab = MainWindow.Instance.Tab_Radio;

            List<Controls.RadioElement> res = new List<RadioElement>();
            foreach (Station s in References.stationCollection.Stations)
            {
                radioTab.AddElement(s);
            }

        }
    }
}
