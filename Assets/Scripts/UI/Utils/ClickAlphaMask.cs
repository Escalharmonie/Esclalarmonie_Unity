using UnityEngine;
using UnityEngine.UI;

namespace UI.Utils
{
    [RequireComponent(typeof(Image))]
    public class ClickAlphaMask : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private float MinimumAlphaThreshold = 0.1f;

        #endregion
    
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = MinimumAlphaThreshold;
        }
    }
}
