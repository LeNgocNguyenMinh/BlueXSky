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
    [SerializeField]private TutorialTextInfo[] tutorialTextList;
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
    //Start the tutorial sequence after show trans finish
    public void StartTutorial()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Sequence seq = DOTween.Sequence();
        seq.Append(rbRectPanel.DOAnchorPos(rbShowPos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        for(int i = 0; i < tutorialTextList.Length; i++)
        {
            string text = tutorialTextList[i].tutorialText;

            seq.AppendCallback(() =>
            {
                tutorialText.text = text;
            });
            seq.Append(tutorialPanel.DOAnchorPos(textShowPos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
            seq.AppendInterval(tutorialTextList[i].displayDuration);
            seq.Append(tutorialPanel.DOAnchorPos(textHidePos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
            seq.AppendInterval(0.2f);
        }
        seq.Append(rbRectPanel.DOAnchorPos(rbHidePos, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        seq.OnComplete(() =>
        {
            Time.timeScale = 1f;
            InGamePauseManager.Instance.ShowPauseBtn();
            InGameSceneSetUp.Instance.StartFight();
        });
    }
}
[System.Serializable]
public class TutorialTextInfo
{
    public string tutorialText;
    public float displayDuration;
}
