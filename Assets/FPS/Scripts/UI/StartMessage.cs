using System.Collections;
using TMPro;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public HeartRateService heartRateService;
    public GameObject maskPanel;
    public float delay = 0.3f;  // Delay in seconds between each dot

    public bool isReady;

    protected void Start()
    {
        ShowLoadingUI();

        heartRateService.OnConnected += HideLoadingUI;
    }

    public void ShowLoadingUI()
    {
        maskPanel.SetActive(true);
        loadingText.gameObject.SetActive(true);
        StartCoroutine(AnimateDots());

        // Mute or pause audio
        AudioListener.volume = 0f;

    }

    IEnumerator AnimateDots()
    {
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {
                loadingText.text = "Connecting..." + new string('.', i);
                yield return new WaitForSeconds(delay);
            }
        }
    }

    public void HideLoadingUI()
    {
        loadingText.gameObject.SetActive(false);
        maskPanel.SetActive(false);

        AudioListener.volume = 0.5f;
    }

}
