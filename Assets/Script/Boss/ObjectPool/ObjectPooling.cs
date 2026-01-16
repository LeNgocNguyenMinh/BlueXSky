using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;
    [SerializeField]private List<PoolObject> listOfPoolObjects = new List<PoolObject>();
    private Dictionary<GameObject, List<GameObject>> pooledObjectsDictionary = new Dictionary<GameObject, List<GameObject>>();

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
    private void Start()
    {
        InitializePools();
    }
    private void InitializePools()
    {
        foreach(var poolObject in listOfPoolObjects)
        {
            List<GameObject> pooledObjects = new List<GameObject>();
            for(int i = 0; i < poolObject.objectPoolSize; i++)
            {
                GameObject obj = Instantiate(poolObject.prefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
            pooledObjectsDictionary.Add(poolObject.prefab, pooledObjects);
        }
    }
    public GameObject GetPooledObject(GameObject prefab)
    {
        if (pooledObjectsDictionary.ContainsKey(prefab))
        {
            foreach (GameObject obj in pooledObjectsDictionary[prefab])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }
        return null;
    }

}
[System.Serializable]
public class PoolObject
{
    public GameObject prefab;
    public int objectPoolSize;
}
