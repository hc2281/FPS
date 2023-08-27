using UnityEngine;
using System.Collections;
using TMPro;
using Unity.FPS.Bluetooth;

namespace Unity.FPS.UI
{
public class BaselineManager : MonoBehaviour
{

        public float baselineValue;
        public PanelController panel;

        private float heartRateSum = 0f;
        private int heartRateCount = 0;

        public bool isMeasuring = false;  // Flag to track if we're currently measuring

        private void Start()
        {
            StartHeartRateMeasurement();
        }

        public void StartHeartRateMeasurement()
        {
            if (isMeasuring)  // Check if we're already measuring
                return;

            StartCoroutine(MeasureHeartRateForOneMinute());
        }

        private IEnumerator MeasureHeartRateForOneMinute()
        {
            isMeasuring = true;  // Set flag to true as we're starting measurement
            yield return new WaitForSeconds(180); // wait for 3 min

            for (int i = 0; i < 120; i++)
            {
                int currentHeartRate = HeartRateService.heartBeatsPerMinute;
                heartRateSum += currentHeartRate;
                heartRateCount++;

                yield return new WaitForSeconds(1f);
            }

            baselineValue = heartRateSum / heartRateCount;
            Debug.Log("Base BPM: " + baselineValue);
            HeartRateDDA.SaveBaseBPM(baselineValue);

            EndMeasure();

            isMeasuring = false;  // Reset the flag once we're done measuring
        }

        private void EndMeasure()
        {
            heartRateSum = 0f;
            heartRateCount = 0;

            panel.HideLoadingUI();
        }

    }

}
