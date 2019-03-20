using System;
using System.Threading;
using NAudio.Wave;

namespace MusicPlayer
{
    public class Mp3
    {
        private string MP3_PATH { get; set; }
        private WaveOutEvent outputDevice;
        private WaveStream waveStream;
        private AudioFileReader audioFile;
        private long position;

        public long Position
        {
            get { return position; }
            set { position = value; PositionChanged(); }
        }
        delegate void ValueChanged(int CurrentValue);
        event ValueChanged ValueHasBeenChanged;
        private void PositionChanged()
        {      
        }
        public long Length { get; set; }

        private void UpdatePosition()
        {
            while((outputDevice.PlaybackState == PlaybackState.Playing))
            {
                TimeSpan timeSpan = audioFile.CurrentTime;
                Position = (timeSpan.Days * 86400) + (timeSpan.Hours * 3600) + (timeSpan.Minutes * 60) + timeSpan.Seconds;
            }
        }

        private void UpdateLength()
        {
            TimeSpan timeSpan = audioFile.TotalTime;
            Length =(timeSpan.Days * 86400) + (timeSpan.Hours * 3600) + (timeSpan.Minutes * 60) + timeSpan.Seconds;
        }
        public void Rewind(int value)
        {
            //audioFile.Position = 20000000 ;
            audioFile.Skip(value);
        }
        public void Volume(float value)
        {
            outputDevice.Volume = value;
        }
        public void Stop()
        {
            outputDevice.Stop();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }
    }
}
