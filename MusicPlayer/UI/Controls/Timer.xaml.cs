using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy Timer.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        public string Time { get; set; }
        public Timer()
        {
            InitializeComponent();

        }
        public void UpdateTime(double Progress, string time)
        {
            Time_Label.Content = time;
            GradientStop gradientStop0 = new GradientStop(Color.FromArgb(100, 170, 170, 170), 0.0f);
            GradientStop gradientStop1 = new GradientStop(Color.FromArgb(100, 170, 170, 170), Progress);
            GradientStop gradientStop2 = new GradientStop(Colors.White, Progress);
            GradientStop gradientStop3 = new GradientStop(Colors.White, 1.0f);
            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(gradientStop0);
            gsc.Add(gradientStop1);
            gsc.Add(gradientStop2);
            gsc.Add(gradientStop3);

            LinearGradientBrush lgb = new LinearGradientBrush(gsc);
            lgb.StartPoint = new Point(0.5, 0);
            lgb.EndPoint = new Point(0.5, 1);
            Time_Label.Foreground = lgb;
            //TimeBorder.BorderBrush = lgb;
        }







    }
}
