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
    }
}
