using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;

public class LoserData : MonoBehaviour
{
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI deathText;

    void Start()
    {
        int kills = ObjectiveKillEnemies.m_KillTotal;
        killsText.text = "Kills: " + kills.ToString();

        int deaths = GameFlowManager.GetDeathCount();
        deathText.text = "Deaths: " + deaths.ToString();
    }
}
