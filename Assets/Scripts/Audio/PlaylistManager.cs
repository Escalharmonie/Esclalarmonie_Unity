using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Audio.Loading;
using Playlist;
using Playlist.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    public class PlaylistManager : MonoBehaviour
    {
        [SerializeField] private MixerManager? MixerManagerRef;
        
        [SerializeField] private UnityEvent<PlaylistData> OnPlaylistLoaded;
        
        public async void LoadPlaylist(PlaylistThumbnailData data)
        {
           PlaylistData? playlist = await PlaylistLoader.LoadPlaylist(data.Path);

           if (playlist is null)
           {
               return;
           }
           
           PlaylistData playlistData = ((PlaylistData)playlist);
           PlaylistSettings settings = playlistData.Settings;

           if (MixerManagerRef == null)
           {
               return;
           }

           MixerManagerRef.SetLowpassCutoff(settings.LowpassCutoff);
           MixerManagerRef.SetHighpassCutoff(settings.HighpassCutoff);
           MixerManagerRef.SetDistortionValue(settings.Distortion);
           
           OnPlaylistLoaded.Invoke(playlistData);
           Debug.Log(playlist);
        }
        
        public void LoadPlaylistWithDelay(PlaylistThumbnailData data)
        {
            Task.Run(() => LoadPlaylist(data));
        }

        private IEnumerator LoadWithDelay(PlaylistThumbnailData data)
        {
            var task = Task.Run(() => LoadPlaylist(data));
            yield return new WaitUntil(() => task.IsCompleted);
        }
    }
}
