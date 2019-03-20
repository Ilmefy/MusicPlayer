namespace MusicPlayer
{
    class Song
    {
        public enum FileExtension
        {
            mp3=0,
            wav=1,
        }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Length { get; set; }
        public string Path { get; set; }
        public FileExtension Extension { get; set; }
    }
}
