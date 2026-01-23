using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributes", menuName = "Scriptable Objects/PlayerAttributes")]
public class PlayerAttributes : ScriptableObject
{
    public GameObject playerPlanePrefab;
    [Header("Upgrade Attribute")]
    public float playerDamage;
    public float damageUpgradeAmount;
    public float damageUpgradeCost;
    public float playerHealth;
    public float healthUpgradeAmount;
    public float healthUpgradeCost;
    [Header("Default Attribute")]
    public float playerFullChargeDuration;
    public float playerBulletSpeed;
    public float playerBaseDelay;
    public float playerFullChargeDelay;
    [Header("Update Attribute")]
    public float playerScrap;
    
    public void ResetAttributes()
    {
        playerDamage = 1f;
        playerHealth = 100f;
        playerScrap = 20f;
    }
}
