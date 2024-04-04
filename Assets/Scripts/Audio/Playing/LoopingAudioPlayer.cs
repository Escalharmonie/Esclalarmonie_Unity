using System;
using Playlist;
using Playlist.Models;
using UnityEngine;
using DG.Tweening;

namespace Audio.Playing
{
    public class LoopingAudioPlayer: MonoBehaviour, IAudioPlayer
    {
        [HideInInspector]
        public AudioSource audioSource;

        private AudioClip? _clip;
        private PlaylistSettings _settings;

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

            _settings = settings;
            
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip= track.Clip;
            audioSource.volume = 0;
            audioSource.Play();
        }

        public void Play()
        {
            audioSource.DOFade(1, _settings.AttackDuration).SetEase(Ease.InSine);
        }

        public void Stop()
        {
            audioSource.DOFade(0, _settings.AttackDuration).SetEase(Ease.OutSine);
        }

        public void Destroy()
        {
            audioSource.DOFade(0, _settings.AttackDuration).SetEase(Ease.OutSine).OnComplete(() => Destroy(gameObject));
        }
    }
}