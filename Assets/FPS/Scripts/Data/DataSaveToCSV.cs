using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.FPS.Game;

[System.Serializable]
public struct ModeBData
{
    public float Time;
    public int HeartRate;
    public float DifficultyIndex;
}


public class DataSaveToCSV : MonoBehaviour
{
    public GameFlowManager gameFlowManager;
    public HeartRateService heartRateService;
    public HeartRateDDA heartRateDDA;

    private float previousTime = 0f;

    string filename = "";

    private List<ModeBData> dataList = new List<ModeBData>();

    void Start()
    {
        filename = Application.dataPath + "/HrDDA.csv";
    }

    public void CollectAndStoreData()
    {
        float totalTime = GameFlowManager.GetTotalTime();

        // If previousTime was below an integer and totalTime is now above, then we've just passed an integer
        if (Mathf.Floor(previousTime) < Mathf.Floor(totalTime))
        {
            ModeBData data = new()
            {
                Time = totalTime,
                HeartRate = heartRateService.heartBeatsPerMinute,
                DifficultyIndex = heartRateDDA.Difficultyindex,
            };
            dataList.Add(data);
        }

        // Update the previous time for the next frame
        previousTime = totalTime;
    }

    private void Update()
    {
        if (heartRateService.isSubscribed)
        {
            CollectAndStoreData();

            if (gameFlowManager.SceneEnd)
            {
                WriteCSV();
                Debug.Log($"CSV file written to \"{filename}\"");
            }
        }
    }

    public void WriteCSV()
    {
        StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine("time,HeartRate,DifficultyIndex");
        foreach (ModeBData data in dataList)
        {
            writer.WriteLine(
                data.Time + "," +
                data.HeartRate + "," +
                data.DifficultyIndex
            );
        }
        writer.Flush();
        writer.Close();
    }
}
