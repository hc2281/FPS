using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;

public class LoserData : MonoBehaviour
{
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI resultsText;

    void Start()
    {
        //int kills = ObjectiveKillEnemies.m_KillTotal;
        int deaths = GameFlowManager.GetDeathCount();
        GameFlowManager.ResetDeathCount();
        //int averageKill = kills / deaths;
        GameFlowManager.ResetTimer();
        resultsText.text = "Deaths: " + deaths.ToString();

        int i = deaths;
        Debug.Log("Total death count:" + i);
        while (LogRecorder.enemyCountList.Count > 0)
        {
            int count = LogRecorder.enemyCountList.Pop();
            resultsText.text += "Round " + i + " Kill Enemy:" + count.ToString() + "\n"; // Append the information
            Debug.Log("Round " + i + " Kill Enemy:" + count);
            i--;
        }
    }

}
