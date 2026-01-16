using UnityEngine;
using System.Collections.Generic;

public class BulletSpawnPoint : MonoBehaviour
{
    private List<float> list4Point;
    private List<float> list2Point;
    public static BulletSpawnPoint Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
    public List<float> GetList4Point()
    {
        return list4Point;
    }
    public void CreateList4Point()
    {
        list4Point = new List<float>();
        float sectionWidth = Screen.width / 4f;
        for (int i = 0; i < 4; i++)
        {
            float centerX = sectionWidth * (i + 0.5f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(centerX, 0, Mathf.Abs(Camera.main.transform.position.z))
        );
            list4Point.Add(worldPos.x);
        }
    }
    public void CreateList2Point()
    {
        list2Point = new List<float>();
        float sectionWidth = Screen.width / 2f;
        for (int i = 0; i < 2; i++)
        {
            float centerX = sectionWidth * (i + 0.5f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(centerX, 0, Mathf.Abs(Camera.main.transform.position.z))
        );
            list2Point.Add(worldPos.x);
        }
    }
    public List<float> GetList2Point()
    {
        return list2Point;
    }
}
