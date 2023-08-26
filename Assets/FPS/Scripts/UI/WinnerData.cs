using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;

public class WinnerData : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI resultsText;
    void Start()
    {
        int deaths = GameFlowManager.GetDeathCount();
        GameFlowManager.ResetDeathCount();
        float timeTaken = GameFlowManager.GetTotalTime() + 1;
        GameFlowManager.ResetTimer();
        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);
        TimeText.text = "Time Taken: " + string.Format("{0:00}:{1:00}", minutes, seconds) + " Death:" + deaths.ToString();

        int i = 0;
        foreach (int count in LogRecorder.enemyCountList)
        {
            resultsText.text = "Round " + i + " Kill Enemy:" + count.ToString();
            i++;
        }

        LogRecorder.enemyCountList.Clear();

    }
}
