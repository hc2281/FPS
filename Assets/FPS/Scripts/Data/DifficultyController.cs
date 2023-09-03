using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.FPS.Bluetooth;
using Unity.FPS.Game;
using System.Collections;
public class DifficultyController : MonoBehaviour
{
    public static string DifficultyLevel;

    private DifficultySelectHandler difficultySelectHandler;
    private string sceneName;
    private static string lastDifficulty = "";

    private bool shouldGetDifficulty = true;

    private void OnEnable()
    {
        GameFlowManager.OnPlayerRestart += HandlePlayerRestart;
    }

    private void OnDisable()
    {
        GameFlowManager.OnPlayerRestart -= HandlePlayerRestart;
    }

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
        if (sceneName == "ModeA" && shouldGetDifficulty)
        {
            GetDifficultyLevel();
        }
    }

    void HandlePlayerRestart()
    {
        shouldGetDifficulty = false;
        StartCoroutine(EasyModeCoroutine());
    }

    IEnumerator EasyModeCoroutine()
    {
        DifficultyLevel = "easy";

        yield return new WaitForSeconds(15f);

        shouldGetDifficulty = true;
    }

    public void GetDifficultyLevel()
    {
        if (HeartRateDDA.Difficultyindex <= 1f / 3f)
            DifficultyLevel = "easy";
        else if (HeartRateDDA.Difficultyindex > 0.8f)
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
