using MusicPlayer.Source.Music;
using MusicPlayer.UI.Controls;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.UI
{
    static class UiManager
    {
        static MainWindow mw = MainWindow.Instance;
        public static void HideMusicProgressBar()
        {
            //mw.MusicProgressBar.Visibility = System.Windows.Visibility.Hidden;
        }
        public static void HideVolumeBar()
        {
         // mw.VolumeChanger.Visibility = System.Windows.Visibility.Hidden;
        }
        public static int GetTabControlCurrentIndex()
        {
            return mw.TabControl.TabIndex;
        }
        public static void SetTabControlWindow(MainWindow.Window Window)
        {
            mw.TabControl.SelectedIndex = (int)Window;
        }
        public static void HideTrackPositionSlider()
        {
            MainWindow.Instance.trackPositionSlider.Visibility = System.Windows.Visibility.Collapsed;
        }
        public static void ShowTrackPositionSlider()
        {
            MainWindow.Instance.trackPositionSlider.Visibility = System.Windows.Visibility.Visible;
        }
        public static void OrderTrackByName(bool OrderAscending)
        {

            List<Track> tracks = References.trackCollection.Tracks.ToList();
            if (OrderAscending)
                tracks = tracks.OrderBy(c => c.Title).ToList();
            else
                tracks = tracks.OrderByDescending(c => c.Title).ToList();

            ChangeOrder(tracks);

        }
        public static void OrderTrackByAuthor(bool OrderAscending)
        {
            List<Controls.SongElement> songElements = MainWindow.Instance._MyMusic.StackPanel.Children.OfType<Controls.SongElement>().ToList();
            List<Track> tracks = References.trackCollection.Tracks.ToList();
            if (OrderAscending)
                tracks = tracks.OrderBy(c => c.Author).ToList();
            else
                tracks = tracks.OrderByDescending(c => c.Author).ToList();
            ChangeOrder(tracks);
        }
        public static void OrderTrackByLength(bool OrderAscending)
        {
            List<Controls.SongElement> songElements = MainWindow.Instance._MyMusic.StackPanel.Children.OfType<Controls.SongElement>().ToList();
            List<Track> tracks = References.trackCollection.Tracks.ToList();
            if (OrderAscending)
                tracks = tracks.OrderBy(c => c.Length).ToList();
            else
                tracks = tracks.OrderByDescending(c => c.Length).ToList();
            ChangeOrder(tracks);
        }
        public static void OrderTrackByDateOfAdding(bool OrderAscending)
        {
            List<Controls.SongElement> songElements = MainWindow.Instance._MyMusic.StackPanel.Children.OfType<Controls.SongElement>().ToList();
            List<Track> tracks = References.trackCollection.Tracks.ToList();
            if (OrderAscending)
                tracks = tracks.OrderBy(c => c.DateOfAdding).ToList();
            else
                tracks = tracks.OrderByDescending(c => c.DateOfAdding).ToList();
            ChangeOrder(tracks);
        }
        private static void ChangeOrder(List<Track> tracks)
        {
            List<Controls.SongElement> songElements = MainWindow.Instance._MyMusic.StackPanel.Children.OfType<Controls.SongElement>().ToList();
            int i = 0;
            foreach (Track t in tracks)
            {
                foreach (var se in songElements)
                {
                    if (se.name == t.Title)
                    {
                        MainWindow.Instance._MyMusic.StackPanel.Children.Remove(se);
                        MainWindow.Instance._MyMusic.StackPanel.Children.Insert(i, se);


                    }
                }
                i++;
            }
        }
    }
}
