using UnityEngine;
using Unity.FPS.Game;

public class DeathCountManager : MonoBehaviour
{

    private int deathCount = 0;

    private void OnEnable()
    {
        GameFlowManager.OnGameEnd += HandleGameEnd;
    }

    private void OnDisable()
    {
        GameFlowManager.OnGameEnd -= HandleGameEnd;
    }

    private void HandleGameEnd()
    {
        // Store the death count and reset it to 0
        deathCount = GameFlowManager.GetDeathCount();

        DataCollector.SaveDeathCount(deathCount);

        // Perform any other logic you need with the death count
        Debug.Log("Death Count: " + deathCount);
    }
}
