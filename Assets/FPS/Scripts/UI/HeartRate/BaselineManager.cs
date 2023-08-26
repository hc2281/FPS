using UnityEngine;
using System.Collections;
using TMPro;

namespace Unity.FPS.UI
{
public class BaselineManager : MonoBehaviour
{
        public TMP_Text Text; // Show "Measured" to inform the player
        public TMP_Text MeasuredText;
        public HeartRateService heartRateService;

        public float baselineValue;
        public PanelController panel;

        public bool isConnected = false;
        private float heartRateSum = 0f;
        private int heartRateCount = 0;

        private bool isMeasuring = false;  // Flag to track if we're currently measuring

        private void Start()
        {
            heartRateService.OnConnected += StartHeartRateMeasurement;
        }

        public void StartHeartRateMeasurement()
        {
            if (isMeasuring)  // Check if we're already measuring
                return;

            isConnected = true;
            Text.text = "Connected";
            Text.color = Color.white;
            StartCoroutine(MeasureHeartRateForOneMinute());
        }

        private IEnumerator MeasureHeartRateForOneMinute()
        {
            isMeasuring = true;  // Set flag to true as we're starting measurement
            yield return new WaitForSeconds(180); // wait for 3 min

            for (int i = 0; i < 120; i++)
            {
                int currentHeartRate = GetCurrentHeartRate();
                heartRateSum += currentHeartRate;
                heartRateCount++;

                yield return new WaitForSeconds(1f);
            }

            baselineValue = heartRateSum / heartRateCount;
            Debug.Log("Base BPM: " + baselineValue);
            HeartRateManager.instance.SetAverageBPM(baselineValue);

            EndMeasure();

            isMeasuring = false;  // Reset the flag once we're done measuring
        }

        private void EndMeasure()
        {
            Text.text = "";
            MeasuredText.color = Color.black;
            MeasuredText.text = "Measured";
            heartRateSum = 0f;
            heartRateCount = 0;

            panel.HideLoadingUI();
        }

        private int GetCurrentHeartRate()
        {
            int HeartRate = heartRateService.heartBeatsPerMinute;

            return HeartRate;
        }

        private void OnDestroy()
        {
            heartRateService.OnConnected -= StartHeartRateMeasurement;
        }

    }

}
