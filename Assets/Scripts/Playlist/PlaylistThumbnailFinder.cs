using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Playlist.Models;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Playlist
{
    public class PlaylistThumbnailFinder : MonoBehaviour
    {
        [FormerlySerializedAs("DebugPlaylistDirectoryPath")] [SerializeField]
        private string PlaylistDirectoryPath = "";

        [SerializeField] private ThumbnailManager? Generator;

        // Start is called before the first frame update
        private void Start()
        {
#if UNITY_EDITOR

            if (string.IsNullOrEmpty(PlaylistDirectoryPath))
            {
                Debug.Log("Path is empty");
                return;
            }

            if (!Directory.Exists(PlaylistDirectoryPath))
            {
                Debug.Log("Path is invalid directory");
                return;
            }
            
#endif
#if UNITY_STANDALONE

            PlaylistDirectoryPath = Directory.GetCurrentDirectory() + "/Playlists";
#endif

            if (string.IsNullOrEmpty(PlaylistDirectoryPath))
            {
                Debug.Log("Path is empty");
                return;
            }

            if (!Directory.Exists(PlaylistDirectoryPath))
            {
                Debug.Log("Path is invalid directory");
                return;
            }

            DirectoryInfo playlistsDirectory = new DirectoryInfo(PlaylistDirectoryPath);

            var thumbnailDataList = LoadPlaylistThumbnails(playlistsDirectory);

            // foreach (PlaylistThumbnailData thumbnailData in thumbnailDataList) Debug.Log(thumbnailData);

            Sprite? thumbnail = thumbnailDataList.First().Thumbnail;

            ColorThief.ColorThief palette = new ColorThief.ColorThief();
            var colors = palette.GetPalette(thumbnail.texture, 6);

            Generator?.GenerateThumbnails(thumbnailDataList);
        }

        private static List<PlaylistThumbnailData> LoadPlaylistThumbnails(DirectoryInfo playlistsDirectory)
        {
            var thumbnails = new List<PlaylistThumbnailData>();

            foreach (DirectoryInfo directory in playlistsDirectory.EnumerateDirectories())
            {
                Texture2D? thumbnailImage = null;

                var filteredFiles = SearchThumbnailImages(directory);

                if (filteredFiles.Count != 0)
                {
                    thumbnailImage = TextureUtils.LoadTextureFromFile(filteredFiles.First());
                }

                Sprite? thumbnailSprite = null;
                if (thumbnailImage != null)
                {
                    thumbnailSprite = Sprite.Create(
                        thumbnailImage,
                        new Rect(0, 0, thumbnailImage.width, thumbnailImage.height),
                        new Vector2(0.5f, 0.5f), 100
                    );
                }

                PlaylistThumbnailData thumbnail =
                    new PlaylistThumbnailData(directory.Name, directory.FullName, thumbnailSprite);
                thumbnails.Add(thumbnail);
            }

            return thumbnails;
        }

        private static List<string> SearchThumbnailImages(DirectoryInfo directoryInfo)
        {
            return Directory
                .EnumerateFiles(directoryInfo.FullName, "thumbnail.*")
                .Where(file =>
                    TextureUtils.ValidFiletypes.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                .ToList();
        }
    }
}