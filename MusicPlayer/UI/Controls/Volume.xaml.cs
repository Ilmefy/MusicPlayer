using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MusicPlayer.UI.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy Volume.xaml
    /// </summary>
    public partial class Volume : UserControl
    {
        private const float AnimationDuration = 0.5f;
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private bool SliderExpanded = false;
        public Volume()
        {
            InitializeComponent();
        }
        public void ExpandSlider()
        {
            while(SliderExpanded)
            {
                //System.Threading.Thread.Sleep(2000);
                if(sw.ElapsedMilliseconds>2000)
                {
                   Dispatcher.Invoke(()=> FoldSlider());
                }
            }
        }
        private void FoldSlider()
        {
            sw.Reset();
            sw.Stop();
            SliderExpanded = false;
            DoubleAnimation da = new DoubleAnimation();
            da.From = Slider.ActualWidth;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(AnimationDuration);
            Slider.BeginAnimation(WidthProperty, da);

        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (SliderExpanded)
                return;
            SliderExpanded = true;
            DoubleAnimation da = new DoubleAnimation();
            da.From = Slider.ActualWidth;
            da.To = 150;
            double? x = da.To - da.From;
            Thickness t = new Thickness(0, 0, (int)x, 0);

            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = Slider.Margin;
            ta.To = t;
            ta.Duration = TimeSpan.FromSeconds(AnimationDuration);
            da.Duration = TimeSpan.FromSeconds(AnimationDuration);
            Slider.BeginAnimation(WidthProperty, da);
            sw.Start();
            Task.Factory.StartNew(() => ExpandSlider());
       //     ExpandSlider();
            //Slider.BeginAnimation(MarginProperty, ta);
        }

        private void Slider_MouseEnter(object sender, MouseEventArgs e)
        {
            sw.Restart();
        }



        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sw.Restart();
        }
    }
}
