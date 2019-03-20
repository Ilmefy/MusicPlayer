using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.Source.Music
{
    static class TrackData
    {
        public static void AddTrack(List<Track> tracks)
        {
            List<Track> tr = new List<Track>();
            string temp = System.IO.File.ReadAllText(Constants.TrackListPath);
            if(!String.IsNullOrEmpty(temp))
            {
                tr = References.trackCollection.Tracks;
            }
            tr.AddRange(tracks);
            tr = tr.Distinct(new TrackDataEqualityComparer()).ToList();
          string data = Newtonsoft.Json.JsonConvert.SerializeObject(tr, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(Constants.TrackListPath, $"{{\"Tracks\":{data}}}");
        }
        public static void UpdateData()
        {
            List<Track> tr = References.trackCollection.Tracks;
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(tr, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(Constants.TrackListPath, $"{{\"Tracks\":{data}}}");
        }
    }
}
