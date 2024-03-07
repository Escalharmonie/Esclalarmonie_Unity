using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Playlist.Models;
using UnityEngine;
using Utils;

namespace Playlist
{
    public class PlaylistThumbnailFinder : MonoBehaviour
    {
        [SerializeField] private string PlaylistDirectoryPath = "";
        // Start is called before the first frame update

        private void Start()
        {
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

            var playlistsDirectory = new DirectoryInfo(PlaylistDirectoryPath);

            var thumbnailDataList = LoadPlaylistThumbnails(playlistsDirectory);

            foreach (PlaylistThumbnailData thumbnailData in thumbnailDataList) Debug.Log(thumbnailData);

            Texture2D? thumbnail = thumbnailDataList.First().Thumbnail;

            if (thumbnail)
            {
                GetComponent<SpriteRenderer>().sprite = Sprite.Create(thumbnail, new Rect(0.0f, 0.0f, thumbnail!.width, thumbnail!.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
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

                var thumbnail = new PlaylistThumbnailData(directory.Name, directory.FullName, thumbnailImage);
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