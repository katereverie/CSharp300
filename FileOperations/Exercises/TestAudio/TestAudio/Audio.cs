using NAudio.Wave;

namespace TestAudio
{
    public static class Audio
    {
        private static string _path = @"D:\GitHub\CloneRepository\C#300\FileOperations\Exercises\TestAudio\TestAudio\bin\Debug\net8.0\DontFearTheReaper.mp3";

        public static void Play()
        {
            try
            {
                if (!File.Exists(_path))
                {
                    Console.WriteLine("Path does not exist.");
                    return;
                }

                using (var audioFile = new AudioFileReader(_path))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    Console.WriteLine("Audio playing...");
                    // Optionally, you can wait for audio to finish before continuing
                    // outputDevice.PlaybackStopped += (sender, args) => Console.WriteLine("Audio playback stopped.");
                    // while (outputDevice.PlaybackState == PlaybackState.Playing) { }
                    Console.WriteLine("Press any key to stop audio...");
                    Console.ReadKey();
                    outputDevice.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during audio playback: {ex.Message}");
            }
        }
    }
}
