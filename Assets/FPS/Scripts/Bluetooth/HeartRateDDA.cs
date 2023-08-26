using UnityEngine;

public class HeartRateDDA : MonoBehaviour
{
    public HeartRateService heartRateService; // Reference to the HeartRateService

    public bool startCalculated = false;

    // Parameters to control the dynamic difficulty adjustment
    public float DeadZoneFactor = 0.2f; 
    public float ScopePercentage = 0.2f;

    public float baselineBPM = 0f;
    public delegate void DifficultyChangedHandler(string newDifficulty);
    public event DifficultyChangedHandler OnDifficultyChanged;
    
    public float Difficultyindex { get; private set; }

    private string difficultyLevel; // The computed difficulty level
    public string DifficultyLevel
    {
        get { return difficultyLevel; }
        set
        {
            difficultyLevel = value;
            OnDifficultyChanged?.Invoke(difficultyLevel); //Once change, inform DifficultyController
        }
    }

    private void Start()
    {
        HeartRateManager.instance.OnBaseBPMCalculated += SaveBaseBPM;
    }

    private void SaveBaseBPM(float bpm)
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
        int HeartRate = heartRateService.heartBeatsPerMinute;
        float HeartRateMedian = HeartRateManager.instance.baseBPM;

        // Compute the difference
        float Change = HeartRate - HeartRateMedian;
      //HeartRateMedian += Change / 30;
        float DeadZone = HeartRateMedian * ScopePercentage * DeadZoneFactor;
      //float PercentageOutside = Mathf.Clamp((Mathf.Abs(Change) - DeadZone) / DeadZone, -1f, 1f);
      //ScopePercentage += 0.02f * PercentageOutside / 60;
        float HeartRateMin = (1f - ScopePercentage) * HeartRateMedian;
        float HeartRateMax = (1f + ScopePercentage) * HeartRateMedian;

        if (Change >= DeadZone)
        {       
            Difficultyindex = Mathf.Clamp((HeartRateMax - HeartRate) / (HeartRateMax - HeartRateMin), 0f, 1f);
            Difficultyindex = Mathf.Round(Difficultyindex * 100f) / 100f;
        }
        else
        {
            Difficultyindex = 0.5f;
        }
        // Compute difficulty level, ranging from 0 to 1
        
        if (Difficultyindex < 1f / 3f)
        {
            difficultyLevel = "easy";
        }
        else if (Difficultyindex > 2f / 3f)
        {
            difficultyLevel = "hard";
        }
        else
        {
            difficultyLevel = "medium";
        }
        Debug.Log("Current Difficulty Index:" + Difficultyindex + " Current Difficulty Level:" + DifficultyLevel);
    }

    private void OnDestroy()
    {
        // Always good to unsubscribe when the object is destroyed to prevent memory leaks
        HeartRateManager.instance.OnBaseBPMCalculated -= SaveBaseBPM;
    }

}
