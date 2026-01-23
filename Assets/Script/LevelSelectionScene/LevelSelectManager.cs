using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance;
    [SerializeField]private CurrentSelectLevel currentSelectLevel;
    [SerializeField]private Button backToMainMenuBtn;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SceneEnter()
    {
        Time.timeScale = 1f;
        PlayerPlaneControlInLevelSelect.Instance.SetCanMove(true);
        SoundControl.Instance.LevelSelectMusicPlay();
        backToMainMenuBtn.interactable = true;
        NotifPopUp.Instance.ShowNotif("Now play 'Cyberpunk' song.", 1f);
    }
    private void OnEnable()
    {
        backToMainMenuBtn.onClick.AddListener(ToMainMenu);
    }
    private void ToMainMenu()
    {
        PlayerPlaneControlInLevelSelect.Instance.SetCanMove(false);
        backToMainMenuBtn.interactable = false;
        SceneTransition.Instance.HideTransStart();
        StartCoroutine(LoadGameScene("MainMenu"));
    }
    private IEnumerator LoadGameScene(string sceneName)
    {
        yield return new WaitForSecondsRealtime(1f);
        // Start to load but not activate the scene immediately
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
        SceneTransition.Instance.ShowTransStart();
    }
    public void SetCurrentSelectLevelID(string levelID)
    {
        currentSelectLevel.currentSelectLevelID = levelID;
    }
    public string GetCurrentSelectLevelID()
    {
        return currentSelectLevel.currentSelectLevelID;
    }
}
