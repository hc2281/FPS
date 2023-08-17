using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRateManager : MonoBehaviour
{
    public static HeartRateManager instance;
    public float baseBPM;
    public bool isCalculated = false;

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
        isCalculated = true;
    }
}
