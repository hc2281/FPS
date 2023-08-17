using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.FPS.Game;

public class baselineManager : MonoBehaviour
{
    public Button startButton; // Drag the 'Start' button from the editor into this field
    public TMP_Text buttonText;
    
    public HeartRateService heartRateService;
    public GameFlowManager gameFlowManager;

    public float baselineHeartRate;

    private float heartRateSum = 0f;
    private int heartRateCount = 0;

    private void Start()
    {
        StartHeartRateMeasurement();
    }

    public void StartHeartRateMeasurement()
    {
        // Debugging step 1: Check if the button click is triggering this method
        Debug.Log("StartHeartRateMeasurement method was called!");

        StartCoroutine(MeasureHeartRateForOneMinute());
    }

    private IEnumerator MeasureHeartRateForOneMinute()
    {
        
        startButton.gameObject.SetActive(false);
        gameFlowManager.gameStarted = true;
        yield return new WaitForSeconds(180); 
        
        Debug.Log("Measure has started!");

        for (int i = 0; i < 120; i++)
        {
            int currentHeartRate = GetCurrentHeartRate();
            heartRateSum += currentHeartRate;
            heartRateCount++;

            yield return new WaitForSeconds(1f);
        }

        baselineHeartRate = heartRateSum / heartRateCount;
        Debug.Log("Average Baseline heart rate over 1 minute: " + baselineHeartRate);
        HeartRateManager.instance.SetAverageBPM(baselineHeartRate);

        startButton.gameObject.SetActive(true);
        buttonText.text = "Measured";
        heartRateSum = 0f;
        heartRateCount = 0;
    }

    private int GetCurrentHeartRate()
    {
        int HeartRate = heartRateService.heartBeatsPerMinute;

        return HeartRate;
    }
}
