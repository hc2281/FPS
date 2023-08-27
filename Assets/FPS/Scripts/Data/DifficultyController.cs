using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.FPS.Bluetooth;
public class DifficultyController : MonoBehaviour
{
    public static string DifficultyLevel;

    private DifficultySelectHandler difficultySelectHandler;
    private string sceneName;
    private static string lastDifficulty = "";

    void Start()
    {
        // Check the current scene name
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "ModeC")
        {
            difficultySelectHandler = FindObjectOfType<DifficultySelectHandler>();
            if (difficultySelectHandler != null)
            {
                difficultySelectHandler.OnDifficultySelected.AddListener(SetDifficulty);
            } 
        }
        else
        {
            DifficultyLevel = "medium"; // Set difficulty to medium for ModeB
            Debug.Log(sceneName + " Current difficulty: " + DifficultyLevel);
        }

    }

    private void Update()
    {
        if (sceneName == "ModeA")
        {
            GetDifficultyLevel();
        }
    }

    public static void GetDifficultyLevel()
    {
        if (HeartRateDDA.Difficultyindex < 0.35f)
            DifficultyLevel = "easy";
        else if (HeartRateDDA.Difficultyindex > 0.7f)
            DifficultyLevel = "hard";
        else
            DifficultyLevel = "medium";

        if (DifficultyLevel != lastDifficulty)
        {
            Debug.Log("Difficulty Changed: " + DifficultyLevel);
            lastDifficulty = DifficultyLevel;
        }
    }

    void SetDifficulty()
    {
        DifficultyLevel = DifficultySelectHandler.Difficulty;
        Debug.Log("Difficulty selected: " + DifficultyLevel);
    }


    void OnDestroy()
    {
        if (difficultySelectHandler != null)
        {
            difficultySelectHandler.OnDifficultySelected.RemoveListener(SetDifficulty);
        }
    }

}
