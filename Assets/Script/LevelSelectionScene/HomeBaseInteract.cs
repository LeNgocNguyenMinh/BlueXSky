using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class HomeBaseInteract : MonoBehaviour
{
    [SerializeField]private Image baseBorder;
    [SerializeField]private TextMeshProUGUI baseStatus;
    private bool showButton = false;
    private void Start()
    {
        baseBorder.enabled = false;   
        showButton = false;
        baseStatus.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerPlane") && !showButton)
        {
            baseBorder.enabled = true;
            showButton = true;
            LandButtonInteract.Instance.OnHomeBase();
            LandButtonInteract.Instance.ShowLandButton();
            baseStatus.text =   $"Home base\n";
            baseStatus.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerPlane") && showButton)
        {
            baseBorder.enabled = false;
            showButton = false;
            LandButtonInteract.Instance.HideLandButton();
            baseStatus.gameObject.SetActive(false);
        }
    }
}
