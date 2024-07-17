using NAudio.Wave;

namespace Hangman.UI
{
    public static class Audio
    {
        private static readonly string _path = @"Data\DontFearTheReaper.mp3";
        private static WaveOutEvent? _outputDevice;
        private static AudioFileReader? _audioFile;
        private static bool _isPlaying;

        public static void Play()
        {
            if (!File.Exists(_path))
            {
                Console.WriteLine("Audio file not found.");
                return;
            }

            _audioFile = new AudioFileReader(_path);
            _outputDevice = new WaveOutEvent();

            // Hook up playback finished event to restart playback
            _outputDevice.PlaybackStopped += (sender, args) =>
            {
                if (_isPlaying)
                {
                    _audioFile.Position = 0;
                    _outputDevice.Play();
                }
            };

            _outputDevice.Init(_audioFile);
            _outputDevice.Play();
            _isPlaying = true;
        }

        public static void Stop()
        {
            _isPlaying = false;
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();
        }
    }
}
