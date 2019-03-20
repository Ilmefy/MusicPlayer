namespace MusicPlayer.Source
{
    static class Global
    {
        public static bool PlaysMusic = false;
        public static object Player;
        public static bool AppInitialized = false;
        public static bool RadioInitiated = false;
        public static bool PlayerTaskStopped=false;
        public static bool PlayerTaskPaused=false;
        public static System.Threading.Tasks.Task PlayerTask;
        public static System.Threading.CancellationTokenSource cancellationTokenSource;
    }
}
