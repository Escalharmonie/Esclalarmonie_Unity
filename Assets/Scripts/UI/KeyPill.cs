using TMPro;
using UI;
using UnityEngine;

public class KeyPill : MonoBehaviour
{
    [SerializeField] private TMP_Text? InfoText;
    [SerializeField] private PillHighlighter? Pill;

    public void UpdateText(string newText)
    {
        if (InfoText != null)
        {
            InfoText.text = newText;
        }
        else
        {
            Debug.LogWarning("KeyPill is missing its TMP_Text reference");
        }
    }
    
    public void SetKeyHighlight(bool isHighlighted)
    {
        if (Pill != null)
        {
            Pill.IsHighlighted = isHighlighted;
        }
        else
        {
            Debug.LogWarning("KeyPill is missing its PillHighlighter reference");
        }
    }
}
