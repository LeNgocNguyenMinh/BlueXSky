using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : Boss
{
    [SerializeField]private GameObject straightBulletPrefab;
    [SerializeField]private GameObject changeRouteBulletPrefab;
    [SerializeField]private GameObject concentraitBulletPrefab;
    [SerializeField]private float straightBulletSpeed;
    [SerializeField]private float changeRouteBulletSpeed;
    [SerializeField]private float concentraitBulletSpeed;
    [SerializeField]private BossInfoManager bossInfoManager;
    public int hitBackBulletCount = 0;
    [SerializeField]private int maxBulletForHitBack;
    public override void BossATK1()
    {
        base.BossATK1();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, BulletSpawnPoint.Instance.GetList4Point().Count);
        Vector3 spawnPos = new Vector3(BulletSpawnPoint.Instance.GetList4Point()[index], transform.position.y, 0);
        FBBulletType1 tmp = Instantiate(straightBulletPrefab, spawnPos, Quaternion.identity).GetComponent<FBBulletType1>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[index], straightBulletSpeed, bossInfoManager.GetBossMaxDamage(), true); 
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[index], straightBulletSpeed, bossInfoManager.GetBossMaxDamage(), false);   
        }
        
    }
    public override void BossATK2()
    {
        base.BossATK2();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, BulletSpawnPoint.Instance.GetList4Point().Count);
        FBBulletType2 tmp = Instantiate(changeRouteBulletPrefab, new Vector3(BulletSpawnPoint.Instance.GetList4Point()[index], transform.position.y, 0), Quaternion.identity).GetComponent<FBBulletType2>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(changeRouteBulletSpeed, bossInfoManager.GetBossMaxDamage(), index, true);
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(changeRouteBulletSpeed, bossInfoManager.GetBossMaxDamage(), index, false);
        }
        
    }
    public override void BossATK3()
    {
        base.BossATK3();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, BulletSpawnPoint.Instance.GetList4Point().Count);
        hitBackBulletCount ++;
        for(int i = 0; i < BulletSpawnPoint.Instance.GetList4Point().Count; i++)
        {
            Vector3 spawnPos = new Vector3(BulletSpawnPoint.Instance.GetList4Point()[i], transform.position.y, 0);
            FBBulletType3 tmp = Instantiate(concentraitBulletPrefab, spawnPos, Quaternion.identity).GetComponent<FBBulletType3>();
            if(i==index)
            {
                if(hitBackBulletCount == maxBulletForHitBack)
                {
                    tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[i], concentraitBulletSpeed, bossInfoManager.GetBossMaxDamage(), false, true);  
                    hitBackBulletCount = 0; 
                }
                else
                {
                    tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[i], concentraitBulletSpeed, bossInfoManager.GetBossMaxDamage(), false, false);  
                }
            }
            else{
                tmp.SetValue(BulletSpawnPoint.Instance.GetList4Point()[i], concentraitBulletSpeed, bossInfoManager.GetBossMaxDamage(), true, false); 
            }
             
        }
    }
}
