using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.FPS.Game;

public class PanelController : MonoBehaviour
{

    public TextMeshProUGUI loadingText;
    public GameObject maskPanel;
    public float delay = 0.3f;
    public MonoBehaviour NotifierReference;
    private IGameStartNotifier gameStartNotifier;
    private DifficultySelectHandler difficultySelectHandler;

    private void Awake()
    {
        gameStartNotifier = NotifierReference as IGameStartNotifier;
        if (gameStartNotifier == null)
        {
            Debug.LogError("The provided NotifierReference does not implement IGameStartNotifier!");
        }
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "ModeC")
        {
            ShowLoadingUI();

            difficultySelectHandler = FindObjectOfType<DifficultySelectHandler>();
            if (difficultySelectHandler != null)
            {
                difficultySelectHandler.OnDifficultySelected.AddListener(HideLoadingUI);
            }
            else
                Debug.Log("Not find DifficultySelectHandler.");
        }
        else
        {
            int death = GameFlowManager.GetDeathCount();
            Debug.Log("Current Death Count:" + death);
            if (death == 0)
                ShowLoadingUI(); // Only show the panel before the game    
            else
            {
                maskPanel.SetActive(false);
                loadingText.gameObject.SetActive(false);
            }
        }
    }

    public void ShowLoadingUI()
    {
        maskPanel.SetActive(true);
        loadingText.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(loadingText.text))
            StartCoroutine(AnimateDots());
        
    }

    IEnumerator AnimateDots()
    {
        int count = 0;
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {
                loadingText.text = "Connecting..." + new string('.', i) + "    " + count + "s";
                yield return new WaitForSeconds(delay);
            }
            count++;
        }
    }

    public void HideLoadingUI()
    {
        loadingText.gameObject.SetActive(false);
        maskPanel.SetActive(false);
        gameStartNotifier?.NotifyGameStart();
    }

    private void OnDestroy()
    {
        if (difficultySelectHandler != null)
        {
            difficultySelectHandler.OnDifficultySelected.RemoveListener(HideLoadingUI);
        }
    }

}
