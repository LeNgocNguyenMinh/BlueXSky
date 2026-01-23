using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class PlanetLevelInteract : MonoBehaviour
{
    [SerializeField]private Image planetBorder;
    [SerializeField]private LevelInformation planetInfo;
    [SerializeField]private TextMeshProUGUI planetStatus;
    private bool showButton = false;
    private void Start()
    {
        planetBorder.enabled = false;   
        showButton = false;
        planetStatus.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerPlane") && !showButton)
        {
            planetBorder.enabled = true;
            showButton = true;
            LevelSelectManager.Instance.SetCurrentSelectLevelID(planetInfo.levelID);
            LandButtonInteract.Instance.OnLand();
            LandButtonInteract.Instance.ShowLandButton();
            planetStatus.text = $"planet name: {planetInfo.planetName}\n" +
                                $"planet code: {planetInfo.levelID}\n";
            planetStatus.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerPlane") && showButton)
        {
            showButton = false;
            LandButtonInteract.Instance.HideLandButton();
            planetBorder.enabled = false;
            planetStatus.gameObject.SetActive(false);
        }
    }
}
