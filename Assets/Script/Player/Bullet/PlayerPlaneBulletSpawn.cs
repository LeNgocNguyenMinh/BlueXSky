using System.Collections;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerPlaneBulletSpawn : MonoBehaviour
{
    public static PlayerPlaneBulletSpawn Instance;
    [SerializeField]private GameObject planeBullet;
    [SerializeField]private Transform firePoint;
    [SerializeField]private PlayerAttributes playerAttributes;
    private float damage;
    private float speed;
    private float originDelay;
    private float fullChargeDelay;
    private float playerFullChargeDuration;
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
        //Base info
        damage = playerAttributes.playerDamage;
        speed = playerAttributes.playerBulletSpeed;
        originDelay = playerAttributes.playerBaseDelay;
        fullChargeDelay = playerAttributes.playerFullChargeDelay;
        playerFullChargeDuration = playerAttributes.playerFullChargeDuration;
        //Start info
        spawnDelay = originDelay;
        countDown = 0f;
        canShoot = false;
        inFullChargeMode = false;
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
        fullChargeCount = playerFullChargeDuration;
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
