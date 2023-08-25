using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DifficultySelectHandler : MonoBehaviour
{
    public static string Difficulty;
    public Button[] buttons;
    public UnityEvent OnDifficultySelected;

    void Start()
    {
        // Attach click handlers to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => HandlerClick());
        }
    }

    public void SetDifficulty(string difficulty)
    {
        Difficulty = difficulty;
        OnDifficultySelected?.Invoke();
    }

    private void HandlerClick()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        Debug.Log("Difficulty Selected. Game Start.");
    }

    void OnDestroy()
    {
        // Remove click handlers when the script is destroyed
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
    }
}