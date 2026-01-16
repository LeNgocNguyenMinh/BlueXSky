using UnityEngine;

public class SecondBoss : Boss
{
    [SerializeField]private GameObject smallBulletPrefab;
    [SerializeField]private GameObject midBulletPrefab;
    [SerializeField]private GameObject bigBulletPrefab;
    [SerializeField]private float smallBulletSpeed;
    [SerializeField]private float midBulletSpeed;
    [SerializeField]private float bigBulletSpeed;
    [SerializeField]private BossInfoManager bossInfoManager;
    public int hitBackBulletCount = 0;
    [SerializeField]private int maxBulletForHitBack;
    public override void BossATK1()
    {
        base.BossATK1();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, BulletSpawnPoint.Instance.GetList4Point().Count);
        Vector3 spawnPos = new Vector3(BulletSpawnPoint.Instance.GetList4Point()[index], transform.position.y, 0);
        SBBulletType1 tmp = Instantiate(smallBulletPrefab, spawnPos, Quaternion.identity).GetComponent<SBBulletType1>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[index], smallBulletSpeed, bossInfoManager.GetBossMaxDamage(), true); 
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[index], smallBulletSpeed, bossInfoManager.GetBossMaxDamage(), false);   
        }
    }
    public override void BossATK2()
    {
        base.BossATK2();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, BulletSpawnPoint.Instance.GetList2Point().Count);
        Vector3 spawnPos = new Vector3(BulletSpawnPoint.Instance.GetList2Point()[index], transform.position.y, 0);
        SBBulletType1 tmp = Instantiate(midBulletPrefab, spawnPos, Quaternion.identity).GetComponent<SBBulletType1>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList2Point()[index], midBulletSpeed, bossInfoManager.GetBossMaxDamage(), true); 
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList2Point()[index], midBulletSpeed, bossInfoManager.GetBossMaxDamage(), false);   
        }
        
    }
    public override void BossATK3()
    {
        base.BossATK3();
        SoundControl.Instance.BossShootSoundPlay();
        int index =  Random.value < 0.5f ? 1 : 2;
        Vector3 spawnPos = new Vector3(BulletSpawnPoint.Instance.GetList4Point()[index], transform.position.y, 0);
        SBBulletType1 tmp = Instantiate(bigBulletPrefab, spawnPos, Quaternion.identity).GetComponent<SBBulletType1>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[index], bigBulletSpeed, bossInfoManager.GetBossMaxDamage(), true); 
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[index], bigBulletSpeed, bossInfoManager.GetBossMaxDamage(), false);   
        }
    }
}
