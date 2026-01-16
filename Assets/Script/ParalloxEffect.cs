using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
 
public class ParadoxEffect : MonoBehaviour {
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float scrollTime = 0.5f;

    private float uvX = 0f;
    private float uvY = 0f;
    public enum ScrollDirection
    {
        left,
        right,
        up,
        down,
    }
    [SerializeField] private ScrollDirection scrollDirection;
    void Start()
    {
        if(scrollDirection == ScrollDirection.left)
        {
            ScrollLeft();
        }
        else if(scrollDirection == ScrollDirection.right)
        {
            ScrollRight();
        }
        else if(scrollDirection == ScrollDirection.up)
        {
            ScrollUp();
        }
        else if(scrollDirection == ScrollDirection.down)
        {
            ScrollDown();
        }
    }
    private void ScrollLeft()
    {
        DOTween.To(() => uvX, value =>
        {
            uvX = value;

            // cập nhật uvRect mỗi frame
            Rect rect = rawImage.uvRect;
            rect.x = uvX;
            rawImage.uvRect = rect;

        }, 1f, scrollTime)                            // 0 → 1 trong 2 giây
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);      
    }
    private void ScrollRight()
    {
        DOTween.To(() => uvX, value =>
        {
            uvX = value;

            // cập nhật uvRect mỗi frame
            Rect rect = rawImage.uvRect;
            rect.x = uvX;
            rawImage.uvRect = rect;

        }, 1f, scrollTime)                            // 0 → 1 trong 2 giây
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);       
    }
    private void ScrollUp()
    {
        DOTween.To(() => uvY, value =>
        {
            uvY = value;

            // cập nhật uvRect mỗi frame
            Rect rect = rawImage.uvRect;
            rect.y = uvY;
            rawImage.uvRect = rect;

        }, -1f, scrollTime)                            // 0 → 1 trong 2 giây
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);      
    }
    private void ScrollDown()
    {
        DOTween.To(() => uvY, value =>
        {
            uvY = value;

            // cập nhật uvRect mỗi frame
            Rect rect = rawImage.uvRect;
            rect.y = uvY;
            rawImage.uvRect = rect;

        }, 1f, scrollTime)                            // 0 → 1 trong 2 giây
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);      
    }

}
