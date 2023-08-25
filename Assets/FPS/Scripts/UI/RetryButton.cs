using UnityEngine;
using UnityEngine.UI;
public class RetryButton : MonoBehaviour
{
    public HeartRateService heartRateService; // Reference to HeartRateService
    public Button retryButton; // Reference to the Retry button

    private float timer = 0f; // Timer to track the duration of isSubscribed being false

    private void Awake()
    {
        retryButton.gameObject.SetActive(false); // Initially hide the button
    }

    private void Update()
    {
        // If isSubscribed is false, increment the timer
        if (!heartRateService.isSubscribed)
        {
            timer += Time.deltaTime;

            // If isSubscribed has been false for more than 10 seconds
            if (timer >= 10f)
            {
                retryButton.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
        }
        else
        {
            timer = 0f; // Reset the timer if isSubscribed becomes true
            retryButton.gameObject.SetActive(false);
        }
    }
}
