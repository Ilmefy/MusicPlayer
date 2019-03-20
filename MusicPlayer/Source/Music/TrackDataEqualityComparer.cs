using System.Collections.Generic;

namespace MusicPlayer.Source.Music
{
    class TrackDataEqualityComparer : IEqualityComparer<Track>
    {
        public bool Equals(Track x, Track y)
        {
            if (x.Path == y.Path)
                return true;
            return false;
        }

        public int GetHashCode(Track obj)
        {
            return obj.Author.GetHashCode();
        }
    }
}
    
