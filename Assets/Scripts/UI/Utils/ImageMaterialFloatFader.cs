using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class ImageMaterialFloatFader : MonoBehaviour
{
    #region Parameters

    [SerializeField] private float TransitionDuration = 0.25f;
    [SerializeField] private Ease TransitionEasing = Ease.InOutSine;

    #endregion
    
    #region Private Members

    private Image? _image;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        
        // Create a material Instance for the image
        _image.material = new Material(_image.material);
        
        if (_image is null)
        {
            WarnMissingImage();
        }
    }

    #region Public Functions
    
    public void SetFloatEase(float newValue, string propertyName ,float? transitionDuration = null, Ease? transitionEasing = null)
    {
        transitionDuration ??= TransitionDuration;
        transitionEasing ??= TransitionEasing;
        
        
        if (_image is not null)
        {
            _image.material.DOFloat(newValue, propertyName, (float)transitionDuration).SetEase((Ease)transitionEasing);
        }
        else
        {
            WarnMissingImage();
        }
    }

    public void SetFloat(float newValue, string propertyName)
    {
        if (_image is not null)
        {
            _image.material.SetFloat(propertyName,newValue);
        }
        else
        {
            WarnMissingImage();
        }
    }

    #endregion

    #region Private Functions

    private void WarnMissingImage()
    {
        Debug.LogWarning("Image Ref Is Missing");
    }

    #endregion
}
