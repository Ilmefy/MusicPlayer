namespace MusicPlayer.Source
{
    class AppInitializer
    {
        public static void Initialize()
        {
            SaveFolderExist();
            RecordingsFolderExist();
            TrackListExist();
        }
        private static  void SaveFolderExist()
        {
            if (System.IO.Directory.Exists(Source.Constants.DocumentsFolder))
                return;
            System.IO.Directory.CreateDirectory(Source.Constants.DocumentsFolder);
        }
        private static void RecordingsFolderExist()
        {
            if (System.IO.Directory.Exists(Source.Constants.RecordingsSaveFolder))
                return;
            System.IO.Directory.CreateDirectory(Source.Constants.RecordingsSaveFolder);
        }
        private static void TrackListExist()
        {

        }
        private static void CreateTrackList()
        {

        }
    }
}
