using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Utils
{
    public class TextureUtils
    {
        public static readonly List<string> ValidFiletypes = new()
        {
            ".png",
            ".jpeg",
            ".jpg"
        };

        /// <summary>
        ///     Load a texture from an image of type png, jpeg or jpg
        /// </summary>
        /// <param name="path"> </param>
        /// <returns></returns>
        public static Texture2D? LoadTextureFromFile(string path)
        {
            foreach (string? type in ValidFiletypes)
                if (path.EndsWith(type))
                {
                    Texture2D? texture = null;
                    byte[] fileData;

                    if (File.Exists(path))
                    {
                        fileData = File.ReadAllBytes(path);
                        texture = new Texture2D(1, 1);
                        texture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
                    }

                    return texture;
                }

            return null;
        }
    }
}