using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MusicPlayer.UI.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy TrackPositionSlider.xaml
    /// </summary>
    public partial class TrackPositionSlider : UserControl
    {
        bool Sliding = false;
        public TrackPositionSlider()
        {
            InitializeComponent();
        }
        public void StartUpdating(double progress, double Length)
        {
            if (Sliding)
                return;
            Slider.Maximum = Length;
            Slider.Value = progress;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (System.Windows.Input.Mouse.LeftButton == System.Windows.Input.MouseButtonState.Released || Slider.IsMouseCaptured==false)
                return;
            Sliding = true;
            Radio.UpdateCurrentTime(Slider.Value);

        }



        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Sliding = false;
        }
    }
}
