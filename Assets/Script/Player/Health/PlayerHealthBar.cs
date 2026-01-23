using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public static PlayerHealthBar Instance;
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
        float target = PlayerHealthControl.Instance.healthCurrentValue / PlayerHealthControl.Instance.healthMaxValue;
        if(healthBarFrontImage.fillAmount > healthBarBackImage.fillAmount)
        {
            healthBarBackImage.fillAmount = healthBarFrontImage.fillAmount;
        }
        healthBarFrontImage.DOFillAmount(target, 1f).SetEase(Ease.Linear).SetUpdate(true);
        healthBarBackImage.DOFillAmount(target, 2f).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UpdateHealthText()
    {
        healthText.text = $"{(int)PlayerHealthControl.Instance.healthCurrentValue:0.0}/{(int)PlayerHealthControl.Instance.healthMaxValue:0.0}";
    }
}