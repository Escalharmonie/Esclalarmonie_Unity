using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ScrollArrows : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    }
    [Min(1)]
    [SerializeField] private int ItemSkip = 4;
    [SerializeField] private float TransitionDuration = 0.25f;
    [SerializeField] private Ease TransitionEasing = Ease.InOutSine;
    [SerializeField] private ScrollRect? ScrollArea;

    public void ScrollRight()
    {
        if (ScrollArea is null)
        {
            return;
        }

        ScrollArea.DOHorizontalNormalizedPos(CalculateHorizontalPosition(Direction.Right), TransitionDuration)
            .SetEase(TransitionEasing);
    }

    public void ScrollLeft()
    {
        if (ScrollArea is null)
        {
            return;
        }

        ScrollArea.DOHorizontalNormalizedPos(CalculateHorizontalPosition(Direction.Left), TransitionDuration)
            .SetEase(TransitionEasing);
    }

    private float CalculateHorizontalPosition(Direction direction)
    {
        if (ScrollArea != null)
        {
            float offset = ItemSkip / math.max(ScrollArea.content.transform.childCount, 1f);
            offset *= direction == Direction.Right ? 1f : -1f;
            return math.clamp(ScrollArea.horizontalNormalizedPosition + offset, 0f, 1f);
        }

        return 0;
    }
}