using UnityEngine;
using System.Collections.Generic;

public class ObjectDictionary : MonoBehaviour
{
    public static ObjectDictionary Instance;
    [SerializeField]private PlayerAttributes playerPlaneAttributes;
    [SerializeField]private List<LevelInformation> levelList;
    private Dictionary<string, LevelInformation> levelDictionary;

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
    public LevelInformation GetLevelInfo(string levelID)
    {
        levelDictionary = new Dictionary<string, LevelInformation>();
        foreach(LevelInformation level in levelList)
        {
            levelDictionary[level.levelID] = level;
        }
        if (levelDictionary.TryGetValue(levelID, out LevelInformation levelInfo))
        {
            return levelInfo;
        }
        Debug.LogWarning($"Không tìm thấy Item ID {levelID} trong dictionary");
        return null; // Trả về null nếu không tìm thấy
    }
    public PlayerAttributes GetPlayerPlaneAtributes()
    {
        return playerPlaneAttributes;
    }
}
