using Playlist.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ThumbnailVisualUpdater : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private TextMeshProUGUI? ThumbnailText;

        [SerializeField] private Image? ThumbnailImage;

        #endregion

        #region Private Members

        private Sprite? defaultImage;

        #endregion

        #region Start

        private void Start()
        {
            if (ThumbnailImage)
            {
                defaultImage = ThumbnailImage is not null ? ThumbnailImage.sprite : null;
            }
        }

        #endregion

        #region Public Functions

        public void UpdateVisuals(PlaylistThumbnailData thumbnailData)
        {
            if (ThumbnailText is not null)
            {
                ThumbnailText.text = thumbnailData.Name;
            }

            if (ThumbnailImage is null)
            {
                return;
            }

            if (thumbnailData.Thumbnail is not null)
            {
                ThumbnailImage.sprite = thumbnailData.Thumbnail;
            }
            else if (defaultImage != null)

            {
                ThumbnailImage.sprite = defaultImage;
            }
        }

        #endregion
    }
}