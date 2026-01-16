using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGamePauseManager : MonoBehaviour
{
    public static InGamePauseManager Instance;
    [SerializeField]private Animator animator;
    [Header("Text Image")]
    [SerializeField]private Image textImage;
    [SerializeField]private Sprite pauseTextSprite;
    [SerializeField]private Sprite overTextSprite;
    [SerializeField]private Sprite winTextSprite;
    [Header("Pause Menu")]
    [SerializeField]private GameObject pausePanel;
    [SerializeField]private Button pauseBtn;
    [SerializeField]private Button resumeBtn;
    [SerializeField]private Button chooseLevelPauseBtn;
    [Header("Game Over Menu")]
    [SerializeField]private GameObject gameOverPanel;
    [SerializeField]private Button chooseLevelOverBtn;
    [SerializeField]private Button restartLevelBtn;
    [Header("Win Menu")]
    [SerializeField]private GameObject winPanel;
    [SerializeField]private Button nextLevelWinBtn;

    private bool isPaused = false;
    private bool isOver = false;
    private bool isWin = false;

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
    public enum AnimState
    {
        PauseOnAnimStart,
        PauseOnAnimFinish,
        PauseOffAnimStart,
        PauseOffAnimFinish,
    }
    public void StartSetUp()
    {
        Debug.Log("1");
        textImage.gameObject.SetActive(false);
        Debug.Log(textImage.gameObject.activeSelf);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        
    }
    private void OnEnable()
    {
        Debug.Log("2");
        pauseBtn.onClick.AddListener(PauseGameOn);
        resumeBtn.onClick.AddListener(ResumeGame);
        chooseLevelPauseBtn.onClick.AddListener(ToLevelSelectScene);
        chooseLevelOverBtn.onClick.AddListener(ToLevelSelectScene);
        restartLevelBtn.onClick.AddListener(RestartLevel);
        nextLevelWinBtn.onClick.AddListener(ToLevelSelectScene);
    }
    private void PauseGameOn()
    {
        Debug.Log("3");
        pauseBtn.gameObject.SetActive(false);
        isPaused = true;
        textImage.sprite = pauseTextSprite;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOn");
    }

    private void ResumeGame()
    {
        Debug.Log("4");
        textImage.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOff");
    }
    public void GameOverMenuOn()
    {
        isOver = true;
        textImage.sprite = overTextSprite;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOn");
    }
    private void GameOverMenuOff()
    {
        textImage.gameObject.SetActive(false);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOff");
    }
    private void RestartLevel()
    {
        InGameSceneSetUp.Instance.RestartFight();
        gameOverPanel.SetActive(false);
        GameOverMenuOff();
    }
    public void WinGameMenuOn()
    {
        isWin = true;
        textImage.sprite = winTextSprite;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOn");
    }
    private void ToLevelSelectScene()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        StartCoroutine(LoadGameScene("LevelSelection"));
    }
    public void AnimationTriggerEvent(AnimState animState)
    {
        if(animState == AnimState.PauseOnAnimFinish)
        {
            textImage.gameObject.SetActive(true);
            if(isPaused)
            {
                pausePanel.SetActive(true);
            }
            else if(isOver)
            {
                gameOverPanel.SetActive(true);
            }
            else if(isWin)
            {
                winPanel.SetActive(true);
            }
        }
        else if(animState == AnimState.PauseOffAnimFinish)
        {
            Time.timeScale = 1f;
            animator.updateMode = AnimatorUpdateMode.Normal;
            if(isPaused)
            {
                pauseBtn.gameObject.SetActive(true);
                pausePanel.SetActive(false);
                isPaused = false;
            }
            else if(isOver)
            {
                gameOverPanel.SetActive(false);
                isOver = false;
            }
            else if(isWin)
            {
                winPanel.SetActive(false);
                isWin = false;
            }
        }
        else if(animState == AnimState.PauseOnAnimStart)
        {
            Time.timeScale = 0f;
        }
        else if(animState == AnimState.PauseOffAnimStart)
        {
        }
    }
    private IEnumerator LoadGameScene(string sceneName)
    {
        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f) 
            { 
                SceneManager.sceneLoaded += OnSceneLoaded;
                operation.allowSceneActivation = true;

            }
            yield return null;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneTransition.Instance.SceneShow();
    }
    public bool GetIsPause()
    {
        return isPaused;
    }
}
