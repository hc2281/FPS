using UnityEngine;
using TMPro;
using Unity.FPS.Game;

namespace Unity.FPS.UI
{
    public class TimerUI : MonoBehaviour
    {
        public GameFlowManager gameFlowManager;
        private TextMeshProUGUI timerText;

        private void Start()
        {
            GameObject managerObject = GameObject.Find("GameManager");
            if (managerObject == null)
            {
                Debug.LogError("No GameObject named 'GameManager' found in the scene.");
                return;
            }

            gameFlowManager = managerObject.GetComponent<GameFlowManager>();
            if (gameFlowManager == null)
            {
                Debug.LogError("No GameFlowManager component found on 'GameManager' GameObject.");
                return;
            }

            timerText = GetComponent<TextMeshProUGUI>();
            if (timerText == null)
            {
                Debug.LogError("No TextMeshProUGUI component found on this GameObject.");
                return;
            }
        }

        private void Update()
        {
            if (gameFlowManager.gameStarted)
            {
                float timerValue = gameFlowManager.GetTimer() + 1;
                int minutes = Mathf.FloorToInt(timerValue / 60F);
                int seconds = Mathf.FloorToInt(timerValue - minutes * 60);
                timerText.text = string.Format("{0:00}:{1:00}", Mathf.Max(0, minutes), Mathf.Max(0, seconds));

                // Check if timer reaches 00:00
                if (gameFlowManager.SceneEnd)
                {
                    this.gameObject.SetActive(false); // Set the TimerUI to be invisible
                }
            }
        }
    }
}
