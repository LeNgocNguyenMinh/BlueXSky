using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthControl : MonoBehaviour
{
    public static BossHealthControl Instance;
    [SerializeField]private BossInfoManager bossInfoManager;
    private float healthCurrentValue;// Health current value
    private float healthMaxValue; // Health max value need to achive for level up
    public Boss boss;
    private bool isDead;

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
        isDead = false;
        healthMaxValue = bossInfoManager.GetBossMaxHealth();
        healthCurrentValue = healthMaxValue;
        BossHealthBar.Instance.SetMaxHealth(); 
        BossHealthBar.Instance.UpdateHealthText();
    }
    public void BossHurt(float damageAmount) //Boss hurt by enemy
    {
        healthCurrentValue -= damageAmount;
        if(healthCurrentValue <= 0 && !isDead)
        {
            isDead = true;
            healthCurrentValue = 0;
            boss.BossDead();
        }
        BossHealthBar.Instance.UpdateHealthText();
        BossHealthBar.Instance.SetCurrentHealth();
    }
    public float GetCurrentHealthValue()
    {
        return healthCurrentValue;
    }
    public float GetMaxHealthValue()
    {
        return healthMaxValue;
    }
}
