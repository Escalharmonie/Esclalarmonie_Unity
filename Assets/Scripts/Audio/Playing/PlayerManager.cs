using System;
using System.Collections.Generic;
using Playlist;
using Playlist.Models;
using Serial;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Audio.Playing
{
    public class PlayerManager: MonoBehaviour
    {
        [SerializeField] private GameObject BasicPlayerPefab;
        [SerializeField] private GameObject LoopingPlayerPefab;

        private PlaylistSettings _currentSettings;

        private List<IAudioPlayer> players = new();  

        private void Start()
        {
            if (BasicPlayerPefab == null)
            {
                Debug.LogError("Player Manager is missing audio prefab");
            }
        }

        public void UpdatePlaylist(PlaylistData playlist)
        {
            _currentSettings = playlist.Settings;

            foreach (IAudioPlayer player in players)
            {
                player.Destroy();
            }
            
            List<IAudioPlayer> newPlayers = new();

            foreach (Track track in playlist.Tracks)
            {
                GameObject playerPrefab = playlist.Settings.AudioIsLooping ? LoopingPlayerPefab : BasicPlayerPefab;
                GameObject playerObject = Instantiate(playerPrefab, transform);
                var player = playerObject.GetComponent<IAudioPlayer>();
                
                player.Setup(track, playlist.Settings);
                
                newPlayers.Add(player);
            }

            players = newPlayers;
        }

        public void PlaySound(List<KeyState> keyStates)
        {

            string baba = "";
            foreach (var state in keyStates)
            {
                baba += state.ToString() + ",";
            }
            
            for (int i = 0; i < keyStates.Count; i++)
            {
                KeyState state = keyStates[i];

                if (players.Count > i)
                {
                    if (state == KeyState.JustPressed)
                    {
                        players[i].Play();
                        continue;
                    }

                    if (state == KeyState.JustReleased)
                    {
                        players[i].Stop();
                    }
                }
            }
        }
    }
}