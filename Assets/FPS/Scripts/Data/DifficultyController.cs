using UnityEngine;
using UnityEngine.SceneManagement;
public class DifficultyController : MonoBehaviour
{
    public static DifficultyController Instance { get; private set; }

    public string DifficultyLevel { get; private set; }

    private DifficultySelectHandler difficultySelectHandler;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        // Make sure the GameObject is a root object
        transform.parent = null;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Check the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "ModeA")
        {
            // Check if heartRateDDA script is attached to the object
            HeartRateDDA hrDDA = GetComponent<HeartRateDDA>();

            if (hrDDA != null)
            {
                // If it's attached, fetch the difficultyLevel from it
                DifficultyLevel = hrDDA.DifficultyLevel;
                if (DifficultyLevel == null)
                    DifficultyLevel = "medium";
                hrDDA.OnDifficultyChanged += HandleDifficultyChanged;        // Now you can use DifficultyLevel in your script as needed
                Debug.Log(sceneName + " Current difficulty: " + DifficultyLevel);
            }
            else
            {
                Debug.Log("Cannot find Game object with HeartRateDDA.cs.");
            }    
        }
        else if (sceneName == "ModeC")
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

    void SetDifficulty()
    {
        DifficultyLevel = DifficultySelectHandler.Difficulty;
        Debug.Log("Difficulty selected: " + DifficultyLevel);
    }

    void HandleDifficultyChanged(string newDifficulty)
    {
        DifficultyLevel = newDifficulty;
        Debug.Log("Difficulty changed to: " + DifficultyLevel);
    }

    void OnDestroy()
    {
        HeartRateDDA hrDDA = GetComponent<HeartRateDDA>();
        if (hrDDA != null)
        {
            hrDDA.OnDifficultyChanged -= HandleDifficultyChanged;
        }
        if (difficultySelectHandler != null)
        {
            difficultySelectHandler.OnDifficultySelected.RemoveListener(SetDifficulty);
        }
    }

}
