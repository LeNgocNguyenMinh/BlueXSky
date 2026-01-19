using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

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
        textImage.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        isPaused = false;
        isOver = false;
        isWin = false;
        
    }
    private void OnEnable()
    {
        pauseBtn.onClick.AddListener(PauseGameOn);
        resumeBtn.onClick.AddListener(ResumeGame);
        chooseLevelPauseBtn.onClick.AddListener(ToLevelSelectScene);
        chooseLevelOverBtn.onClick.AddListener(ToLevelSelectScene);
        restartLevelBtn.onClick.AddListener(RestartLevel);
        nextLevelWinBtn.onClick.AddListener(ToLevelSelectScene);
    }
    private void PauseGameOn()
    {
        isPaused = true;
        pauseBtn.gameObject.SetActive(false);
        textImage.sprite = pauseTextSprite;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOn");
    }

    private void ResumeGame()
    {
        isPaused = false;
        textImage.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOff");
    }
    public void SetPauseGameActive(bool value)
    {
        pauseBtn.gameObject.SetActive(value);
    }
    public void GameOverMenuOn()
    {
        isOver = true;
        pauseBtn.gameObject.SetActive(false);
        textImage.sprite = overTextSprite;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("PauseOn");
    }
    private void GameOverMenuOff()
    {
        isOver = false;
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
        pauseBtn.gameObject.SetActive(false);
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
            pauseBtn.gameObject.SetActive(true);
            pausePanel.SetActive(false);
            winPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            isPaused = false;
            isWin = false;
            isOver = false;
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
