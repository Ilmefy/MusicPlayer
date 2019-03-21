using System;

namespace MusicPlayer.Source
{
    static class Constants
    {
        
        public static readonly string DocumentsFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\IllPlayer";
        public static readonly string StationListPath = $"{DocumentsFolder}\\StationList.bin";
        public static readonly string RecordingsSaveFolder = DocumentsFolder + @"\Recordings";
        public static readonly string TrackListPath = $"{DocumentsFolder}\\TrackList.json";
        public static readonly string PlaylisFilePath = $"{DocumentsFolder}\\Playlists.json";
    }
}
