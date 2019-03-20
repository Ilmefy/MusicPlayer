using System.Collections.Generic;

namespace MusicPlayer.Source.Music
{
    class TrackCollection
    {
        private List<Track> _tracks;

        public List<Track> Tracks
        {
            get { return _tracks; }
            set { _tracks = value; }
        }
    }
}
