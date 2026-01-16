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
    private bool hideAnimStart;
    private bool showAnimStart;
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
    public void SceneHide()
    {
        sceneTransitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        sceneTransitionAnimator.SetTrigger("SceneHide");
    }
    public void SceneShow()
    {
        sceneTransitionAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        sceneTransitionAnimator.SetTrigger("SceneShow");
    }
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
                Time.timeScale = 0f;
                GameTutorial.Instance.StartTutorial();
            }
            if(SceneManager.GetActiveScene().name == "LevelSelection")
            {
                Time.timeScale = 1f;
                LevelSelectManager.Instance.SceneEnter();
            }
            showAnimFinish = true;
        }
        if(state == SceneTransitionState.ShowTransStart)
        {
            if(SceneManager.GetActiveScene().name == "InGame")
            {
                InGameSceneSetUp.Instance.ReadyFight();
            }
            showAnimStart = true;
        }
        if(state == SceneTransitionState.HideTransStart)
        {
            Time.timeScale = 0f;
            hideAnimStart = true;
        }

    }
    public bool IsHideAnimationFinish()
    {
        return hideAnimFinish;
    }
    public bool IsShowAnimationFinish()
    {
        return showAnimFinish;
    }
    public bool IsHideAnimationStart()
    {
        return hideAnimStart;
    }
    public bool IsShowAnimationStart()
    {
        return showAnimStart;
    }
}
