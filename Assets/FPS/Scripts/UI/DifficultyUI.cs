using UnityEngine;
using TMPro;

public class DifficultyUI : MonoBehaviour
{
    public HeartRateDDA heartRateDDA; // Reference to the HeartRateDDA script
    public TextMeshProUGUI difficultyText; // Reference to your TextMeshPro text component

    void Update()
    {
        if (heartRateDDA.startCalculated)
        {
            difficultyText.text = $"{heartRateDDA.difficultyindex}";
        }
        
    }
}

