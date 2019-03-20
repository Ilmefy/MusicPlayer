using System;
using System.IO;
using System.Text.RegularExpressions;
namespace MusicPlayer
{
    class Music
    {

        public void FindMusic(string Path)
        {
            string[] music = Directory.GetFiles(Path, "*.mp3", SearchOption.AllDirectories);
            
        }
        private int GetLength(string Path)
        {
            NAudio.Wave.Mp3FileReader mp3FileReader = new NAudio.Wave.Mp3FileReader(Path);
            TimeSpan timeSpan = mp3FileReader.TotalTime;
            int Length = (timeSpan.Days * 86400) + (timeSpan.Hours * 3600) + (timeSpan.Minutes * 60) + timeSpan.Seconds;
            if (Length == 0 && timeSpan.Milliseconds > 500)
                Length = 1;
            mp3FileReader.Dispose();
            return Length;

        }
        private string[] GetName(string FileName)
        {
            string[] data = new string[2];
            Regex regex = new Regex("(.*)-(.*)");
            FileName = Path.GetFileNameWithoutExtension(FileName);
            Match match= regex.Match(FileName);
            string patternAuthor = "-(.*)";
            string patternTitle = "(.*)-";
            string Author = Regex.Replace(match.ToString(), patternAuthor, "");

            string Title = Regex.Replace(match.ToString(), patternTitle, "");
            if (!String.IsNullOrEmpty(Author))
            {
                data[0] = Author;

            }
            else
                data[0] = "";
            if (String.IsNullOrEmpty(Title))
                data[1] = FileName;
            else
                data[1] = Title;

            return data;
        }
        private Song.FileExtension ChooseExtension(string extension)
        {
            switch(extension)
            {
                case ".mp3":
                    return Song.FileExtension.mp3;
                case ".wav":
                    return Song.FileExtension.wav;
            }
            return Song.FileExtension.mp3;
        }

    }
}
