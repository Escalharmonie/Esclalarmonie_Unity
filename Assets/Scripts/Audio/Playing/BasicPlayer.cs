using System;
using Playlist;
using Playlist.Models;
using UnityEngine;

namespace Audio.Playing
{
    public class BasicPlayer: MonoBehaviour, IAudioPlayer
    {
        [HideInInspector]
        public AudioSource audioSource;

        private AudioClip? _clip;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                Debug.LogError("BasicPlayer is missing Audio Source");
                
                Destroy(gameObject);
            }
        }

        public void Setup(Track track, PlaylistSettings settings)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                Debug.LogError("BasicPlayer is missing Audio Source");
                
                return;
            }
            audioSource.Stop();
            audioSource.loop = false;
            _clip= track.Clip;
            audioSource.volume = 1;
        }

        public void Play()
        {
            if (_clip)
            {
                audioSource.PlayOneShot(_clip);
            }
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