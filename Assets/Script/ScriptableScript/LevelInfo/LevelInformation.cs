using UnityEngine;

[CreateAssetMenu(fileName = "LevelInformation", menuName = "Scriptable Objects/LevelInformation")]
public class LevelInformation : ScriptableObject
{
    public string levelID;
    public string planetName;
    public GameObject bossPrefab;
}
