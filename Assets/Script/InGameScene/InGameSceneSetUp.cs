using UnityEngine;
using System.Collections.Generic;

public class InGameSceneSetUp : MonoBehaviour
{
    public static InGameSceneSetUp Instance;
    [SerializeField] private CurrentSelectLevel currentSelectLevel;
    [SerializeField] private Transform bossSpawnPoint;
    private GameObject boss;
    private GameObject playerPlane;
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
    public void ReadyFight()
    {
        CreateListOfPoint.Instance.CreateList4PointX();
        CreateListOfPoint.Instance.CreateList2PointX();
        CreateListOfPoint.Instance.CreateList4PointY();
        boss = Instantiate(ObjectDictionary.Instance.GetLevelInfo(currentSelectLevel.currentSelectLevelID).bossPrefab,  new Vector3(0, CreateListOfPoint.Instance.GetList4PointY()[3], 0), Quaternion.identity);
        boss.GetComponent<Boss>().SetCanShoot(false);
        boss.GetComponent<BossHealthControl>().SetStartupHealth();
        playerPlane = Instantiate(ObjectDictionary.Instance.GetPlayerPlaneAtributes().playerPlanePrefab, Vector3.zero, Quaternion.identity);
        playerPlane.GetComponent<PlayerPlaneController>().SetStartValue();
        playerPlane.GetComponent<PlayerPlaneBulletSpawn>().SetStartValue();
        playerPlane.GetComponent<PlayerPlaneController>().SetStartPosition();
        playerPlane.GetComponent<PlayerHealthControl>().SetStartHealth();
        playerPlane.GetComponent<PlayerPlaneEnergyCharge>().SetStartValue();      
        InGamePauseManager.Instance.StartSetUp();
    }
    public void StartFight()
    {
        boss.GetComponent<Boss>().SetCanShoot(true);        
        playerPlane.GetComponent<PlayerPlaneBulletSpawn>().SetCanShoot(true);
        playerPlane.GetComponent<PlayerPlaneController>().SetCanMove(true);
        SoundControl.Instance.InGameMusicPlay();
        NotifPopUp.Instance.ShowNotif("Now play '1980' song.");
    }
    public void PauseFight()
    {
        boss.GetComponent<Boss>().SetCanShoot(false);        
        playerPlane.GetComponent<PlayerPlaneBulletSpawn>().SetCanShoot(false);
        playerPlane.GetComponent<PlayerPlaneController>().SetCanMove(false);
    }
    public void RestartFight()
    {
        BossHealthControl.Instance.SetStartupHealth();
        PlayerHealthControl.Instance.SetStartHealth();
        PlayerPlaneEnergyCharge.Instance.SetStartValue();
    }
}
