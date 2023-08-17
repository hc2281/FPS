using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    private string difficultyLevel; // This is the backing field

    public string DifficultyLevel
    {
        get { return difficultyLevel; }
        private set { difficultyLevel = value; }
    }

    void Start()
    {
        // Check if heartRateDDA script is attached to the object
        HeartRateDDA hrDDA = GetComponent<HeartRateDDA>();

        if (hrDDA != null)
        {
            // If it's attached, fetch the difficultyLevel from it
            DifficultyLevel = hrDDA.difficultyLevel;
        }
        else
        {
            // If it's not attached, default the difficulty to "medium"
            DifficultyLevel = "medium";
        }

        // Now you can use difficultyLevel in your script as needed
        Debug.Log("Current difficulty: " + DifficultyLevel);
    }
}
