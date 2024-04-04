using System.Collections;
using Playlist.Models;
using UnityEngine;
using DG.Tweening;


namespace Audio.Playing
{
    public class OneTimePlayer : MonoBehaviour
    {
        public void PlayOnce(AudioClip clip, PlaylistSettings settings)
        {
            AudioSource? source = GetComponent<AudioSource>();

            if (source is null)
            {
                Debug.LogError("One Time player does not have an audio source attached");
                Destroy(gameObject);
                return;
            }

            StartCoroutine(PlayOnce(source, clip, settings));
        }

        IEnumerator PlayOnce(AudioSource source, AudioClip clip, PlaylistSettings settings)
        {
            source.loop = false;
            source.volume = 0;
            source.clip = clip;
            source.Play();
            source.DOFade(1, Mathf.Min(settings.AttackDuration, clip.length)).SetEase(Ease.InSine);
            yield return new WaitForSeconds(clip.length + 0.1f);
        
            Destroy(gameObject);
        }
    }
}
