using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using WinForms = System.Windows.Forms;
namespace MusicPlayer.UI.Elements
{
    /// <summary>
    /// Logika interakcji dla klasy MyMusic.xaml
    /// </summary>
    public partial class MyMusic : UserControl
    {
        public MyMusic()
        {
            InitializeComponent();

        }
        private bool _MusicSearcher;

        private bool OrderedAscending = false;
        public bool MusicSearcher
        {
            get { return _MusicSearcher; }
            set { _MusicSearcher = value; OnMusicSearcherChange(); }
        }
        private void OnMusicSearcherChange()
        {
            if (_MusicSearcher)
            {
                FindMusic.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            FindMusic.Visibility = System.Windows.Visibility.Hidden;
        }
        [DebuggerStepThrough]
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image i = sender as Image;
            System.Windows.Media.Animation.DoubleAnimation da = new System.Windows.Media.Animation.DoubleAnimation();
            da.From = i.ActualWidth;

            da.To = i.ActualWidth * 1.5d;
            da.Duration = TimeSpan.FromSeconds(0.1f);
            da.Duration = TimeSpan.FromSeconds(0.1f);
            i.BeginAnimation(WidthProperty, da);
            i.BeginAnimation(HeightProperty, da);
        }

        [DebuggerStepThrough]
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image i = sender as Image;
            System.Windows.Media.Animation.DoubleAnimation da = new System.Windows.Media.Animation.DoubleAnimation();
            da.From = i.ActualWidth;

            da.To = i.ActualWidth / 1.5f;
            da.Duration = TimeSpan.FromSeconds(0.1f);
            i.BeginAnimation(WidthProperty, da);
            i.BeginAnimation(HeightProperty, da);
        }
        [DebuggerStepThrough]
        private void Image_MouseEnter_1(object sender, MouseEventArgs e)
        {

        }
        private void SearchAudioFiles(string path)
        {
            List<string> files = new List<string>();
            files = System.IO.Directory.GetFiles(path, "*.mp3").ToList();
            files.AddRange(System.IO.Directory.GetFiles(path, "*.wav").ToList());
            if (files.Any())
                CreateSongElement(files);
        }
        public void AddElement(Source.Music.Track Track)
        {
            UI.Controls.SongElement se = new Controls.SongElement()
            {
                Author = Track.Author,
                Length = Track.Length,
                name = Track.Title,
                Track = Track,
                Added=AddedToText(Track.DateOfAdding),
            };
            StackPanel.Children.Add(se);
        }
        private string AddedToText(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;
            int minutes =(int)timeSpan.TotalMinutes;
            int days = (int)timeSpan.TotalDays;
            int Months = (int)timeSpan.TotalDays / 30;
            if (minutes <= 1)
                return "Minutę temu";
            if (minutes > 1 && minutes < 60)
                return $"{minutes}min temu";
            if(timeSpan.TotalHours<24)
            {
                return $"{timeSpan.TotalHours}godzin temu";
            }
            if (timeSpan.TotalDays == 1)
                return "wczoraj";
            if(days > 1 && days < 7)
            {
                return $"{timeSpan.TotalDays}dni temu";
            }
            if (days >= 7 && days < 14)
                return "Tydzień temu";
            if (days >= 14 && days < 21)
                return "Dwa tygodnie temu";
            if (days >= 21 && days < 28)
                return "Trzy tygodnie temu";
            if (days >= 28)
                return $"{Months}m temu";
            return "";
        }
        private void CreateSongElement(List<string> files)
        {
            string[] data;
            string title = "";
            string author = "";
            List<Source.Music.Track> tracks = new List<Source.Music.Track>();
            List<Source.Music.Track> already = References.trackCollection.Tracks;

            string length = "";
            foreach (string s in files)
            {
                if (already.Where(c => c.Path == s).Any())
                    continue;
                data = GetName(s);
                title = data[1];
                author = data[0];
                length = GetLength(s);

                Source.Music.Track tr = new Source.Music.Track();
                References.trackCollection.Tracks.Add(tr);
                tr.Path = s;
                tr.Length = length;
                tr.Title = title;
                tr.Author = author;
                tr.DateOfAdding = DateTime.Now;
                tr.LengthDouble = GetLengthToDouble(s);
                tracks.Add(tr);
                UI.Controls.SongElement se = new Controls.SongElement()
                {
                    name = title,
                    Author = author,
                    Length = length,
                    Track = tr,

                };
                StackPanel.Children.Add(se);
            }
            Source.Music.TrackData.AddTrack(tracks);
        }



