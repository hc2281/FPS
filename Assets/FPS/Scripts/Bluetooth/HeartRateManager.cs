using UnityEngine;
using System; // Needed for the Action delegate

public class HeartRateManager : MonoBehaviour
{
    public static HeartRateManager instance;
    public float baseBPM;
    public event Action<float> OnBaseBPMCalculated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAverageBPM(float bpm)
    {
        baseBPM = bpm;
        OnBaseBPMCalculated?.Invoke(baseBPM);
    }
}
