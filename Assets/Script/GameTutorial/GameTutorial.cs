using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameTutorial : MonoBehaviour
{
    public static GameTutorial Instance;
    [SerializeField]private RectTransform tutorialPanel;
    [SerializeField]private TextMeshProUGUI tutorialText;
    [SerializeField]private Vector3 textHidePos;
    [SerializeField]private Vector3 textShowPos;
    [SerializeField]private float displayDuration;
    [SerializeField]private Animator animator;
    [SerializeField]private RectTransform rbRectPanel;
    [SerializeField]private Vector3 rbHidePos;
    [SerializeField]private Vector3 rbShowPos;
    public string[] tutorialTexts = { "Welcome to the field captain!", "Avoid the RED bullet", "Collect the BLUE one", "Ready", "3", "2", "1", "Go!"};
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
    public void StartTutorial()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Sequence seq = DOTween.Sequence();
        seq.Append(rbRectPanel.DOAnchorPos(rbShowPos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        for(int i = 0; i < tutorialTexts.Length; i++)
        {
            string text = tutorialTexts[i];

            seq.AppendCallback(() =>
            {
                tutorialText.text = text;
            });
            seq.Append(tutorialPanel.DOAnchorPos(textShowPos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
            seq.AppendInterval(displayDuration);
            seq.Append(tutorialPanel.DOAnchorPos(textHidePos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
            seq.AppendInterval(0.2f);
        }
        seq.Append(rbRectPanel.DOAnchorPos(rbHidePos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        seq.OnComplete(() =>
        {
            Time.timeScale = 1f;
            InGameSceneSetUp.Instance.StartFight();
        });
    }
}
