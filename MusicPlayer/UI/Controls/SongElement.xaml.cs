using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace MusicPlayer.UI.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy SongElement.xaml
    /// </summary>
    public partial class SongElement : UserControl
    {
        private bool _fav;
        private bool isFavouriteTab;

        public bool FavouriteTab
        {
            get { return isFavouriteTab; }
            set { isFavouriteTab = value; }
        }

        public bool Favourite
        {
            get { return _fav; }
            set { _fav = value; }
        }
        private void OnFavouriteChange()
        {
            if (_fav)
            {
                Accept.Source = new BitmapImage(new Uri("/UI/Icons/accept_active.png", UriKind.Relative));
                return;
            }
            Accept.Source = new BitmapImage(new Uri("/UI/Icons/accept_active.png", UriKind.Relative));

        }
        private const double AnimationFrom = 20;
        System.Threading.CancellationTokenSource CT;
        private string _added;
        public SongElement DeepCopy()
        {

            var temp = new SongElement();
            temp.Added = this.Added;
            temp.Author = this.Author;
            temp.name = this.name;
            temp.Track = this.Track;
            //temp.Playing = ff;
            temp.Favourite = this.Favourite;
            temp.Length = this.Length;
            return temp;

        }
        public string Added
        {
            get { return _added; }
            set { _added = value; OnAddedChange(); }
        }
        private void OnAddedChange()
        {
            Label_Added.Content = _added;
        }


        private void SwitchPlayIcon()
        {
            if (Source.Global.PlaysMusic)
            {
                PlayButton.Source = new BitmapImage(new Uri("/UI/Icons/pause.png", UriKind.Relative));
                return;
            }
            PlayButton.Source = new BitmapImage(new Uri("/UI/Icons/play.png", UriKind.Relative));

        }
        public SongElement()
        {
            InitializeComponent();
        }
        private string _name;
        private Source.Music.Track _track;

        public Source.Music.Track Track
        {
            get { return _track; }
            set { _track = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; OnNameChange(); }
        }
        private void OnNameChange() => Label_Name.Content = _name;



        private string _author;
        private string _length;

        public string Length
        {
            get { return _length; }
            set { _length = value; OnLengthChange(); }
        }
        private void OnLengthChange() => Label_Length.Content = _length;
        public string Author
        {
            get { return _author; }
            set { _author = value; OnAuthorChange(); }
        }
        private void OnAuthorChange() => Label_Author.Content = _author;
        #region Visuals
        [DebuggerStepThrough]
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image i = sender as Image;
            System.Windows.Media.Animation.DoubleAnimation da = new System.Windows.Media.Animation.DoubleAnimation();
            da.From = i.ActualWidth;

            da.To = i.ActualWidth * 1.5d;
            da.Duration = TimeSpan.FromSeconds(0.1f);
            i.BeginAnimation(MaxWidthProperty, da);
            i.BeginAnimation(MaxHeightProperty, da);

        }
        [DebuggerStepThrough]
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image i = sender as Image;
            System.Windows.Media.Animation.DoubleAnimation da = new System.Windows.Media.Animation.DoubleAnimation();
            da.From = i.ActualWidth;

            da.To = AnimationFrom;
            da.Duration = TimeSpan.FromSeconds(0.1f);
            i.BeginAnimation(MaxWidthProperty, da);
            i.BeginAnimation(MaxHeightProperty, da);
        }
        [DebuggerStepThrough]
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Animation.ColorAnimation ca = new System.Windows.Media.Animation.ColorAnimation();
            SolidColorBrush sc = (SolidColorBrush)Main.Background;
            ca.From = (Color)sc.Color;
            ca.To = ((Color)ColorConverter.ConvertFromString("#0C000000"));
            ca.Duration = TimeSpan.FromSeconds(0.75f);
            Main.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }
        [DebuggerStepThrough]
        private void Main_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Animation.ColorAnimation ca = new System.Windows.Media.Animation.ColorAnimation();
            SolidColorBrush sc = (SolidColorBrush)Main.Background;
            ca.From = (Color)sc.Color;
            ca.To = ((Color)ColorConverter.ConvertFromString("#00000000"));
            ca.Duration = TimeSpan.FromSeconds(0.25f);
            Main.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }
        #endregion
        /// <summary>
        /// NIE KURWA JEGO MAĆ RUSZAĆ TEGO. CHUJ WIE JAK TO DZIAŁA, ALE ,,U MNIE DZIAŁO"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            
            if (Source.Global.PlaysMusic)
            {
                ChangeProgress(1, 2);
                //prevent from starting again same track
                if (Source.Global.Player == this)
                {
                    Source.Global.PlaysMusic = false;
                    Source.Global.PlayerTaskPaused = true;
                    Radio.Pause();
                    return;
                }

                else
                {
                    Radio.Stop();
                    Source.Global.cancellationTokenSource.Cancel();
                    Source.Global.PlayerTaskStopped = false;
                    Source.Global.PlayerTaskPaused = false;
                    Source.Global.Player = this;

                    var tokenSource = new System.Threading.CancellationTokenSource();
                    Source.Global.cancellationTokenSource = tokenSource;
                    System.Threading.CancellationToken ct = tokenSource.Token;
                    Source.Global.PlayerTask = Task.Factory.StartNew(() => { Radio.Play(_track.Path, this); }, ct);



                    Source.Global.PlaysMusic = true;
                    return;
                }
                //Source.Global.Player = null;

            }
            if (!Source.Global.PlaysMusic)
            {
                if (Source.Global.Player == this)
                {
                    Source.Global.PlaysMusic = true;
                    Radio.Resume();
                    return;
                }

                Source.Global.Player = this;
                Source.Global.PlaysMusic = true;
                Source.Global.PlayerTaskPaused = false;
                var tokenSource = new System.Threading.CancellationTokenSource();
                Source.Global.cancellationTokenSource = tokenSource;
                System.Threading.CancellationToken ct = tokenSource.Token;

                Source.Global.PlayerTask = Task.Factory.StartNew(() => { Radio.Play(_track.Path, this); }, ct);
            }
        }
        public SongElement AddElement(Source.Music.Track t)
        {
            SongElement se = new SongElement()
            {
                Author = t.Author,
                name = t.Title,
                Track = t,
                Length = t.Length,
            };
            return se;
        }
        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Favourite)
            {
                Accept.Source = new BitmapImage(new Uri("/UI/Icons/accept.png", UriKind.Relative));
                Favourite = false;
                return;
            }
            else
            {
                Track.Favourite = true;
                Favourite = true;
                //if(MainWindow.Instance.)
                //    MainWindow.Instance._MyFavourite.StackPanel.Children.Add(DeepCopy());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Passed">How much time passed in seconds</param>
        /// <param name="Length">Total length of Track in seconds</param>
        public void ChangeProgress(double Passed, double Length)
        {
        
            //Color ProgressColor = (Color)ColorConverter.ConvertFromString("#7F007415");
            //Color BackgroundColor = (Color)ColorConverter.ConvertFromString("#00000000");
            //double Progress = Passed / Length;
            //GradientStop gradientStop1 = new GradientStop(ProgressColor,0);
            //GradientStop gradientStop2 = new GradientStop(ProgressColor,Progress);
            //GradientStop gradientStop3 = new GradientStop(BackgroundColor,(Progress+0.1d));
            //GradientStop gradientStop4 = new GradientStop(BackgroundColor,1.0d);
            //GradientStopCollection gradientStops = new GradientStopCollection();
            //gradientStops.Add(gradientStop1);
            //gradientStops.Add(gradientStop2);
            //gradientStops.Add(gradientStop3);
            //gradientStops.Add(gradientStop4);
            //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(gradientStops);
            //linearGradientBrush.StartPoint = new Point(0.5, 0);
            //linearGradientBrush.EndPoint = new Point(0.5, 1);
            //Main.Background = linearGradientBrush;
        }
    }
}
