using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Playlist.Models
{
    // All data necessary to display the playlist in the UI
    [Serializable]
    public struct PlaylistThumbnailData
    {
        public string Name;

        public string Path;

        public Sprite? Thumbnail;

        public PlaylistThumbnailData(string name, string path, Sprite? thumbnail)
        {
            Name = name;
            Path = path;
            Thumbnail = thumbnail;
        }

        public override string ToString()
        {
            string output = $"Playlist \"{Name}\" \n" +
                            $"Stored at \"{Path}\" \n";

            output += Thumbnail is not null ? "With thumbnail\n" : "Without thumbnail\n";
            return output;
        }
    }
}
