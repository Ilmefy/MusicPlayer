using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy LeftBarMenuOption.xaml
    /// </summary>
    public partial class LeftBarMenuOption : UserControl
    {
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        private ImageSource iConSource;
        public ImageSource IconSource
        {
            get { return iConSource; }
            set { iConSource = value; ImageSourceChanged(); }
        }
        private void ImageSourceChanged()
        {
            Icon.Source = iConSource;
        }
        private string menuTitle;
        public string MenuTitle
        {
            get { return menuTitle; }
            set { menuTitle = value; MenuTitleChanged(); }
        }
        private void MenuTitleChanged()
        {
            Label.Content = menuTitle;
        }
        //public ImageSource ImageSource
        //{
        //    get { return (ImageSource)GetValue(ImageSourceProperty); }
        //    set { SetValue(ImageSourceProperty, value); }
        //}
        //public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(LeftBarMenuOption), new UIPropertyMetadata(0));


        public LeftBarMenuOption()
        {
            InitializeComponent();
            
           // Icon.Source = imageSource;
       //     OnIconUriChange();
        }
        private void Koloruj()
        {

            BitmapImage bitmap = new BitmapImage();
        }
        private void OnIconUriChange()
        {
            //Uri u = new Uri("MusicPlayer.Properties.Resources._51590756_338173343449201_5846092364128452608_n;");           
            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.UriSource = u;
            //Icon.Source = bitmapImage;
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Animation.DoubleAnimation doubleAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            doubleAnimation.From = Icon.Height;
            doubleAnimation.To = 40;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.1f);
            Icon.BeginAnimation(HeightProperty, doubleAnimation);
            Icon.BeginAnimation(WidthProperty, doubleAnimation);
            Label.Foreground = new SolidColorBrush(Colors.White);
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Animation.DoubleAnimation doubleAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            doubleAnimation.From = Icon.Height;
            doubleAnimation.To = 30;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.1f);
            Icon.BeginAnimation(HeightProperty, doubleAnimation);
            Icon.BeginAnimation(WidthProperty, doubleAnimation);
            Label.Foreground = new SolidColorBrush(Color.FromArgb(100,166,166,166));
        }
    }
}
