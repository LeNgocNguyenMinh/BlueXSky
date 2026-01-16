using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPlaneEnergyCharge : MonoBehaviour
{
    public static PlayerPlaneEnergyCharge Instance;
    [SerializeField]private Image energyBar;
    private float currentEnergy;
    [SerializeField]private float maxEnergy;
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
        currentEnergy = 0;
        energyBar.fillAmount = 0f;
    }
    public void ResetEnergy()
    {
        currentEnergy = 0;
        energyBar.DOFillAmount(0f, 0.5f).SetEase(Ease.Linear);
    }
    public void AddEnergy()
    {
        if(PlayerPlaneBulletSpawn.Instance.inFullChargeMode)return;
        currentEnergy ++;
        if(currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
            PlayerPlaneBulletSpawn.Instance.InFullChargeMode();
            ResetEnergy();
        }
        float target = (float)currentEnergy / maxEnergy;
        energyBar.DOFillAmount(target, 1f).SetEase(Ease.Linear);
    } 
}
