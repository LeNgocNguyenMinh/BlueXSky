using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributes", menuName = "Scriptable Objects/PlayerAttributes")]
public class PlayerAttributes : ScriptableObject
{
    public float playerBaseDamage;
    public float playerFullChargeDuration;
    public float playerBulletSpeed;
    public float playerBaseDelay;
    public float playerFullChargeDelay;
    public float playerCoin;
    public GameObject playerPlanePrefab;
    public void ResetAttributes()
    {
        playerBaseDamage = 1f;
        playerCoin = 0f;
    }
}
