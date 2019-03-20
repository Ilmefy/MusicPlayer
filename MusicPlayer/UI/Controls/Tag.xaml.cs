using System.Windows.Controls;

namespace MusicPlayer.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy Tag.xaml
    /// </summary>
    public partial class Tag : UserControl
    {
        private string tag;

        public string TagName
        {
            get { return tag; }
            set { tag = value; OnTagNameChange(); }
        }
        private void OnTagNameChange()
        {
            label.Content = tag;
        }
        public Tag()
        {
            InitializeComponent();
        }
    }
}
