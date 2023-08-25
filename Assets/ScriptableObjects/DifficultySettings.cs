using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Game/DifficultySettings")]
public class DifficultySettings : ScriptableObject
{
    public float EasyHealthMultiplier = 1.2f;
    public float NormalHealthMultiplier = 1.0f;
    public float HardHealthMultiplier = 0.8f;

    public float GetCurrentMultiplier(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                return EasyHealthMultiplier;
            case "Hard":
                return HardHealthMultiplier;
            default:
                return NormalHealthMultiplier;
        }
    }
}

