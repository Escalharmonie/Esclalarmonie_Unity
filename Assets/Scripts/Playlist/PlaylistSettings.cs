using System;
using UnityEngine;

namespace Playlist
{
    [Serializable]
    public struct PlaylistSettings
    {
        public float AttackDuration;

        public float ReleaseDuration;

        public bool AudioIsLooping;

        public float HighpassCutoff;

        public float LowpassCutoff;

        public float Distortion;
        
        public PlaylistSettings(float attackDuration = 0.1f, float releaseDuration = 0.1f, bool audioIsLooping = false, float highpassCutoff = 22_000f, float lowpassCutoff = 10f, float distortion = 0)
        {
            AttackDuration = attackDuration;
            ReleaseDuration = releaseDuration;
            AudioIsLooping = audioIsLooping;
            HighpassCutoff = highpassCutoff;
            LowpassCutoff = lowpassCutoff;
            Distortion = distortion;
        }

        public void LogSettingsAsJson()
        {
            Debug.Log(JsonUtility.ToJson(this));
        } 
    }
}