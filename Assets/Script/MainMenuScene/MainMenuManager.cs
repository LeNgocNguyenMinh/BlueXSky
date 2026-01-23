    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections;
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]private Button startButton;
        [SerializeField]private Button exitButton;
        [SerializeField]private Button continueButton;
        private void OnEnable()
        {
            EnableButton();
            startButton.onClick.AddListener(OnStartButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }
        public void EnableButton()
        {
            startButton.interactable = true;
            exitButton.interactable = true;
            continueButton.interactable = true;
        }
        public void DisableButton()
        {
            startButton.interactable = false;
            exitButton.interactable = false;
            continueButton.interactable = false;
        }
        public void OnStartButtonClicked()
        {
            DisableButton();
            SceneTransition.Instance.HideTransStart();
            StartCoroutine(LoadNewGameScene("LevelSelection"));
        }
        private IEnumerator LoadNewGameScene(string sceneName)
        {
            yield return new WaitForSeconds(2f);
            // Start to load but not activate the scene immediately
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f && SceneTransition.Instance.IsHideAnimationFinish()) 
                { 
                    SceneManager.sceneLoaded += OnNewGameSceneLoaded;
                    operation.allowSceneActivation = true;

                }
                yield return null;
            }
        }
        private void OnNewGameSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnNewGameSceneLoaded;
            SceneTransition.Instance.ShowTransStart();
        }
        private void OnExitButtonClicked()
        {
            DisableButton();
            Debug.Log("Exit Button Clicked");
            // Thêm logic thoát trò chơi ở đây
            Application.Quit();
        }
        private void OnContinueButtonClicked()
        {
            DisableButton();
            Debug.Log("Continue Button Clicked");
            // Thêm logic tiếp tục trò chơi ở đây
        }
    }
