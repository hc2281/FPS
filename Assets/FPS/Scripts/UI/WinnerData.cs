using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;

public class WinnerData : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        //int kills = ObjectiveKillEnemies.m_KillTotal;

        float timeTaken = GameFlowManager.GetTotalTime() + 1;
        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt(timeTaken % 60);
        resultText.text = "Time Taken: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
