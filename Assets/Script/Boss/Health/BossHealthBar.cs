using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public static BossHealthBar Instance;
    [SerializeField]private Image healthBarFrontImage;
    [SerializeField]private Image healthBarBackImage;
    [SerializeField]private TextMeshProUGUI healthText;
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
    public void SetMaxHealth()
    {
        healthBarFrontImage.fillAmount = 1f;
        healthBarBackImage.fillAmount = 1f;
    }

    public void SetCurrentHealth()
    {
        float target = BossHealthControl.Instance.GetCurrentHealthValue() / BossHealthControl.Instance.GetMaxHealthValue();
        if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        }
        healthBarFrontImage.DOFillAmount(target, 1f).SetEase(Ease.Linear).SetUpdate(true);
        healthBarBackImage.DOFillAmount(target, 2f).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UpdateHealthText()
    {
        healthText.text = $"{BossHealthControl.Instance.GetCurrentHealthValue():0.0}/{BossHealthControl.Instance.GetMaxHealthValue():0.0}";
    }
}