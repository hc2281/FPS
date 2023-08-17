using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using Unity.FPS.Game;

public class SaveToCSV : MonoBehaviour
{
    // Reference to the HeartRateService
    
    public GameFlowManager gameFlowManager;

    private HeartRateService heartRateService;
    private string filePath;

    void Start()
    {
        // Setting the path to save the CSV file
        filePath = Path.Combine(Application.persistentDataPath, "heartRateData.csv");
    }

    // Call this method when you want to save data
    public void SaveDataToCSV()
    {
        StringBuilder csv = new StringBuilder();

        // Add header
        csv.AppendLine("Time,HeartRate");

        float currentTime = gameFlowManager.GetTimer();

        for (int i = 0; i < heartRateService.heartRateData.Count; i++)
        {
            csv.AppendLine($"{currentTime + i * Time.deltaTime},{heartRateService.heartRateData[i]}");
        }

        File.WriteAllText(filePath, csv.ToString());
        Debug.Log("Data saved to: " + filePath);
    }

    private void OnEnable()
    {
        GameFlowManager.OnGameSessionEnd += SaveDataToCSV;
    }

    private void OnDisable()
    {
        GameFlowManager.OnGameSessionEnd -= SaveDataToCSV;
    }

}
