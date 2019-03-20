using System;
using System.Collections.Generic;

namespace MusicPlayer
{
    public class Station
    {

        public string Name { get; set; }
        public List<string> Urls { get; set; }
        public Language language { get; set; }
        public List<string> Description = new List<string>();
        [Flags]
        public enum Language
        {
            Polish = 0x0,
            English = 0x1,
            Germany = 0x2,
            France = 0x4,
        }
        public enum Genre
        {
            Pop=1,
            HeavyMetal=2
        }
    }
}

