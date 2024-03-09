using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInfo : MonoBehaviour
{
    #region Parameters

    [SerializeField] private TextMeshProUGUI? NameText;
    [SerializeField] private TextMeshProUGUI? NameDataText;
    [SerializeField] private Image Icon;

    #endregion

    #region Public Members

    public string InfoName
    {
        get => _infoName;
        set
        {
            _infoName = value;

            if (NameText is not null)
            {
                NameText.text = _infoName;
            }
        }
    }

    public string InfoData
    {
        get => _infoData;
        set
        {
            _infoData = value;

            if (NameDataText is not null)
            {
                NameDataText.text = _infoName;
            }
        }
    }

    public Sprite? InfoIcon
    {
        get => _infoIcon;
        set
        {
            _infoIcon = value;

            if (Icon is not null)
            {
                Icon.sprite = _infoIcon;
            }
        }
    }

    #endregion

    #region Private Members

    private string _infoName = "";
    private string _infoData = "";
    private Sprite? _infoIcon;

    #endregion
}