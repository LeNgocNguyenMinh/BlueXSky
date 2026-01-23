using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeBase : MonoBehaviour
{
    public static HomeBase Instance;
    [Header("Scrap Update")]
    [SerializeField]private TextMeshProUGUI scrapText;
    [SerializeField]private PlayerAttributes playerAttributes;
    [SerializeField]private Button exitBtn;
    [Header("Damage Upgrade")]
    [SerializeField]private Button damageUpdateBtn;
    [SerializeField]private TextMeshProUGUI damageText;
    [SerializeField]private TextMeshProUGUI damageUpgradeCostText;
    [Header("Health Upgrade")]
    [SerializeField]private Button healthUpdateBtn;
    [SerializeField]private TextMeshProUGUI healthText;
    [SerializeField]private TextMeshProUGUI healthUpgradeCostText;
    [SerializeField]private RectTransform homeBaseRect;
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
    public void SetStartUp()
    {
        homeBaseRect.gameObject.SetActive(false);
    }
    public void EnterHomeBase()
    {
        UpdateInfo();
        homeBaseRect.gameObject.SetActive(true);
    }
    public void UpdateInfo()
    {
        damageUpgradeCostText.text = $"{playerAttributes.damageUpgradeCost} scraps";
        healthUpgradeCostText.text = $"{playerAttributes.healthUpgradeCost} scraps";
        scrapText.text = $" {playerAttributes.playerScrap}";
        damageText.text = $"{playerAttributes.playerDamage:0.00}";
        healthText.text = $"{playerAttributes.playerHealth:0.00}";
    }
    private void OnEnable()
    {
        exitBtn.onClick.AddListener(OnExitBtnClick);
        damageUpdateBtn.onClick.AddListener(OnDamageUpdateBtnClick);
        healthUpdateBtn.onClick.AddListener(OnHealthUpdateBtnClick);
    }
    private void OnExitBtnClick()
    {
        PlayerPlaneControlInLevelSelect.Instance.SetCanMove(true);
        homeBaseRect.gameObject.SetActive(false);
    }
    private void OnDamageUpdateBtnClick()
    {
        if(playerAttributes.playerScrap < playerAttributes.damageUpgradeCost)
        {
            NotifPopUp.Instance.ShowNotif("Not enough scrap!", 1f);
            return;
        }
        playerAttributes.playerScrap -=  playerAttributes.damageUpgradeCost;
        playerAttributes.playerDamage += playerAttributes.damageUpgradeAmount;
        UpdateInfo();
    }
    private void OnHealthUpdateBtnClick()
    {
        if(playerAttributes.playerScrap < playerAttributes.healthUpgradeCost)
        {
            NotifPopUp.Instance.ShowNotif("Not enough scrap!", 1f);
            return;
        }
        playerAttributes.playerScrap -= playerAttributes.healthUpgradeCost;
        playerAttributes.playerHealth += playerAttributes.healthUpgradeAmount;
        UpdateInfo();
    }

}
