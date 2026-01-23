using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthControl : MonoBehaviour
{
    public static PlayerHealthControl Instance;
    [SerializeField]private PlayerAttributes playerAttributes;
    public float healthCurrentValue;// Health current value
    public float healthMaxValue; // Health max value need to achive for level up

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
    public void SetStartValue()
    {
        healthMaxValue = playerAttributes.playerHealth;
        healthCurrentValue = healthMaxValue;
        PlayerHealthBar.Instance.SetMaxHealth();
        PlayerHealthBar.Instance.UpdateHealthText();
    }
    public void PlayerHurt(float damageAmount) //Player hurt by enemy
    {
        healthCurrentValue -= damageAmount;
        if(healthCurrentValue <= 0)
        {
            SoundControl.Instance.PlayerDeathSoundPlay();
            healthCurrentValue = 0;
            InGamePauseManager.Instance.GameOverMenuOn();
        }
        PlayerHealthBar.Instance.UpdateHealthText();
        PlayerHealthBar.Instance.SetCurrentHealth(); 
    }
}
