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
    private int hitBackBulletCount = 0;
    [SerializeField]private int maxBulletForHitBack;
    public override void BossATK1()
    {
        base.BossATK1();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, CreateListOfPoint.Instance.GetList4PointX().Count);
        Vector3 spawnPos = new Vector3(CreateListOfPoint.Instance.GetList4PointX()[index], transform.position.y, 0);
        FBBulletType1 tmp = Instantiate(straightBulletPrefab, spawnPos, Quaternion.identity).GetComponent<FBBulletType1>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(CreateListOfPoint.Instance.GetList4PointX()[index], straightBulletSpeed, BossInfoManager.GetBossMaxDamage(), true); 
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(CreateListOfPoint.Instance.GetList4PointX()[index], straightBulletSpeed, BossInfoManager.GetBossMaxDamage(), false);   
        }
        
    }
    public override void BossATK2()
    {
        base.BossATK2();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, CreateListOfPoint.Instance.GetList4PointX().Count);
        FBBulletType2 tmp = Instantiate(changeRouteBulletPrefab, new Vector3(CreateListOfPoint.Instance.GetList4PointX()[index], transform.position.y, 0), Quaternion.identity).GetComponent<FBBulletType2>();
        hitBackBulletCount ++;
        if(hitBackBulletCount == maxBulletForHitBack)
        {
            tmp.SetValue(changeRouteBulletSpeed, BossInfoManager.GetBossMaxDamage(), index, true);
            hitBackBulletCount = 0; 
        }
        else
        {
            tmp.SetValue(changeRouteBulletSpeed, BossInfoManager.GetBossMaxDamage(), index, false);
        }
        
    }
    public override void BossATK3()
    {
        base.BossATK3();
        SoundControl.Instance.BossShootSoundPlay();
        int index = Random.Range(0, CreateListOfPoint.Instance.GetList4PointX().Count);
        hitBackBulletCount ++;
        for(int i = 0; i < CreateListOfPoint.Instance.GetList4PointX().Count; i++)
        {
            Vector3 spawnPos = new Vector3(CreateListOfPoint.Instance.GetList4PointX()[i], transform.position.y, 0);
            FBBulletType3 tmp = Instantiate(concentraitBulletPrefab, spawnPos, Quaternion.identity).GetComponent<FBBulletType3>();
            if(i==index)
            {
                if(hitBackBulletCount == maxBulletForHitBack)
                {
                    tmp.SetValue(CreateListOfPoint.Instance.GetList4PointX()[i], concentraitBulletSpeed, BossInfoManager.GetBossMaxDamage(), false, true);  
                    hitBackBulletCount = 0; 
                }
                else
                {
                    tmp.SetValue(CreateListOfPoint.Instance.GetList4PointX()[i], concentraitBulletSpeed, BossInfoManager.GetBossMaxDamage(), false, false);  
                }
            }
            else{
                tmp.SetValue(CreateListOfPoint.Instance.GetList4PointX()[i], concentraitBulletSpeed, BossInfoManager.GetBossMaxDamage(), true, false); 
            }
             
        }
    }
}
