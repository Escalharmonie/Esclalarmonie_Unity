using Playlist;
using Playlist.Models;

namespace Audio.Playing
{
    public interface IAudioPlayer
    {
        void Setup(Track track, PlaylistSettings settings);
        void Play();
        void Stop();
        void Destroy();
    }
}