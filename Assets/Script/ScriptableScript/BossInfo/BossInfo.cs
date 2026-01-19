using UnityEngine;

[CreateAssetMenu(fileName = "BossInfo", menuName = "Scriptable Objects/BossInfo")]
public class BossInfo : ScriptableObject
{
    public string bossID;
    public float bossBaseHealth;
    public float bossBaseDamage;
    public float bossBaseReward;
}
