using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Source.Playlist
{
    static class PlayListManager
    {
        public static void AddPlaylist(string Name)
        {
            References.playlistCollection.Playlists.Add(new Playlist() { Name = Name, DateOfCreating = DateTime.Now });
        }
        public static void AddTrackToPlaylist(uint TrackId, uint PlaylistId)
        {
            References.playlistCollection.Playlists.Where(c => c.ID == PlaylistId).First().Tracks.Add(TrackId);
        }
    }
}
