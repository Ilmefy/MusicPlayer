using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayer.UI.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        public Notification()
        {
            InitializeComponent();

        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ColorAnimation ca = new ColorAnimation();
            var solidColorBrush = (SolidColorBrush)((sender as Border).Background);
            ca.From = solidColorBrush.Color;
            ca.To = Colors.White;
            ca.Duration = TimeSpan.FromSeconds(0.2f);
            (sender as Border).BeginAnimation(BackgroundProperty, ca);
        }
    }
}
