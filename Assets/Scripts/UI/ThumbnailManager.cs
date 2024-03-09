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

        #endregion
        
        #region Private Members

        private List<ThumbnailInfo> thumbnailList = new();
        
        #endregion
        
        public void GenerateThumbnails(List<PlaylistThumbnailData> thumbnailDataList)
        {
            foreach (var thumbnailData in thumbnailDataList)
            {
                // foreach (Transform child in transform)
                // {
                //     Destroy(child.gameObject);
                // }
                //
                // thumbnailList.Clear();
                
                GameObject thumbnail = Instantiate(ThumbnailPrefab, transform);
                var visualUpdater = thumbnail.GetComponent<ThumbnailVisualUpdater>();

                var currentThumbnail = new ThumbnailInfo(thumbnail,thumbnailData);
                
                thumbnailList.Add(currentThumbnail);
                
                thumbnail.GetComponent<Button>().onClick.AddListener(() => OnThumbnailClicked(currentThumbnail));
                
                
                visualUpdater.UpdateVisuals(thumbnailData);
            }
            
            OnThumbnailsLoaded.Invoke();
        }

        private void OnThumbnailClicked(ThumbnailInfo info)
        {
            foreach (ThumbnailInfo? thumbnail in thumbnailList)
            {
                bool isClickedThumbnail = thumbnail.Item2.Name == info.Item2.Name;

                thumbnail.Item1.GetComponent<ThumbnailHighlighter>().IsHighlighted = isClickedThumbnail;
            }
        }
    }
}
