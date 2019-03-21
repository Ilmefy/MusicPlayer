using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MusicPlayer.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy RadioElement.xaml
    /// </summary>
    public partial class RadioElement : UserControl
    {
        System.Threading.CancellationTokenSource CT;
        bool Plays = false;
        bool Records = false;
        public enum Genre
        {
            Pop = 0x1,
            HeavyMetal = 0x2,
            Westfall = 0x4,
            xd = 0x8,
        }
        private Station station;

        public Station Station
        {
            get { return station; }
            set { station = value; }
        }

        private string[] incomingSongs;

        public string[] IconmingSongs
        {
            get { return incomingSongs; }
            set { incomingSongs = value; }
        }

        #region StationName
        private string stationName;

        public string StationName
        {
            get { return stationName; }
            set { stationName = value; OnStationNameChange(); }
        }
        private void OnStationNameChange()
        {
            Label_Name.Content = stationName;
        }
        #endregion
        #region StationDescription
        private string stationDescription;

        public string StationDescription
        {
            get { return stationDescription; }
            set { stationDescription = value; OnStationDescriptionChange(); }
        }
        public bool NameStartsWith(string text)
        {
            if (StationName.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                return true;
            return false;
        }
        public bool NameContains(string text)
        {
            text = text.ToLower();
            string temp = StationName.ToLower();
            if (temp.Contains(text))
                return true;
            return false;
        }
        private void OnStationDescriptionChange()
        {
            Label_Description.Content = stationDescription;
        }
        #endregion
        #region iconSource
        private string iconSource;

        public string IconSource
        {
            get { return iconSource; }
            set { iconSource = value; OnIconSourceChange(); }
        }
        private void OnIconSourceChange()
        {
            Uri iconUri = new Uri(iconSource, UriKind.Relative);
            BitmapImage iconBitmap = new BitmapImage(iconUri);
            AlbumIcon.Source = iconBitmap;
        }
        #endregion
        //private readonly DependencyProperty GenreProperty = DependencyProperty.Register("StationGenre", typeof(Station.Genre), typeof(RadioElement));

        private Genre stationGenre;

        public Genre StationGenre
        {
            get { return stationGenre; }
            set { stationGenre = value; OnGenreChange(); }
        }


        private void OnGenreChange()
        {
            List<Genre> genres = new List<Genre>();
            Genre[] genresArray = (Genre[])Enum.GetValues(typeof(Genre));
            foreach (Genre g in genresArray)
            {
                if (stationGenre.HasFlag(g))
                    genres.Add(g);
            }
            UInt16 enumCount = (UInt16)Count();
            if (enumCount > 0)
            {
                TagRight.Children.Add(CreateTag(genres[0], TagLocation.Bottom));

                if (enumCount > 1)
                {
                    TagRight.Children.Add(CreateTag(genres[1], TagLocation.Top));

                    if (enumCount > 2)
                    {
                        TagLeft.Children.Add(CreateTag(genres[2], TagLocation.Bottom));
                        if (enumCount > 3)
                            TagLeft.Children.Add(CreateTag(genres[3], TagLocation.Top));
                    }
                }
            }

        }
        private enum TagLocation
        {
            Top = 0,
            Bottom = 1
        }
        private Tag CreateTag(Genre genre, TagLocation location)
        {
            Thickness thickness = new Thickness();
            switch (location)
            {
                case TagLocation.Top:
                    thickness = new Thickness(0, 5, 0, 0);
                    break;
                case TagLocation.Bottom:
                    thickness = new Thickness(0, 5, 0, 0);
                    break;

            }
            Tag tg = new Tag()
            {
                TagName = genre.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = thickness,
                HorizontalAlignment = HorizontalAlignment.Right,
            };
            return tg;
        }
        private UInt32 Count()
        {
            Genre genre = stationGenre;
            UInt32 v = (UInt32)genre;
            v = v - ((v >> 1) & 0x55555555); // reuse input as temporary
            v = (v & 0x33333333) + ((v >> 2) & 0x33333333); // temp
            UInt32 c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
            return c;
        }

        private UInt16 index;

        public UInt16 Index
        {
            get { return index; }
            set { index = value; }
        }




        public RadioElement()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Source.Global.RadioInitiated)
                Radio.Pause();
            if (Plays)
            {
                Plays = false;

                PlayButton.Source = new BitmapImage(new Uri("/UI/Icons/play.png", UriKind.Relative));
                return;
            }
            else
            {
                Source.Global.PlayerTaskPaused = false;
                Task.Factory.StartNew(() => Radio.Play(station.Urls[0], null));
                Plays = true;
                Stopwatch sw = new Stopwatch();
                foreach (RadioElement re in MainWindow.Instance.Tab_Radio.StationList.Children)
                {
                    re.PlayButton.Source = new BitmapImage(new Uri("/UI/Icons/play.png", UriKind.Relative));
                    re.Plays = false;
                }
                PlayButton.Source = new BitmapImage(new Uri("/UI/Icons/pause.png", UriKind.Relative));
            }

        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (!Records)
            {
                System.Threading.CancellationTokenSource ct = new System.Threading.CancellationTokenSource();
                Records = true;
                string URl = "https://stream.open.fm/109?type=.aac&user=800086344018&player_group=WWW&user-agent=OpenFM-Chameleon%2F1.103.0062%20(BROWSER%3BChrome%3B72.0.3626.121)%20(OS%3BWindows%3BWindows%20NT%2010.0)";
                Recording.Source = new BitmapImage(new Uri("/UI/Icons/record_Active.png", UriKind.Relative));
                Task task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        using (ct.Token.Register(System.Threading.Thread.CurrentThread.Abort))
                        {
                            Radio.Record(URl,"XD");
                        }

                    }
                    catch (System.Threading.ThreadAbortException)
                    {
                        return;
                    }
                }, ct.Token);
                CT = ct;


                return;
            }
            Recording.Source = new BitmapImage(new Uri("/UI/Icons/record.png", UriKind.Relative));
            CT.Cancel();
            Records = false;

        }
        public void UpdateRadioElement(Station station)
        {
            Label_Name.Content = station.Name;
            Label_Description.Content = station.Description[0];
        }
        
    }
}
