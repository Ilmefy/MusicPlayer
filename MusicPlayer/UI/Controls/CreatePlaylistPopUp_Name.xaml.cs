using MusicPlayer.Source.Playlist;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayer.UI.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy CreatePlaylistPopUp_Name.xaml
    /// </summary>
    public partial class CreatePlaylistPopUp_Name : UserControl
    {
        public CreatePlaylistPopUp_Name()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                Playlist playlist = new Playlist();
                playlist.Name = (sender as TextBox).Text;
                UI.UiManager.CreatePlayListControl(playlist);
                MainWindow.Instance.LeftBarContainer.Children.Remove(this);
            }
        }
    }
}
