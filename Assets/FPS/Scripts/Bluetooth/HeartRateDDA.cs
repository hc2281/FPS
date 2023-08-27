using UnityEngine;

namespace Unity.FPS.Bluetooth
{
    public class HeartRateDDA : MonoBehaviour
    {
        public static bool startCalculated = false;

        // Parameters to control the dynamic difficulty adjustment
        //public float DeadZoneFactor = 0.2f;
        public float ScopePercentage = 0.2f;

        public static float baselineBPM = 0f;

        public static float Difficultyindex;

        public static void SaveBaseBPM(float bpm)
        {
            baselineBPM = bpm;
        }

        void Update()
        {
            if (baselineBPM != 0)
            {
                startCalculated = true;
                CalculateDifficulty();
            }

        }

        void CalculateDifficulty()
        {
            // Fetch the current heart rate and baseline from HeartRateService
            int HeartRate = HeartRateService.heartBeatsPerMinute;
            float HeartRateMedian = baselineBPM;
            //float DeadZone = HeartRateMedian * ScopePercentage * DeadZoneFactor;
            float IdeaHeartRate = HeartRateMedian * (1f + ScopePercentage * (1f / 3f));
            // Compute the difference
            //float Change = HeartRate - HeartRateMedian;
            //if (Change < 0)
            //{
            //    float PercentageOutside = Mathf.Clamp(Change / DeadZone, -1f, 0f);
            //    ScopePercentage -= 0.05f * PercentageOutside;
            //}
            float HeartRateMin = HeartRateMedian - HeartRateMedian * ScopePercentage * (1f / 3f);
            float HeartRateMax = IdeaHeartRate + HeartRateMedian * ScopePercentage * (1f / 3f);

            Difficultyindex = Mathf.Clamp((HeartRateMax - HeartRate) / (HeartRateMax - HeartRateMin), 0f, 1f);
        }

    }

}
