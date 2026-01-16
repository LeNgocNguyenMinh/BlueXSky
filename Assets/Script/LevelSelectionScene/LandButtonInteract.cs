using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LandButtonInteract : MonoBehaviour
{
    public static LandButtonInteract Instance;
    [SerializeField] private Button landBtn;
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
        landBtn.onClick.AddListener(StartFight);
    }
    public void StartFight()
    {
        SceneTransition.Instance.SceneHide();
        StartCoroutine(LoadFightScene("InGame"));
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
        SceneTransition.Instance.SceneShow();
        InGameSceneSetUp.Instance.StartFight();
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