        private void CreateSongData(string Title, string Author, string Length, string Path)
        {
            string data = $"{{\"Name\":{Title},\"Author\":{Author},\"Length\":\"{Length}\",\"Path\":{Path}}}";

        }
        private double GetLengthToDouble(string Path)
        {
            NAudio.Wave.Mp3FileReader mp3FileReader;
            NAudio.Wave.WaveFileReader waveFileReader;
            TimeSpan timeSpan = new TimeSpan();
            try
            {
                if (System.IO.Path.GetExtension(Path) == ".mp3")
                {
                    mp3FileReader = new NAudio.Wave.Mp3FileReader(Path);
                    timeSpan = mp3FileReader.TotalTime;
                    mp3FileReader.Dispose();
                }
                if (System.IO.Path.GetExtension(Path) == ".wav")
                {
                    waveFileReader = new NAudio.Wave.WaveFileReader(Path);
                    waveFileReader.Dispose();
                    timeSpan = waveFileReader.TotalTime;
                }
                return timeSpan.TotalSeconds;
            }
            catch (Exception)
            {
                return 0.0d;
                
            }

        }
        private string GetLength(string Path)
        {
            NAudio.Wave.Mp3FileReader mp3FileReader;
            NAudio.Wave.WaveFileReader waveFileReader;
            TimeSpan timeSpan = new TimeSpan();
            try
            {
                if (System.IO.Path.GetExtension(Path) == ".mp3")
                {
                    mp3FileReader = new NAudio.Wave.Mp3FileReader(Path);
                    timeSpan = mp3FileReader.TotalTime;
                    mp3FileReader.Dispose();
                }
                if (System.IO.Path.GetExtension(Path) == ".wav")
                {
                    waveFileReader = new NAudio.Wave.WaveFileReader(Path);
                    waveFileReader.Dispose();
                    timeSpan = waveFileReader.TotalTime;
                }
                int dd = timeSpan.Days;
                int hh = timeSpan.Hours;
                int mm = timeSpan.Minutes;
                int ss = timeSpan.Seconds;
                string t = "";
                if (dd > 0)
                    t += $"{dd}D:";
                if (hh > 0)
                    t += $"{hh}H:";
                if (mm > 0)
                    t += $"{mm}M:";
                if (ss > 0)
                    t += $"{ss}s";
                return t;

            }
            catch (Exception)
            {

                return "";
            }

        }
        private string[] GetName(string FileName)
        {
            string[] data = new string[2];
            Regex regex = new Regex("(.*)-(.*)");
            FileName = System.IO.Path.GetFileNameWithoutExtension(FileName);
            Match match = regex.Match(FileName);
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

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            string x = "";
            using (var Dialog = new WinForms.FolderBrowserDialog())
            {
                Dialog.ShowDialog();
                SearchAudioFiles(Dialog.SelectedPath);
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OrderByNameButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!OrderedAscending)
            {
                UI.UiManager.OrderTrackByName(OrderedAscending);
                OrderedAscending = true;
                return;
            }
            UI.UiManager.OrderTrackByName(OrderedAscending);
            OrderedAscending = false;


        }

        private void OrderByAuthor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!OrderedAscending)
            {
                UI.UiManager.OrderTrackByAuthor(OrderedAscending);
                OrderedAscending = true;
                return;
            }
            UI.UiManager.OrderTrackByAuthor(OrderedAscending);
            OrderedAscending = false;
        }

        private void OrderByLength_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!OrderedAscending)
            {
                UI.UiManager.OrderTrackByLength(OrderedAscending);
                OrderedAscending = true;
                return;
            }
            UI.UiManager.OrderTrackByLength(OrderedAscending);
            OrderedAscending = false;
        }

        private void OrderByAddingDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!OrderedAscending)
            {
                UI.UiManager.OrderTrackByDateOfAdding(OrderedAscending);
                OrderedAscending = true;
                return;
            }
            UI.UiManager.OrderTrackByDateOfAdding(OrderedAscending);
            OrderedAscending = false;
        }
    }
}
