using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Source.Playlist
{
    public  class PlaylistCollection
    {
        private List<Playlist> _playlists;

        public  List<Playlist> Playlists
        {
            get { return _playlists; }
            set { _playlists = value; }
        }

    }
}
