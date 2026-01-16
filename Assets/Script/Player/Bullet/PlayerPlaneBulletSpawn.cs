using System.Collections;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;

public class PlayerPlaneBulletSpawn : MonoBehaviour
{
    public static PlayerPlaneBulletSpawn Instance;
    [SerializeField]private GameObject planeBullet;
    [SerializeField]private float speed;
    [SerializeField]private float fullChargeLength;
    [SerializeField]private float originDelay;
    [SerializeField]private float fullChargeDelay;
    [SerializeField]private Transform firePoint;
    [SerializeField]private float damage;
    private float countDown;
    private float spawnDelay;
    private bool canShoot;
    private float fullChargeCount;
    public bool inFullChargeMode;
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
        spawnDelay = originDelay;
        inFullChargeMode = false;
        countDown = 0f;
        canShoot = false;
    }
    private void Update()
    {
        if(canShoot)
        {
            if(inFullChargeMode)
            {
                if(fullChargeCount <= 0f)
                {
                    ExitFullChargeMode();
                }
                else
                {
                    fullChargeCount -= Time.deltaTime;
                }
                
            }
            if(countDown <= 0f)
            {
                countDown = spawnDelay;
                BulletInstantiate();
                return;
            }
            countDown -= Time.deltaTime;
        }
        
    }
    public void BulletInstantiate()
    {
        GameObject bullet = ObjectPooling.Instance.GetPooledObject(planeBullet);
        if(bullet != null)
        {
            bullet.transform.position = firePoint.position;
            SoundControl.Instance.PlayerShootSoundPlay();
            bullet.SetActive(true);
            PlayerPlaneBullet tmp = bullet.GetComponent<PlayerPlaneBullet>();
            tmp.SetValue(speed, damage);
            return;
        }
    }
    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }
    public bool GetCanShoot()
    {
        return canShoot;
    }
    public void InFullChargeMode()
    {
        spawnDelay = fullChargeDelay;
        fullChargeCount = fullChargeLength;
        inFullChargeMode = true;
    }
    public void ExitFullChargeMode()
    {
        spawnDelay = originDelay;
        inFullChargeMode = false;
    }
    public bool IsInFullChargeMode()
    {
        return inFullChargeMode;
    }
}
