using System.Windows.Media;

//There's no flag in the MediaPlayer class that indicates if a soundtrack is currently playing
//so inheritance is my workaround. Note that none of the methods can be overridden
namespace LoopingAudio_net
{
    internal class MediaPlayer_ : MediaPlayer
    {
        /// <summary>
        /// Is the media not Paused, Stopped, Closed, Null... etc
        /// </summary>
        internal bool IsSongPlaying { get; private set; } //

        public MediaPlayer_() : base()
        {
            IsSongPlaying = false;
        }

        public void PlaySong()
        {
            IsSongPlaying = true;
            Play();
        }

        public void PauseSong(bool alert = false)
        {
            if(!alert)
                IsSongPlaying = false;
            Pause();
        }

        public void CloseAndStop()
        {
            Close();
            IsSongPlaying = false;
        }
    }
}
