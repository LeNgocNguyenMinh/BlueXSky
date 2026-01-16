using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Nhớ import DOTween

public class FloatEffect : MonoBehaviour
{
    public float amplitude = 10f;       // Biên độ dao động (độ cao trôi nổi)
    public float duration = 1f;         // Thời gian một chu kỳ đi lên/đi xuống
    public float phaseOffset = 0f;      // Độ trễ pha (nếu cần dùng cho nhiều text khác nhau)

    private Vector2 startPos;

    void OnEnable()
    {
        startPos = transform.localPosition;

        // Bắt đầu hiệu ứng với độ trễ phaseOffset (nếu có)
        transform.DOLocalMoveY(startPos.y + amplitude, duration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetDelay(phaseOffset)
                 .SetUpdate(true);
    }

    void OnDisable()
    {
        // Hủy tween khi object bị disable để tránh lỗi hoặc memory leak
        transform.DOKill();
    }
}
