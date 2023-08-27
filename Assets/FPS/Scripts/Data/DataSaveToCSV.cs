using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.FPS.Game;
using Unity.FPS.Bluetooth;

[System.Serializable]
public struct ModeBData
{
    public float Time;
    public int HeartRate;
    public float Difficulty;
    public string DifficultyLevel;
}


public class DataSaveToCSV : MonoBehaviour
{
    public GameFlowManager gameFlowManager;
    //public HeartRateDDA heartRateDDA;
    private float previousTime = 0f;

    string filename = "";

    private List<ModeBData> dataList = new List<ModeBData>(300);

    void Start()
    {
        string folderPath = Path.Combine(Application.dataPath, "results");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string dateTimeString = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        filename = Path.Combine(folderPath, $"HrDDA_{dateTimeString}.csv");


    }

    public void CollectAndStoreData()
    {        
        float totalTime = GameFlowManager.GetTotalTime();

        Debug.Log("Current BPM: " + HeartRateService.heartBeatsPerMinute);
        Debug.Log("Current Difficultyindex: " + HeartRateDDA.Difficultyindex);
        Debug.Log("Current DifficultyLevel: " + DifficultyController.DifficultyLevel);
        // If previousTime was below an integer and totalTime is now above, then we've just passed an integer
        if (Mathf.Floor(previousTime) < Mathf.Floor(totalTime))
        {
            ModeBData data = new()
            {
                Time = totalTime,
                HeartRate = HeartRateService.heartBeatsPerMinute,
                Difficulty = HeartRateDDA.Difficultyindex,
                DifficultyLevel = DifficultyController.DifficultyLevel,
            };
            dataList.Add(data);
        }

        // Update the previous time for the next frame
        previousTime = totalTime;
    }

    private void Update()
    {
        if (gameFlowManager.gameStarted)
        {
            CollectAndStoreData();
        }
        if (gameFlowManager.SceneEnd)
        {
            WriteCSV();  
        }
    }

    public void WriteCSV()
    {
        StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine("time,HeartRate,Difficulty,Level");
        foreach (ModeBData data in dataList)
        {
            writer.WriteLine(
                data.Time + "," +
                data.HeartRate + "," +
                data.Difficulty + "," + 
                data.DifficultyLevel
            );
        }
        Debug.Log($"CSV file written to \"{filename}\"");
        writer.Flush();
        writer.Close();
    }
}
