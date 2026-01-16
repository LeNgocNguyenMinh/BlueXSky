using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControl : MonoBehaviour
{
    public static SoundControl Instance;
    [SerializeField]private AudioSource musicSrc;
    [SerializeField]private AudioSource sfxSrc;
    [Header("----------Game Music----------")]
    [SerializeField]public AudioClip mainMenuMusic;
    [SerializeField]public AudioClip levelSelectMusic;
    [SerializeField]public AudioClip inGameMusic;
    [Header("----------Player Audio Clips----------")]
    [SerializeField]public AudioClip playerShootSound;
    [SerializeField]public AudioClip playerDeathSound;
    [Header("----------Boss Audio Clips----------")]
    [SerializeField]public AudioClip bossShootSound;
    [SerializeField]public AudioClip bossDeathSound;
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
    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
        if(sceneName == "MainMenu")
        {
            Debug.Log("mainMenuMusicPlay");
            MainMenuMusicPlay();
        }
    }
    public void PlayerShootSoundPlay()
    {
        PlaySFX(playerShootSound);
    }
    public void PlayerDeathSoundPlay()
    {
        PlaySFX(playerDeathSound);
    }
    public void BossShootSoundPlay()
    {
        PlaySFX(bossShootSound);
    }
    public void BossDeathSoundPlay()
    {
        PlaySFX(bossDeathSound);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSrc.clip = clip;
        musicSrc.loop = true;
        musicSrc.Play();
    }
    public void InGameMusicPlay()
    {
        PlayMusic(inGameMusic);
    }
    public void MainMenuMusicPlay()
    {
        PlayMusic(mainMenuMusic);
    }
   public void LevelSelectMusicPlay()
    {
        PlayMusic(levelSelectMusic);
    }
}
