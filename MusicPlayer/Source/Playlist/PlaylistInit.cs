using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Source.Playlist
{
    public  static class PlaylistInit
    {
        public static void Init()
        {
            string Data = System.IO.File.ReadAllText(Constants.PlaylisFilePath);
            PlaylistCollection Playlist = Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistCollection>(Data);
            References.playlistCollection = Playlist;
        }

    }
}
