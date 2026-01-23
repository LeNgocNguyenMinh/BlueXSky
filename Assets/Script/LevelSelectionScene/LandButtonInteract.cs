using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class LandButtonInteract : MonoBehaviour
{
    public static LandButtonInteract Instance;
    [SerializeField] private Button landBtn;
    private bool isOnLand;
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
    public void OnEnable()
    {
        landBtn.onClick.AddListener(ButtonInteractive);
    }
    public void ToInGameScene()
    {
        SceneTransition.Instance.HideTransStart();
        StartCoroutine(LoadFightScene("InGame"));
    }
    private void ButtonInteractive()
    {
        PlayerPlaneControlInLevelSelect.Instance.SetCanMove(false);
        if(isOnLand)
        {
            ToInGameScene();
        }
        else
        {
            HomeBase.Instance.EnterHomeBase();
        }
    }
    public void OnLand()
    {
        isOnLand = true;
    }
    public void OnHomeBase()
    {
        isOnLand = false;
    }
    private IEnumerator LoadFightScene(string sceneName)
    {
        yield return null;
        // Start to load but not activate the scene immediately
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // wait for the scene to load
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f && SceneTransition.Instance.IsHideAnimationFinish()) 
            { 
                SceneManager.sceneLoaded += OnFightSceneLoaded;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    private void OnFightSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnFightSceneLoaded;
        SceneTransition.Instance.ShowTransStart();
    }
    public void EnableLandButton()
    {
        landBtn.interactable = true;
    }
    public void DisableLandButton()
    {
        landBtn.interactable = false;
    }
    public void HideLandButton()
    {
        landBtn.gameObject.SetActive(false);
    }
    public void ShowLandButton()
    {
        landBtn.gameObject.SetActive(true);
    }
}
