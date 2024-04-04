using Playlist;
using Playlist.Models;
using UnityEngine;

namespace Audio.Playing
{
    public class EnhancedBasicPlayer : MonoBehaviour, IAudioPlayer
    {
        [SerializeField] private GameObject OneTimePlayer;
    
        private AudioClip? _clip;
        private PlaylistSettings? _settings;
        
        public void Setup(Track track, PlaylistSettings settings)
        {
            _clip = track.Clip;
            _settings = settings;
        }

        public void Play()
        {
            GameObject newInstance = Instantiate(OneTimePlayer, null, true);

            OneTimePlayer? player = newInstance.GetComponent<OneTimePlayer>();

            if (player is null)
            {
                Debug.LogError("Instantiated one time player is missing its script");
                return;
            }

            if (_clip is null)
            {
                Debug.LogError("Missing a clip");
                return;
            }

            if (_settings is null)
            {
                return;
            }

            player.PlayOnce(_clip, _settings.Value);
        }

        public void Stop()
        {
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
