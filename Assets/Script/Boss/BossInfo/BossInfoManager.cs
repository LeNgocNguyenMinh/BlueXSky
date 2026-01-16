using System.Collections.Generic;
using UnityEngine;

public class BossInfoManager : MonoBehaviour
{
    [SerializeField]private BossInfo bossInfo;
    private string bossID;
    private float bossMaxHealth;
    private float bossMaxDamage;
    public string GetBossID()
    {
        return bossInfo.bossID;
    }
    public float GetBossMaxHealth()
    {
        return bossInfo.bossBaseHealth;
    }
    public float GetBossMaxDamage()
    {
        return bossInfo.bossBaseDamage;
    }
}
