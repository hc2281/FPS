using UnityEngine;
using UnityEngine.SceneManagement;
public class DifficultyController : MonoBehaviour
{
    public static DifficultyController Instance { get; private set; }

    public string DifficultyLevel { get; private set; }

    public Color EasyEyeColor = Color.yellow;  // Example color for easy
    public Color HardEyeColor = new Color(0.5f, 0f, 0.5f);    // Example color for hard
    public Color NormalEyeColor = Color.red;  // Example color for normal

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

        if (sceneName == "ModeB")
        {
            DifficultyLevel = "medium"; // Set difficulty to medium for ModeB
        }
        else if (sceneName == "ModeA")
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
                Debug.Log("Current difficulty: " + DifficultyLevel);
            }
        }
        else
        {
            difficultySelectHandler = FindObjectOfType<DifficultySelectHandler>();
            if (difficultySelectHandler != null)
            {
                difficultySelectHandler.OnDifficultySelected.AddListener(SetDifficulty);
            } 
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

    public Color GetAttackEyeColor()
    {
        switch (DifficultyLevel)
        {
            case "easy":
                return EasyEyeColor;
            case "hard":
                return HardEyeColor;
            case "medium":
            default:
                return NormalEyeColor;
        }
    }

}
