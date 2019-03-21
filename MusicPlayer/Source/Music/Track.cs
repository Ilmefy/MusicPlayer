using System;

namespace MusicPlayer.Source.Music
{
    public class Track
    {
        public string Title { get; set; }    
        public string Author { get; set; }
        public string Length { get; set; }
        public double LengthDouble { get; set; }
        public string Path { get; set; }
        private bool _favourite;
        public uint ID { get; set; }
        public bool Favourite
        {
            get { return _favourite; }
            set { _favourite = value; OnFavouriteChange(); }
        }
        private void OnFavouriteChange()
        {
            
        }
        public DateTime ToFavouriteAdded { get; set; }
        //Id of playlist
        public int[] BelongsToPlaylist { get; set;}
        public DateTime DateOfAdding { get; set; }


        
        private string AddedToString()
        {
            string s = "";
            int Passed= (DateTime.Now.DayOfYear - DateOfAdding.DayOfYear);
            if (Passed == 1)
                return "Wczoraj";
            if (Passed >= 7 && Passed < 14)
                return "Tydzień temu";
            if (Passed >= 14 && Passed < 21)
                return "Dwa tygodnie temu";
            if (Passed >= 21 && Passed < 28)
                return "Trzy tygodnie temu";
            return "Miesiac";
        }
    }
}
