using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class NotifPopUp : MonoBehaviour
{
    public static NotifPopUp Instance;
    [SerializeField]private RectTransform notifPanel;
    [SerializeField]private TextMeshProUGUI notifText;
    [SerializeField]private Vector3 textHidePos;
    [SerializeField]private Vector3 textShowPos;
    [SerializeField]private float displayDuration;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowNotif(string text)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            notifText.text = text;
        });
        seq.Append(notifPanel.DOAnchorPos(textShowPos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        seq.AppendInterval(displayDuration);
        seq.Append(notifPanel.DOAnchorPos(textHidePos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
    }
}
