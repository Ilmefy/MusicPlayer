using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer
{
    class StationCollection
    {
        private List<Station> stations;

        public List<Station> Stations
        {
            get { return stations; }
            set { stations = value; }
        }
        public Station GetByName(string name)
        {
            return stations.Where(c => c.Name == name).First();
        }
        public List<Station> GetByLanguage(Station.Language language)
        {
            return stations.Where(c => c.language == language).ToList();
        }
        public List<Station> SearchStation(string phrase)
        {
            List<Station> stationsContains = stations.Where(c => c.Name.Contains(phrase)).ToList();
            List<Station> stationsStarts = stations.Where(c => c.Name.StartsWith(phrase)).ToList();
            return stationsStarts.Concat(stationsContains).ToList();
        }
    }
}
