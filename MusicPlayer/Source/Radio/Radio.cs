using NAudio.Wave;
using System;
using System.IO;
using System.Net;
namespace MusicPlayer
{
    static class Radio
    {
        static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        public static double TrackLength { get; set; }
        public static double Position;
        public static MediaFoundationReader mediaFoundationReader;
        static WaveOutEvent woe;
        /// <summary>
        /// NIE KURWA JEGO MAĆ RUSZAĆ TEGO. CHUJ WIE JAK TO DZIAŁA, ALE ,,U MNIE DZIAŁO"
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="sender"></param>
        public static void Play(string Url, object sender)
        {

            if (woe!=null)
                woe.Dispose();
            using (var mf = new MediaFoundationReader(Url))               
            using (var wo = new WaveOutEvent())
            {
                mediaFoundationReader = mf;
                wo.Init(mf);

                if (Url.StartsWith("http"))
                    TrackLength = 0;
                else
                {
                    TrackLength = mf.TotalTime.TotalSeconds;
                }
                if (Source.Global.RadioInitiated == false)
                {
                    wo.Volume = 0.0f;
                    Source.Global.RadioInitiated = true;
                }
       
                woe = wo;
                woe.Play();
                    while (!Source.Global.PlayerTaskPaused && wo.PlaybackState == PlaybackState.Playing)
                    {


                            Position = mf.CurrentTime.TotalSeconds;
                            MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.trackPositionSlider.StartUpdating(Position,TrackLength));
                            if (wo.PlaybackState != PlaybackState.Playing)
                                Source.Global.PlayerTaskPaused = true;
                        
                        System.Threading.Thread.Sleep(1000);                    
                    }
                
                mf.Dispose();
                wo.Dispose();
                Source.Global.cancellationTokenSource.Cancel();

            }
        }
        public static void UpdateCurrentTime(double seconds)
        {
            mediaFoundationReader.CurrentTime = TimeSpan.FromSeconds(seconds);
        }
        public static void Play(string Url, object sender, int Length)
        {
            TrackLength = Length;
            Play(Url, sender);
        }
        /// <summary>
        /// NIE KURWA JEGO MAĆ RUSZAĆ TEGO. CHUJ WIE JAK TO DZIAŁA, ALE ,,U MNIE DZIAŁO"
        /// </summary>
        public static void Stop()
        {
            woe.Stop();
            Source.Global.PlayerTaskPaused = true;
            Source.Global.PlaysMusic = false;
            Source.Global.PlayerTaskStopped = true;
        }
        /// <summary>
        /// NIE KURWA JEGO MAĆ RUSZAĆ TEGO. CHUJ WIE JAK TO DZIAŁA, ALE ,,U MNIE DZIAŁO"
        /// </summary>
        public static void Resume()
        {
            woe.Play();
            Source.Global.PlayerTaskPaused = false;
        }
        /// <summary>
        /// NIE KURWA JEGO MAĆ RUSZAĆ TEGO. CHUJ WIE JAK TO DZIAŁA, ALE ,,U MNIE DZIAŁO"
        /// </summary>
        public static void Pause()
        {
            woe.Pause();
            Source.Global.PlayerTaskPaused = true;

        }
        public static void Volume(int volume)
        {
            player.settings.volume = volume;
        }
        public static void Record(string downloadPath, string StationName)
        {
            try
            {
                HttpWebRequest request;
                const int bytesToGet = 4096;
                request = WebRequest.Create(downloadPath) as HttpWebRequest;
                string fileName = $"xddd{ DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
                fileName = System.Text.RegularExpressions.Regex.Replace(fileName, ":", " ");
                var file = File.Create(Source.Constants.RecordingsSaveFolder + $"/{fileName}.mp3");
                file.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    using (Stream fileStream = File.OpenWrite(Source.Constants.RecordingsSaveFolder + $"/{fileName}.mp3"))
                    {
                        byte[] bytes = new byte[bytesToGet];
                        int count = stream.Read(bytes, 0, bytesToGet);
                        while (count > 0)
                        {
                            fileStream.Write(bytes, 0, count);
                            count = stream.Read(bytes, 0, bytesToGet);


                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                return;
            }


        }
    }
}

