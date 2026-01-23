using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneTransition : MonoBehaviour
{
    [SerializeField]private Animator sceneTransitionAnimator;
    public static SceneTransition Instance;
    private bool hideAnimFinish;
    private bool showAnimFinish;
    public enum SceneTransitionState
    {
        HideTransStart,
        HideTransFinish,
        ShowTransStart,
        ShowTransFinish
    }
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
    public void ResetValue()
    {
        hideAnimFinish = false;
        showAnimFinish = false;
    }
    //Hide Trans Start
    public void HideTransStart()
    {
        sceneTransitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        sceneTransitionAnimator.SetTrigger("SceneHide");
    }
    //Show trans Start
    public void ShowTransStart()
    {
        if(SceneManager.GetActiveScene().name == "InGame")
        {
            InGameSceneSetUp.Instance.ReadyFight();
            InGamePauseManager.Instance.HidePauseBtn();
            InGameSceneSetUp.Instance.PauseFight();
        }
        if(SceneManager.GetActiveScene().name == "LevelSelection")
        {
            Debug.Log("Can't move");
            PlayerPlaneControlInLevelSelect.Instance.SetCanMove(false);
        }
        sceneTransitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        sceneTransitionAnimator.SetTrigger("SceneShow");
    }
    //Event in Begin and End of Animation
    public void AnimationTriggerEvent(SceneTransitionState state)
    {
        if(state == SceneTransitionState.HideTransFinish)
        {
            hideAnimFinish = true;
        }
        if(state == SceneTransitionState.ShowTransFinish)
        {
            if(SceneManager.GetActiveScene().name == "InGame")
            {
                GameTutorial.Instance.StartTutorial();
            }
            if(SceneManager.GetActiveScene().name == "LevelSelection")
            {
                LevelSelectManager.Instance.SceneEnter();
            }
            showAnimFinish = true;
        }
    }
    //Check is hide trans finish
    public bool IsHideAnimationFinish()
    {
        return hideAnimFinish;
    }
    //Check is show trans finish
    public bool IsShowAnimationFinish()
    {
        return showAnimFinish;
    }
}
