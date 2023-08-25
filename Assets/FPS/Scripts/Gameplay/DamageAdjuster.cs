using UnityEngine;
using Unity.FPS.Gameplay;
using Unity.FPS.Game;
public class DamageAdjuster : MonoBehaviour
{
    private ProjectileChargeParameters projectileParams;
    
    private void Start()
    {
        // Get the ProjectileChargeParameters component
        projectileParams = GetComponent<ProjectileChargeParameters>();

        // If the component exists, adjust the damage based on difficulty
        if (projectileParams)
        {
            string Difficulty = DifficultyController.Instance.DifficultyLevel;
            switch (Difficulty)
            {
                case "easy":
                    projectileParams.Damage = new MinMaxFloat()
                    {
                        Min = projectileParams.Damage.Min * 0.95f,
                        Max = projectileParams.Damage.Max * 0.95f
                    };
                    Debug.Log("Damage Adjusted to easy mode.");
                    break;

                case "hard":
                    projectileParams.Damage = new MinMaxFloat()
                    {
                        Min = projectileParams.Damage.Min * 1.05f,
                        Max = projectileParams.Damage.Max * 1.05f
                    };
                    Debug.Log("Damage Adjusted to hard mode.");
                    break;

                // For medium difficulty, no change is made
                case "medium":
                default:
                    break;
            }
        }
        else
            Debug.Log("No ProjectileChargeParameters Found.");
    }
}
