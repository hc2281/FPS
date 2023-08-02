using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;

public class WinnerData : MonoBehaviour
{
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI timeText;
    private float timeTaken;

    void Start()
    {
        int kills = ObjectiveKillEnemies.m_KillTotal;
        killsText.text = "Kills: " + kills.ToString();

        timeTaken = GameFlowManager.GetTotalTime();
        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);
        timeText.text = "Time Taken: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
