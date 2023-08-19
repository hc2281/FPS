using UnityEngine;

public class HeartRateDDA : MonoBehaviour
{
    public HeartRateService heartRateService; // Reference to the HeartRateService

    public bool startCalculated = false;

    // Parameters to control the dynamic difficulty adjustment
    public float DeadZoneFactor = 0.2f; // Example value, adjust as needed
    public float ScopePercentage = 0.2f; // Example value, adjust as needed
    
    public float difficultyindex { get; private set; } // The computed difficulty level
    public string difficultyLevel { get; private set; } // The computed difficulty level

    void Update()
    {
        if (HeartRateManager.instance.isCalculated)
        {
            startCalculated = true;
            CalculateDifficulty();
        }
        
    }

    void CalculateDifficulty()
    {
        // Fetch the current heart rate and baseline from HeartRateService
        int HeartRate = heartRateService.heartBeatsPerMinute;
        float HeartRateMedian = HeartRateManager.instance.baseBPM;

        // Compute the difference
        float Change = HeartRate - HeartRateMedian;
        HeartRateMedian += Change / 30;
        float DeadZone = HeartRateMedian * ScopePercentage * DeadZoneFactor;
        float PercentageOutside = Mathf.Clamp((Mathf.Abs(Change) - DeadZone) / DeadZone, -1f, 1f);
        ScopePercentage += 0.02f * PercentageOutside / 60;
        float HeartRateMin = (1f - ScopePercentage) * HeartRateMedian;
        float HeartRateMax = (1f + ScopePercentage) * HeartRateMedian;

        // Compute difficulty level, ranging from 0 to 1
        difficultyindex = Mathf.Clamp((HeartRateMax - HeartRate) / (HeartRateMax - HeartRateMin), 0f, 1f);
        difficultyindex = Mathf.Round(difficultyindex * 100f) / 100f;

        if (difficultyindex < 1f / 3f)
        {
            difficultyLevel = "easy";
        }
        else if (difficultyindex < 2f / 3f)
        {
            difficultyLevel = "medium";
        }
        else
        {
            difficultyLevel = "hard";
        }

    }
}
