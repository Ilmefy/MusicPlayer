using System.Linq;

namespace MusicPlayer.Source.Music
{
    static class TrackInit
    {
        public static void Init()
        {
            string Data = System.IO.File.ReadAllText(Constants.TrackListPath);
            TrackCollection trackCollection = Newtonsoft.Json.JsonConvert.DeserializeObject<TrackCollection>(Data);

            //To prevent null's in Description
            foreach (Track s in trackCollection.Tracks)
            {
                if (!s.Path.Any())
                {
                }
            }

            References.trackCollection = trackCollection;
            CreateTrackList();
        }
        private static void CreateTrackList()
        {
            UI.Elements.MyMusic myMusic = MainWindow.Instance._MyMusic;
            UI.Elements.MyMusic Fav = MainWindow.Instance._MyFavourite;
            foreach (Track t in References.trackCollection.Tracks)
            {
                myMusic.AddElement(t);
                if (t.Favourite)
                    Fav.AddElement(t);

            }
        }
    }

    }
