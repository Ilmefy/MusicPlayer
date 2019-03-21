using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Source.Playlist
{
   public class Playlist
    {
        public string Name { get; set; }
        public DateTime DateOfCreating{ get; set; }
        public uint ID { get; set; }
        public List<uint> Tracks { get; set; }
    }
}
