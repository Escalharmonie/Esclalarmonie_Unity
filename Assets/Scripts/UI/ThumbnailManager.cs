using System;
using System.Collections.Generic;
using Playlist.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    using ThumbnailInfo = Tuple<GameObject, PlaylistThumbnailData>;

    public class ThumbnailManager : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private GameObject? ThumbnailPrefab;

        #endregion

        #region Events

        public UnityEvent OnThumbnailsLoaded;

        public UnityEvent<PlaylistThumbnailData> OnPlaylistThumbnailClicked;

        #endregion

        #region Private Members

        private List<ThumbnailInfo> thumbnailList = new();

        private ThumbnailInfo? lastThumbnail;
        
        #endregion

        public void GenerateThumbnails(List<PlaylistThumbnailData> thumbnailDataList)
        {
            thumbnailList.Clear();
            foreach (Transform child in transform) Destroy(child.gameObject);

            foreach (PlaylistThumbnailData thumbnailData in thumbnailDataList)
            {
                GameObject thumbnail = Instantiate(ThumbnailPrefab, transform);

                ThumbnailVisualUpdater? visualUpdater = thumbnail.GetComponent<ThumbnailVisualUpdater>();
                var currentThumbnail = new ThumbnailInfo(thumbnail, thumbnailData);

                thumbnailList.Add(currentThumbnail);

                thumbnail.GetComponent<Button>().onClick.AddListener(() => ThumbnailClicked(currentThumbnail));

                visualUpdater.UpdateVisuals(thumbnailData);
            }

            OnThumbnailsLoaded.Invoke();
        }

        private void ThumbnailClicked(ThumbnailInfo info)
        {
            foreach (var thumbnail in thumbnailList)
            {
                bool isClickedThumbnail = thumbnail.Item2.Path == info.Item2.Path;
                if (isClickedThumbnail)
                {
                    Texture2D? thumbnailTexture = thumbnail.Item2.Thumbnail?.texture;
                    Color? mainColor = null;

                    if (thumbnailTexture is not null)
                    {
                        ColorThief.ColorThief dominant = new();
                        
                        mainColor = dominant.GetColor(thumbnailTexture).UnityColor;
                    }

                    if (thumbnail.Item2.Path != lastThumbnail?.Item2.Path)
                    {
                        ThemeManager.Instance?.UpdateThemePalette(mainColor, true);
                        OnPlaylistThumbnailClicked.Invoke(thumbnail.Item2);
                    }
                }

                thumbnail.Item1.GetComponent<ThumbnailHighlighter>().IsHighlighted = isClickedThumbnail;
            }
        }
    }
}