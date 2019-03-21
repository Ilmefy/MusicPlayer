using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayer.Source;
using MusicPlayer.Source.Playlist;

namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window CurrentWindow;
        public static MainWindow Instance;
        private static Mp3 mp3;
        public enum Window
        {
            Home=0x01,
            Radio=0x02,
            Search=0x04,
            Favourite=0x08,
            Playlist=0x10,
        }
        public MainWindow()
        {


            Stopwatch sw = new Stopwatch();
            
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(MainWindow), MainWindow.KeyDownEvent, new RoutedEventHandler(Window_KeyDown));
            Instance = this;

            AppInitializer.Initialize();
            UI.UiManager.SetTabControlWindow(Window.Home);
            CurrentWindow = Window.Home;
            Task.Factory.StartNew(()=> MainWindow.Instance.Dispatcher.Invoke(() => Source.Radio.RadioInit.LoadData()));
            Task.Factory.StartNew(()=> MainWindow.Instance.Dispatcher.Invoke(()=> Source.Music.TrackInit.Init()));
            Task.Factory.StartNew(()=> MainWindow.Instance.Dispatcher.Invoke(()=> Source.Playlist.PlaylistInit.Init()));
            Music m = new Music();


            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            Source.Global.AppInitialized = true;
          

        }
        private void Window_KeyDown(object sender, RoutedEventArgs e )
        {

            switch((e as KeyEventArgs).Key)
            {
                case Key.Space:
                  
                //    Radio.Pause();
                    break;
                
            }
        }
        private void OnProcessExit(object sender, EventArgs e)
        {
            Source.Music.TrackData.UpdateData();
        }
        public void Update()
        {
        }
        private void UpdateTimeBar()
        {
            while (mp3 != null)
            {
                long SoundLength = mp3.Length;
                long SoundPosition = mp3.Position;
                float Position = 0.0f;
                if (SoundPosition != 0)
                {
                    SoundPosition = mp3.Position;
                    Position = SoundLength / SoundPosition;
                }
            }
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Label lb = sender as Label;
            lb.Foreground = new SolidColorBrush(Colors.White);    
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Label lb = sender as Label;
            lb.Foreground = new SolidColorBrush(Color.FromArgb(100,230,230, 230));
        }

        private void LeftBarMenuOption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LeftBarMenuOption c = sender as LeftBarMenuOption;
            UI.UiManager.SetTabControlWindow((Window)(c.Index));
            CurrentWindow = (Window)(c.Index);
        }
        private void Volume_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UI.Controls.Volume v = sender as UI.Controls.Volume;
            double value = v.Slider.Value / v.Slider.Maximum;
            int vol = (int)(value * (double)100);
            Radio.Volume(vol);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VolumeSlider.ExpandSlider();
        }
        private void UpdateTime()
        {

                if (Global.PlaysMusic)
                {

                }
                else
                {
                    DateTime dt = DateTime.Now;
                    string Time = dt.ToShortTimeString();
                double progress =(double) dt.Second/(double)60;
                //timer.UpdateTime(0.75, "12:35");
                }
                
            

        }

        private void TrackPositionSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //UI.Controls.TrackPositionSlider tps = sender as UI.Controls.TrackPositionSlider;
            //double value = tps.Slider.Value / tps.Slider.Maximum;
            //double TrackLength = Radio.TrackLength;
            //double seconds = TrackLength * value;
            //Radio.UpdateCurrentTime(seconds);
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Animation.DoubleAnimation da = new System.Windows.Media.Animation.DoubleAnimation();
            da.From = (sender as Image).ActualWidth;
            da.To= (sender as Image).ActualWidth*1.25f;
            da.Duration = TimeSpan.FromSeconds(0.1f);
            (sender as Image).BeginAnimation(WidthProperty, da);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            double To = 0;
            if ((sender as Image).Name == "PlayButton")
                To = 45;
            else
                To = 35;
            System.Windows.Media.Animation.DoubleAnimation da = new System.Windows.Media.Animation.DoubleAnimation();
            da.From = (sender as Image).ActualWidth;
            da.To = To;
            da.Duration = TimeSpan.FromSeconds(0.1f);
            (sender as Image).BeginAnimation(WidthProperty, da);
        }

        private void LeftBarPlaylist_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            //UI.UiManager.GeneratePlaylistControls();
        }

        private void MenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        private void MenuItem_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Prevent from adding more textBoxes
            if (MainWindow.Instance.LeftBarContainer.Children.OfType<UI.Controls.CreatePlaylistPopUp_Name>().Count() > 0)
                return;
            var textBox = new UI.Controls.CreatePlaylistPopUp_Name();
            textBox.Height = 30;
            MainWindow.Instance.LeftBarContainer.Children.Add(textBox);
        }
    }
}
